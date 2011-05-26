﻿using System;
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
        #region Internal Methods
        /// <summary>
        /// Update jumping logic
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        internal void Update(AbstractSprite sprite, double timeDelta)
        {
            if (sprite.IsTryingToJump)
                StartOrContinueJump(sprite, timeDelta);

            sprite.JumpingCycle.Increment(timeDelta / Math.Max(sprite.MaximumWalkingHeight, sprite.CurrentWalkingSpeed));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Start or continue jump
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta">time delta</param>
        private void StartOrContinueJump(AbstractSprite sprite, double timeDelta)
        {
            if (!sprite.IsNeedToJumpAgain)
            {
                if (sprite.Ground != null)
                {
                    sprite.JumpingCycle.Reset();
                    sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                    sprite.Ground = null;
                    if (sprite is PlayerSprite)
                        SoundManager.PlayJumpSound();
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