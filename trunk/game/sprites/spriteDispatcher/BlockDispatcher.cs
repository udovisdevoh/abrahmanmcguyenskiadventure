using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

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
                    yPosition = Math.Round(ground[xPosition] + yOffset - 2.0);
                    //segmentBeingDrawnCurrentWidth++;


                    int uniqueBlockKey = (int)xPosition * 4000 + (int)yPosition;

                    if (!addedBlockMemory.Contains(uniqueBlockKey))
                    {
                        if (!IsHigherThanHigherGroundThan(xPosition, yPosition - 2.0, ground, level))
                        {
                            if (IGroundHelper.IsGroundVisible(ground, level, xPosition))
                            {
                                spritePopulation.Add(new BrickSprite(xPosition, yPosition, random, true));
                                addedBlockMemory.Add(uniqueBlockKey);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Whether Y position is higher than a ground in level which is higher than provided ground
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <param name="ground">Ground</param>
        /// <param name="level">Level</param>
        /// <returns>Whether Y position is higher than a ground in level which is higher than provided ground</returns>
        private static bool IsHigherThanHigherGroundThan(double xPosition, double yPosition, Ground ground, Level level)
        {
            double groundHeight = ground[xPosition];
            foreach (Ground otherGround in level)
            {
                double otherGroundHeight = otherGround[xPosition];
                if (otherGroundHeight < groundHeight)
                {
                    if (yPosition < otherGroundHeight)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion
    }
}
