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
        internal static void DispatchBlocks(Ground ground, Level level, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, Random random)
        {
            double yPosition;

            AbstractWave yDistanceFromGroundWave = BuildBlockYDistanceFromGroundWave(random);
            AbstractWave densityWave = BlockDispatcher.BuildDensityWave(random);

            int groundSamplingWidthMin = random.Next(1, 7);
            int groundSamplingWidthMax = random.Next(4, 15);
            int groundSamplingWidthCurrent = 0;
            double sampledGroundYPosition = 0;

            double minimumGroundDistance = 2.0;// (double)random.Next(3, 7);

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

                if (level.Ceiling != null && IGroundHelper.IsHigherThanOtherGrounds(ground, level, xPosition) && yOffset > 0)
                    yOffset = -yOffset;
                else if (yOffset > 0)
                    continue;

                yPosition = Math.Round(sampledGroundYPosition + yOffset - minimumGroundDistance);

                #region Must not be to close to ceiling
                if (level.Ceiling != null)
                {
                    while (yPosition - 1 - level.Ceiling[xPosition] <= Program.absoluteMaxCeilingHeight)
                        yPosition++;
                    while (yPosition - 1 - level.Ceiling[xPosition - 1.5] <= Program.absoluteMaxCeilingHeight)
                        yPosition++;
                    while (yPosition - 1 - level.Ceiling[xPosition + 1.5] <= Program.absoluteMaxCeilingHeight)
                        yPosition++;
                }
                #endregion

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


                if (!addedBlockMemory.Contains((int)xPosition, (int)yPosition))
                {
                    if (IGroundHelper.IsGroundVisible(ground, level, xPosition))
                    {
                        StaticSprite blockSprite;
                        if (random.NextDouble() < BlockDispatcher.anarchyBlockProbability)
                            blockSprite = new AnarchyBlockSprite(xPosition, yPosition, random, false);
                        else if (random.NextDouble() < BlockDispatcher.hiddenAnarchyBlockProbability)
                            blockSprite = new AnarchyBlockSprite(xPosition, yPosition, random, true);
                        else if (random.NextDouble() < BlockDispatcher.indestructibleBlockProbability)
                            blockSprite = new BrickSprite(xPosition, yPosition, random, false);
                        else
                            blockSprite = new BrickSprite(xPosition, yPosition, random, true);

                        spritePopulation.Add(blockSprite);
                        addedBlockMemory.Add((int)xPosition, (int)yPosition);
                    }
                }
            }
        }

        /// <summary>
        /// For block dispatcher, build wave for block segment distance from ground
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave for block segment distance from ground</returns>
        internal static AbstractWave BuildBlockYDistanceFromGroundWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(4, 32, 2, 8, random, false, true));
            } while (random.Next(0, 3) != 0);
            wavePack.Normalize((double)random.Next(2, 5));

            return wavePack;
        }
        #endregion
    }
}
