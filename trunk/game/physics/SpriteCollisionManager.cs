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
        #region Internal Methods
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
                                            MonsterSprite monsterSprite = (MonsterSprite)otherSprite;

                                            SoundManager.PlayHitSound();
                                            AbstractSprite jumpedOnConvertedSprite = ((MonsterSprite)otherSprite).GetConverstionSprite(random);

                                            if (jumpedOnConvertedSprite != null) //If sprite is converted into another sprite when getting jumped on
                                            {
                                                otherSprite.IsAlive = false;
                                                otherSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
                                                spritePopulation.Add(jumpedOnConvertedSprite);

                                                if (monsterSprite.IsInstantKickConvertedSprite)
                                                {
                                                    ((MonsterSprite)jumpedOnConvertedSprite).IsNoAiDefaultDirectionWalkingRight = sprite.XPosition < otherSprite.XPosition; //default direction for helmet
                                                    jumpedOnConvertedSprite.CurrentWalkingSpeed = jumpedOnConvertedSprite.WalkingAcceleration;
                                                }
                                                else
                                                    ((MonsterSprite)jumpedOnConvertedSprite).IsWalkEnabled = false;
                                            }
                                            else if (monsterSprite.IsToggleWalkWhenJumpedOn) //Start/stop (for helmets)
                                            {
                                                KickOrStopHelmet(sprite, monsterSprite, level, timeDelta);
                                            }
                                            else //Other sprite will be damaged
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
                            if (otherSprite is MonsterSprite)
                            {
                                MonsterSprite monsterSprite = (MonsterSprite)otherSprite;

                                if (monsterSprite.IsToggleWalkWhenJumpedOn && !monsterSprite.IsWalkEnabled) //Start/stop (for helmets)
                                {
                                    KickOrStopHelmet(sprite, monsterSprite, level, timeDelta);
                                }
                                else if (!sprite.HitCycle.IsFired && !monsterSprite.KickedHelmetCycle.IsFired) //Damage to player
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Kick or stop helmet (if moving, stop, if not kick it)
        /// </summary>
        /// <param name="sprite">kicker</param>
        /// <param name="monsterSprite">kicked</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        private void KickOrStopHelmet(AbstractSprite sprite, MonsterSprite monsterSprite, Level level, double timeDelta)
        {
            monsterSprite.IsWalkEnabled = !monsterSprite.IsWalkEnabled;
            if (monsterSprite.IsWalkEnabled)
            {
                monsterSprite.IsNoAiDefaultDirectionWalkingRight = sprite.XPosition < monsterSprite.XPosition; //default direction for helmet
                monsterSprite.CurrentWalkingSpeed = monsterSprite.WalkingAcceleration;
                monsterSprite.KickedHelmetCycle.Fire();    
            }
        }
        #endregion
    }
}
