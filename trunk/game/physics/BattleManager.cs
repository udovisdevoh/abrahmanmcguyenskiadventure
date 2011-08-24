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
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, List<AbstractSprite> sortedVisibleSpriteList, PlayerSprite playerSpriteReference)
        {
            foreach (AbstractSprite otherSprite in sortedVisibleSpriteList)
            {
                if (sprite == otherSprite || !otherSprite.IsAlive || !otherSprite.IsVulnerableToPunch || !Physics.IsDetectCollisionPunchOrKick(sprite, otherSprite))
                    continue;

                if (otherSprite is MushroomSprite && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                {
                    powerUpManager.UpdateTouchMushroom((PlayerSprite)sprite, (MushroomSprite)otherSprite);
                    break;
                }
                else if (otherSprite is RastaHatSprite && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                {
                    powerUpManager.UpdateTouchRastaHat((PlayerSprite)sprite, (RastaHatSprite)otherSprite);
                    break;
                }
                else if (otherSprite is PeyoteSprite && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                {
                    powerUpManager.UpdateTouchPeyote((PlayerSprite)sprite, (PeyoteSprite)otherSprite);
                    break;
                }
                else if (otherSprite is MusicNoteSprite && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                {
                    powerUpManager.UpdateTouchMusicNote((PlayerSprite)sprite, (MusicNoteSprite)otherSprite);
                    break;
                }
                else if (otherSprite is WhiskySprite && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                {
                    powerUpManager.UpdateTouchWhisky((PlayerSprite)sprite, (WhiskySprite)otherSprite);
                    break;
                }
                else if (otherSprite is BandanaSprite && sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                {
                    powerUpManager.UpdateTouchBandana((PlayerSprite)sprite, (BandanaSprite)otherSprite);
                    break;
                }
                else if (otherSprite is MonsterSprite)
                {
                    if (!otherSprite.PunchedCycle.IsFired && otherSprite != playerSpriteReference.CarriedSprite)
                    {
                        MonsterSprite monsterSprite = (MonsterSprite)otherSprite;
                        if (!monsterSprite.KickedHelmetCycle.IsFired)
                        {
                            if (sprite is PlayerSprite && ((PlayerSprite)sprite).IsBeaver)
                                SoundManager.PlayBeaverAttackSound();
                            else
                                SoundManager.PlayPunchSound();

                            if (sprite is PlayerSprite && ((PlayerSprite)sprite).InvincibilityCycle.IsFired && monsterSprite.IsVulnerableToInvincibility)
                            {
                                monsterSprite.IsAlive = false;
                                monsterSprite.JumpingCycle.Fire();
                            }
                            else
                            {
                                monsterSprite.HitCycle.Fire();
                                monsterSprite.PunchedCycle.Fire();
                                monsterSprite.CurrentDamageReceiving = sprite.AttackStrengthCollision;
                            }

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
                    break;
                }
            }
        }
    }
}
