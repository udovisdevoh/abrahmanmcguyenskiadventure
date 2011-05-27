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
                    if (!otherSprite.PunchedCycle.IsFired)
                    {
                        MonsterSprite monsterSprite = (MonsterSprite)otherSprite;
                        if (sprite != monsterSprite)
                        {
                            if (Physics.IsDetectCollisionPunchOrKick(sprite, monsterSprite))
                            {
                                SoundManager.PlayPunchSound();
                                monsterSprite.HitCycle.Fire();
                                monsterSprite.PunchedCycle.Fire();
                                monsterSprite.CurrentDamageReceiving = sprite.AttackStrengthCollision;

                                monsterSprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                                monsterSprite.JumpingCycle.Reset();
                                monsterSprite.JumpingCycle.Fire();

                                if (monsterSprite.IsTryingToWalkRight)
                                    monsterSprite.CurrentWalkingSpeed = monsterSprite.MaxRunningSpeed;
                                else
                                    monsterSprite.CurrentWalkingSpeed = monsterSprite.MaxRunningSpeed * -1.0;

                                monsterSprite.IsTryingToJump = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
