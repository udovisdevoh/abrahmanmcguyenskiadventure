using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Represents a single riff
    /// </summary>
    public class Riff : IRiff, IList<Note>
    {
        #region Fields
        /// <summary>
        /// Note list
        /// </summary>
        private List<Note> noteList = new List<Note>();

        /// <summary>
        /// Midi instrument
        /// </summary>
        private int midiInstrument = 0;

        /// <summary>
        /// Whether riff is drum
        /// </summary>
        private bool isDrum = false;

        /// <summary>
        /// Tempo
        /// </summary>
        private int tempo = 120;
        #endregion

        #region Properties
        /// <summary>
        /// Midi instrument
        /// </summary>
        public int MidiInstrument
        {
            get
            {
                if (isDrum)
                    return 0;

                return midiInstrument;
            }
            set { midiInstrument = value; }
        }

        /// <summary>
        /// Whether riff is drum
        /// </summary>
        public bool IsDrum
        {
            get { return isDrum; }
            set { isDrum = value; }
        }

        /// <summary>
        /// Riff's length
        /// </summary>
        public double Length
        {
            get
            {
                double length = 0.0;
                foreach (Note note in noteList)
                    length += note.Length;

                return length;
            }
        }

        /// <summary>
        /// Tempo
        /// </summary>
        public int Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Join two riff to make a riff pack
        /// </summary>
        /// <param name="riff1">riff 1</param>
        /// <param name="riff2">riff 2</param>
        /// <returns>riff pack</returns>
        public static IRiff operator +(Riff riff1, IRiff riff2)
        {
            RiffPack summ = new RiffPack();
            summ.Add(riff1);
            summ.Add(riff2);
            return summ;
        }
        #endregion

        #region IList<Note> Members
        /// <summary>
        /// Add note
        /// </summary>
        /// <param name="item">note to add</param>
        public void Add(Note item)
        {
            if (!Contains(item))
                noteList.Add(item);
        }

        /// <summary>
        /// Remove all notes
        /// </summary>
        public void Clear()
        {
            noteList.Clear();
        }

        /// <summary>
        /// Whether contains note
        /// </summary>
        /// <param name="item">note</param>
        /// <returns>Whether contains note</returns>
        public bool Contains(Note item)
        {
            foreach (Note currentNote in noteList)
                if (currentNote.Equals(item))
                    return true;
            return false;
        }

        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(Note[] array, int arrayIndex)
        {
            noteList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// How manu notes
        /// </summary>
        public int Count
        {
            get { return noteList.Count; }
        }

        /// <summary>
        /// Whether riff is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove note
        /// </summary>
        /// <param name="item">note</param>
        /// <returns>whether removal worked</returns>
        public bool Remove(Note item)
        {
            foreach (Note currentNote in noteList)
                if (currentNote.Equals(item))
                    return noteList.Remove(currentNote);
            return false;
        }

        /// <summary>
        /// Note enumerator
        /// </summary>
        /// <returns>Note enumerator</returns>
        public IEnumerator<Note> GetEnumerator()
        {
            return noteList.GetEnumerator();
        }

        /// <summary>
        /// Note enumerator
        /// </summary>
        /// <returns>Note enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return noteList.GetEnumerator();
        }

        /// <summary>
        /// Index of note
        /// </summary>
        /// <param name="item">note</param>
        /// <returns>Index of note</returns>
        public int IndexOf(Note item)
        {
            int index = 0;
            foreach (Note currentNote in noteList)
            {
                if (currentNote.Equals(item))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Insert note at index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">note</param>
        public void Insert(int index, Note item)
        {
            noteList.Insert(index, item);
        }

        /// <summary>
        /// Remove note at index
        /// </summary>
        /// <param name="index">index</param>
        public void RemoveAt(int index)
        {
            noteList.RemoveAt(index);
        }

        /// <summary>
        /// Get note at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>note at index</returns>
        public Note this[int index]
        {
            get
            {
                return noteList[index];
            }
            set
            {
                noteList[index] = value;
            }
        }
        #endregion

        #region IRiff Members
        /// <summary>
        /// Whether riffs are equal
        /// </summary>
        /// <param name="other">other riff</param>
        /// <returns>Whether riffs are equal</returns>
        public bool Equals(IRiff other)
        {
            if (other is Riff)
            {
                Riff otherRiff = (Riff)other;

                if (otherRiff.Count != this.Count)
                    return false;

                int noteIndex = 0;
                foreach (Note note in this)
                    if (otherRiff[noteIndex] != note)
                        return false;

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
