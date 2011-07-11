﻿using System;
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
    internal static class BlockDispatcherWave
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch blocks
        /// </summary>
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="addedBlockMemory">to remember blocks that are already there</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchBlocks(Ground ground, Level level, SpritePopulation spritePopulation, HashSet<int> addedBlockMemory, Random random)
        {
            double yPosition;

            AbstractWave yDistanceFromGroundWave = BuildBlockYDistanceFromGroundWave(random);
            AbstractWave anarchyBlockProbabilityWave = BlockDispatcher.BuildSpecialBlockTypeProbabilityWave(random);
            AbstractWave hiddenAnarchyBlockProbabilityWave = BlockDispatcher.BuildSpecialBlockTypeProbabilityWave(random);
            AbstractWave indestructibleBlockProbabilityWave = BlockDispatcher.BuildSpecialBlockTypeProbabilityWave(random);
            AbstractWave densityWave = BlockDispatcher.BuildDensityWave(random);

            int groundSamplingWidthMin = random.Next(1, 7);
            int groundSamplingWidthMax = random.Next(4, 15);
            int groundSamplingWidthCurrent = 0;
            double sampledGroundYPosition = 0;

            double minimumGroundDistance = 3.0;// (double)random.Next(3, 7);

            for (double xPosition = level.LeftBound; xPosition < level.RightBound; xPosition++)
            {
                if (densityWave[xPosition] < 0.0625)
                    continue;

                if (groundSamplingWidthCurrent <= 0)
                {
                    groundSamplingWidthCurrent = random.Next(groundSamplingWidthMin, Math.Max(groundSamplingWidthMax,groundSamplingWidthMin));
                    sampledGroundYPosition = ground[xPosition];
                }
                groundSamplingWidthCurrent--;

                double yOffset = yDistanceFromGroundWave[xPosition];

                if (yOffset > 0)
                    continue;

                yPosition = Math.Round(sampledGroundYPosition + yOffset - minimumGroundDistance);

                if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition, yPosition - 1.5, ground, level))
                    continue;
                else if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition - 0.5, yPosition - 1.5, ground, level))
                    continue;
                else if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition + 0.5, yPosition - 1.5, ground, level))
                    continue;
                else if (yPosition >= ground[xPosition] - minimumGroundDistance)
                    continue;
                else if (yPosition >= ground[xPosition - 0.5] - minimumGroundDistance)
                    continue;
                else if (yPosition >= ground[xPosition + 0.5] - minimumGroundDistance)
                    continue;

                int uniqueBlockKey = (int)xPosition * 4000 + (int)yPosition;

                if (!addedBlockMemory.Contains(uniqueBlockKey))
                {
                    if (IGroundHelper.IsGroundVisible(ground, level, xPosition))
                    {
                        StaticSprite blockSprite;
                        if (anarchyBlockProbabilityWave[xPosition] > 1.0 || anarchyBlockProbabilityWave[xPosition]< -1.0)
                            blockSprite = new AnarchyBlockSprite(xPosition, yPosition, random, false);
                        else if (hiddenAnarchyBlockProbabilityWave[xPosition] > 1.0 || hiddenAnarchyBlockProbabilityWave[xPosition] < -1.0)
                            blockSprite = new AnarchyBlockSprite(xPosition, yPosition, random, true);
                        else if (indestructibleBlockProbabilityWave[xPosition] > 1.0 || indestructibleBlockProbabilityWave[xPosition] < -1.0)
                            blockSprite = new BrickSprite(xPosition, yPosition, random, false);
                        else
                            blockSprite = new BrickSprite(xPosition, yPosition, random, true);

                        spritePopulation.Add(blockSprite);
                        addedBlockMemory.Add(uniqueBlockKey);
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// For block dispatcher, build wave for block segment distance from ground
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave for block segment distance from ground</returns>
        private static AbstractWave BuildBlockYDistanceFromGroundWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(4, 32, 2, 8, random, false, true));
            } while (random.Next(0, 3) != 0);
            wavePack.Normalize((double)random.Next(2, 8));

            return wavePack;
        }
        #endregion
    }
}