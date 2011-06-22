using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages sprite collisions
    /// </summary>
    internal class SpriteCollisionManager
    {
        #region Fields and parts
        /// <summary>
        /// Manages blocks (open, close, break etc)
        /// </summary>
        private BlockManager blockManager = new BlockManager();

        /// <summary>
        /// Manages jumping on beaver and other beaver stuff
        /// </summary>
        private BeaverManager beaverManager = new BeaverManager();

        /// <summary>
        /// Manages touching powerups
        /// </summary>
        private PowerUpManager powerUpManager = new PowerUpManager();

        /// <summary>
        /// To convert sprites
        /// </summary>
        private SpriteConversionManager spriteConverter = new SpriteConversionManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Manage sprite collisions
        /// </summary>
        /// <param name="sprite">current sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="program">program itself</param>
        /// <param name="gameMetaState">game meta state</param>
        /// <param name="gameState">game state</param>
        /// <param name="random">random number generator</param>
        internal void Update(AbstractSprite sprite, Level level, float timeDelta, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, Program program, GameMetaState gameMetaState, GameState gameState, Random random)
        {
            List<AbstractSprite> sortedVisibleSpriteList = SpriteDistanceSorter.Sort(sprite, visibleSpriteList);

            foreach (AbstractSprite otherSprite in sortedVisibleSpriteList)
            {
                if (sprite == otherSprite || !Physics.IsDetectCollision(sprite, otherSprite) || otherSprite == sprite.CarriedSprite)
                    continue;

                if (sprite is PlayerSprite && otherSprite is MushroomSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchMushroom((PlayerSprite)sprite, (MushroomSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is PeyoteSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchPeyote((PlayerSprite)sprite, (PeyoteSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is WhiskySprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchWhisky((PlayerSprite)sprite, (WhiskySprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is MusicNoteSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchMusicNote((PlayerSprite)sprite, (MusicNoteSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is RastaHatSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchRastaHat((PlayerSprite)sprite, (RastaHatSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is VortexSprite && sprite.IsTryToWalkUp && !((PlayerSprite)sprite).FromVortexCycle.IsFired)
                {
                    UpdateGoToVortex((PlayerSprite)sprite, (VortexSprite)otherSprite, program, gameMetaState, gameState);
                }
                else if (otherSprite is StaticSprite && otherSprite.IsImpassable && otherSprite.IsAlive && sprite.IGround == null && !sprite.IsCrossGrounds)
                {
                    blockManager.UpdateJumpOnBlock(sprite, (StaticSprite)otherSprite, spritePopulation, level, visibleSpriteList, random);
                }
                else if (sprite.IGround == null && sprite.IsAlive && !(sprite is FireBallSprite) && sprite.YPosition < otherSprite.YPosition && (!(otherSprite is MonsterSprite) || ((MonsterSprite)otherSprite).IsJumpableOn)) //Player IS jumping on the monster
                {
                    if (sprite is PlayerSprite && otherSprite is BeaverSprite && !((PlayerSprite)sprite).IsBeaver && !program.UserInput.isPressLeaveBeaver && sprite.CurrentJumpAcceleration < 0)
                    {
                        beaverManager.UpdateJumpOnBeaver((PlayerSprite)sprite, (BeaverSprite)otherSprite);
                    }
                    else
                    {
                        UpdateJumpOnSprite(sprite, otherSprite, level, spritePopulation, timeDelta, random);
                    }
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && ((MonsterSprite)otherSprite).IsToggleWalkWhenJumpedOn && !((MonsterSprite)otherSprite).IsWalkEnabled) //Start/stop (for helmets)
                {
                    if (sprite.IsRunning && sprite.IGround != null && sprite.CarriedSprite == null && otherSprite is ICarriable && !sprite.AttackingCycle.IsFired && otherSprite.IsAlive)
                    {
                        sprite.CarriedSprite = otherSprite;
                    }
                    else
                    {
                        KickOrStopHelmet(sprite, (MonsterSprite)otherSprite, level, timeDelta);
                    }
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && otherSprite.IsAlive)
                {
                    UpdateDirectCollision((PlayerSprite)sprite, (MonsterSprite)otherSprite, level, timeDelta, spritePopulation, random);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Player will go to vortex
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="vortexSprite">vortex sprite</param>
        /// <param name="program">program itself</param>
        /// <param name="gameMetaState">meta state</param>
        /// <param name="gameState">game state</param>
        private void UpdateGoToVortex(PlayerSprite playerSprite, VortexSprite vortexSprite, Program program, GameMetaState gameMetaState, GameState gameState)
        {
            gameMetaState.SetWarpBack(vortexSprite.DestinationSeed, gameState.Seed);
            program.ChangeGameState(vortexSprite.DestinationSeed);
        }

        /// <summary>
        /// Direct collision between player and monster (player will receive damage unless it's a newly kicked helmet)
        /// </summary>
        /// <param name="sprite">player (normally)</param>
        /// <param name="monsterSprite">monster</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        private void UpdateDirectCollision(PlayerSprite sprite, MonsterSprite monsterSprite, Level level, float timeDelta, SpritePopulation spritePopulation, Random random)
        {
            if (sprite.HitCycle.IsFired || monsterSprite.KickedHelmetCycle.IsFired || sprite.FromVortexCycle.IsFired)
                return;

            if (sprite.InvincibilityCycle.IsFired && monsterSprite.IsVulnerableToInvincibility)
            {
                SoundManager.PlayHitSound();
                monsterSprite.IsAlive = false;
                monsterSprite.JumpingCycle.Fire();
            }
            else if (monsterSprite.IsCanDoDamageToPlayerWhenTouched)
            {
                sprite.HitCycle.Fire();

                if (sprite.IsBeaver)
                {
                    SoundManager.PlayBeaverHitSound();
                    sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                    sprite.IGround = null;
                    sprite.JumpingCycle.Fire();
                    sprite.IsBeaver = false;
                    BeaverSprite beaverSprite = new BeaverSprite(sprite.XPosition, sprite.YPosition, random);
                    spritePopulation.Add(beaverSprite);
                    beaverSprite.IsWalkEnabled = true;
                }
                else
                {
                    if (sprite is PlayerSprite && !sprite.IsTiny)
                        ((PlayerSprite)sprite).ChangingSizeAnimationCycle.Fire();

                    SoundManager.PlayHit2Sound();
                    if (sprite.IsDoped)
                        sprite.IsDoped = false;
                    if (sprite.IsRasta)
                        sprite.IsRasta = false;
                    sprite.IsTiny = true;
                    sprite.CurrentDamageReceiving = monsterSprite.AttackStrengthCollision;
                }
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
        private void UpdateJumpOnSprite(AbstractSprite sprite, AbstractSprite otherSprite, Level level, SpritePopulation spritePopulation, float timeDelta, Random random)
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
                                if (otherSprite.Bounciness > 1.0 && sprite is PlayerSprite)
                                    SoundManager.PlayTrampolineSound();
                                #endregion
                            }
                        }

                        if (!(sprite is PlayerSprite))
                            return;


                        if (otherSprite is MonsterSprite)
                        {
                            MonsterSprite monsterSprite = (MonsterSprite)otherSprite;

                            if (monsterSprite is CrystalBallSprite)
                                SoundManager.PlayGlassBreakSound();
                            else
                                SoundManager.PlayHitSound();

                            if (((MonsterSprite)otherSprite).IsEnableJumpOnConversion && (!(sprite is PlayerSprite) || !((PlayerSprite)sprite).InvincibilityCycle.IsFired)) //If sprite is converted into another sprite when getting jumped on
                            {
                                AbstractSprite jumpedOnConvertedSprite = ((MonsterSprite)otherSprite).GetConverstionSprite(random);
                                if (jumpedOnConvertedSprite != null)
                                    spriteConverter.PerformSpriteConversion(sprite, otherSprite, jumpedOnConvertedSprite, spritePopulation);                                
                            }
                            else if (monsterSprite.IsToggleWalkWhenJumpedOn) //Start/stop (for helmets)
                            {
                                KickOrStopHelmet(sprite, monsterSprite, level, timeDelta);
                            }
                            else 
                            {
                                if (sprite is PlayerSprite && ((PlayerSprite)sprite).InvincibilityCycle.IsFired && monsterSprite.IsVulnerableToInvincibility)
                                {
                                    monsterSprite.IsAlive = false;
                                    monsterSprite.JumpingCycle.Fire();
                                }
                                else //Other sprite (monster) will be damaged
                                {
                                    otherSprite.HitCycle.Fire();
                                    otherSprite.CurrentDamageReceiving = sprite.AttackStrengthCollision;
                                }
                            }
                        }

                        if (sprite is PlayerSprite)
                            ((PlayerSprite)sprite).IsNeedToJumpAgain = true;
                    }
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
        public void KickOrStopHelmet(AbstractSprite sprite, MonsterSprite monsterSprite, Level level, float timeDelta)
        {
            if (monsterSprite.KickedHelmetCycle.IsFired)
                return;

            if (sprite is PlayerSprite && ((PlayerSprite)sprite).InvincibilityCycle.IsFired)
            {
                SoundManager.PlayHitSound();
                monsterSprite.IsAlive = false;
                monsterSprite.JumpingCycle.Fire();
                return;
            }

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
