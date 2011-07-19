using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Remembers which notes are being played
    /// </summary>
    class RiffPackPlayerMemory
    {
        #region Fields
        /// <summary>
        /// List of notes being played
        /// </summary>
        private Dictionary<Riff, Note> listPlayingNote = new Dictionary<Riff, Note>();

        /// <summary>
        /// Remaining notes per riff
        /// </summary>
        private Dictionary<Riff, Queue<Note>> remainingNotesPerRiff = new Dictionary<Riff, Queue<Note>>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Clear riffPlayer memory
        /// </summary>
        public void Clear()
        {
            listPlayingNote.Clear();
            remainingNotesPerRiff.Clear();
        }

        /// <summary>
        /// Get note being played from riff
        /// </summary>
        /// <param name="riff">riff</param>
        /// <returns>note being played from riff</returns>
        public Note GetPlayingNote(Riff riff)
        {
            Note note;
            if (listPlayingNote.TryGetValue(riff, out note))
                return note;
            else
                return null;
        }

        /// <summary>
        /// Set playing note in riff
        /// </summary>
        /// <param name="riff">riff</param>
        /// <param name="note">playing note in riff</param>
        public void SetPlayingNote(Riff riff, Note note)
        {
            listPlayingNote[riff] = note;
        }

        /// <summary>
        /// Next note to play in riff
        /// </summary>
        /// <param name="riff">riff</param>
        /// <param name="currentTime">current time</param>
        /// <returns>note to play in riff</returns>
        public Note GetNoteToPlay(Riff riff, double currentTime)
        {
            Queue<Note> remainingNoteList = GetRemainingNoteList(riff);

            if (remainingNoteList.Count < 1)
                return null;

            if (currentTime >= remainingNoteList.Peek().RiffPosition)
                return remainingNoteList.Dequeue();
            return null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// List of remaining notes in riff
        /// </summary>
        /// <param name="riff">riff</param>
        /// <returns>List of remaining notes</returns>
        private Queue<Note> GetRemainingNoteList(Riff riff)
        {
            Queue<Note> remainingNoteList;
            if (!remainingNotesPerRiff.TryGetValue(riff, out remainingNoteList))
            {
                remainingNoteList = new Queue<Note>(riff);
                remainingNotesPerRiff.Add(riff, remainingNoteList);
            }
            return remainingNoteList;
        }
        #endregion
    }
}
