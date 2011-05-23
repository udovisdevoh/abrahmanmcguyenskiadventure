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
    /// Manages fist/kick fight logic
    /// </summary>
    internal class BattleManager
    {
        /// <summary>
        /// Update fist/kick fight logic
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList)
        {
            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (otherSprite is MonsterSprite)
                {
                    if (sprite != otherSprite)
                    {
                        if (Physics.IsDetectCollisionPunchOrKick(sprite, otherSprite))
                        {
                            SoundManager.PlayPunchSound();
                            otherSprite.HitCycle.Fire();
                            otherSprite.PunchedCycle.Fire();
                            otherSprite.CurrentDamageReceiving = sprite.AttackStrengthCollision;

                            otherSprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                            otherSprite.JumpingCycle.Reset();
                            otherSprite.JumpingCycle.Fire();
                            otherSprite.IsTryingToJump = true;
                        }
                    }
                }
            }
        }
    }
}
