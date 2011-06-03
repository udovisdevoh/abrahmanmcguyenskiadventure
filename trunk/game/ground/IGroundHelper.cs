﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Operations and transformations on Ground (level surfaces)
    /// </summary>
    internal static class IGroundHelper
    {
        /// <summary>
        /// Highest ground below sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">List of visible sprites</param>
        /// <returns>Highest ground below sprite</returns>
        internal static IGround GetHighestVisibleIGroundBelowSprite(AbstractSprite sprite, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            IGround highestIGroundBelowSprite = null;
            double highestHeight = -1;

            foreach (Ground ground in level)
            {
                double currentHeight = ground[sprite.XPosition];

                if (sprite.YPosition <= currentHeight)
                {
                    if (highestHeight == -1 || currentHeight < highestHeight)
                    {
                        if (IsGroundVisible(ground, level, sprite.XPosition))
                        {
                            highestHeight = currentHeight;
                            highestIGroundBelowSprite = ground;
                        }
                    }
                }
            }

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (otherSprite.IsImpassable && otherSprite.IsAlive)
                {
                    bool isHorizontalCollision = (sprite.RightBound > otherSprite.LeftBound && sprite.LeftBound < otherSprite.LeftBound) || (sprite.LeftBound < otherSprite.RightBound && sprite.LeftBound > otherSprite.LeftBound);
                    isHorizontalCollision |= sprite.RightBound == otherSprite.RightBound;
                    isHorizontalCollision |= sprite.LeftBound == otherSprite.LeftBound;
                    isHorizontalCollision |= sprite.LeftBound > otherSprite.LeftBound && sprite.RightBound < otherSprite.RightBound;
                    isHorizontalCollision |= sprite.LeftBound < otherSprite.LeftBound && sprite.RightBound > otherSprite.RightBound;

                    if (isHorizontalCollision)
                    {
                        double currentHeight = otherSprite.TopBound;
                        if (sprite.YPosition <= currentHeight)
                        {
                            if (otherSprite.TopBound <= currentHeight)
                            {
                                if (highestHeight == -1 || currentHeight < highestHeight)
                                {
                                    highestHeight = currentHeight;
                                    highestIGroundBelowSprite = otherSprite;
                                }
                            }
                        }
                    }
                }
            }

            return highestIGroundBelowSprite;
        }

        /// <summary>
        /// Whether ground is visible at X Position
        /// </summary>
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <param name="xPosition">X Position</param>
        /// <returns>Whether ground is visible at X Position</returns>
        internal static bool IsGroundVisible(Ground ground, Level level, double xPosition)
        {
            double yPosition = ground[xPosition];

            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];
                if (currentGround == ground)
                    break;

                if (currentGround[xPosition] < yPosition)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Get lowest visible ground for current sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>lowest visible ground for current sprite, or null if nothing found</returns>
        internal static Ground GetLowestVisibleGround(AbstractSprite sprite, Level level)
        {
            Ground lowestGround = null;
            double lowestHeight = double.NegativeInfinity;

            foreach (Ground ground in level)
            {
                double currentHeight = ground[sprite.XPosition];

                if (currentHeight > lowestHeight)
                {
                    if (IsGroundVisible(ground, level, sprite.XPosition))
                    {
                        lowestHeight = currentHeight;
                        lowestGround = ground;
                    }
                }
            }
            return lowestGround;
        }

        /// <summary>
        /// Whether ground is transparent at X position
        /// </summary>
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <param name="x">x position</param>
        /// <returns></returns>
        internal static bool IsTransparentAt(Ground ground, Level level, double x)
        {
        	return ground.IsTransparent && IGroundHelper.IsHigherThanOtherGrounds(ground, level, x);
        }
        
        /// <summary>
        /// Whether ground is higher than other grounds in level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="xInput">x value</param>
        /// <returns>Whether ground is higher than other grounds</returns>
        internal static bool IsHigherThanOtherGrounds(Ground ground, Level level, double xInput)
        {
            double yOutput = ground[xInput];
            foreach (Ground otherGround in level)
                if (otherGround != ground)
                    if (otherGround[xInput] < yOutput)
                        return false;
            return true;
        }

        /// <summary>
        /// Get frontmost ground having accessible walking height for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="iGround">iGround</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <returns>frontmost ground having accessible walking height for sprite</returns>
        internal static IGround GetFrontmostGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, IGround iGround, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            IGround frontMostGroundHavingAccessibleWalkingHeightForSprite = null;
            double groundHeight = iGround[sprite.XPosition];

            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];

                if (currentGround == iGround)
                    break;

                double currentGroundHeight = currentGround[sprite.XPosition];

                if (currentGroundHeight < groundHeight && groundHeight - currentGroundHeight <= sprite.MaximumWalkingHeight)
                {
                    frontMostGroundHavingAccessibleWalkingHeightForSprite = currentGround;
                    break;
                }
            }

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (otherSprite.IsImpassable && otherSprite.IsAlive)
                {
                    if ((sprite.RightBound > otherSprite.LeftBound && sprite.LeftBound < otherSprite.LeftBound) || (sprite.LeftBound < otherSprite.RightBound && sprite.LeftBound > otherSprite.LeftBound))
                    {
                        double currentHeight = otherSprite.TopBound;
                        if (sprite.YPosition <= currentHeight)
                        {
                            if (Math.Abs(groundHeight - currentHeight) <= sprite.MaximumWalkingHeight)
                            {
                                frontMostGroundHavingAccessibleWalkingHeightForSprite = otherSprite;
                            }
                        }
                    }
                }
            }


            return frontMostGroundHavingAccessibleWalkingHeightForSprite;
        }
        
        /// <summary>
        /// Get highest ground having accessible walking height for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="iGround">iGround</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        /// <returns>highest ground having accessible walking height for sprite</returns>
        internal static IGround GetHighestGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, IGround iGround, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
        	double groundHeight = iGround[sprite.XPosition];
            double highestHeight = double.PositiveInfinity;
            IGround highestGround = null;

            foreach (Ground currentGround in level)
            {
                double currentGroundHeight = currentGround[sprite.XPosition];

                if (currentGroundHeight < highestHeight && Math.Abs(groundHeight - currentGroundHeight) <= sprite.MaximumWalkingHeight)
                {
                    highestGround = currentGround;
                    highestHeight = currentGroundHeight;
                }
            }

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (otherSprite.IsImpassable && otherSprite.IsAlive)
                {
                    if ((sprite.RightBound > otherSprite.LeftBound && sprite.LeftBound < otherSprite.LeftBound) || (sprite.LeftBound < otherSprite.RightBound && sprite.LeftBound > otherSprite.LeftBound))
                    {
                        double currentHeight = otherSprite.TopBound;
                        if (sprite.YPosition <= currentHeight)
                        {
                            if (otherSprite.TopBound <= currentHeight)
                            {
                                if (highestHeight == -1 || currentHeight < highestHeight)
                                {
                                    if (currentHeight < highestHeight && Math.Abs(groundHeight - currentHeight) <= sprite.MaximumWalkingHeight)
                                    {
                                        highestHeight = currentHeight;
                                        highestGround = otherSprite;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return highestGround;
        }

        /// <summary>
        /// Whether topGround's height is stacked on bottomGround's height
        /// </summary>
        /// <param name="topGround">topGround</param>
        /// <param name="bottomGround">bottomGround</param>
        /// <returns>Whether topGround's height is stacked on bottomGround's height</returns>
        internal static bool IsSpriteIGroundHeightStackedOn(IGround topGround, IGround bottomGround)
        {
            if (!(topGround is AbstractSprite) || !(bottomGround is AbstractSprite))
                return false;

            AbstractSprite topSprite = (AbstractSprite)topGround;
            AbstractSprite bottomSprite = (AbstractSprite)bottomGround;

            return bottomSprite.TopBound - topSprite.YPosition < 0.1;
        }
    }
}