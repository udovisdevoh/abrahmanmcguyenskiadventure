using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Analyze interval between notes playing at the same time
    /// </summary>
    class IntervalAnalyzer
    {
        #region Public Methods
        /// <summary>
        /// Whether interval is found in specified riff pack
        /// </summary>
        /// <param name="position">note position</param>
        /// <param name="length">note length</param>
        /// <param name="pitch">note pitch</param>
        /// <param name="intervalToFind">interval to find</param>
        /// <param name="riffPack">riff pack to look into</param>
        /// <returns>Whether interval is found in specified riff pack</returns>
        public bool IsFoundInterval(double position, double length, int pitch, int intervalToFind, RiffPack riffPack)
        {
            foreach (Riff riff in riffPack)
                if (IsFoundInterval(position, length, pitch, intervalToFind, riff))
                    return true;
            return false;
        }

        /// <summary>
        /// Whether interval is found in specified riff
        /// </summary>
        /// <param name="position">note position</param>
        /// <param name="length">note length</param>
        /// <param name="pitch">note pitch</param>
        /// <param name="intervalToFind">interval to find</param>
        /// <param name="riff">riff to look into</param>
        /// <returns>Whether interval is found in specified riff pack</returns>
        public bool IsFoundInterval(double position, double length, int pitch, int intervalToFind, Riff riff)
        {
            HashSet<int> intervalList = GetIntervalList(position, length, pitch, riff);
            return intervalList.Contains(intervalToFind);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the list of intervals from simultaneous notes
        /// </summary>
        /// <param name="position">note position</param>
        /// <param name="length">note length</param>
        /// <param name="pitch">note pitch</param>
        /// <param name="riff">riff</param>
        /// <returns>list of intervals</returns>
        private HashSet<int> GetIntervalList(double position, double length, int pitch, Riff riff)
        {
            HashSet<int> intervalList = new HashSet<int>();
            HashSet<int> simultaneousNoteList = GetSimultaneousNotes(position, length, riff);

            foreach (int otherNotePitch in simultaneousNoteList)
            {
                int interval = otherNotePitch - pitch;
                while (interval > 6)
                    interval -= 12;
                while (interval <= -6)
                    interval += 12;
                intervalList.Add(interval);
            }

            return intervalList;
        }

        /// <summary>
        /// Notes that are being played at the same time
        /// </summary>
        /// <param name="position">position</param>
        /// <param name="length">length</param>
        /// <param name="riff">riff</param>
        /// <returns>Notes that are being played at the same time</returns>
        private HashSet<int> GetSimultaneousNotes(double position, double length, Riff riff)
        {
            HashSet<int> simultaneousNotes = new HashSet<int>();

            foreach (Note note in riff)
            {
                if (note.Velocity < 1)
                    continue;

                if (note.RiffPosition <= position && (note.RiffPosition + note.Length) >= position)
                {
                    simultaneousNotes.Add(note.Pitch);
                }
                else if (note.RiffPosition <= (position + length) && (note.RiffPosition + note.Length) >= (position + length))
                {
                    simultaneousNotes.Add(note.Pitch);
                }
                else if (note.RiffPosition <= position && note.RiffPosition + note.Length >= position + length)
                {
                    simultaneousNotes.Add(note.Pitch);
                }
                else if (note.RiffPosition >= position && note.RiffPosition + note.Length <= position + length)
                {
                    simultaneousNotes.Add(note.Pitch);
                }
            }
            return simultaneousNotes;
        }
        #endregion
    }
}
