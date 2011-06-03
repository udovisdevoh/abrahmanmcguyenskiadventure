using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages player's projectiles
    /// </summary>
    class PlayerProjectileManager
    {
        /// <summary>
        /// Update player's projectile throwing logic
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal void Update(PlayerSprite playerSprite, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, Random random)
        {
            if (playerSprite.IsTryThrowingBall)
            {
                int ballCount = 0;
                foreach (AbstractSprite otherSprite in visibleSpriteList)
                    if (otherSprite is FireBallSprite)
                        ballCount++;

                if (ballCount < Program.maxPlayerFireBallPerScreen)
                {
                    double xPosition = (playerSprite.IsTryingToWalkRight) ? playerSprite.RightBound + 0.5: playerSprite.LeftBound - 0.5;
                    SoundManager.PlayFireBallSound();
                    FireBallSprite fireBallSprite = new FireBallSprite(xPosition, playerSprite.TopBound + 0.33, random);
                    fireBallSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
                    fireBallSprite.CurrentWalkingSpeed = fireBallSprite.MaxWalkingSpeed;
                    fireBallSprite.CurrentJumpAcceleration = -25;
                    spritePopulation.Add(fireBallSprite);
                }
            }
        }
    }
}
