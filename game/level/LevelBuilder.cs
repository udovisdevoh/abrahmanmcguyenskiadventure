using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    internal static class LevelBuilder
    {
        /// <summary>
        /// Build level bound
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="skillLevel">skill level</param>
        /// <returns>level bound</returns>
        internal static double BuildLevelBound(Random random, int skillLevel)
        {
            return random.Next(0, 200 * (skillLevel + 1)) + 30;
        }

        /// <summary>
        /// Build bound type
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Bound type</returns>
        internal static LevelBoundType BuildBoundType(Random random)
        {
            return (LevelBoundType)random.Next(0, 4);
        }
    }
}
