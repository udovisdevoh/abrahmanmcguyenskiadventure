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
                        if (sprite is PlayerSprite && otherSprite is MushroomSprite && otherSprite.IsAlive)
                        {
                            UpdateTouchMushroom((PlayerSprite)sprite, (MushroomSprite)otherSprite);
                        }
                        else if (sprite is PlayerSprite && otherSprite is ShishaSprite && otherSprite.IsAlive)
                        {
                            UpdateTouchShisha((PlayerSprite)sprite, (ShishaSprite)otherSprite);
                        }
                        else if (otherSprite is StaticSprite && otherSprite.IsImpassable && sprite.IGround == null && sprite.YPosition > otherSprite.YPosition)
                        {
                            UpdateJumpUnderBlock((PlayerSprite)sprite, (StaticSprite)otherSprite, spritePopulation,random);
                        }
                        else if (sprite.IGround == null && sprite.YPosition < otherSprite.YPosition) //Player IS jumping on the monster
                        {
                            UpdateJumpOnSprite(sprite, otherSprite, level, spritePopulation, timeDelta, random);
                        }
                        else if (otherSprite is MonsterSprite && otherSprite.IsAlive)
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
        /// Player jumps under anarchy block. It may output something from it (mushroom, flower, etc)
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="anarchyBlockSprite">block</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        private void UpdateJumpUnderBlock(PlayerSprite playerSprite, StaticSprite block, SpritePopulation spritePopulation, Random random)
        {
            if (playerSprite.YPosition >= playerSprite.YPositionPrevious)
                return;

            playerSprite.CurrentJumpAcceleration = playerSprite.StartingJumpAcceleration / -4.0;
            playerSprite.IsNeedToJumpAgain = true;

            //Must be well centered, or else, don't open the block
            if (Math.Abs(playerSprite.XPosition - block.XPosition) > block.Width / 2.25)
                return;

            if (block is AnarchyBlockSprite && !((AnarchyBlockSprite)block).IsFinalized)
            {
                SoundManager.PlayGrowSound();
                ((AnarchyBlockSprite)block).BumpCycle.Fire();
                ((AnarchyBlockSprite)block).IsFinalized = true;

                AbstractSprite powerUpSprite = ((AnarchyBlockSprite)block).GetPowerUpSprite(playerSprite, random);
                if (powerUpSprite is IGrowable)
                    ((IGrowable)powerUpSprite).GrowthCycle.Fire();
                spritePopulation.Add(powerUpSprite);
            }
            else
            {
                SoundManager.PlayHelmetBumpSound();
            }
        }

        /// <summary>
        /// Player touches mushroom and get health
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="mushroomSprite">mushroom</param>
        private void UpdateTouchMushroom(PlayerSprite playerSprite, MushroomSprite mushroomSprite)
        {
            SoundManager.PlayPowerUpSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            playerSprite.Health -= mushroomSprite.AttackStrengthCollision;
            mushroomSprite.IsAlive = false;
            mushroomSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Sprite touches shisha
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="shishaSprite">shisha sprite</param>
        private void UpdateTouchShisha(PlayerSprite playerSprite, ShishaSprite shishaSprite)
        {
            SoundManager.PlayPowerUpSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            playerSprite.IsDoped = true;
            shishaSprite.IsAlive = false;
            shishaSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

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
                if (sprite is PlayerSprite)
                    ((PlayerSprite)sprite).IsDoped = false;
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
        private void UpdateJumpOnSprite(AbstractSprite sprite, AbstractSprite otherSprite, Level level, SpritePopulation spritePopulation, double timeDelta, Random random)
        {
            if (sprite is PlayerSprite || (sprite is MonsterSprite && ((MonsterSprite)sprite).IsCanJump))
            {
                if (sprite.CurrentJumpAcceleration < 0) //if sprite is falling
                {
                    if (!otherSprite.HitCycle.IsFired) //if other sprite is not being hit already
                    {   
                        if (sprite.CurrentJumpAcceleration <= 0)
                        {
                            if (otherSprite.Bounciness > 0)
                            {
                                #region Jumper sprite bounces
                                sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration * otherSprite.Bounciness;
                                sprite.JumpingCycle.Reset();
                                sprite.JumpingCycle.Fire();
                                sprite.IsTryingToJump = true;
                                if (otherSprite.Bounciness > 1.0)
                                    SoundManager.PlayTrampolineSound();
                                #endregion
                            }
                        }

                        if (otherSprite is MonsterSprite)
                        {
                            MonsterSprite monsterSprite = (MonsterSprite)otherSprite;
                            SoundManager.PlayHitSound();

                            if (((MonsterSprite)otherSprite).IsEnableJumpOnConversion) //If sprite is converted into another sprite when getting jumped on
                            {
                                AbstractSprite jumpedOnConvertedSprite = ((MonsterSprite)otherSprite).GetConverstionSprite(random);
                                if (jumpedOnConvertedSprite != null)
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
                {
                    ((MonsterSprite)newSprite).IsWalkEnabled = false;
                    if (((MonsterSprite)newSprite).IsEnableSpontaneousConversion)
                        ((MonsterSprite)newSprite).SpontaneousTransformationCycle.Fire(); //We schedule an eventual transformation from helmet to monster
                }
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
                monsterSprite.SpontaneousTransformationCycle.StopAndReset();
            }
            else if (monsterSprite.IsEnableSpontaneousConversion)
            {
                monsterSprite.SpontaneousTransformationCycle.Fire(); //We schedule an eventual transformation from helmet to monster
            }
        }
        #endregion
    }
}
