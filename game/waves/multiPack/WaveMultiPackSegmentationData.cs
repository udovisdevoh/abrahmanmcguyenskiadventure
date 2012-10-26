using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents information about where wave packs are segmented and what are the X offsets for wavepack
    /// </summary>
    internal class WaveMultiPackSegmentationData
    {
        #region Fields and parts
        /// <summary>
        /// List of wave packs
        /// </summary>
        private List<WavePack> wavePackList = new List<WavePack>();

        private List<double> previousWaveWidthList = new List<double>();

        private List<double> xOffsetList = new List<double>();

        private List<double> yOffsetList = new List<double>();
        #endregion

        #region Internal Methods
        internal void Clear()
        {
            wavePackList.Clear();
            previousWaveWidthList.Clear();
            xOffsetList.Clear();
            yOffsetList.Clear();
        }

        internal void Add(WavePack wavePack)
        {
            if (wavePackList.Count == 0)
            {
                Add(wavePack, 0.0, 0.0, 0.0);
            }
            else
            {
                WavePack previousWavePack = wavePackList[wavePackList.Count - 1];
                double previousWavePackYOffset = previousWaveWidthList[wavePackList.Count - 1];
                double previousWavePackWidth = BuildWavePackWidth(previousWavePack);
                #warning Implement GetBestXOffset()
                double bestXOffset = 0;// GetBestXOffset(wavePack, previousWavePack, previousWavePackWidth);
                double yOffset = (previousWavePack[bestXOffset] + previousWavePackYOffset) - wavePack[bestXOffset];
                Add(wavePack, previousWavePackWidth, bestXOffset, yOffset);
            }
        }

        /// <summary>
        /// Get active wave at X position
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="xOffset">x offset to move X</param>
        /// <param name="yOffset">y offset to move Y</param>
        /// <returns>Active wave at X position</returns>
        internal WavePack GetSelectedWavePackAt(double x, out double xOffset, out double yOffset)
        {
            double currentX = 0.0;
            int waveIndex = 0;

            foreach (double waveWidth in previousWaveWidthList)
            {
                currentX += waveWidth;

                if (currentX > x)
                    break;

                waveIndex++;
            }

            WavePack wavePack = wavePackList[waveIndex];
            xOffset = xOffsetList[waveIndex];
            yOffset = yOffsetList[waveIndex];

            return wavePack;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add wave pack
        /// </summary>
        /// <param name="previosWaveWidth">width of the previous wave</param>
        /// <param name="wavePack">wave pack</param>
        /// <param name="xOffset">x offset to move X</param>
        /// <param name="yOffset">y offset to move Y</param>
        private void Add(WavePack wavePack, double previosWaveWidth, double xOffset, double yOffset)
        {
            wavePackList.Add(wavePack);
            previousWaveWidthList.Add(previosWaveWidth);
            xOffsetList.Add(xOffset);
            yOffsetList.Add(yOffset);
        }

        /// <summary>
        /// Gets wave pack's widest wave's cycle length
        /// </summary>
        /// <param name="wavePack">wave pack</param>
        /// <returns>wave pack's widest wave's cycle length</returns>
        private double BuildWavePackWidth(WavePack wavePack)
        {
            double widestWaveLength = 0;

            foreach (AbstractWave abstractWave in wavePack)
            {
                if (abstractWave is Wave)
                {
                    Wave wave = (Wave)abstractWave;
                    if (wave.WaveLength > widestWaveLength)
                    {
                        widestWaveLength = wave.WaveLength;
                    }
                }
            }

            return widestWaveLength;
        }
        #endregion
    }
}
