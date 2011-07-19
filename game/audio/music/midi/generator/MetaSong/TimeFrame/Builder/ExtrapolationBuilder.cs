using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class ExtrapolationBuilder
    {
        #region Public Methods
        public List<BarBlock> BuildExtrapolation(Random random, BarBlock currentPeak, int barCountForEachVerse)
        {
            List<BarBlock> extrapolation = new List<BarBlock>();

            BarBlock previousBarBlock = null;
            BarBlock currentBarBlock = null;
            for (int i = 0; i < barCountForEachVerse; i++)
            {
                if (previousBarBlock == null)
                    currentBarBlock = new BarBlock(currentPeak);
                else
                {
                    currentBarBlock = new BarBlock(currentBarBlock);
                    if (currentBarBlock.Count > 1)
                        currentBarBlock.RemoveAt(currentBarBlock.Count - 1);
                }

                extrapolation.Add(currentBarBlock);

                previousBarBlock = currentPeak;
            }

            extrapolation.Reverse();
            return extrapolation;
        }
        #endregion
    }
}
