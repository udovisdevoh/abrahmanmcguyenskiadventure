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
                            UpdateJumpOnMonster(sprite, otherSprite, level, spritePopulation, timeDelta, random);
                        }
                        else if (otherSprite is MonsterSprite)
                        {
                            UpdateDirectCollision(sprite, (MonsterSprite)otherSprite, level, timeDelta);
                        }
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Direct collision between player and monster (player will receive damage unless it's a newly kicked helmet)
        /// </summary>
        /// <param name="sprite">player (normally)</param>
        /// <param name="monsterSprite">monster</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        private void UpdateDirectCollision(AbstractSprite sprite, MonsterSprite monsterSprite, Level level, double timeDelta)
        {
            if (monsterSprite.IsToggleWalkWhenJumpedOn && !monsterSprite.IsWalkEnabled) //Start/stop (for helmets)
            {
                KickOrStopHelmet(sprite, monsterSprite, level, timeDelta);
            }
            else if (!sprite.HitCycle.IsFired && !monsterSprite.KickedHelmetCycle.IsFired) //Damage to player
            {
                SoundManager.PlayHit2Sound();
                sprite.HitCycle.Fire();
                sprite.CurrentDamageReceiving = monsterSprite.AttackStrengthCollision;
            }
        }

        /// <summary>
        /// When a sprite jumps on another one
        /// </summary>
        /// <param name="sprite">jumper</param>
        /// <param name="otherSprite">jumped on</param>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="random">random number generator</param>
        private void UpdateJumpOnMonster(AbstractSprite sprite, AbstractSprite otherSprite, Level level, SpritePopulation spritePopulation, double timeDelta, Random random)
        {
            if (sprite is PlayerSprite || (sprite is MonsterSprite && ((MonsterSprite)sprite).IsCanJump))
            {
                if (sprite.CurrentJumpAcceleration < 0) //if sprite is falling
                {
                    if (!otherSprite.HitCycle.IsFired) //if other sprite is not being hit already
                    {
                        #region Jumper sprite bounces
                        sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                        sprite.JumpingCycle.Reset();
                        sprite.JumpingCycle.Fire();
                        sprite.IsTryingToJump = true;
                        #endregion

                        if (otherSprite is MonsterSprite)
                        {
                            MonsterSprite monsterSprite = (MonsterSprite)otherSprite;
                            SoundManager.PlayHitSound();
                            AbstractSprite jumpedOnConvertedSprite = ((MonsterSprite)otherSprite).GetConverstionSprite(random);

                            if (jumpedOnConvertedSprite != null) //If sprite is converted into another sprite when getting jumped on
                            {
                                PerformSpriteConversion(sprite, otherSprite, jumpedOnConvertedSprite, spritePopulation);                                
                            }
                            else if (monsterSprite.IsToggleWalkWhenJumpedOn) //Start/stop (for helmets)
                            {
                                KickOrStopHelmet(sprite, monsterSprite, level, timeDelta);
                            }
                            else //Other sprite (monster) will be damaged
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

        /// <summary>
        /// Convert sprite to another one
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="oldSprite">sprite to convert (old sprite)</param>
        /// <param name="newSprite">new sprite (to replace old sprite)</param>
        /// <param name="spritePopulation">sprite population</param>
        private void PerformSpriteConversion(AbstractSprite playerSprite, AbstractSprite oldSprite, AbstractSprite newSprite, SpritePopulation spritePopulation)
        {
            oldSprite.IsAlive = false;
            oldSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
            spritePopulation.Add(newSprite);

            if (newSprite is MonsterSprite && oldSprite is MonsterSprite)
            {
                if (((MonsterSprite)oldSprite).IsInstantKickConvertedSprite)
                {
                    ((MonsterSprite)newSprite).IsNoAiDefaultDirectionWalkingRight = playerSprite.XPosition < oldSprite.XPosition; //default direction for helmet
                    newSprite.CurrentWalkingSpeed = newSprite.WalkingAcceleration;
                }
                else
                    ((MonsterSprite)newSprite).IsWalkEnabled = false;
            }
        }

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
                SoundManager.PlayHelmetKickSound();
                monsterSprite.IsNoAiDefaultDirectionWalkingRight = sprite.XPosition < monsterSprite.XPosition; //default direction for helmet
                monsterSprite.CurrentWalkingSpeed = monsterSprite.WalkingAcceleration;
                monsterSprite.KickedHelmetCycle.Fire();    
            }
        }
        #endregion
    }
}
