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
        /// <param name="addedBlockMemory">memory of added blocks</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchPipes(Level level, SpritePopulation spritePopulation, Random random)
        {
            double pipeDensity = random.NextDouble() * 0.01 + 0.02;
            int pipeCount = (int)Math.Round(pipeDensity * level.Size);

            DispatchUpwardPipes((int)Math.Ceiling(((double)pipeCount) / 2.0), level, spritePopulation, random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Dispatch upwards
        /// </summary>
        /// <param name="pipeCount">how many upward pipes</param>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">all other sprites</param>
        /// <param name="random">random number generator</param>
        private static void DispatchUpwardPipes(int pipeCount, Level level, SpritePopulation spritePopulation, Random random)
        {
            const int maxTryCount = 20;
            for (int i = 0; i < pipeCount; i++)
            {
                for (int tryCount = 0; tryCount < maxTryCount; tryCount++)
                {
                    double xPosition = random.NextDouble() * level.Size + level.LeftBound;
                    double yPosition = SpriteDispatcher.GetRandomVisibleGround(level, random, xPosition)[xPosition];

                    PipeSprite pipeSprite = new PipeSprite(xPosition, yPosition, random);

                    spritePopulation.Add(pipeSprite);

                    if (IsInAcceptableDistance(pipeSprite, spritePopulation))
                    {
                        break;
                    }
                    else
                    {
                        spritePopulation.Remove(pipeSprite);
                    }
                }
            }
        }

        /// <summary>
        /// Whether pipe sprite is in acceptable distance to other sprites
        /// </summary>
        /// <param name="pipeSprite">pipe sprite</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns>Whether pipe sprite is in acceptable distance to other sprites</returns>
        private static bool IsInAcceptableDistance(PipeSprite pipeSprite, SpritePopulation spritePopulation)
        {
            foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
            {
                if (pipeSprite == otherSprite)
                    continue;

                if (Physics.IsDetectCollision(pipeSprite, pipeSprite.XPosition, pipeSprite.YPosition - 1.0, 2.0, otherSprite))
                    return false;
                else if (Physics.IsDetectCollision(pipeSprite, pipeSprite.XPosition, pipeSprite.YPosition + 1.0, 2.0, otherSprite))
                    return false;
            }

            return true;
        }
        #endregion
    }
}
