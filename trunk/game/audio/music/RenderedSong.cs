using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.Midi;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Rendered song
    /// </summary>
    internal class RenderedSong
    {
        #region Field and parts
        /// <summary>
        /// Current position in list of messages
        /// </summary>
        private int pointer;

        /// <summary>
        /// List of midi messages
        /// </summary>
        private List<MessageInfo> listMessageInfo;
        #endregion

        #region Constructor
        /// <summary>
        /// Create rendered song from song
        /// </summary>
        /// <param name="song">song</param>
        public RenderedSong(Song song)
        {
            pointer = 0;
            listMessageInfo = new List<MessageInfo>();

            int channel = 0;
            foreach (InstrumentTrack instrumentTrack in song)
            {
                RenderInstrumentTrack(listMessageInfo, instrumentTrack, song.ChordProgression, channel);
                channel++;
            }

            listMessageInfo = new List<MessageInfo>(from note in listMessageInfo orderby note.TimePosition select note);
        }
        #endregion

        #region Private Methods
        private void RenderInstrumentTrack(List<MessageInfo> listMessageInfo, InstrumentTrack instrumentTrack, ChordProgression chordProgression, int channel)
        {
            if (instrumentTrack.InstrumentType == InstrumentType.Drum)
                channel = 10;
            else if (channel >= 10)
                channel++;

            //We set the midi instrument
            listMessageInfo.Add(new MessageInfo(0, new ChannelMessage(ChannelCommand.ProgramChange, channel, instrumentTrack.MidiInstrument, 0)));

            foreach (Riff riff in instrumentTrack)
                RenderRiff(listMessageInfo, riff, chordProgression, channel);
        }

        private void RenderRiff(List<MessageInfo> listMessageInfo, Riff riff, ChordProgression chordProgression, int channel)
        {
        }
        #endregion
    }
}