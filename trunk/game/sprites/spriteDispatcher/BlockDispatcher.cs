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
            HashSet<int> addedBlockMemory = new HashSet<int>();

            //AbstractWave segmentWidthWave = WaveBuilder.BuildBlockSegmentWidthWave(random);
            //AbstractWave xSegmentDistanceWave = WaveBuilder.BuildXBlockSegmentDistanceWave(random);

            double yPosition;
            //double segmentBeingDrawnCurrentWidth = 0.0;
            //double desiredSegmentWidth;
            foreach (Ground ground in level)
            {
                AbstractWave yDistanceFromGroundWave = WaveBuilder.BuildBlockYDistanceFromGroundWave(random);

                for (double xPosition = level.LeftBound; xPosition < level.RightBound; xPosition++)
                {
                    double yOffset = yDistanceFromGroundWave[xPosition];

                    if (yOffset > 0)
                        continue;

                    //desiredSegmentWidth = segmentWidthWave[xPosition];
                    yPosition = Math.Round(ground[xPosition] + yOffset + 2.0);
                    //segmentBeingDrawnCurrentWidth++;

                    int uniqueBlockKey = (int)xPosition + (int)yPosition * 100000;

                    if (!addedBlockMemory.Contains(uniqueBlockKey))
                    {
                        spritePopulation.Add(new BrickSprite(xPosition, yPosition, random, true));
                        addedBlockMemory.Add(uniqueBlockKey);
                    }
                }
            }
        }
        #endregion
    }
}
