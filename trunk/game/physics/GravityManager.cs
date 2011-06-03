﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages stuff that are falling
    /// </summary>
    internal class GravityManager
    {
        #region Internal Methods
        /// <summary>
        /// Apply gravity to sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (!sprite.IsAffectedByGravity)
                return;

            if (sprite.IGround != null) //No gravity, sprite is on a ground
            {
                sprite.CurrentJumpAcceleration = 0;
                return;
            }

            if (sprite is IGrowable && ((IGrowable)sprite).GrowthCycle.IsFired)
                return;


            IGround closestDownGround = IGroundHelper.GetHighestVisibleIGroundBelowSprite(sprite, level, visibleSpriteList);
            if (closestDownGround == null)
            {
                if (sprite.YPositionPrevious <= sprite.YPosition) //if sprite is not fall/jumping up but only falling down
                {
                    Ground lowestVisibleGround = IGroundHelper.GetLowestVisibleGround(sprite, level);
                    if (sprite.YPosition - lowestVisibleGround[sprite.XPosition] < sprite.MinimumFallingHeight)
                    {
                        if (sprite.IsAlive)
                        {
                            sprite.IGround = lowestVisibleGround;
                            sprite.YPosition = sprite.IGround[sprite.XPosition];
                            sprite.CurrentJumpAcceleration = 0;
                        }
                    }
                    else
                    {
                        ApplyGravityMovement(sprite, timeDelta);
                        ApplyGravityAcceleration(sprite, timeDelta);
                    }
                }
                else
                {
                    ApplyGravityMovement(sprite, timeDelta);
                    ApplyGravityAcceleration(sprite, timeDelta);
                }
            }
            else
            {
                double closestDownGroundHeight = closestDownGround[sprite.XPosition];
                ApplyGravityMovement(sprite, timeDelta);

                if (!sprite.IsTryingToJump || sprite.JumpingCycle.IsFinished)
                {
                    ApplyGravityAcceleration(sprite, timeDelta);
                }

                if (sprite.YPosition >= closestDownGroundHeight && sprite.IsAlive)
                {
                    sprite.YPosition = closestDownGroundHeight;
                    sprite.IGround = closestDownGround;
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Apply gravity's acceleration
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        private void ApplyGravityAcceleration(AbstractSprite sprite, double timeDelta)
        {
            sprite.CurrentJumpAcceleration -= 4.0 * timeDelta;
        }

        /// <summary>
        /// Apply gravity's movement
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        private void ApplyGravityMovement(AbstractSprite sprite, double timeDelta)
        {
            sprite.YPosition -= sprite.CurrentJumpAcceleration / 50 * timeDelta;
        }
        #endregion
    }
}