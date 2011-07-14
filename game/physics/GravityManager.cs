using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;

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
            if (!sprite.IsAffectedByGravity && sprite.IsAlive)
                return;

            if (sprite.IGround != null) //No gravity, sprite is on a ground
            {
                sprite.CurrentJumpAcceleration = 0;
                return;
            }

            if (sprite is IGrowable && ((IGrowable)sprite).GrowthCycle.IsFired)
                return;

            if (sprite.IsCrossGrounds)
            {
                ApplyGravityMovement(sprite, timeDelta);
                ApplyGravityAcceleration(sprite, timeDelta);
            }
            else
            {
                IGround closestDownGround = IGroundHelper.GetHighestVisibleIGroundBelowSprite(sprite, level, visibleSpriteList);
                if (closestDownGround == null)
                {
                    if (sprite.YPositionPrevious <= sprite.YPosition) //if sprite is not fall/jumping up but only falling down
                    {
                        Ground lowestVisibleGround = IGroundHelper.GetLowestVisibleGround(sprite, level);
                        if (sprite.YPosition - lowestVisibleGround[sprite.XPosition] < sprite.MinimumFallingHeight)
                        {
                            if (sprite.IsAlive && !sprite.IsCrossGrounds)
                            {
                                sprite.IGround = lowestVisibleGround;
                                sprite.YPosition = sprite.IGround[sprite.XPosition];
                                sprite.CurrentJumpAcceleration = 0;
                            }
                        }
                        else
                        {
                            ApplyGravityMovement(sprite, timeDelta);
                            if (!sprite.IsCurrentlyInFreeFallY)
                                ApplyGravityAcceleration(sprite, timeDelta);
                        }
                    }
                    else
                    {
                        ApplyGravityMovement(sprite, timeDelta);
                        if (!sprite.IsCurrentlyInFreeFallY)
                            ApplyGravityAcceleration(sprite, timeDelta);
                    }
                }
                else
                {
                    double closestDownGroundHeight = closestDownGround[sprite.XPosition];
                    ApplyGravityMovement(sprite, timeDelta);

                    if (!sprite.IsTryingToJump || sprite.JumpingCycle.IsFinished)
                    {
                        if (!sprite.IsCurrentlyInFreeFallY)
                            ApplyGravityAcceleration(sprite, timeDelta);
                    }

                    if (sprite.YPosition >= closestDownGroundHeight && sprite.IsAlive && !sprite.IsCrossGrounds)
                    {
                        sprite.YPosition = closestDownGroundHeight;
                        sprite.IGround = closestDownGround;
                    }
                }
            }


            if (sprite.IGround != null)
            {
                sprite.IsCurrentlyInFreeFallX = false;
                sprite.IsCurrentlyInFreeFallY = false;

                if (sprite.IsAlive && sprite is MonsterSprite && ((MonsterSprite)sprite).IsMakeSoundWhenTouchGround)
                    SoundManager.PlayHelmetBumpSound();
            }

            if (sprite.IsAlive && sprite is MonsterSprite && ((MonsterSprite)sprite).IsDieOnTouchGround && sprite.IGround != null)
                sprite.IsAlive = false;
        }

        /// <summary>
        /// Apply gravity until sprite falls on ground or die
        /// </summary>
        /// <param name="spriteToUpdate">sprite to update</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void ApplyFullGravityForce(AbstractSprite spriteToUpdate, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            spriteToUpdate.IsFullGravityOnNextFrame = false;
            IGround ground = IGroundHelper.GetHighestVisibleIGroundBelowSprite(spriteToUpdate, level, visibleSpriteList, false);
            if (ground != null)
            {
                spriteToUpdate.YPosition = ground[spriteToUpdate.XPosition];
                spriteToUpdate.IGround = ground;
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
            double speed = sprite.CurrentJumpAcceleration / 50 * timeDelta;
            if (speed < 0 && sprite is PlayerSprite && ((PlayerSprite)sprite).IsRasta && sprite.IsTryingToJump)
            {
                if (Math.Abs(speed) > Math.Abs(Program.rastaFallingSpeed))
                {
                    sprite.CurrentJumpAcceleration = -16.0 * timeDelta;
                    return;
                }
            }
            
            sprite.CurrentJumpAcceleration -= 4.0 * timeDelta;           
        }

        /// <summary>
        /// Apply gravity's movement
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        private void ApplyGravityMovement(AbstractSprite sprite, double timeDelta)
        {
            double speed = sprite.CurrentJumpAcceleration / 50 * timeDelta;

            double maxFallingSpeed = Math.Abs(sprite.MaxFallingSpeed);

            if (sprite is PlayerSprite && ((PlayerSprite)sprite).IsRasta && sprite.IsTryingToJump)
                maxFallingSpeed = Program.rastaFallingSpeed;

            if (sprite.IsInWater && sprite.IsAlive && !(sprite is FireBallSprite))
                maxFallingSpeed = Program.waterMaxFallingSpeed;

            if (speed < 0 && Math.Abs(speed) > maxFallingSpeed)
            {
                speed = -maxFallingSpeed;
            }

            if (speed > 0 && sprite.IsInWater)
            {
                speed *= Program.waterJumpingSpeedMultiplier;
            }

            sprite.YPosition -= speed;
        }
        #endregion
    }
}
