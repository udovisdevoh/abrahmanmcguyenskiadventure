using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// To dispatch lianas
    /// </summary>
    internal static class LianaDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch lianas
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">all sprites</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchLianas(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, Random random)
        {
            const int maxTryCount = 100;
            const double minGroundDistance = 5.0;
            double density = random.NextDouble() * 0.15;
            int countToAdd = (int)Math.Round(level.Size * density);

            while (countToAdd > 0)
            {
                int tryCount = 0;
                bool isCanAdd;
            
            tryAgain:
                isCanAdd = true;
                Ground attachedGround;
                
                if (level.Ceiling != null && random.Next(0, level.Count + 1) == 1)
                    attachedGround = level.Ceiling;
                else
                    attachedGround = level[random.Next(level.Count)];

                double xPosition = random.NextDouble() * level.Size + level.LeftBound;
                double yPosition = attachedGround[xPosition];
                LianaSprite lianaSprite = new LianaSprite(xPosition, yPosition, random);
                spritePopulation.Add(lianaSprite);
                lianaSprite.YPosition += lianaSprite.Height;

                Ground groundBelow = (Ground)IGroundHelper.GetHighestVisibleIGroundBelowSprite(lianaSprite, level, null, false);

                if (groundBelow == null || groundBelow == attachedGround || groundBelow[xPosition] - attachedGround[xPosition] <= minGroundDistance)
                    isCanAdd = false;

                if (!isCanAdd)
                {
                    spritePopulation.Remove(lianaSprite);
                    tryCount++;
                    if (tryCount < maxTryCount)
                    {
                        goto tryAgain;
                    }
                }

                countToAdd--;
            }
        }
        #endregion
    }
}
