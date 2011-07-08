using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatch blocks
    /// </summary>
    internal static class BlockDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch blocks
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchBlocks(Level level, SpritePopulation spritePopulation, Random random)
        {
            AbstractWave yDistanceFromGroundWave = WaveBuilder.BuildBlockYDistanceFromGroundWave(random);
            AbstractWave segmentWidthWave = WaveBuilder.BuildBlockSegmentWidthWave(random);
            AbstractWave xSegmentDistanceWave = WaveBuilder.BuildXBlockSegmentDistanceWave(random);
        }
        #endregion
    }
}
