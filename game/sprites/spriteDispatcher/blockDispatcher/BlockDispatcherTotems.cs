﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatch blocks on ground (as hills)
    /// </summary>
    internal static class BlockDispatcherTotems
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
        internal static int DispatchBlocks(Ground ground, Level level, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, AbstractGameMode gameMode, Random random)
        {
            int totalBlockAdded = 0;
            double yPosition;

            AbstractWave yDistanceFromGroundWave = BuildBlockYDistanceFromGroundWave(random);

            int groundSamplingWidthMin = random.Next(1, 7);
            int groundSamplingWidthMax = random.Next(4, 15);

            if (gameMode.BlockDensityMultiplicator != 1.0)
            {
                groundSamplingWidthMin = (int)(Math.Round((double)groundSamplingWidthMin / gameMode.BlockDensityMultiplicator));
                groundSamplingWidthMax = (int)(Math.Round((double)groundSamplingWidthMax / gameMode.BlockDensityMultiplicator));
            }
            
            int groundSamplingWidthCurrent = 0;
            double sampledGroundYPosition = 0;


            for (double xPosition = level.LeftBound; xPosition < level.RightBound; xPosition++)
            {
                if (xPosition > -2.0 && xPosition < 2.0) //Clear the entrance portal
                    continue;

                if (ground[xPosition - 0.7] > Program.holeHeight / 2.0 || ground[xPosition + 0.7] > Program.holeHeight / 2.0)
                    continue;

                if (groundSamplingWidthCurrent <= 0)
                {
                    groundSamplingWidthCurrent = 1;// random.Next(groundSamplingWidthMin, Math.Max(groundSamplingWidthMax, groundSamplingWidthMin));
                    sampledGroundYPosition = ground[xPosition];
                }
                groundSamplingWidthCurrent--;

                double yOffset = yDistanceFromGroundWave[xPosition] + 6;

                /*if (level.Ceiling != null && IGroundHelper.IsHigherThanOtherGrounds(ground, level, xPosition) && yOffset > 0)
                    yOffset = -yOffset;
                else */if (yOffset > 0)
                    continue;

                //To prevent some totems that can't be jumped over because they are on the tip of a sharp hill
                if (ground[xPosition - 1.0] - (ground[xPosition] + yOffset) > 2.5)
                    continue;
                else if (ground[xPosition + 1.0] - (ground[xPosition] + yOffset) > 2.5)
                    continue;

                #region Must not be to close to ceiling
                if (level.Ceiling != null)
                {
                    if (ground[xPosition] + yOffset - 1 - level.Ceiling[xPosition] <= Program.absoluteMaxCeilingHeight + 1)
                        continue;
                    else if (ground[xPosition] + yOffset - 1 - level.Ceiling[xPosition - 1.5] <= Program.absoluteMaxCeilingHeight + 1)
                        continue;
                    else if (ground[xPosition] + yOffset - 1 - level.Ceiling[xPosition + 1.5] <= Program.absoluteMaxCeilingHeight + 1)
                        continue;
                }
                #endregion

                yPosition = Math.Round(sampledGroundYPosition + yOffset);
                bool isCouldAddBlock;

                do
                {
                    isCouldAddBlock = false;
                    bool isPreventAddBlock = false;
                    if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition, yPosition - 1.5, ground, level))
                        isPreventAddBlock = true;
                    else if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition - 0.5, yPosition - 1.5, ground, level))
                        isPreventAddBlock = true;
                    else if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition + 0.5, yPosition - 1.5, ground, level))
                        isPreventAddBlock = true;
                    else if (yPosition - 1 >= ground[xPosition])
                        isPreventAddBlock = true;
                    else if (yPosition - 1 >= ground[xPosition - 0.5])
                        isPreventAddBlock = true;
                    else if (yPosition - 1 >= ground[xPosition + 0.5])
                        isPreventAddBlock = true;

                    if (!isPreventAddBlock)
                    {
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
                                isCouldAddBlock = true;
                                totalBlockAdded++;

                                foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
                                {
                                    if (otherSprite is VortexSprite && Physics.IsDetectCollision(blockSprite, otherSprite))
                                    {
                                        isCouldAddBlock = false;
                                        spritePopulation.Remove(blockSprite);
                                        totalBlockAdded--;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    yPosition += 1.0;
                } while (isCouldAddBlock);
            }
            return totalBlockAdded;
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
                wavePack.Add(WaveBuilder.BuildIndividualWave(0, 8, 0, 2.5, random, true, false, false));
            } while (random.Next(0, 4) != 0);
            wavePack.Normalize(8.0);

            return wavePack;
        }
        #endregion
    }
}
