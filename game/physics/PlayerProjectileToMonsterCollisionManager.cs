using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages collisions between fireball and monster
    /// </summary>
    class PlayerProjectileToMonsterCollisionManager
    {
        /// <summary>
        /// Manages collisions between fireball and monster
        /// </summary>
        /// <param name="fireBallSprite">fire ball</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        internal void Update(AbstractSprite projectile, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (!projectile.IsAlive)
                return;

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (projectile != otherSprite && !(otherSprite is PlayerSprite) && !(otherSprite is IPlayerProjectile) && !otherSprite.HitCycle.IsFired)
                {
                    if (otherSprite is MonsterSprite && !(otherSprite is BeaverSprite) && Physics.IsDetectCollision(projectile, otherSprite))
                    {
                        SoundManager.PlayHitSound();
                        otherSprite.HitCycle.Fire();
                        if (!(projectile is KiBallSprite))
                            projectile.IsAlive = false;

                        if (!(otherSprite is MonsterSprite) || !(((MonsterSprite)otherSprite).IsResistantToPlayerProjectile))
                        {
                            otherSprite.CurrentDamageReceiving = projectile.AttackStrengthCollision;
                        }
                    }
                }
            }
        }
    }
}
