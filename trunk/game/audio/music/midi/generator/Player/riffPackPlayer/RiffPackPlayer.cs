using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AbrahmanAdventure.audio.midi;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// RiffPack player (plays songs)
    /// </summary>
    class RiffPackPlayer
    {
        #region Const
        /// <summary>
        /// Midi time precision
        /// </summary>
        private const double timePrecision = 0.001;

        /// <summary>
        /// Midi time multiplicator
        /// </summary>
        private const double timeMultiplicator = 8333.3333333;
        #endregion

        #region Event
        /// <summary>
        /// When playing note
        /// </summary>
        public event EventHandler OnNoteOn;

        /// <summary>
        /// When stopping note
        /// </summary>
        public event EventHandler OnNoteOff;

        /// <summary>
        /// When black note time has passed
        /// </summary>
        public event EventHandler OnBlackNoteTimeElapsed;
        #endregion

        #region Fields
        /// <summary>
        /// Midi note player
        /// </summary>
        private NotePlayer notePlayer;

        /// <summary>
        /// Remembers which notes are being played
        /// </summary>
        private RiffPackPlayerMemory riffPackPlayerMemory = new RiffPackPlayerMemory();

        /// <summary>
        /// Remembers channel for each instrument
        /// </summary>
        private ChannelMemory channelMemory = new ChannelMemory();

        /// <summary>
        /// Whether is currently stopping
        /// </summary>
        private bool isStopping = false;

        /// <summary>
        /// Whether is playing
        /// </summary>
        private bool isPlaying = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Build riff pack player
        /// </summary>
        public RiffPackPlayer()
        {
            notePlayer = new NotePlayer();
            notePlayer.OnNoteOn += NoteOnHandler;
            notePlayer.OnNoteOff += NoteOffHandler;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Play riffPack
        /// </summary>
        /// <param name="riffPack">song to play</param>
        /// <param name="outputDevice">midi device</param>
        public void Play(RiffPack riffPack, OutputDevice outputDevice)
        {
            isPlaying = true;
            riffPackPlayerMemory.Clear();
            channelMemory.Clear();

            channelMemory.InitInstrumentsAndChannels(riffPack, outputDevice);
            
            
            //outputDevice.Send(new ChannelMessage(ChannelCommand.ProgramChange, channel, riff.MidiInstrument, 0));


            double currentTime = 0.0;
            double totalTime = GetLongestRiffLength(riffPack) + timePrecision;
            double lastTimeBlackNoteElapsed = 0.0;
            int channel;

            while (currentTime < totalTime && !isStopping)
            {
                foreach (Riff riff in riffPack)
                {
                    channel = channelMemory.GetChannel(riff.MidiInstrument, riff.IsDrum);
                    UpdateRiff(riff, currentTime, outputDevice, channel);
                }

                int timeMs = (int)(timePrecision * timeMultiplicator / riffPack.Tempo * 120.0);
                Thread.Sleep(timeMs);

                currentTime += timePrecision;

                if (currentTime >= lastTimeBlackNoteElapsed + 0.0625)
                {
                    lastTimeBlackNoteElapsed += 0.0625;
                    if (OnBlackNoteTimeElapsed != null) OnBlackNoteTimeElapsed(this, null);
                }

                if (currentTime >= totalTime)
                {
                    currentTime = 0;
                    lastTimeBlackNoteElapsed = 0;
                    riffPackPlayerMemory.Clear();
                }
            }
            TurnAllNotesOff(riffPack, outputDevice);

            isStopping = false;
            isPlaying = false;
        }

        /// <summary>
        /// Stop music
        /// </summary>
        public void Stop()
        {
            isStopping = true;
        }
        #endregion

        #region Event Handlers
        private void NoteOnHandler(object source, EventArgs e)
        {
            if (OnNoteOn != null) OnNoteOn(source, e);
        }

        private void NoteOffHandler(object source, EventArgs e)
        {
            if (OnNoteOff != null) OnNoteOff(source, e);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initialize midi instruments
        /// </summary>
        /// <param name="riffPack">riff pack</param>
        /// <param name="outputDevice">midi output device</param>
        private void InitInstruments(RiffPack riffPack, OutputDevice outputDevice)
        {
            int channel;
            int counter = 0;
            foreach (Riff riff in riffPack)
            {
                if (riff.IsDrum)
                {
                    channel = 9;
                }
                else
                {
                    channel = counter;
                    counter++;
                    if (counter == 9)
                        counter++;
                }
                outputDevice.Send(new ChannelMessage(ChannelCommand.ProgramChange, channel, riff.MidiInstrument, 0));
            }
        }

        /// <summary>
        /// Get longest riff's length
        /// </summary>
        /// <param name="riffPack">riff pack</param>
        /// <returns>longest riff's length</returns>
        private double GetLongestRiffLength(RiffPack riffPack)
        {
            double longestRiffLength = 0;
            double currentLength;
            foreach (Riff riff in riffPack)
            {
                currentLength = riff.Length;
                if (currentLength > longestRiffLength)
                {
                    longestRiffLength = currentLength;
                }
            }
            return longestRiffLength;
        }

        /// <summary>
        /// Play a note or stop a note in riff
        /// </summary>
        /// <param name="riff">riff</param>
        /// <param name="currentTime">current time</param>
        /// <param name="outputDevice">midi output device</param>
        /// <param name="channel">midi channel</param>
        private void UpdateRiff(Riff riff, double currentTime, OutputDevice outputDevice, int channel)
        {
            Note noteToStop = riffPackPlayerMemory.GetPlayingNote(riff);

            if (noteToStop != null && currentTime >= noteToStop.RiffPosition + noteToStop.Length)
                notePlayer.NoteOff(noteToStop, outputDevice, channel);

            Note noteToPlay = riffPackPlayerMemory.GetNoteToPlay(riff, currentTime);

            if (noteToPlay != null)
            {
                riffPackPlayerMemory.SetPlayingNote(riff, noteToPlay);
                notePlayer.NoteOn(noteToPlay, outputDevice, channel);
            }
        }

        private void TurnAllNotesOff(RiffPack riffPack, OutputDevice outputDevice)
        {
            foreach (Riff riff in riffPack)
                TurnAllNotesOff(riff, outputDevice);
        }

        private void TurnAllNotesOff(Riff riff, OutputDevice outputDevice)
        {
            int channel = channelMemory.GetChannel(riff.MidiInstrument, riff.IsDrum);
            Note noteToStop = riffPackPlayerMemory.GetPlayingNote(riff);
            if (noteToStop != null)
                notePlayer.NoteOff(noteToStop, outputDevice, channel);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether is playing
        /// </summary>
        public bool IsPlaying
        {
            get { return isPlaying; }
        }
        #endregion
    }
}
