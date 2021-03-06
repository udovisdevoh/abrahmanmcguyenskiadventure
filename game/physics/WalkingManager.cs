﻿using System;
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
        /// <param name="playerSpriteReference">reference to player sprite</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList, PlayerSprite playerSpriteReference)
        {
            if (sprite is StaticSprite)
                return;
            else if (!sprite.IsTryingToWalk && sprite.CurrentWalkingSpeed <= 0)
                return;
            else if (sprite is MonsterSprite && !((MonsterSprite)sprite).IsWalkEnabled && !sprite.IsCurrentlyInFreeFallX)
                return;

            double desiredWalkingDistance;
            double walkingDistance;
            double previousWalkingSpeed = sprite.CurrentWalkingSpeed;

            if (!sprite.IsCurrentlyInFreeFallX)
            {
                if (sprite.IsTryingToWalk)
                    sprite.CurrentWalkingSpeed += sprite.WalkingAcceleration;

                if (sprite.IsInWater && sprite is PlayerSprite)
                {
                    if (sprite.IsRunning)
                        sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxRunningSpeed * Program.waterWalkingSpeedMultiplier);
                    else
                        sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxWalkingSpeed * Program.waterWalkingSpeedMultiplier);
                }
                else
                {
                    if (sprite.IsRunning)
                        sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxRunningSpeed);
                    else
                        sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxWalkingSpeed);
                }
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


                if (sprite is IAngleProjectile)
                    MoveAngleProjectileSprite(sprite, Math.Abs(walkingDistance), ((IAngleProjectile)sprite).AngleIndex);
                else
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

            if (sprite is PlayerSprite)
            {
                if (sprite.IsTiny)
                    sprite.WalkingCycle.Increment(timeDelta * sprite.CurrentWalkingSpeed * 2.0);
                else
                    sprite.WalkingCycle.Increment(timeDelta * sprite.CurrentWalkingSpeed * 1.5);                
            }
            else
                sprite.WalkingCycle.Increment(timeDelta * sprite.CurrentWalkingSpeed);


            if (sprite is IMovingGround && playerSpriteReference.IGround == sprite)
            {
                playerSpriteReference.XPosition += walkingDistance;
                playerSpriteReference.YPosition = sprite.TopBound;
            }
        }

        /// <summary>
        /// Get farthest walking distance without collision
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="desiredDistance">desired walking distance</param>
        /// <param name="level">level</param>
        /// <returns>farthest walking distance without collision</returns>
        internal double GetFarthestWalkingDistanceNoCollision(AbstractSprite sprite, double desiredDistance, Level level, HashSet<AbstractSprite> visibleSpriteList)
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

        #region Private Methods
        /// <summary>
        /// Sprite is an angled projectile, move it according to its angle
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="absoluteDistance">absolute distance</param>
        /// <param name="angleIndex">angle's index</param>
        private void MoveAngleProjectileSprite(AbstractSprite sprite, double absoluteDistance, byte angleIndex)
        {
            switch (angleIndex)
            {
                case 0:
                    sprite.YPosition -= absoluteDistance;
                    break;
                case 1:
                    sprite.YPosition -= absoluteDistance * 0.7;
                    sprite.XPosition += absoluteDistance * 0.7;
                    break;
                case 2:
                    sprite.XPosition += absoluteDistance;
                    break;
                case 3:
                    sprite.YPosition += absoluteDistance * 0.7;
                    sprite.XPosition += absoluteDistance * 0.7;
                    break;
                case 4:
                    sprite.YPosition += absoluteDistance;
                    break;
                case 5:
                    sprite.YPosition += absoluteDistance * 0.7;
                    sprite.XPosition -= absoluteDistance * 0.7;
                    break;
                case 6:
                    sprite.XPosition -= absoluteDistance;
                    break;
                default:
                    sprite.YPosition -= absoluteDistance * 0.7;
                    sprite.XPosition -= absoluteDistance * 0.7;
                    break;
            }
        }
        #endregion
    }
}
