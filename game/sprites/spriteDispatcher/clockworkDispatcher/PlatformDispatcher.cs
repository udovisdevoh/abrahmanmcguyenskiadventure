using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatches platforms
    /// </summary>
    internal static class PlatformDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch platforms (on path, elevators)
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        internal static void Dispatch(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> wagonIgnoreList, Random random)
        {
            HashSet<int> elevatorIgnoreList = new HashSet<int>();

            double density = random.NextDouble() * 0.15;
            density *= Program.platformDensityAdjustment;


            double wagonSpeed = random.NextDouble() * 0.05 + 0.04;

            int platformCount = (int)((double)(level.Size * density));

            for (int i = 0; i < platformCount; i++)
                AddPlatform(level, spritePopulation, waterInfo, elevatorIgnoreList, wagonIgnoreList, wagonSpeed, random);
        }

        /// <summary>
        /// Closest distance from ignore list elements
        /// </summary>
        /// <param name="roundedXPosition"></param>
        /// <param name="ignoreList"></param>
        /// <returns></returns>
        internal static int GetClosestDistanceFromIgnoreListElement(int roundedXPosition, HashSet<int> ignoreList)
        {
            if (ignoreList.Contains(roundedXPosition))
                return 0;

            int closestValue = int.MaxValue;

            foreach (int otherValue in ignoreList)
            {
                int currentDistance = Math.Abs(roundedXPosition - otherValue);
                if (currentDistance < closestValue)
                    closestValue = currentDistance;
            }

            return closestValue;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add a platform at random position
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        private static void AddPlatform(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> elevatorIgnoreList, HashSet<int> wagonIgnoreList, double wagonSpeed, Random random)
        {
            bool isElevator = level.Path == null || random.NextDouble() > 0.5;

            if (isElevator)
                AddElevator(level, spritePopulation, waterInfo, elevatorIgnoreList, random);
            else
                AddWagon(level, spritePopulation, waterInfo, wagonIgnoreList, wagonSpeed, random);
        }

        private static void AddWagon(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> ignoreList, double wagonSpeed, Random random)
        {
            for (int tryCount = 0; tryCount < 100; tryCount++)
            {
                double xPosition = random.NextDouble() * level.Size + level.LeftBound;

                int roundedXPosition = (int)(Math.Round(xPosition));

                int closestDistanceFromIgnoreListElement = GetClosestDistanceFromIgnoreListElement(roundedXPosition, ignoreList);

                if (closestDistanceFromIgnoreListElement < 14)
                    continue;

                ignoreList.Add(roundedXPosition);

                double yPosition = level.Path[xPosition];


                Platform platform = new Platform(xPosition, yPosition, random, false, 0, false, 0, wagonSpeed);
                spritePopulation.Add(platform);

                platform.IsTryingToWalkRight = random.NextDouble() > 0.5;

                platform.IGround = level.Path;
                break;
            }
        }

        /// <summary>
        /// Add elevator
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">water info</param>
        /// <param name="random">random number generator</param>
        private static void AddElevator(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> ignoreList, Random random)
        {
            double xPosition;
            double yPosition;
            double elevatorHeight;

            if (TryFindBestElevatorPosition(level, spritePopulation, waterInfo, random, ignoreList, out xPosition, out yPosition, out elevatorHeight))
            {
                double elevatorSpeed = (random.NextDouble() * 3.0) + 0.5;

                elevatorHeight *= (random.NextDouble() * 0.6 + 0.8);

                Platform platform = new Platform(xPosition, yPosition, random, false, 0.0, true, elevatorHeight, elevatorSpeed);
                spritePopulation.Add(platform);
                platform.XPosition = xPosition;//to change previous xPosition
            }
        }

        /// <summary>
        /// Find best position for elevator
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <returns>whether position could be found</returns>
        private static bool TryFindBestElevatorPosition(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, Random random, HashSet<int> ignoreList, out double xPosition, out double yPosition, out double elevatorHeight)
        {
            xPosition = 0;
            yPosition = 0;
            elevatorHeight = 0;
            for (int i = 0; i < 100; i++)
            {
                xPosition = (int)(random.NextDouble() * level.Size) + level.LeftBound;

                for (int j = 0; j < 100; j++)
                {
                    int groundId = random.Next(0, level.Count);

                    if (level[groundId].IsHoleAt(xPosition) && level[groundId].IsHoleAt(xPosition + 1.5) && level[groundId].IsHoleAt(xPosition - 1.5))
                    {
                        double holeXBoundRight = GetHoleXBound(xPosition, groundId, level, true);
                        double holeXBoundLeft = GetHoleXBound(xPosition, groundId, level, false);

                        xPosition = (holeXBoundRight + holeXBoundLeft) / 2.0;

                        int roundedXPosition = (int)Math.Round(xPosition);

                        if (!ignoreList.Contains(roundedXPosition))
                        {
                            ignoreList.Add(roundedXPosition);

                            yPosition = level[groundId].GetGroundHeightNoHole(xPosition);

                            double holeYBoundTop = GetHoleYBound(xPosition, yPosition, level, true);
                            double holeYBoundBottom = GetHoleYBound(xPosition, yPosition, level, false);

                            elevatorHeight = holeYBoundBottom - holeYBoundTop;

                            yPosition = (holeYBoundTop + holeYBoundBottom) / 2.0;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get top or bottom bound of hole
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="level">level</param>
        /// <param name="isTop">true: top, false: bottom</param>
        /// <returns>top or bottom bound of hole</returns>
        private static double GetHoleYBound(double xPosition, double yPosition, Level level, bool isTop)
        {
            double yBound = yPosition;
            bool isFound = false;

            foreach (Ground ground in level)
            {
                double currentHeight = ground.GetGroundHeightNoHole(xPosition);
                if (isTop)
                {
                    if (currentHeight < yBound || !isFound)
                    {
                        isFound = true;
                        yBound = currentHeight;
                    }
                }
                else
                {
                    if (currentHeight > yBound || !isFound)
                    {
                        isFound = true;
                        yBound = currentHeight;
                    }
                }
            }

            return yBound;
        }

        /// <summary>
        /// Get hole's bound
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="groundId">ground's index</param>
        /// <param name="level">level</param>
        /// <param name="isRight">true: right bound, false: left bound</param>
        /// <returns>hole's left or right bound</returns>
        private static double GetHoleXBound(double xPosition, int groundId, Level level, bool isRight)
        {
            double xBound = xPosition;

            while (level[groundId].IsHoleAt(xBound))
            {
                if (isRight)
                    xBound += 0.25;
                else
                    xBound -= 0.25;
            }

            return xBound;
        }
        #endregion
    }
}
