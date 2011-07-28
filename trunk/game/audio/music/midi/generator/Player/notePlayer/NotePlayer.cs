using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AbrahmanAdventure.audio.midi;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Play midi notes
    /// </summary>
    class NotePlayer
    {
        #region Event
        /// <summary>
        /// When playing note
        /// </summary>
        public event EventHandler OnNoteOn;

        /// <summary>
        /// When stopping note
        /// </summary>
        public event EventHandler OnNoteOff;
        #endregion

        #region Public Methods
        /// <summary>
        /// Play midi notes
        /// </summary>
        /// <param name="note">note</param>
        /// <param name="outputDevice">output device</param>
        /// <param name="channel">midi channel</param>
        public void Play(Note note, OutputDevice outputDevice, int channel)
        {
            NoteOn(note, outputDevice, channel);
            int timeMs = (int)(note.Length * 10000.0);
            Thread.Sleep(timeMs);
            NoteOff(note, outputDevice, channel);
        }

        /// <summary>
        /// Note on
        /// </summary>
        /// <param name="note">note</param>
        /// <param name="outputDevice">midi output device</param>
        /// <param name="channel">midi channel</param>
        public void NoteOn(Note note, OutputDevice outputDevice, int channel)
        {
            if (note.Velocity < 4)
                return;
            outputDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, channel, note.Pitch, ((note.Velocity * SongPlayer.Volume) / 10)));
            if (OnNoteOn != null) OnNoteOn(note, null);
        }

        /// <summary>
        /// Note off
        /// </summary>
        /// <param name="note">note</param>
        /// <param name="outputDevice">midi output device</param>
        /// <param name="channel">midi channel</param>
        public void NoteOff(Note note, OutputDevice outputDevice, int channel)
        {
            outputDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, channel, note.Pitch, 0));
            if (OnNoteOff != null) OnNoteOff(note, null);
        }
        #endregion
    }
}
