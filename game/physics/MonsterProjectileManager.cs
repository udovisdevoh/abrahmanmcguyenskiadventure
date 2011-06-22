using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    internal class MonsterProjectileManager
    {
        #region Internal methods
        /// <summary>
        /// Update monster's projectile
        /// </summary>
        /// <param name="iProjectileShooter">projectile shooter</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="playerSprite">player sprite</param>
        internal void Update(IProjectileShooter iProjectileShooter, SpritePopulation spritePopulation, PlayerSprite playerSprite, float timeDelta, Random random)
        {
            if (Math.Abs(playerSprite.XPosition - iProjectileShooter.XPosition) > iProjectileShooter.MaxShootingDistance)
                return;

            iProjectileShooter.ShootingCycle.Increment(timeDelta);

            if (iProjectileShooter.ShootingCycle.IsFinished)
            {
                SoundManager.PlayThrowSound();
                AbstractSprite projectile = iProjectileShooter.GetProjectile(random);

                projectile.JumpingCycle.Fire();
                projectile.CurrentJumpAcceleration = projectile.StartingJumpAcceleration;
                projectile.CurrentWalkingSpeed = projectile.MaxWalkingSpeed;

                spritePopulation.Add(projectile);

                if (projectile is MonsterSprite)
                    ((MonsterSprite)projectile).IsNoAiDefaultDirectionWalkingRight = iProjectileShooter.XPosition < playerSprite.XPosition;

                iProjectileShooter.ShootingCycle.TotalTimeLength = (float)random.NextDouble() * (iProjectileShooter.MaxShootingTimeBetween - iProjectileShooter.MinShootingTimeBetween) + iProjectileShooter.MinShootingTimeBetween;
                iProjectileShooter.ShootingCycle.Fire();
            }
        }
        #endregion
    }
}
