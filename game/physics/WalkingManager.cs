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

            //We adjust walking distance according to the slope
            if (walkingDistance != 0)
            {
                if (sprite.Ground != null)
                {
                    double slope = Physics.GetSlopeRatio(sprite, sprite.Ground, walkingDistance);
                    
                    double adjustedSquareRootSlope = Math.Sqrt(Math.Abs(slope)) * 0.75;
                    if (adjustedSquareRootSlope > 0 != slope > 0)
                        adjustedSquareRootSlope *= -1;

                    if (slope != 0)
                    {
                        //We escalate a hill
                        if (slope < 0)
                        {
                            walkingDistance += (adjustedSquareRootSlope * walkingDistance);
                            sprite.CurrentWalkingSpeed += (adjustedSquareRootSlope * walkingDistance) * sprite.WalkingAcceleration;
                        }

                        //We sometimes make fall the sprite
                        if (slope > 0.5)
                            sprite.Ground = null;
                    }
                }

                sprite.XPosition += walkingDistance;
            }

            //Must prevent sprite from accelerating while pushing on a collision
            if (sprite.Ground != null && walkingDistance == 0 && previousWalkingSpeed > 0.1)
                sprite.CurrentWalkingSpeed = 0;

            if (sprite.Ground != null)
            {
                Ground frontestGroundHavingAccessibleWalkingHeightForSprite = GetFrontmostGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.Ground, level);

                //If a ground is obstructing current ground, and it is accessible for sprite, use that ground instead
                if (frontestGroundHavingAccessibleWalkingHeightForSprite != null)
                    sprite.Ground = frontestGroundHavingAccessibleWalkingHeightForSprite;

                double groundHeight = sprite.Ground.TerrainWave[sprite.XPosition];

                //We sometimes make fall the sprite
                /*if (sprite.YPosition < groundHeight - sprite.MinimumFallingHeight)
                    sprite.Ground = null;
                else*/
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
                    if (Physics.IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, true))
                        return previousDistance;
                    previousDistance = currentDistance;
                }
            }
            else
            {
                for (double currentDistance = 0; currentDistance >= desiredDistance; currentDistance -= Program.collisionDetectionResolution)
                {
                    if (Physics.IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, true))
                        return previousDistance;
                    previousDistance = currentDistance;
                }
            }
            return previousDistance;
        }

        /// <summary>
        /// Get frontmost ground having accessible walking height for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <returns>frontmost ground having accessible walking height for sprite</returns>
        private Ground GetFrontmostGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, Ground ground, Level level)
        {
            double groundHeight = ground.TerrainWave[sprite.XPosition];

            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];

                if (currentGround == ground)
                    break;

                double currentGroundHeight = currentGround.TerrainWave[sprite.XPosition];

                if (currentGroundHeight < groundHeight && groundHeight - currentGroundHeight <= sprite.MaximumWalkingHeight)
                    return currentGround;
            }
            return null;
        }
        #endregion
    }
}
