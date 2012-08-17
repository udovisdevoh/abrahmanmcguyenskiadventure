using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatches floating wheels, seesaws and pendulums
    /// </summary>
    internal static class AmusementDispatcher
    {
        #region Fields and parts
        /// <summary>
        /// Temporary list of parent bearings
        /// </summary>
        private static List<AbstractBearing> _parentBearingList = new List<AbstractBearing>();

        /// <summary>
        /// Temporary list of random grounds
        /// </summary>
        private static List<Ground> _randomGroundList = new List<Ground>();

        /// <summary>
        /// Temporaty list of visible grounds
        /// </summary>
        private static List<Ground> _randomVisibleGroundList = new List<Ground>();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Dispatch wheels
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        internal static void Dispatch(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, HashSet<int> amusementIgnoreList, Random random)
        {
            double density = random.NextDouble() * 0.15;
            density *= Program.amusementDensityAdjustment;

            int amusementCount = (int)((double)(level.Size * density));

            for (int i = 0; i < amusementCount; i++)
            {
                double speed = random.NextDouble() * 1.5 + 1.2;
                bool isFloating = random.NextDouble() > Program.recursiveAmusementStructureProbability;
                AddAmusement(level, spritePopulation, waterInfo, speed, isFloating, amusementIgnoreList, random);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add amusement
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="speed">speed</param>
        /// <param name="random">random number generator</param>
        /// <param name="isFloating">true: structure is floating, false: structure is attached to something</param>
        private static void AddAmusement(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, double speed, bool isFloating, HashSet<int> amusementIgnoreList, Random random)
        {
            AbstractBearing amusement;

            int amusementType = random.Next(0, 3);

            int platformCount;

            double xPosition;
            double yPosition;
            double radius;
            bool isShowCircumference = false;

            double tensionRatio;

            double supportHeight = 0;
            /*if (random.NextDouble() > 0.333)
                supportHeight = 0;
            else
                supportHeight = random.NextDouble() * 1.5 + 0.5;*/

            AbstractBearing parentStructure = null;

            if (!isFloating)
            {
                parentStructure = GetRandomParentBearing(spritePopulation, random);
                if (parentStructure == null)
                    isFloating = true;
            }

            if (!isFloating)
            {
                xPosition = parentStructure.XPosition;
                yPosition = parentStructure.YPosition;
            }
            else
            {
                xPosition = 0;//will be reassigned soon
                for (int i = 0; i < 100; i++)
                {
                    xPosition = (random.NextDouble() * level.Size) + level.LeftBound;
                    int roundedXPosition = (int)(Math.Round(xPosition));
                    if (PlatformDispatcher.GetClosestDistanceFromIgnoreListElement(roundedXPosition, amusementIgnoreList) < 14)
                    {
                        amusementIgnoreList.Add(roundedXPosition);
                        break;
                    }
                }

                yPosition = GetRandomYPosition(level, xPosition, random);
            }


            if (amusementType == 0) //pendulum
            {
                radius = random.NextDouble() * 3.0 + 4.0;
                platformCount = 1;
                double amplitude = random.NextDouble() * 3 + 4;

                if (isFloating)
                    yPosition -= radius;

                amusement = new Pendulum(xPosition, yPosition, random, radius, speed * 7, amplitude, false, supportHeight);
                amusement.IsAffectedByGravity = false;
            }
            else if (amusementType == 1) //see saw
            {
                radius = random.NextDouble() * 1.0 + 1.5;
                platformCount = random.Next(2, 5);
                radius *= ((double)platformCount / 3.0);


                if (platformCount <= 2 && random.NextDouble() > 0.5)
                    radius *= random.NextDouble() * 2 + 1;


                if (platformCount != 2)
                    isShowCircumference = random.NextDouble() > 0.5;

                bool isTension = random.NextDouble() > 0.5;

                if (isFloating)
                    yPosition -= radius;

                tensionRatio = random.NextDouble();

                amusement = new SeeSaw(xPosition, yPosition, random, radius, speed, 1.0, tensionRatio, false, true, isShowCircumference, isTension, supportHeight);
                amusement.IsAffectedByGravity = false;
            }
            else //wheel
            {
                radius = random.NextDouble() * 1.0 + 3.5;
                platformCount = random.Next(1, 8);
                radius *= ((double)platformCount / 3.0);


                if (platformCount <= 2 && random.NextDouble() > 0.5)
                    radius *= random.NextDouble() * 2 + 1;

                if (platformCount != 2)
                    isShowCircumference = random.NextDouble() > 0.5;

                if (isFloating)
                    yPosition -= radius;

                double firstChildOffset = random.NextDouble();

                amusement = new Wheel(xPosition, yPosition, random, radius, firstChildOffset, speed, false, isShowCircumference, true, supportHeight);
                amusement.IsAffectedByGravity = false;
            }

            spritePopulation.Add(amusement);

            double supportHeightChild;
            if (random.NextDouble() > 0.5)
                supportHeightChild = 0;
            else
                supportHeightChild = random.NextDouble() * 1.5 + 0.5;

            for (int i = 0; i < platformCount; i++)
            {
                bool isEvil = (amusementType != 1) && random.NextDouble() < ((double)(level.SkillLevel + 1) / 16);

                AbstractLinkage platformOrFlailBall;

                if (isEvil)
                    platformOrFlailBall = new FlailBall(xPosition, yPosition, random, false, supportHeightChild);
                else
                    platformOrFlailBall = new Platform(xPosition, yPosition, random, false, supportHeightChild, false, 0.0, 0.0);

                spritePopulation.Add(platformOrFlailBall);
                amusement.AddChild(platformOrFlailBall);
            }


            if (parentStructure != null)
            {
                if (!parentStructure.IsContainSubStructure && parentStructure.IGround == null)
                {
                    if (amusement is IWheel)
                        parentStructure.YPosition -= ((IWheel)amusement).Radius;
                    else if (amusement is Pendulum)
                        parentStructure.YPosition -= ((Pendulum)amusement).Height;
                }
                parentStructure.AddChild(amusement);

                #region platform is parent is pendulum
                if (parentStructure is Pendulum)
                {
                    AbstractLinkage foundChild = null;
                    foreach (AbstractLinkage child in parentStructure.ChildList)
                    {
                        if (child is Platform)
                        {
                            foundChild = child;
                            break;
                        }
                    }
                    if (foundChild != null)
                    {
                        parentStructure.ChildList.Remove(foundChild);
                        spritePopulation.Remove(foundChild);
                    }
                }
                #endregion
            }
            else
            {
                amusement.IsBoundToGroundForever = false;
                amusement.IsAffectedByGravity = false;
                amusement.IGround = null;
            }
        }

        /// <summary>
        /// Random y position (on one of the highest visible ground) at x position
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="xPosition">x position</param>
        /// <param name="random">random number generator</param>
        /// <returns>Random y position (on one of the highest visible ground) at x position</returns>
        private static double GetRandomYPosition(Level level, double xPosition, Random random)
        {
            _randomGroundList.Clear();

            for (int i = 0; i < 3; i++)
            {
                Ground ground = GetRandomVisibleGround(level, xPosition, random);
                if (ground != null && !_randomGroundList.Contains(ground))
                    _randomGroundList.Add(ground);
            }

            double highestGroundHeight = double.PositiveInfinity;

            Ground highestGround = null;

            foreach (Ground ground in _randomGroundList)
            {
                double currentHeight = ground.GetGroundHeightNoHole(xPosition);
                if (ground[xPosition] < highestGroundHeight)
                {
                    highestGround = ground;
                    highestGroundHeight = currentHeight;
                }
            }

            if (highestGround != null)
                return highestGroundHeight - (random.NextDouble() * 4.0);
            else
                return 0.0;//oops, this shouldn't happen
        }

        private static Ground GetRandomVisibleGround(Level level, double xPosition, Random random)
        {
            _randomVisibleGroundList.Clear();

            foreach (Ground ground in level)
                if (!ground.IsPathOnly && ground != level.Ceiling && IGroundHelper.IsGroundVisible(ground, level, xPosition))
                    _randomVisibleGroundList.Add(ground);

            if (_randomVisibleGroundList.Count > 0)
                return _randomVisibleGroundList[random.Next(_randomVisibleGroundList.Count)];
            else
                return null;
        }

        /// <summary>
        /// Get random parent bearing
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        /// <returns>random parent bearing</returns>
        private static AbstractBearing GetRandomParentBearing(SpritePopulation spritePopulation, Random random)
        {
            _parentBearingList.Clear();
            foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
                if (sprite is AbstractBearing)
                    _parentBearingList.Add((AbstractBearing)sprite);

            if (_parentBearingList.Count == 0)
                return null;

            return _parentBearingList[random.Next(_parentBearingList.Count)];
        }
        #endregion
    }
}
