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
    class FireBallToMonsterCollisionManager
    {
        /// <summary>
        /// Manages collisions between fireball and monster
        /// </summary>
        /// <param name="fireBallSprite">fire ball</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        internal void Update(FireBallSprite fireBallSprite, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (!fireBallSprite.IsAlive)
                return;

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (fireBallSprite != otherSprite && !(otherSprite is PlayerSprite) && !(otherSprite is FireBallSprite) && !otherSprite.HitCycle.IsFired)
                {
                    if (otherSprite is MonsterSprite && Physics.IsDetectCollision(fireBallSprite, otherSprite))
                    {
                        SoundManager.PlayHitSound();
                        otherSprite.HitCycle.Fire();
                        otherSprite.CurrentDamageReceiving = fireBallSprite.AttackStrengthCollision;
                    }
                }
            }
        }
    }
}
