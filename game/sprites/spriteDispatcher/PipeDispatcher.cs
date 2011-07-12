using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// To dispatch pipes
    /// </summary>
    internal static class PipeDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch pipes in level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchPipes(Level level, SpritePopulation spritePopulation, Random random)
        {
            double pipeDensity = random.NextDouble() * 0.01 + 0.02;
            int pipeCount = (int)Math.Round(pipeDensity * level.Size);


        }
        #endregion
    }
}
