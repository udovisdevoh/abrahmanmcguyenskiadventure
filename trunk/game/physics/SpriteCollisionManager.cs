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
        /// Manage vines and stuff like that
        /// </summary>
        private IClimbableManager climbableManager = new IClimbableManager();

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
        internal void Update(AbstractSprite sprite, PlayerSprite playerSpriteReference, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList, List<AbstractSprite> sortedVisibleSpriteList, SpritePopulation spritePopulation, Program program, GameMetaState gameMetaState, GameState gameState, Random random)
        {
            #region We unassign sprite.IClimbingOn if needed (it will be re-assigned later if there is a collision detection with vine or liana)
            IClimbable wasClimbingOnAtPreviousFrame = sprite.IClimbingOn;
            if (sprite.IClimbingOn != null && (!(sprite.IClimbingOn is LianaSprite) || sprite.YPosition > sprite.IClimbingOn.YPosition || sprite.YPosition < sprite.IClimbingOn.TopBound))
                sprite.IClimbingOn = null;
            #endregion

            foreach (AbstractSprite otherSprite in sortedVisibleSpriteList)
            {
                if (sprite == otherSprite || !Physics.IsDetectCollision(sprite, otherSprite) || otherSprite == sprite.CarriedSprite)
                    continue;

                if (otherSprite is IClimbable)
                    climbableManager.UpdateClimber(sprite, otherSprite, wasClimbingOnAtPreviousFrame, program.UserInput);

                if (sprite is PlayerSprite && otherSprite is MushroomSprite && otherSprite.IsAlive)
                {
                    gameState.GameMode.UpdateTouchMushroom((PlayerSprite)sprite, (MushroomSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is PeyoteSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchPeyote((PlayerSprite)sprite, (PeyoteSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is WhiskySprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchWhisky((PlayerSprite)sprite, (WhiskySprite)otherSprite);
                    if (SongPlayer.IRiff != SongGenerator.GetInvincibilitySong(gameState.Seed, gameState.GameMode))
                    {
                        SongPlayer.StopSync();
                        SongPlayer.IRiff = SongGenerator.GetInvincibilitySong(gameState.Seed, gameState.GameMode);
                        SongPlayer.PlayAsync();
                    }
                }
                else if (sprite is PlayerSprite && otherSprite is MusicNoteSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchMusicNote((PlayerSprite)sprite, (MusicNoteSprite)otherSprite, gameState.GameMode);
                }
                else if (sprite is PlayerSprite && otherSprite is RastaHatSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchRastaHat((PlayerSprite)sprite, (RastaHatSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is BuddhaSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchBuddha((PlayerSprite)sprite, (BuddhaSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is BandanaSprite && otherSprite.IsAlive)
                {
                    powerUpManager.UpdateTouchBandana((PlayerSprite)sprite, (BandanaSprite)otherSprite);
                    /*if (SongPlayer.IRiff != SongGenerator.GetNinjaSong(gameState.Seed, 0))
                    {
                        SongPlayer.StopSync();
                        SongPlayer.IRiff = SongGenerator.GetNinjaSong(gameState.Seed, 0);
                        SongPlayer.PlayAsync();
                    }*/
                }
                else if (sprite is PlayerSprite && otherSprite is VortexSprite && sprite.IsTryToWalkUp && !((PlayerSprite)sprite).FromVortexCycle.IsFired && (sprite.IGround != null || sprite.YPosition <= otherSprite.YPosition))
                {
                    UpdateGoToVortex((PlayerSprite)sprite, (VortexSprite)otherSprite, program, gameMetaState, gameState);
                }
                else if (otherSprite is StaticSprite && otherSprite.IsImpassable && otherSprite.IsAlive && !sprite.IsCrossGrounds && sprite != playerSpriteReference.CarriedSprite)
                {
                    blockManager.UpdateBlockCollision(sprite, (StaticSprite)otherSprite, spritePopulation, level, visibleSpriteList, playerSpriteReference, gameState.GameMode, random);
                }
                else if (sprite.IGround == null && sprite.IsAlive && !(sprite is IPlayerProjectile) && sprite.YPosition < otherSprite.YPosition) //Player IS jumping on the monster
                {
                    if (sprite is PlayerSprite && otherSprite is BeaverSprite && !((PlayerSprite)sprite).IsBeaver && !program.UserInput.isPressLeaveBeaver && sprite.CurrentJumpAcceleration < 0)
                    {
                        beaverManager.UpdateJumpOnBeaver((PlayerSprite)sprite, (BeaverSprite)otherSprite);
                    }
                    else if (!(otherSprite is IMovingGround))
                    {
                        if (sprite is PlayerSprite 
                            && otherSprite is MonsterSprite
                            && !((MonsterSprite)otherSprite).IsJumpableOn
                            && (!(sprite is PlayerSprite)
                            || !((PlayerSprite)sprite).IsBeaver)
                            || sprite is PlayerSprite && ((otherSprite is MonsterSprite) && !((MonsterSprite)otherSprite).IsJumpableOnEvenByBeaver))
                        {
                            //It is impossible to jump on this sprite
                            UpdateDirectCollision((PlayerSprite)sprite, (MonsterSprite)otherSprite, level, gameState, timeDelta, spritePopulation, random);
                        }
                        else if (sprite is BeaverSprite && ((BeaverSprite)sprite).IsAiEnabled && otherSprite is PlayerSprite)
                        {
                            //The AI controlled ninja beaver won't bounce on the player
                        }
                        else
                        {
                            UpdateJumpOnSprite(sprite, otherSprite, level, spritePopulation, timeDelta, gameState.GameMode, random);
                        }
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
                        KickOrStopHelmet(sprite, (MonsterSprite)otherSprite, level, gameState.GameMode, timeDelta);
                    }
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && otherSprite.IsAlive)
                {
                    UpdateDirectCollision((PlayerSprite)sprite, (MonsterSprite)otherSprite, level, gameState, timeDelta, spritePopulation, random);
                }
                else if (sprite is PlayerSprite && otherSprite is FlailBall && otherSprite.IsAlive)
                {
                    UpdateFlailCollision((PlayerSprite)sprite, (FlailBall)otherSprite, timeDelta, spritePopulation, random, gameState);
                }

                else if ((sprite is PlayerSprite || sprite is MonsterSprite) && otherSprite is Platform)
                {
                    UpdatePlatformMovesUpCatchSprite(sprite, (Platform)otherSprite);
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
            if (vortexSprite.IsGoingIn)
                gameMetaState.SetWarpBack(vortexSprite.DestinationSeed, gameState.Seed);

            gameMetaState.SkillLevelForUnknownLevels = gameState.Level.SkillLevel + vortexSprite.IncrementSkillOffset;//Will only be effective if vortexSprite.IncrementSkillOffset != 0

            program.ChangeGameState(vortexSprite.DestinationSeed);
        }

        /// <summary>
        /// Direct collision between player and monster (player will receive damage unless it's a newly kicked helmet)
        /// </summary>
        /// <param name="sprite">player (normally)</param>
        /// <param name="monsterSprite">monster</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        private void UpdateDirectCollision(PlayerSprite sprite, MonsterSprite monsterSprite, Level level, GameState gameState, double timeDelta, SpritePopulation spritePopulation, Random random)
        {
            if (sprite.HitCycle.IsFired || monsterSprite.KickedHelmetCycle.IsFired || sprite.FromVortexCycle.IsFired)
                return;

            if (Physics.IsSpriteInDeadZone(sprite, monsterSprite))
                return;

            if (monsterSprite.IsAlive)
            {
                if (sprite.InvincibilityCycle.IsFired && monsterSprite.IsVulnerableToInvincibility)
                {
                    SoundManager.PlayHitSound();
                    gameState.GameMode.PerformDestroyMonsterExtraLogic(sprite, monsterSprite, level.SkillLevel);
                    monsterSprite.IsAlive = false;
                    monsterSprite.JumpingCycle.Fire();
                }
                else if (monsterSprite.IsCanDoDamageToPlayerWhenTouched)
                {
                    if (!monsterSprite.IsCanDoDamageWhenInFreeFall && (monsterSprite.IsCurrentlyInFreeFallX || monsterSprite.IsCurrentlyInFreeFallY))
                        return;

                    sprite.HitCycle.Fire();

                    if (sprite.IsBeaver)
                    {
                        CollisionRemoveBeaver(sprite, spritePopulation, random);
                    }
                    else
                    {
                        gameState.GameMode.CollisionRemoveSuitOrBecomeSmallOrDie(sprite, monsterSprite);
                    }
                }
            }
        }

        private void UpdateFlailCollision(PlayerSprite playerSprite, FlailBall flailBall, double timeDelta, SpritePopulation spritePopulation, Random random, GameState gameState)
        {
            if (!playerSprite.InvincibilityCycle.IsFired && !playerSprite.HitCycle.IsFired)
            {
                playerSprite.HitCycle.Fire();

                if (playerSprite.IsBeaver)
                {
                    CollisionRemoveBeaver(playerSprite, spritePopulation, random);
                }
                else
                {
                    gameState.GameMode.CollisionRemoveSuitOrBecomeSmallOrDie(playerSprite, flailBall);
                }
            }
        }

        private void CollisionRemoveBeaver(PlayerSprite playerSprite, SpritePopulation spritePopulation, Random random)
        {
            SoundManager.PlayBeaverHitSound();
            playerSprite.CurrentJumpAcceleration = playerSprite.StartingJumpAcceleration;
            playerSprite.IGround = null;
            playerSprite.JumpingCycle.Fire();
            playerSprite.IsBeaver = false;
            BeaverSprite beaverSprite = new BeaverSprite(playerSprite.XPosition, playerSprite.YPosition, random);
            spritePopulation.Add(beaverSprite);
            beaverSprite.IsWalkEnabled = true;
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
        private void UpdateJumpOnSprite(AbstractSprite sprite, AbstractSprite otherSprite, Level level, SpritePopulation spritePopulation, double timeDelta, AbstractGameMode gameMode, Random random)
        {
            if (Physics.IsSpriteInDeadZone(otherSprite, sprite))
                return;

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
                                KickOrStopHelmet(sprite, monsterSprite, level, gameMode, timeDelta);
                            }
                            else 
                            {
                                if (sprite is PlayerSprite && ((PlayerSprite)sprite).InvincibilityCycle.IsFired && monsterSprite.IsVulnerableToInvincibility)
                                {
                                    gameMode.PerformDestroyMonsterExtraLogic((PlayerSprite)sprite, monsterSprite,level.SkillLevel);
                                    monsterSprite.IsAlive = false;
                                    monsterSprite.JumpingCycle.Fire();
                                }
                                else if (otherSprite is MonsterSprite && ((MonsterSprite)otherSprite).IsJumpableOn) //Other sprite (monster) will be damaged
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
        public void KickOrStopHelmet(AbstractSprite sprite, MonsterSprite monsterSprite, Level level, AbstractGameMode gameMode, double timeDelta)
        {
            if (monsterSprite.KickedHelmetCycle.IsFired)
                return;

            if (sprite is PlayerSprite && ((PlayerSprite)sprite).InvincibilityCycle.IsFired)
            {
                SoundManager.PlayHitSound();
                gameMode.PerformDestroyMonsterExtraLogic((PlayerSprite)sprite, monsterSprite, level.SkillLevel);
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

        /// <summary>
        /// Platform is moving up then catches sprite that was on other ground
        /// </summary>
        /// <param name="sprite">sprite (player or monster)</param>
        /// <param name="platform">platform</param>
        private void UpdatePlatformMovesUpCatchSprite(AbstractSprite sprite, Platform platform)
        {
            if (sprite.IGround != null && sprite.IGround != platform)
            {
                if (platform.TopBound < sprite.YPosition && platform.TopBoundPrevious >= sprite.YPosition)
                {
                    sprite.IGround = platform;
                    sprite.YPositionKeepPrevious = platform.TopBound;
                }
            }
        }
        #endregion
    }
}
