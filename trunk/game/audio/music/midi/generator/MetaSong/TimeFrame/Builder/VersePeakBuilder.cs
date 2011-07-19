using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class VersePeakBuilder
    {
        #region Public Methods
        public BarBlock BuildVersePeak(Random random, List<BarBlock> listVersePeaks, int riffTrackCount, int verseCount)
        {
            int desiredCount = riffTrackCount / 2;
            if (desiredCount < 1)
                desiredCount = 1;


            BarBlock versePeak = new BarBlock();

            HashSet<int> listAllowedRiff = BuildListAllowedRiff(riffTrackCount, listVersePeaks);

            for (int i = 0; i < desiredCount; i++)
            {
                if (listAllowedRiff.Count < 1)
                    break;

                int value = GetRandomValue(random, listAllowedRiff);
                listAllowedRiff.Remove(value);
                versePeak.Add(value);
            }

            return versePeak;
        }

        private int GetRandomValue(Random random, HashSet<int> listAllowedRiff)
        {
            int key = random.Next(0, listAllowedRiff.Count);
            List<int> list = new List<int>(listAllowedRiff);
            return list[key];
        }
        #endregion

        #region Private Methods
        private HashSet<int> BuildListAllowedRiff(int riffTrackCount, List<BarBlock> listVersePeaks)
        {
            HashSet<int> listAllowedRiff = new HashSet<int>();

            for (int i = 0; i < riffTrackCount; i++)
                listAllowedRiff.Add(i);

            /*foreach (BarBlock currentBarBlock in listVersePeaks)
                foreach (int currentRiffNumber in currentBarBlock)
                    if (listAllowedRiff.Count > 1)
                        listAllowedRiff.Remove(currentRiffNumber);*/

            return listAllowedRiff;
        }
        #endregion
    }
}
