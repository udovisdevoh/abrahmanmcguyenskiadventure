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
                    SoundManager.PlayFireBallSound();
                    FireBallSprite fireBallSprite = new FireBallSprite(playerSprite.XPosition, playerSprite.YPosition - playerSprite.Height * 0.5, random);
                    fireBallSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
                    fireBallSprite.CurrentWalkingSpeed = fireBallSprite.MaxWalkingSpeed;
                    spritePopulation.Add(fireBallSprite);
                }
            }
        }
    }
}
