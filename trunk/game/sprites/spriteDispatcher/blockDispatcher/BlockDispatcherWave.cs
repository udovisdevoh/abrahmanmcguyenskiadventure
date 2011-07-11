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
            //AbstractWave segmentWidthWave = WaveBuilder.BuildBlockSegmentWidthWave(random);
            //AbstractWave xSegmentDistanceWave = WaveBuilder.BuildXBlockSegmentDistanceWave(random);

            double yPosition;
            //double segmentBeingDrawnCurrentWidth = 0.0;
            //double desiredSegmentWidth;

            AbstractWave yDistanceFromGroundWave = BuildBlockYDistanceFromGroundWave(random);
            AbstractWave anarchyBlockProbabilityWave = BuildSpecialBlockTypeProbabilityWave(random);
            AbstractWave hiddenAnarchyBlockProbabilityWave = BuildSpecialBlockTypeProbabilityWave(random);
            AbstractWave indestructibleBlockProbabilityWave = BuildSpecialBlockTypeProbabilityWave(random);
            AbstractWave densityWave = BuildDensityWave(random);

            int groundSamplingWidthMin = random.Next(1, 7);
            int groundSamplingWidthMax = random.Next(4, 15);
            int groundSamplingWidthCurrent = 0;
            double sampledGroundYPosition = 0;

            double minimumGroundDistance = 3.0;// (double)random.Next(3, 7);

            for (double xPosition = level.LeftBound; xPosition < level.RightBound; xPosition++)
            {
                if (densityWave[xPosition] < 0.125)
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

                //desiredSegmentWidth = segmentWidthWave[xPosition];
                yPosition = Math.Round(sampledGroundYPosition + yOffset - minimumGroundDistance);
                //segmentBeingDrawnCurrentWidth++;

                if (IsHigherThanHigherGroundThan(xPosition, yPosition - 1.5, ground, level))
                    continue;
                else if (IsHigherThanHigherGroundThan(xPosition - 0.5, yPosition - 1.5, ground, level))
                    continue;
                else if (IsHigherThanHigherGroundThan(xPosition + 0.5, yPosition - 1.5, ground, level))
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
            } while (random.Next(0, 2) != 0);
            wavePack.Normalize((double)random.Next(2, 8));

            return wavePack;
        }

        /// <summary>
        /// For block dispatcher, build wave for probability of having a visible anarchy block
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>For block dispatcher, wave for probability of having a visible anarchy block</returns>
        private static AbstractWave BuildSpecialBlockTypeProbabilityWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(0.5, 8, 0, 1, random, false, true));
            } while (random.Next(0, 7) != 0);
            double normalizationFactor = random.NextDouble() * 0.5 + 1.0;
            wavePack.Normalize(normalizationFactor);

            return wavePack;
        }

        /// <summary>
        /// For block dispatcher, build wave for probability of having blocks (0 = yes)
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>For block dispatcher, build wave for probability of having blocks (0 = yes)</returns>
        private static AbstractWave BuildDensityWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(4, 16, 0, 1, random, false, true));
            } while (random.Next(0, 5) != 0);
            wavePack.Normalize();

            return wavePack;
        }

        /// <summary>
        /// For block dispatcher, build wave for block segment width
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave for block segment width</returns>
        private static AbstractWave BuildBlockSegmentWidthWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(1, 16, 1, 32, random, false, true));
            } while (random.Next(0, 3) != 0);
            return wavePack;
        }

        /// <summary>
        /// For block dispatcher, build wave for block segment distance between each others
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave for block segment distance between each others</returns>
        private static AbstractWave BuildXBlockSegmentDistanceWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(1, 32, 1, 9, random, false, true));
            } while (random.Next(0, 3) != 0);
            return wavePack;
        }
        #endregion
    }
}
