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
    class PlayerProjectileCollisionManager
    {
        private BlockManager blockManager;

        public PlayerProjectileCollisionManager(BlockManager blockManager)
        {
            this.blockManager = blockManager;
        }

        /// <summary>
        /// Manages collisions between fireball and monster
        /// </summary>
        /// <param name="fireBallSprite">fire ball</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        internal void Update(AbstractSprite projectile, Level level, HashSet<AbstractSprite> visibleSpriteList, PlayerSprite playerSpriteReference, SpritePopulation spritePopulation, AbstractGameMode gameMode, Random random)
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
                    else if (projectile is KiBallSprite && (otherSprite is BrickSprite || otherSprite is AnarchyBlockSprite) && otherSprite.IsAlive)
                    {
                        if (Physics.IsDetectCollision(projectile, otherSprite))
                        {
                            blockManager.TryOpenOrBreakBlock(projectile, (StaticSprite)otherSprite, spritePopulation, visibleSpriteList, level, playerSpriteReference, gameMode, random);
                        }
                    }
                    else if (projectile is KiBallSprite && otherSprite is FlailBall && otherSprite.IsAlive)
                    {
                        if (Physics.IsDetectCollision(projectile, otherSprite))
                        {
                            SoundManager.PlayHitSound();
                            otherSprite.IsAlive = false;
                            if (((FlailBall)otherSprite).ParentNode != null)
                            {
                                ((FlailBall)otherSprite).ParentNode.RemoveChild((AbstractLinkage)otherSprite);
                            }
                        }
                    }
                }
            }
        }
    }
}
