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
        /// Manages powerups
        /// </summary>
        private PowerUpManager powerUpManager = new PowerUpManager();

        /// <summary>
        /// Update fist/kick fight logic
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="sortedVisibleSpriteList">visible sprite sorted list</param>
        /// <param name="playerSpriteReference">player sprite</param>
        internal void Update(PlayerSprite playerSprite, Level level, AbstractGameMode gameMode, double timeDelta, List<AbstractSprite> sortedVisibleSpriteList, PlayerSprite playerSpriteReference)
        {
            foreach (AbstractSprite otherSprite in sortedVisibleSpriteList)
            {
                if (playerSprite == otherSprite || !otherSprite.IsAlive || !otherSprite.IsVulnerableToPunch || !Physics.IsDetectCollisionPunchOrKick(playerSprite, otherSprite))
                    continue;

                if (otherSprite is MushroomSprite && playerSprite.IsBeaver)
                {
                    gameMode.UpdateTouchMushroom(playerSprite, (MushroomSprite)otherSprite);
                    break;
                }
                else if (otherSprite is RastaHatSprite && playerSprite.IsBeaver)
                {
                    powerUpManager.UpdateTouchRastaHat(playerSprite, (RastaHatSprite)otherSprite);
                    break;
                }
                else if (otherSprite is BuddhaSprite && playerSprite.IsBeaver)
                {
                    powerUpManager.UpdateTouchBuddha(playerSprite, (BuddhaSprite)otherSprite);
                    break;
                }
                else if (otherSprite is PeyoteSprite && playerSprite.IsBeaver)
                {
                    powerUpManager.UpdateTouchPeyote(playerSprite, (PeyoteSprite)otherSprite);
                    break;
                }
                else if (otherSprite is MusicNoteSprite && playerSprite.IsBeaver)
                {
                    powerUpManager.UpdateTouchMusicNote(playerSprite, (MusicNoteSprite)otherSprite, gameMode);
                    break;
                }
                else if (otherSprite is WhiskySprite && playerSprite.IsBeaver)
                {
                    powerUpManager.UpdateTouchWhisky(playerSprite, (WhiskySprite)otherSprite);
                    break;
                }
                else if (otherSprite is BandanaSprite && playerSprite.IsBeaver)
                {
                    powerUpManager.UpdateTouchBandana(playerSprite, (BandanaSprite)otherSprite);
                    break;
                }
                else if (otherSprite is MonsterSprite)
                {
                    if (!otherSprite.PunchedCycle.IsFired && otherSprite != playerSpriteReference.CarriedSprite)
                    {
                        MonsterSprite monsterSprite = (MonsterSprite)otherSprite;
                        if (!monsterSprite.KickedHelmetCycle.IsFired)
                        {
                            if (playerSprite.IsBeaver)
                                SoundManager.PlayBeaverAttackSound();
                            else if (playerSprite.IsNinja && !playerSprite.IsTryUseNunchaku)
                                SoundManager.PlayGoreSound();
                            else
                                SoundManager.PlayPunchSound();

                            if (playerSprite.InvincibilityCycle.IsFired && monsterSprite.IsVulnerableToInvincibility)
                            {
                                gameMode.PerformKillMonsterExtraLogic(playerSprite, monsterSprite, level.SkillLevel);
                                monsterSprite.IsAlive = false;
                                monsterSprite.JumpingCycle.Fire();
                            }
                            else
                            {
                                if (monsterSprite is ICarriable && monsterSprite.IsToggleWalkWhenJumpedOn && !monsterSprite.IsWalkEnabled && playerSprite.CarriedSprite == null && monsterSprite.IsAlive)
                                {
                                    //We carry it
                                    playerSprite.CarriedSprite = monsterSprite;
                                }
                                else if (!monsterSprite.IsVulnerableToKatanaButNotPunch || (playerSprite.IsNinja && !playerSprite.IsBeaver))
                                {
                                    //We attack it
                                    monsterSprite.HitCycle.Fire();
                                    monsterSprite.PunchedCycle.Fire();
                                    monsterSprite.CurrentDamageReceiving = playerSprite.AttackStrengthCollision;
                                }
                            }

                            monsterSprite.CurrentJumpAcceleration = playerSprite.StartingJumpAcceleration;
                            monsterSprite.JumpingCycle.Reset();
                            monsterSprite.JumpingCycle.Fire();

                            if (monsterSprite.IsTryingToWalkRight)
                                monsterSprite.CurrentWalkingSpeed = monsterSprite.MaxRunningSpeed;
                            else
                                monsterSprite.CurrentWalkingSpeed = monsterSprite.MaxRunningSpeed * -1.0;

                            monsterSprite.IsTryingToJump = true;
                        }
                    }
                    break;
                }
            }
        }
    }
}
