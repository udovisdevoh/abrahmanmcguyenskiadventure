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
    /// Manages collsions between helmets and monsters
    /// </summary>
    internal class HelmetToMonsterCollisionManager
    {
        /// <summary>
        /// Manages collsions between helmets and monsters
        /// </summary>
        /// <param name="helmet">helmet sprite</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(HelmetSprite helmet, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (helmet != otherSprite && !(otherSprite is PlayerSprite) && !otherSprite.HitCycle.IsFired)
                {
                    if (Physics.IsDetectCollision(helmet, otherSprite))
                    {
                        SoundManager.PlayHitSound();
                        otherSprite.HitCycle.Fire();
                        otherSprite.CurrentDamageReceiving = helmet.AttackStrengthCollision * 2.0;//Yes, twice damage to monsters
                    }
                }
            }
        }
    }
}
