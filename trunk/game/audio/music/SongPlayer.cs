using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.Midi;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Music player
    /// </summary>
    internal class SongPlayer
    {
        #region Fields and parts
        private OutputDevice outputDevice;

        private NoteOffScheduler noteOffScheduler;

        private Song lastSongPlayed = null;

        private double timePointer;

        private double timePointerPrevious;
        #endregion

        #region Constructor
        public SongPlayer()
        {
            outputDevice = outputDevice = new OutputDevice(0);
            noteOffScheduler = new NoteOffScheduler();
            timePointer = 0;
            timePointerPrevious = 0;
        }
        #endregion

        #region Play music
        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="song">music</param>
        /// <param name="timeDelta">time delta</param>
        internal void Play(Song song, double timeDelta)
        {
            if (song != lastSongPlayed)
            {
                SetInstruments(song);
                lastSongPlayed = song;
                AllNotesOff();
                noteOffScheduler.Reset();
                timePointer = 0;
                timePointerPrevious = 0;
                timeDelta = 0;
            }

            timeDelta /= 4;

            Chord currentChord = song.ChordProgression.GetChordAtTime(timePointer);

            foreach (InstrumentTrack instrumentTrack in song)
                Play(instrumentTrack, currentChord);

            noteOffScheduler.TurnOffScheduledNotes(timePointer, timePointerPrevious, outputDevice);

            timePointerPrevious = timePointer;
            timePointer += timeDelta;
            while (timePointer >= song.Length)
            {
                timePointer -= song.Length;
                timePointerPrevious = 0;
                noteOffScheduler.Reset();
                //AllNotesOff();
            }
        }
        #endregion

        #region Private Methods
        private void Play(InstrumentTrack instrumentTrack, Chord chord)
        {
            double timePointerRelativeToRiff;
            double timePointerPreviousRelativeToRiff;
            Riff riff = instrumentTrack.GetRiffAtTime(timePointer, timePointerPrevious, out timePointerRelativeToRiff, out timePointerPreviousRelativeToRiff);
            int channel = instrumentTrack.Channel;
            Play(riff, channel, timePointerRelativeToRiff, timePointerPreviousRelativeToRiff, chord, instrumentTrack.InstrumentType , noteOffScheduler);
        }

        private void Play(Riff riff, int channel, double riffTimePointer, double riffTimePointerPrevious, Chord chord, InstrumentType instrumentType, NoteOffScheduler noteOffScheduler)
        {
            double noteLength;
            if (riff.RythmPattern.IsBeatBetween(riffTimePointerPrevious, riffTimePointer, out noteLength))
            {
                int pitch = GetPitch(riff, riffTimePointer, chord, instrumentType);
                int velocity = GetVelocity(riff, riffTimePointer, chord, instrumentType);
                outputDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, channel, pitch, velocity));
                noteOffScheduler.Add(new MessageInfo(riffTimePointer + noteLength, new ChannelMessage(ChannelCommand.NoteOff, channel, pitch, 0)));
            }
        }

        private int GetVelocity(Riff riff, double riffTimePointer, Chord chord, InstrumentType instrumentType)
        {
            return 100;
        }

        private int GetPitch(Riff riff, double riffTimePointer, Chord chord, InstrumentType instrumentType)
        {
            return 64;
        }

        private void AllNotesOff()
        {
            for (int channel = 0; channel < 10; channel++)
                for (int pitch = 0; pitch < 128; pitch++)
                    outputDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, channel, pitch, 0));
        }

        private void SetInstruments(Song song)
        {
            foreach (InstrumentTrack instrumentTrack in song)
                outputDevice.Send(new ChannelMessage(ChannelCommand.ProgramChange, instrumentTrack.Channel, instrumentTrack.MidiInstrument, 0));
        }
        #endregion
    }
}