using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Monsters that shoot projectiles
    /// </summary>
    interface IProjectileShooter
    {
        /// <summary>
        /// Get projectile
        /// </summary>
        /// <param name="random">random</param>
        /// <returns>Get projectile</returns>
        SideScrollerSprite GetProjectile(Random random);

        /// <summary>
        /// Shooting cycle
        /// </summary>
        Cycle ShootingCycle
        {
            get;
        }

        /// <summary>
        /// Get the type of a projectile
        /// </summary>
        Type ProjectileType
        {
            get;
        }

        /// <summary>
        /// Max projectile count per screen (if less that 1: infinite)
        /// </summary>
        int MaxProjectileCountPerScreen
        {
            get;
        }

        /// <summary>
        /// Minimum time between shots
        /// </summary>
        double MinShootingTimeBetween
        {
            get;
        }

        /// <summary>
        /// Maximum time between shots
        /// </summary>
        double MaxShootingTimeBetween
        {
            get;
        }

        /// <summary>
        /// Maximum shooting distance
        /// </summary>
        double MaxShootingDistance
        {
            get;
        }

        double XPosition
        {
            get;
        }

        double RightBound
        {
            get;
        }

        double LeftBound
        {
            get;
        }
    }
}
