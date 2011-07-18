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
        private List<Note> noteList;
        #endregion

        #region Constructor
        /// <summary>
        /// Create rendered song from song
        /// </summary>
        /// <param name="song">song</param>
        public RenderedSong(Song song)
        {
            pointer = 0;
            noteList = new List<Note>();

            foreach (InstrumentTrack instrumentTrack in song)
                RenderInstrumentTrack(noteList, instrumentTrack);

            noteList = new List<Note>(from note in noteList orderby note.position select note);
        }
        #endregion

        #region Private Methods
        private void RenderInstrumentTrack(List<Note> noteList, InstrumentTrack instrumentTrack)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
