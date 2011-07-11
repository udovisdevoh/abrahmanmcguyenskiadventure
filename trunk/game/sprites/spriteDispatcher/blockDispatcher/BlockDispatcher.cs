using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    internal static class BlockDispatcher
    {
        /// <summary>
        /// Dispatch blocks
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchBlocks(Level level, SpritePopulation spritePopulation, Random random)
        {
            HashSet<int> addedBlockMemory = new HashSet<int>();
            foreach (Ground ground in level)
            {
                BlockDispatcherWave.DispatchBlocks(ground, level, spritePopulation, addedBlockMemory, random);
            }
        }
    }
}
