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
        internal void Update(IProjectileShooter iProjectileShooter, SpritePopulation spritePopulation, HashSet<SideScrollerSprite> visibleSpriteList, PlayerSprite playerSprite, double timeDelta, Random random)
        {
            if (Math.Abs(playerSprite.XPosition - iProjectileShooter.XPosition) > iProjectileShooter.MaxShootingDistance)
                return;

            iProjectileShooter.ShootingCycle.Increment(timeDelta);

            //If there are too much projectiles on the screen, we reset the shooting cycle so we try again later
            if (iProjectileShooter.MaxProjectileCountPerScreen > 0 && CountProjectileOfShooterOnScreen(iProjectileShooter, visibleSpriteList) >= iProjectileShooter.MaxProjectileCountPerScreen)
            {
                iProjectileShooter.ShootingCycle.Reset();
                iProjectileShooter.ShootingCycle.Fire();
            }

            if (iProjectileShooter.ShootingCycle.IsFinished)
            {
                SoundManager.PlayThrowSound();
                SideScrollerSprite projectile = iProjectileShooter.GetProjectile(random);

                projectile.JumpingCycle.Fire();
                projectile.CurrentJumpAcceleration = projectile.StartingJumpAcceleration;
                projectile.CurrentWalkingSpeed = projectile.MaxWalkingSpeed;

                spritePopulation.Add(projectile);

                if (projectile is MonsterSprite)
                    ((MonsterSprite)projectile).IsNoAiDefaultDirectionWalkingRight = iProjectileShooter.XPosition < playerSprite.XPosition;

                iProjectileShooter.ShootingCycle.TotalTimeLength = random.NextDouble() * (iProjectileShooter.MaxShootingTimeBetween - iProjectileShooter.MinShootingTimeBetween) + iProjectileShooter.MinShootingTimeBetween;
                iProjectileShooter.ShootingCycle.Fire();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Count the projectiles on screen that may be thrown by the projectile shooter
        /// </summary>
        /// <param name="iProjectileShooter">projectile shooter</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <returns>How many projectiles on screen that may be thrown by the projectile shooter</returns>
        private int CountProjectileOfShooterOnScreen(IProjectileShooter iProjectileShooter, HashSet<SideScrollerSprite> visibleSpriteList)
        {
            int count = 0;
            Type projectileType = iProjectileShooter.ProjectileType;
            foreach (SideScrollerSprite sprite in visibleSpriteList)
                if (sprite.GetType() == projectileType)
                    count++;

            return count;
        }
        #endregion
    }
}
