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
        internal void Update(AbstractSprite sprite, Level level, double timeDelta)
        {
            if (sprite.IsTryingToWalk || sprite.CurrentWalkingSpeed > 0)
                TryMakeWalk(sprite, timeDelta, level);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Try to make walk the sprice if there are no collisions
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="level">level</param>
        private void TryMakeWalk(AbstractSprite sprite, double timeDelta, Level level)
        {
            if (sprite is MonsterSprite && !((MonsterSprite)sprite).IsWalkEnabled)
                return;

            double desiredWalkingDistance;
            double walkingDistance;
            double previousWalkingSpeed = sprite.CurrentWalkingSpeed;

            if (sprite.IsTryingToWalk)
                sprite.CurrentWalkingSpeed += sprite.WalkingAcceleration;

            if (sprite.IsRunning)
                sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxRunningSpeed);
            else
                sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxWalkingSpeed);

            if (sprite.IsTryingToWalkRight)
            {
                desiredWalkingDistance = timeDelta * sprite.CurrentWalkingSpeed;
                walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite, desiredWalkingDistance + sprite.Width / 2.0, level);
                walkingDistance -= sprite.Width / 2.0;
                walkingDistance = Math.Max(0, walkingDistance);
            }
            else
            {
                desiredWalkingDistance = -timeDelta * sprite.CurrentWalkingSpeed;
                walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite, desiredWalkingDistance - sprite.Width / 2.0, level);
                walkingDistance += sprite.Width / 2.0;
                walkingDistance = Math.Min(0, walkingDistance);
            }

            if (walkingDistance != 0)
            {
                #region Slope logic (slowing down when escalating, falling off a cliff
                if (sprite.Ground != null)
                {
                    double slope = Physics.GetSlopeRatio(sprite, sprite.Ground, walkingDistance, sprite.IsTryingToWalkRight);
                    
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
                            sprite.Ground = null;
                    }
                }
                #endregion

                sprite.XPosition += walkingDistance;
            }

            //Must prevent sprite from accelerating while pushing on a collision
            if (sprite.Ground != null && walkingDistance == 0 && previousWalkingSpeed > 0.1)
                sprite.CurrentWalkingSpeed = 0;

            if (sprite.Ground != null)
            {
            	Ground frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite;
            	if (sprite.IsTryToWalkUp)
                	frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite = GroundHelper.GetHighestGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.Ground, level);
            	else
            		frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite = GroundHelper.GetFrontmostGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.Ground, level);

                //If a ground is obstructing current ground, and it is accessible for sprite, use that ground instead
                if (frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite != null)
                    sprite.Ground = frontmostOrHighestGroundHavingAccessibleWalkingHeightForSprite;

                double groundHeight = sprite.Ground[sprite.XPosition];

                sprite.YPosition = groundHeight;
            }

            sprite.WalkingCycle.Increment(timeDelta * sprite.CurrentWalkingSpeed);
        }

        /// <summary>
        /// Get farthest walking distance without collision
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="desiredDistance">desired walking distance</param>
        /// <param name="level">level</param>
        /// <returns>farthest walking distance without collision</returns>
        private double GetFarthestWalkingDistanceNoCollision(AbstractSprite sprite, double desiredDistance, Level level)
        {
            double previousDistance = 0;
            if (desiredDistance > 0)
            {
                for (double currentDistance = 0; currentDistance <= desiredDistance; currentDistance += Program.collisionDetectionResolution)
                {
                    if (Physics.IsDetectCollision(sprite, sprite.XPosition + currentDistance, level))
                        return previousDistance;
                    previousDistance = currentDistance;
                }
            }
            else
            {
                for (double currentDistance = 0; currentDistance >= desiredDistance; currentDistance -= Program.collisionDetectionResolution)
                {
                    if (Physics.IsDetectCollision(sprite, sprite.XPosition + currentDistance, level))
                        return previousDistance;
                    previousDistance = currentDistance;
                }
            }
            return previousDistance;
        }
        #endregion
    }
}
