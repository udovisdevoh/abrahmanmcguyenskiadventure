﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatches bicycles moving wheels
    /// </summary>
    internal static class VehicleDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch pendulums
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        internal static void Dispatch(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> wagonIgnoreList, Random random)
        {
            double density = random.NextDouble() * 0.15;
            density *= Program.vehicleDensityAdjustment;

            double speed = random.NextDouble() * 1.5 + 1.2;

            int vehicleCount = (int)((double)(level.Size * density));

            for (int i = 0; i < vehicleCount; i++)
                AddVehicle(level, spritePopulation, waterInfo, wagonIgnoreList, speed, random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add vehicle
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="wagonIgnoreList">ignore list</param>
        /// <param name="speed">speed</param>
        /// <param name="random">random number generator</param>
        private static void AddVehicle(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> ignoreList, double speed, Random random)
        {
            bool isWheel = random.NextDouble() > 0.5;

            bool isPendulum = (isWheel) && random.NextDouble() > 0.666;
            
            double radius = random.NextDouble() * 1.0 + 1.5;

            bool isEvil = (isWheel) && random.NextDouble() < ((double)(level.SkillLevel + 1) / 16);

            int platformCount;

            if (isPendulum)
                platformCount = 1;
            else if (isWheel)
                platformCount = random.Next(1, 5);
            else
                platformCount = random.Next(2, 5);

            double ropeLength = random.NextDouble() * 5 + 3.2;
            double amplitude = random.NextDouble() * 3 + 4;


            bool isShowCircumference = (platformCount != 2) && random.NextDouble() > 0.5;
            double firstChildOffset = random.NextDouble();
            double tensionRatio = random.NextDouble();
            double supportHeight = (random.NextDouble() > 0.5) ? 0.0 : random.NextDouble() * 1.5 + 0.5;
            bool isTension = (platformCount == 2) && random.NextDouble() > 0.5;

            if (isTension && !isWheel && platformCount == 2)
                platformCount++;

            if (!isPendulum)
                radius *= ((double)platformCount / 3.0);


            for (int tryCount = 0; tryCount < 100; tryCount++)
            {
                double xPosition = random.NextDouble() * level.Size + level.LeftBound;

                int roundedXPosition = (int)(Math.Round(xPosition));

                int closestDistanceFromIgnoreListElement = PlatformDispatcher.GetClosestDistanceFromIgnoreListElement(roundedXPosition, ignoreList);

                if (closestDistanceFromIgnoreListElement < 14)
                    continue;

                ignoreList.Add(roundedXPosition);

                double yPosition = level.Path[xPosition] + 0.25;


                AbstractBearing vehicle;
                
                if (isPendulum)
                {
                    vehicle = new Pendulum(xPosition, yPosition, random, ropeLength, speed, amplitude);
                }
                else if (isWheel)
                {
                    vehicle = new Wheel(xPosition, yPosition, random, radius, firstChildOffset, speed, false, isShowCircumference, true, 0.0);
                }
                else
                {
                    vehicle = new SeeSaw(xPosition, yPosition, random, radius, speed, 1.0, tensionRatio, false, true, isShowCircumference, isTension, 0.0);
                }
                spritePopulation.Add(vehicle);

                for (int i = 0; i < platformCount; i++)
                {
                    AbstractLinkage platformOrFlailBall;

                    if (isEvil)
                        platformOrFlailBall = new FlailBall(xPosition, yPosition, random, false, supportHeight);
                    else
                        platformOrFlailBall = new Platform(xPosition, yPosition, random, false, supportHeight, false, 0.0, 0.0);

                    spritePopulation.Add(platformOrFlailBall);
                    vehicle.AddChild(platformOrFlailBall);
                }

                vehicle.IGround = level.Path;
                vehicle.IsBoundToGroundForever = true;

                vehicle.IsTryingToWalkRight = random.NextDouble() > 0.5;
                vehicle.IGround = level.Path;

                break;
            }
        }
        #endregion
    }
}
