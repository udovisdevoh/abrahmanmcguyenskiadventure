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
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, Random random)
        {
            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (sprite != otherSprite)
                {
                    if (Physics.IsDetectCollision(sprite, otherSprite))
                    {
                        if (sprite.Ground == null && sprite.YPosition < otherSprite.YPosition) //Player IS jumping on the monster
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
                                            SoundManager.PlayHitSound();
                                            AbstractSprite jumpedOnConvertedSprite = ((MonsterSprite)otherSprite).GetConverstionSprite(random);

                                            if (jumpedOnConvertedSprite != null) //If sprite is converted into another sprite when getting jumped on
                                            {
                                                otherSprite.IsAlive = false;
                                                otherSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
                                                spritePopulation.Add(jumpedOnConvertedSprite);
                                            }
                                            else
                                            {
                                                otherSprite.HitCycle.Fire();
                                                otherSprite.CurrentDamageReceiving = sprite.AttackStrengthCollision;
                                            }
                                        }

                                        if (sprite is PlayerSprite)
                                            ((PlayerSprite)sprite).IsNeedToJumpAgain = true;
                                    }
                                }
                            }
                        }
                        else //Player is NOT jumping on the monster
                        {
                            if (otherSprite is MonsterSprite && !sprite.HitCycle.IsFired)
                            {
                                SoundManager.PlayHit2Sound();
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
