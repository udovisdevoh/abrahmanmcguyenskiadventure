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
        AbstractSprite GetProjectile(Random random);

        /// <summary>
        /// Shooting cycle
        /// </summary>
        Cycle ShootingCycle
        {
            get;
        }

        /// <summary>
        /// Minimum time between shots
        /// </summary>
        float MinShootingTimeBetween
        {
            get;
        }

        /// <summary>
        /// Maximum time between shots
        /// </summary>
        float MaxShootingTimeBetween
        {
            get;
        }

        /// <summary>
        /// Maximum shooting distance
        /// </summary>
        float MaxShootingDistance
        {
            get;
        }

        float XPosition
        {
            get;
        }

        float RightBound
        {
            get;
        }

        float LeftBound
        {
            get;
        }
    }
}
