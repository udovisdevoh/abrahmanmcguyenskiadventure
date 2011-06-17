using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manage jumping on beaver and other beaver stuff
    /// </summary>
    internal class BeaverManager
    {
        #region Fields and parts
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random = new Random();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Jumping on beaver
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="beaverSprite">beaver</param>
        internal void UpdateJumpOnBeaver(PlayerSprite playerSprite, BeaverSprite beaverSprite)
        {
            SoundManager.PlayBeaverUpSound();
            playerSprite.IsBeaver = true;
            beaverSprite.IsAlive = false;
            beaverSprite.YPosition = Program.totalHeightTileCount + 1.0;
        }

        /// <summary>
        /// Player leaves beaver
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="spritePopulation">sprite population</param>
        internal void LeaveBeaver(PlayerSprite playerSprite, SpritePopulation spritePopulation)
        {

            playerSprite.IsBeaver = false;
            BeaverSprite beaverSprite = new BeaverSprite(playerSprite.XPosition, playerSprite.YPosition, random);
            spritePopulation.Add(beaverSprite);
            if (beaverSprite.IGround == null)
                beaverSprite.IsCurrentlyInFreeFallX = true;
            beaverSprite.IsWalkEnabled = false;
            playerSprite.CurrentJumpAcceleration = playerSprite.StartingJumpAcceleration;

            beaverSprite.IsTryingToWalkRight = playerSprite.IsTryingToWalkRight;
            beaverSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
            beaverSprite.CurrentWalkingSpeed = playerSprite.CurrentWalkingSpeed;

            playerSprite.JumpingCycle.Fire();
            playerSprite.IsTryingToSpin = true;

            playerSprite.YPosition -= playerSprite.Height / 4.0;

            playerSprite.IsTryingToJump = true;
        }
        #endregion
    }
}
