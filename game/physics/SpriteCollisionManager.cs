using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages sprite collisions
    /// </summary>
    internal class SpriteCollisionManager
    {
        /// <summary>
        /// Manage sprite collisions
        /// </summary>
        /// <param name="sprite">current sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList)
        {
            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (sprite != otherSprite)
                {
                    if (Physics.IsDetectCollision(sprite, otherSprite))
                    {
                        if (sprite.Ground == null && sprite.YPosition < otherSprite.YPosition)
                        {
                            if (sprite is PlayerSprite || (sprite is MonsterSprite && ((MonsterSprite)sprite).IsCanJump))
                            {
                                if (sprite.CurrentJumpAcceleration < 0) //if sprite is falling
                                {
                                    if (!otherSprite.HitCycle.IsFired)
                                    {
                                        sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                                        sprite.JumpingCycle.Reset();
                                        sprite.JumpingCycle.Fire();
                                        sprite.IsTryingToJump = true;

                                        if (otherSprite is MonsterSprite)
                                        {
                                            otherSprite.HitCycle.IsFirstFrame = true;
                                            otherSprite.HitCycle.Fire();
                                            otherSprite.CurrentDamageReceiving = sprite.AttackStrengthCollision;
                                        }

                                        if (sprite is PlayerSprite)
                                            ((PlayerSprite)sprite).IsNeedToJumpAgain = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (otherSprite is MonsterSprite && !sprite.HitCycle.IsFired)
                            {
                                sprite.HitCycle.IsFirstFrame = true;
                                sprite.HitCycle.Fire();
                                sprite.CurrentDamageReceiving = otherSprite.AttackStrengthCollision;
                            }
                        }
                    }
                }
            }
        }
    }
}
