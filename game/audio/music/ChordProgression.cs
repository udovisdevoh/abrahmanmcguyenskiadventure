using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    internal enum Chord { CMajor, FMajor, GMajor, DMinor, EMinor, AMinor, CMajor7th, FMajor7th, GMajor7th, DMinor7th, EMinor7th, AMinor7th }
    /// <summary>
    /// Chord progression
    /// </summary>
    internal class ChordProgression
    {
        #region Fields and parts
        private List<Chord> chordList;

        private double chordTimeSpanBarCount;
        #endregion

        #region Constructor
        public ChordProgression(Random random)
        {
            chordList = new List<Chord>();
            chordTimeSpanBarCount = Math.Pow(2, random.Next(0, 2));
            bool isMajor = random.Next(0, 2) == 1;
            int chordCount = random.Next(1, 3) * 8;

            for (int i = 0; i < chordCount; i++)
            {
                if (i % 4 == 3) //Last chord of block is always the fundamental
                {
                    if (isMajor)
                        chordList.Add(Chord.CMajor);
                    else
                        chordList.Add(Chord.AMinor);
                }
                else
                {
                    bool is7th = random.Next(0, 7) == 1;
                    bool isOutOfMajorMinorRange = random.Next(0, 7) == 1;
                    chordList.Add(GetRandomChord(random, isMajor || isOutOfMajorMinorRange, is7th));
                }
            }
        }
        #endregion

        #region Private Method
        private Chord GetRandomChord(Random random, bool isMajor, bool is7th)
        {
            if (isMajor)
            {
                if (is7th)
                {
                    return (Chord)random.Next(6, 9);
                }
                else
                {
                    return (Chord)random.Next(0, 3);
                }
            }
            else
            {
                if (is7th)
                {
                    return (Chord)random.Next(9, 12);
                }
                else
                {
                    return (Chord)random.Next(3, 6);
                }
            }
        }
        #endregion
    }
}
