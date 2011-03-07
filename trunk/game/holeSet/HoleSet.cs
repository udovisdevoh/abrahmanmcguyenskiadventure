using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents wave modelization of holes in a level
    /// </summary>
    internal class HoleSet
    {
        #region Fields and parts
        /// <summary>
        /// For Y position of holes
        /// </summary>
        private IWave holeYPositionWave;

        /// <summary>
        /// For thickness of holes (on Y axis)
        /// </summary>
        private IWave holeThicknessWave;

        /// <summary>
        /// For probability of having holes
        /// </summary>
        private IWave holeProbabilityWave;

        /// <summary>
        /// Threshold for probability of having holes
        /// </summary>
        private double holeProbabilityThreshold;
        #endregion

        #region Constructor
        /// <summary>
        /// Build wave modelization of holes for a level
        /// </summary>
        /// <param name="random">random number generator</param>
        public HoleSet(Random random)
        {
            holeYPositionWave = WaveBuilder.BuildWavePack(random);
            holeYPositionWave.Normalize(1.0, true);

            holeThicknessWave = WaveBuilder.BuildWavePack(random);
            holeThicknessWave.Normalize(1.0, true);

            holeProbabilityWave = WaveBuilder.BuildWavePack(random);
            holeProbabilityWave.Normalize(1.0, true);

            holeProbabilityThreshold = random.NextDouble();
        }
        #endregion
    }
}
