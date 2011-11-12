using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatches trampolines
    /// </summary>
    internal static class TrampolineDispatcher
    {
        /// <summary>
        /// Dispatch trampolines
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchTrampolines(Level level, SpritePopulation spritePopulation, Random random)
        {
            double trampolineDensity = random.NextDouble() * 0.03 + 0.007;
            int trampolineCount = (int)(trampolineDensity * level.Size);

            double xPosition, yPosition;
            for (int i = 0; i < trampolineCount; i++)
            {
                xPosition = random.NextDouble() * level.Size + level.LeftBound;
                Ground ground = SpriteDispatcher.GetRandomVisibleGround(level, random, xPosition);
                yPosition = ground[xPosition];

                TrampolineSprite trampolineSprite = new TrampolineSprite(xPosition, yPosition, random);

                spritePopulation.Add(trampolineSprite);
            }
        }
    }
}
