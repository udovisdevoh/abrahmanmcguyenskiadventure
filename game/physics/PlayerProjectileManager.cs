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
                    if (otherSprite is IPlayerProjectile)
                        ballCount++;

                if (playerSprite.IsBodhi || ballCount < Program.maxPlayerFireBallPerScreen)
                {
                    playerSprite.ThrowBallCycle.Fire();
                    double xPosition = (playerSprite.IsTryingToWalkRight) ? playerSprite.RightBound + 0.5: playerSprite.LeftBound - 0.5;
                    
                    IPlayerProjectile projectileSprite;

                    if (playerSprite.IsBodhi)
                    {
                        SoundManager.PlayKiBallSound();
                        if (playerSprite.IsCrouch)
                            projectileSprite = new KiBallSprite(xPosition, playerSprite.YPosition, random);
                        else
                            projectileSprite = new KiBallSprite(xPosition, playerSprite.TopBound + 1.0, random);
                    }
                    else if (playerSprite.IsDoped)
                    {
                        SoundManager.PlayFireBallSound();
                        projectileSprite = new FireBallSprite(xPosition, playerSprite.TopBound + 0.33, random);
                    }
                    else
                        projectileSprite = new ShurikenSprite(xPosition, playerSprite.TopBound + 0.33, random);

                    if (playerSprite.IsDoped || playerSprite.IsNinja)
                    {
                        projectileSprite.CurrentJumpAcceleration = -30;
                        projectileSprite.IsCurrentlyInFreeFallX = true;
                        projectileSprite.IsCurrentlyInFreeFallY = true;
                    }
                    projectileSprite.CurrentWalkingSpeed = playerSprite.CurrentWalkingSpeed + projectileSprite.MaxWalkingSpeed;
                    projectileSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
                    spritePopulation.Add((AbstractSprite)projectileSprite);
                }
            }
        }
    }
}
