﻿using System;
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
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchPipes(Level level, SpritePopulation spritePopulation, int skillLevel, WaterInfo waterInfo, Random random)
        {
            double pipeDensity = random.NextDouble() * 0.01 + 0.02;
            int pipeCount = (int)Math.Round(pipeDensity * level.Size);

            if (pipeCount < 1)
                return;

            int pluggedPipeCount = (int)Math.Round((random.NextDouble() * 0.6 + 0.4) * (double)pipeCount);

            int upwardPipeCount = (int)Math.Ceiling(((double)pipeCount) / 2.0);
            int downwardPipeCount = pipeCount - upwardPipeCount;

            DispatchUpwardOrDownwardPipes(upwardPipeCount, level, spritePopulation, true, waterInfo, random);
            if (downwardPipeCount > 0)
                DispatchUpwardOrDownwardPipes(downwardPipeCount, level, spritePopulation, false, waterInfo, random);

            List<PipeSprite> pipeList = GetPipeList(spritePopulation);
            pipeCount = pipeList.Count;

            PlugSomePipes(pluggedPipeCount, pipeList, level, random);

            int drillCount = BuildDrillCount(skillLevel, pipeCount, random);
            int blackDrillCount = BuildDrillCount(skillLevel, drillCount, random);
            if (skillLevel < 5)
                blackDrillCount = 0;
            int whiteDrillCount = drillCount - blackDrillCount;
            PlugSomeDrills(whiteDrillCount, pipeList, spritePopulation, false, random);
            PlugSomeDrills(blackDrillCount, pipeList, spritePopulation, true, random);
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
        private static void DispatchUpwardOrDownwardPipes(int pipeCount, Level level, SpritePopulation spritePopulation, bool isUpward, WaterInfo waterInfo, Random random)
        {
            const int maxTryCount = 100;
            const double maxSlopeHeight = 1.5;
            for (int i = 0; i < pipeCount; i++)
            {
                for (int tryCount = 0; tryCount < maxTryCount; tryCount++)
                {
                    double xPosition = random.NextDouble() * level.Size + level.LeftBound;
                    Ground attachedGround = SpriteDispatcher.GetRandomVisibleGround(level, random, xPosition, !isUpward);

                    double yPosition = attachedGround[xPosition];

                    PipeSprite pipeSprite = new PipeSprite(xPosition, yPosition, isUpward, random);

                    if (!isUpward)
                    {
                        if (attachedGround != level.Ceiling)
                            pipeSprite.TopBound = pipeSprite.YPosition + 1.0;
                        else
                            pipeSprite.TopBound = pipeSprite.YPosition;
                    }

                    spritePopulation.Add(pipeSprite);

                    if (isUpward)
                    {
                        if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition, pipeSprite.TopBound + 0.5, attachedGround, level))
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition - 1.1, pipeSprite.TopBound + 0.5, attachedGround, level))
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (BlockDispatcher.IsHigherThanHigherGroundThan(xPosition + 1.1, pipeSprite.TopBound + 0.5, attachedGround, level))
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                    }
                    else
                    {
                        IGround groundBelowPipe = IGroundHelper.GetHighestVisibleIGroundBelowSprite(pipeSprite, level, null, false);

                        if (groundBelowPipe == null)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (groundBelowPipe[xPosition] > Program.holeHeight / 2.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (groundBelowPipe[xPosition + 2.0] > Program.holeHeight / 2.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (groundBelowPipe[xPosition - 2.0] > Program.holeHeight / 2.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (groundBelowPipe[xPosition] - pipeSprite.YPosition < 2.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (groundBelowPipe[xPosition + 2.0] - pipeSprite.YPosition < 2.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (groundBelowPipe[xPosition - 2.0] - pipeSprite.YPosition < 2.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (attachedGround == level.Ceiling && (waterInfo == null || waterInfo.HeightInDouble > pipeSprite.YPosition) && groundBelowPipe[xPosition] - pipeSprite.YPosition > 10.0)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (attachedGround != level.Ceiling && pipeSprite.TopBound < yPosition + 0.5)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (attachedGround != level.Ceiling && pipeSprite.TopBound < attachedGround[xPosition - 1.5] + 0.5)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (attachedGround != level.Ceiling && pipeSprite.TopBound < attachedGround[xPosition + 1.5] + 0.5)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                    }


                    if (pipeSprite.YPosition > Program.holeHeight / 2.0)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }

                    #region Must not be to close to ceiling
                    if (level.Ceiling != null && attachedGround != level.Ceiling)
                    {
                        if (pipeSprite.TopBound - level.Ceiling[xPosition] <= Program.absoluteMaxCeilingHeight)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (pipeSprite.TopBound - level.Ceiling[xPosition - 2.0] <= Program.absoluteMaxCeilingHeight)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                        else if (pipeSprite.TopBound - level.Ceiling[xPosition + 2.0] <= Program.absoluteMaxCeilingHeight)
                        {
                            spritePopulation.Remove(pipeSprite);
                            continue;
                        }
                    }
                    #endregion

                    if (Math.Abs(attachedGround[xPosition - 2.0] - attachedGround[xPosition]) > maxSlopeHeight)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }
                    else if (Math.Abs(attachedGround[xPosition] - attachedGround[xPosition + 2.0]) > maxSlopeHeight)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }
                    else if (Math.Abs(attachedGround[xPosition - 2.0] - attachedGround[xPosition + 2.0]) > maxSlopeHeight)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }
                    else if (Math.Abs(attachedGround[xPosition - 1.0] - attachedGround[xPosition + 1.0]) > maxSlopeHeight)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }
                    else if (Math.Abs(attachedGround[xPosition - 1.0] - attachedGround[xPosition + 2.0]) > maxSlopeHeight)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }
                    else if (Math.Abs(attachedGround[xPosition - 2.0] - attachedGround[xPosition + 1.0]) > maxSlopeHeight)
                    {
                        spritePopulation.Remove(pipeSprite);
                        continue;
                    }


                    if (IsAtAcceptablePosition(pipeSprite) && IsInAcceptableDistanceToOtherSprites(pipeSprite, spritePopulation))
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
        /// Plug some drills
        /// </summary>
        /// <param name="drillCount">how many drills</param>
        /// <param name="pipeList">list of pipes</param>
        /// <param name="spritePopulation">all the sprites</param>
        /// <param name="isBlack">whether we plug black drills</param>
        /// <param name="random">random number generator</param>
        private static void PlugSomeDrills(int drillCount, List<PipeSprite> pipeList, SpritePopulation spritePopulation, bool isBlack, Random random)
        {
            pipeList = new List<PipeSprite>(pipeList);
            while (drillCount > 0 && pipeList.Count > 0)
            {
                PipeSprite pipeSprite = GetRandomPipe(pipeList, random);
                if (pipeSprite.LinkedDrill == null)
                {
                    DrillSprite drillSprite = new DrillSprite(pipeSprite.XPosition, pipeSprite.YPosition, isBlack, pipeSprite.IsUpSide, random);
                    spritePopulation.Add(drillSprite);
                    pipeList.Remove(pipeSprite);


                    if (pipeSprite.IsUpSide)
                        drillSprite.YPosition = pipeSprite.TopBound;
                    else
                        drillSprite.TopBound = pipeSprite.YPosition;

                    pipeSprite.LinkedDrill = drillSprite;

                    drillCount--;
                }
            }
        }

        /// <summary>
        /// Whether pipe sprite is in acceptable distance to other sprites
        /// </summary>
        /// <param name="pipeSprite">pipe sprite</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns>Whether pipe sprite is in acceptable distance to other sprites</returns>
        private static bool IsInAcceptableDistanceToOtherSprites(PipeSprite pipeSprite, SpritePopulation spritePopulation)
        {
            foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
            {
                if (pipeSprite == otherSprite)
                    continue;

                if (Physics.IsDetectCollision(pipeSprite, pipeSprite.XPosition, pipeSprite.YPosition - 2.0, 2.0, otherSprite))
                    return false;
                else if (Physics.IsDetectCollision(pipeSprite, pipeSprite.XPosition, pipeSprite.YPosition + 2.0, 2.0, otherSprite))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Whether pipe is at acceptable position in level
        /// </summary>
        /// <param name="pipeSprite">pipe sprite/param>
        /// <returns>Whether pipe is at acceptable position in level</returns>
        private static bool IsAtAcceptablePosition(PipeSprite pipeSprite)
        {
            return pipeSprite.XPosition <= -2.0 || pipeSprite.XPosition >= 2.0; //Clear the entrance portal
        }

        /// <summary>
        /// List of pipes in sprite population
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns></returns>
        private static List<PipeSprite> GetPipeList(SpritePopulation spritePopulation)
        {
            List<PipeSprite> pipeList = new List<PipeSprite>();
            foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
                if (sprite is PipeSprite)
                    pipeList.Add((PipeSprite)sprite);
            return pipeList;
        }

        /// <summary>
        /// Plug some pipes together
        /// </summary>
        /// <param name="pipeCountToPlug">how many pipes to plug</param>
        /// <param name="pipeList">all the pipes</param>
        /// <param name="random">random number generator</param>
        private static void PlugSomePipes(int pipeToPlugCount, List<PipeSprite> pipeList, Level level, Random random)
        {
            pipeList = new List<PipeSprite>(pipeList);
            while (pipeToPlugCount >= 2 && pipeList.Count >= 2) //If there is one pipe left to plug, it can't be plugged
            {
                PipeSprite pipe1 = GetLeftMostPipe(pipeList);
                pipeList.Remove(pipe1);
                //PipeSprite pipe2 = GetRandomPipe(pipeList, random);
                PipeSprite pipe2 = GetClosestUnpluggedPipe(pipe1, pipeList);
                pipeList.Remove(pipe2);

                if (SpriteDistanceSorter.GetExactDistanceTile(pipe1, pipe2) <= level.Size / 3.0)
                    pipe1.LinkedPipe = pipe2;
            }
        }

        /// <summary>
        /// Get leftmost pipe
        /// </summary>
        /// <param name="pipeList">pipe list</param>
        /// <returns>leftmost pipe</returns>
        private static PipeSprite GetLeftMostPipe(List<PipeSprite> pipeList)
        {
            PipeSprite leftMostPipe = null;
            double leftMostX = -1;
            foreach (PipeSprite pipeSprite in pipeList)
            {
                if (leftMostPipe == null || pipeSprite.XPosition < leftMostX)
                {
                    leftMostX = pipeSprite.XPosition;
                    leftMostPipe = pipeSprite;
                }
            }
            return leftMostPipe;
        }

        /// <summary>
        /// Get closest pipe
        /// </summary>
        /// <param name="pipe">pipe</param>
        /// <param name="pipeList">pipe list</param>
        /// <returns>Get closest pipe</returns>
        private static PipeSprite GetClosestUnpluggedPipe(PipeSprite pipe, List<PipeSprite> pipeList)
        {
            PipeSprite closestPipe = null;
            double closestDistance = -1.0;

            foreach (PipeSprite otherPipe in pipeList)
            {
                if (otherPipe == pipe || otherPipe.LinkedPipe != null)
                    continue;
                double distance = Math.Sqrt(Math.Pow(pipe.XPosition - otherPipe.XPosition, 2.0) + Math.Pow(pipe.YPosition - otherPipe.YPosition, 2.0));
                if (closestPipe == null || distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPipe = otherPipe;
                }
            }

            return closestPipe;
        }

        /// <summary>
        /// Random pipe from list
        /// </summary>
        /// <param name="pipeList">list of pipes</param>
        /// <param name="random">random number generator</param>
        /// <returns>Random pipe from list</returns>
        private static PipeSprite GetRandomPipe(List<PipeSprite> pipeList, Random random)
        {
            return pipeList[random.Next(pipeList.Count)];
        }

        /// <summary>
        /// Determine how many drills
        /// </summary>
        /// <param name="skillLevel">skill level (from 0 to 10+)</param>
        /// <param name="pipeCount">how many pipes</param>
        /// <param name="isBlack">whether we generate black drills (False: could be either black or white)</param>
        /// <param name="random">random number generator</param>
        /// <returns>how many drills</returns>
        private static int BuildDrillCount(int skillLevel, int pipeCount, Random random)
        {
            double skillLevelAdjustmentRatio = Math.Sqrt(((double)skillLevel) + 1.0);

            int drillCount = (int)Math.Round(random.NextDouble() * skillLevelAdjustmentRatio * (double)pipeCount);
            drillCount = Math.Max(drillCount, 0);
            drillCount = Math.Min(drillCount, pipeCount);

            return drillCount;
        }
        #endregion
    }
}
