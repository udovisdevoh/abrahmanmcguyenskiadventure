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
            if (!helmet.IsWalkEnabled && !helmet.IsCurrentlyInFreeFallX)
                return;

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (helmet != otherSprite && !(otherSprite is PlayerSprite) && !otherSprite.HitCycle.IsFired && !(otherSprite is FireBallSprite))
                {
                    if (otherSprite is MonsterSprite)
                    {
                        if (Physics.IsDetectCollision(helmet, otherSprite))
                        {
                            helmet.IsCurrentlyInFreeFallX = false;
                            helmet.IsCurrentlyInFreeFallY = false;
                            SoundManager.PlayHitSound();
                            otherSprite.HitCycle.Fire();
                            otherSprite.CurrentDamageReceiving = helmet.AttackStrengthCollision * 2.0;//Yes, twice damage to monsters
                        }
                    }
                    else if (otherSprite is StaticSprite && helmet.IGround != null && otherSprite.IsImpassable)
                    {
                        double virtualOffsetX = (helmet.IsTryingToWalkRight) ? 0.25 : -0.25;
                        if (Physics.IsDetectCollision(helmet, helmet.XPosition + virtualOffsetX, helmet.YPosition, 1.0, otherSprite))
                        {

                        }
                    }
                }
            }
        }
    }
}
