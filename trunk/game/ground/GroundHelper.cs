using System;
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
    internal static class GroundHelper
    {
        /// <summary>
        /// Highest ground below sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>Highest ground below sprite</returns>
        internal static Ground GetHighestVisibleGroundBelowSprite(AbstractSprite sprite, Level level)
        {
            Ground highestGroundBelowSprite = null;
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
                            highestGroundBelowSprite = ground;
                        }
                    }
                }
            }
            return highestGroundBelowSprite;
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
        	return ground.IsTransparent && GroundHelper.IsHigherThanOtherGrounds(ground, level, x);
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
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <returns>frontmost ground having accessible walking height for sprite</returns>
        internal static Ground GetFrontmostGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, Ground ground, Level level)
        {
            double groundHeight = ground[sprite.XPosition];

            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];

                if (currentGround == ground)
                    break;

                double currentGroundHeight = currentGround[sprite.XPosition];

                if (currentGroundHeight < groundHeight && groundHeight - currentGroundHeight <= sprite.MaximumWalkingHeight)
                    return currentGround;
            }
            return null;
        }
        
        /// <summary>
        /// Get highest ground having accessible walking height for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <returns>highest ground having accessible walking height for sprite</returns>
        internal static Ground GetHighestGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, Ground ground, Level level)
        {
        	double groundHeight = ground[sprite.XPosition];
            double highestHeight = double.PositiveInfinity;
            Ground highestGround = null;

            foreach (Ground currentGround in level)
            {
                double currentGroundHeight = currentGround[sprite.XPosition];

                if (currentGroundHeight < highestHeight && groundHeight - currentGroundHeight <= sprite.MaximumWalkingHeight)
                {
                    highestGround = currentGround;
                    highestHeight = currentGroundHeight;
                }
            }
            return highestGround;
        }
    }
}
