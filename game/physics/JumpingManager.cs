using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages jumping logic
    /// </summary>
    internal class JumpingManager
    {
        #region Fields and parts
        /// <summary>
        /// Manages liana stuff
        /// </summary>
        private LianaManager lianaManager = new LianaManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Update jumping logic
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="playerSpriteReference">player sprite reference</param>
        /// <param name="timeDelta">time delta</param>
        internal void Update(AbstractSprite sprite, PlayerSprite playerSpriteReference, AbstractGameMode gameMode, double timeDelta)
        {
            if (sprite is StaticSprite)
                return;

            if (sprite.IsTryingToJump)
                StartOrContinueJump(sprite, timeDelta, gameMode);

            #region We manage ninja flip cycle
            if (sprite is PlayerSprite && sprite.IGround == null && sprite.IClimbingOn == null && ((PlayerSprite)sprite).IsNinja && ((PlayerSprite)sprite).NinjaFlipCycle.IsFired)
            {
                if (sprite.IsInWater || sprite.AttackingCycle.IsFired)
                    ((PlayerSprite)sprite).NinjaFlipCycle.StopAndReset();
                else
                    ((PlayerSprite)sprite).NinjaFlipCycle.Increment(timeDelta);
            }
            #endregion

            sprite.JumpingCycle.Increment(timeDelta / Math.Max(sprite.MaximumWalkingHeight, sprite.CurrentWalkingSpeed));

            if (sprite is IMovingGround && playerSpriteReference.IGround == sprite)
                playerSpriteReference.YPosition = sprite.TopBound;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Start or continue jump
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        private void StartOrContinueJump(AbstractSprite sprite, double timeDelta, AbstractGameMode gameMode)
        {
            if (!sprite.IsNeedToJumpAgain)
            {
                if (sprite.IsInWater)
                {
                    if (!sprite.IsNeedToJumpAgain && sprite.IsAlive)
                    {
                        sprite.JumpingCycle.Reset();
                        if (sprite is PlayerSprite)
                            sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration * Program.waterJumpingAccelerationMultiplier;
                        else
                            sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                        sprite.IGround = null;
                        sprite.IsNeedToJumpAgain = true;
                        if (sprite is PlayerSprite)
                            SoundManager.PlayDiveOutSound();

                        if (sprite.IClimbingOn != null && sprite.IClimbingOn is LianaSprite)
                            sprite.IClimbingOn = null;
                    }
                }
                else if (gameMode.IsAllowBodhiAirJump && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBodhi && sprite.CurrentJumpAcceleration <= 0 && sprite.IClimbingOn == null)
                {
                    if (!sprite.IsNeedToJumpAgain && sprite.IsAlive)
                    {
                        sprite.JumpingCycle.Reset();
                        sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                        sprite.IGround = null;
                        sprite.IsNeedToJumpAgain = true;
                        SoundManager.PlayBodhiJumpSound();
                        if (sprite.IClimbingOn != null && sprite.IClimbingOn is LianaSprite)
                            sprite.IClimbingOn = null;
                    }
                }
                else if (sprite.IGround != null)
                {
                    sprite.JumpingCycle.Reset();
                    sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;

                    sprite.IGround = null;
                    if (sprite is PlayerSprite)
                    {
                        if (((PlayerSprite)sprite).IsNinja)
                            ((PlayerSprite)sprite).NinjaFlipCycle.Fire();

                        SoundManager.PlayJumpSound();
                    }
                }
                else if (sprite.IClimbingOn != null)
                {
                    if (!sprite.IsNeedToJumpAgain && sprite.IsAlive)
                    {
                        sprite.JumpingCycle.Reset();
                        sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;

                        sprite.IGround = null;
                        sprite.IsNeedToJumpAgain = true;

                        if (sprite.IClimbingOn is LianaSprite)
                            sprite.IgnoreThisIClimbable = sprite.IClimbingOn;
                        sprite.IClimbingOn = null;
                        if (sprite is PlayerSprite)
                        {
                            if (((PlayerSprite)sprite).IsNinja)
                                ((PlayerSprite)sprite).NinjaFlipCycle.Fire();
                            SoundManager.PlayJumpSound();
                        }
                    }
                }

                if (sprite.CurrentJumpAcceleration < 0)
                {
                    sprite.IsNeedToJumpAgain = true;
                }
            }
        }
        #endregion
    }
}
