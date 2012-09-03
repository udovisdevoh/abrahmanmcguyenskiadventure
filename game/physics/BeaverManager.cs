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

            if (playerSprite.LatestNinjaBeaver != null)
            {
                RemoveNinjaStatusFromBeaver(playerSprite.LatestNinjaBeaver);
                playerSprite.LatestNinjaBeaver = null;
            }
        }

        /// <summary>
        /// Player leaves beaver
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="spritePopulation">sprite population</param>
        internal void LeaveBeaver(PlayerSprite playerSprite, SpritePopulation spritePopulation, AbstractGameMode gameMode)
        {
            playerSprite.IsBeaver = false;
            BeaverSprite beaverSprite = new BeaverSprite(playerSprite.XPosition, playerSprite.YPosition, random);

            if (playerSprite.IGround != null)
                beaverSprite.IGround = playerSprite.IGround;

            spritePopulation.Add(beaverSprite);
            if (beaverSprite.IGround == null)
                beaverSprite.IsCurrentlyInFreeFallX = true;

            if (playerSprite.IsNinja || playerSprite.IsBodhi || gameMode.IsBeaverAlwaysStrongAi)
            {
                if (playerSprite.LatestNinjaBeaver != null)
                    RemoveNinjaStatusFromBeaver(playerSprite.LatestNinjaBeaver);

                playerSprite.LatestNinjaBeaver = beaverSprite;
                beaverSprite.StartingJumpAcceleration = playerSprite.StartingJumpAcceleration * 1.2;

                SetBeaverAi(beaverSprite, playerSprite, gameMode.IsBeaverAlwaysStrongAi);
            }
            else
            {
                beaverSprite.IsWalkEnabled = false;
            }

            playerSprite.CurrentJumpAcceleration = playerSprite.StartingJumpAcceleration;

            beaverSprite.IsTryingToWalkRight = playerSprite.IsTryingToWalkRight;
            beaverSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
            beaverSprite.CurrentWalkingSpeed = playerSprite.CurrentWalkingSpeed;

            playerSprite.JumpingCycle.Fire();

            playerSprite.IGround = null;
            playerSprite.YPosition -= 0.5;

            playerSprite.IsTryingToJump = true;
        }

        internal void SetBeaverAi(BeaverSprite beaverSprite, PlayerSprite playerSprite, bool isStrongAgressiveAi)
        {
            //we give special status to latest beaver voluntarily left by ninja
            beaverSprite.IsAiEnabled = true;
            beaverSprite.MaxWalkingSpeed = playerSprite.MaxRunningSpeed;
            beaverSprite.IsAvoidFall = true;
            beaverSprite.IsCanJump = true;
            beaverSprite.SafeDistanceAi = 3.5;
            beaverSprite.IsWalkEnabled = true;
            beaverSprite.IsAttackMonsters = isStrongAgressiveAi;
        }
        #endregion

        #region Private Methods
        private void RemoveNinjaStatusFromBeaver(BeaverSprite beaverSprite)
        {
            //We remove ninja status from previous latest beaver that was left by ninja and make it regular ninja
            beaverSprite.IsAiEnabled = false;
            beaverSprite.MaxWalkingSpeed = BeaverSprite.DefaultMaxWalkingSpeed;
            beaverSprite.IsAvoidFall = false;
            beaverSprite.StartingJumpAcceleration = BeaverSprite.DefaultStartingJumpAcceleration;
            beaverSprite.IsWalkEnabled = false;
            beaverSprite.SafeDistanceAi = 0.0;
        }
        #endregion
    }
}
