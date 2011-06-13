using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages walking logic
    /// </summary>
    internal class WalkingManager
    {
        #region Internal Methods
        /// <summary>
        /// Update walking logic for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (sprite is StaticSprite)
                return;
            else if (!sprite.IsTryingToWalk && sprite.CurrentWalkingSpeed <= 0)
                return;
            else if (sprite is MonsterSprite && !((MonsterSprite)sprite).IsWalkEnabled)
                return;

            double desiredWalkingDistance;
            double walkingDistance;
            double previousWalkingSpeed = sprite.CurrentWalkingSpeed;

            if (!sprite.IsCurrentlyInFreeFall && !sprite.IsCurrentlyInFreeFallX)
            {
                if (sprite.IsTryingToWalk)
                    sprite.CurrentWalkingSpeed += sprite.WalkingAcceleration;

                if (sprite.IsRunning)
                    sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxRunningSpeed);
                else
                    sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxWalkingSpeed);
            }

            if (sprite.IsTryingToWalkRight)
            {
                desiredWalkingDistance = timeDelta * sprite.CurrentWalkingSpeed;
                walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite, desiredWalkingDistance + sprite.Width / 2.0, level, visibleSpriteList);
                walkingDistance -= sprite.Width / 2.0;
                walkingDistance = Math.Max(0, walkingDistance);
            }
            else
            {
                desiredWalkingDistance = -timeDelta * sprite.CurrentWalkingSpeed;
                walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite, desiredWalkingDistance - sprite.Width / 2.0, level, visibleSpriteList);
                walkingDistance += sprite.Width / 2.0;
                walkingDistance = Math.Min(0, walkingDistance);
            }

            if (walkingDistance != 0)
            {
                #region Slope logic (slowing down when escalating, falling off a cliff
                if (sprite.IGround != null)
                {
                    double slope = Physics.GetSlopeRatio(sprite, sprite.IGround, walkingDistance, sprite.IsTryingToWalkRight);
                    
                    double adjustedEffectFromSlope = Math.Sqrt(Math.Abs(slope)) * 0.75;
                    
                    if (adjustedEffectFromSlope > 0 != slope > 0)
                        adjustedEffectFromSlope *= -1;

                    if (slope != 0)
                    {
                        if (slope < 0 || sprite.IsCrouch) //if we must go up a hill
                        {
                            double slopeAdjustmentRatio = sprite.IsCrouch ? 0.5 : 0.15;
                            sprite.CurrentWalkingSpeed *= Math.Min(1.0, 1.0 + slope * slopeAdjustmentRatio);
                        }

                        //We sometimes make fall the sprite
                        if ((!sprite.IsCrouch && slope > 0.8) || (sprite.IsCrouch && slope > 3))
                            sprite.IGround = null;
                    }

                    //We sometimes make fall the sprite
                    if (sprite.IGround is AbstractSprite)
                    {
                        AbstractSprite spriteGround = (AbstractSprite)sprite.IGround;
                        if (sprite.RightBound < spriteGround.LeftBound || sprite.LeftBound > spriteGround.RightBound)
                            sprite.IGround = null;
                    }
                }
                #endregion

                sprite.XPosition += walkingDistance;
            }

            //Must prevent sprite from accelerating while pushing on a collision
            if (sprite.IGround != null && walkingDistance == 0 && previousWalkingSpeed > 0.1)
                sprite.CurrentWalkingSpeed = 0;

            if (sprite.IGround != null)
            {
            	IGround frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite;
            	if (sprite.IsTryToWalkUp)
                	frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite = IGroundHelper.GetHighestGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.IGround, level, visibleSpriteList);
            	else
            		frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite = IGroundHelper.GetFrontmostGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.IGround, level, visibleSpriteList);

                //If a ground is obstructing current ground, and it is accessible for sprite, use that ground instead
                if (frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite != null)
                    sprite.IGround = frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite;

                double groundHeight = sprite.IGround[sprite.XPosition];

                sprite.YPosition = groundHeight;
            }

            if (sprite.IsTiny)
                sprite.WalkingCycle.Increment(timeDelta * sprite.CurrentWalkingSpeed * 1.5);
            else
                sprite.WalkingCycle.Increment(timeDelta * sprite.CurrentWalkingSpeed);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get farthest walking distance without collision
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="desiredDistance">desired walking distance</param>
        /// <param name="level">level</param>
        /// <returns>farthest walking distance without collision</returns>
        private double GetFarthestWalkingDistanceNoCollision(AbstractSprite sprite, double desiredDistance, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (sprite.IsCrossGrounds)
                return desiredDistance;

            double previousDistance = 0;
            if (desiredDistance > 0)
            {
                for (double currentDistance = 0; currentDistance <= desiredDistance; currentDistance += Program.collisionDetectionResolution)
                {
                    if (Physics.IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, visibleSpriteList))
                        return previousDistance;
                    previousDistance = currentDistance;
                }
            }
            else
            {
                for (double currentDistance = 0; currentDistance >= desiredDistance; currentDistance -= Program.collisionDetectionResolution)
                {
                    if (Physics.IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, visibleSpriteList))
                        return previousDistance;
                    previousDistance = currentDistance;
                }
            }
            return previousDistance;
        }
        #endregion
    }
}
