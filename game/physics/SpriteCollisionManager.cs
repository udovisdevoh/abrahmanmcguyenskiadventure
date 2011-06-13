﻿using System;
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
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, Program program, GameMetaState gameMetaState, GameState gameState, Random random)
        {
            List<AbstractSprite> sortedVisibleSpriteList = SpriteDistanceSorter.Sort(sprite, visibleSpriteList);

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (sprite == otherSprite || !Physics.IsDetectCollision(sprite, otherSprite))
                    continue;

                if (sprite is PlayerSprite && otherSprite is MushroomSprite && otherSprite.IsAlive)
                {
                    UpdateTouchMushroom((PlayerSprite)sprite, (MushroomSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is PeyoteSprite && otherSprite.IsAlive)
                {
                    UpdateTouchPeyote((PlayerSprite)sprite, (PeyoteSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is WhiskySprite && otherSprite.IsAlive)
                {
                    UpdateTouchWhisky((PlayerSprite)sprite, (WhiskySprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is MusicNoteSprite && otherSprite.IsAlive)
                {
                    UpdateTouchMusicNote((PlayerSprite)sprite, (MusicNoteSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is RastaHatSprite && otherSprite.IsAlive)
                {
                    UpdateTouchRastaHat((PlayerSprite)sprite, (RastaHatSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is VortexSprite && sprite.IsTryToWalkUp && !((PlayerSprite)sprite).FromVortexCycle.IsFired)
                {
                    UpdateGoToVortex((PlayerSprite)sprite, (VortexSprite)otherSprite, program, gameMetaState, gameState);
                }
                else if (otherSprite is StaticSprite && otherSprite.IsImpassable && otherSprite.IsAlive && sprite.IGround == null && !sprite.IsCrossGrounds)
                {
                    UpdateJumpOnBlock(sprite, (StaticSprite)otherSprite, spritePopulation, level, visibleSpriteList, random);
                }
                else if (sprite.IGround == null && sprite.IsAlive && !(sprite is FireBallSprite) && sprite.YPosition < otherSprite.YPosition && (!(otherSprite is MonsterSprite) || ((MonsterSprite)otherSprite).IsJumpableOn)) //Player IS jumping on the monster
                {
                    UpdateJumpOnSprite(sprite, otherSprite, level, spritePopulation, timeDelta, random);
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && ((MonsterSprite)otherSprite).IsToggleWalkWhenJumpedOn && !((MonsterSprite)otherSprite).IsWalkEnabled) //Start/stop (for helmets)
                {
                    KickOrStopHelmet(sprite, (MonsterSprite)otherSprite, level, timeDelta);
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && otherSprite.IsAlive)
                {
                    UpdateDirectCollision((PlayerSprite)sprite, (MonsterSprite)otherSprite, level, timeDelta);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Player jumps towards impassable block. It may output something from it (mushroom, flower, etc)
        /// </summary>
        /// <param name="sprite">jumper</param>
        /// <param name="anarchyBlockSprite">block</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        private void UpdateJumpOnBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, Level level, HashSet<AbstractSprite> visibleSpriteList, Random random)
        {
            double angleFromSpritePreviousPositionToBlock = Physics.GetAngleDegree(sprite.XPositionPrevious, sprite.TopBoundPrevious + block.Height, block.XPosition, block.YPosition);

            if (angleFromSpritePreviousPositionToBlock >= 45 && angleFromSpritePreviousPositionToBlock <= 135)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, level, visibleSpriteList, random);
            }
            else
            {
                UpdateJumpOnBlockSide(sprite, block);
            }
        }

        /// <summary>
        /// Sprite jumps under a block
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="block">block</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of currently visible sprites</param>
        /// <param name="random">random number generator</param>
        private void UpdateJumpUnderBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, Level level, HashSet<AbstractSprite> visibleSpriteList, Random random)
        {
            if (sprite.YPosition < block.YPosition)
                return;

            sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration / -4.0;

                        

            //Only expell the sprite if it doesn't make force the sprite to go lower than the ground
            IGround highestVisibleGroundBelowSprite = IGroundHelper.GetHighestVisibleIGroundBelowSprite(sprite, level, visibleSpriteList);
            if (highestVisibleGroundBelowSprite == null || highestVisibleGroundBelowSprite[sprite.XPosition] > block.YPosition + sprite.Height + 0.01)
                sprite.TopBoundKeepPrevious = block.YPosition + 0.01;


            if (!(sprite is PlayerSprite))
                return;

            sprite.IsNeedToJumpAgain = true;
            AbstractSprite powerUpSprite = null;

            if (block is AnarchyBlockSprite && !((AnarchyBlockSprite)block).IsFinalized)
            {
                ((AnarchyBlockSprite)block).BumpCycle.Fire();
                ((AnarchyBlockSprite)block).IsFinalized = true;

                if (((AnarchyBlockSprite)block).BlockContent == BlockContent.MusicNote)
                {
                    SoundManager.PlayCoinSound();
                }
                else
                {
                    SoundManager.PlayGrowSound();
                    powerUpSprite = ((AnarchyBlockSprite)block).GetPowerUpSprite(sprite, random);
                    if (powerUpSprite is IGrowable)
                        ((IGrowable)powerUpSprite).GrowthCycle.Fire();
                    spritePopulation.Add(powerUpSprite);
                }

                foreach (AbstractSprite spriteStackedOnBlock in visibleSpriteList)
                    if (spriteStackedOnBlock.IGround == block && spriteStackedOnBlock is MonsterSprite && powerUpSprite != spriteStackedOnBlock)
                        UpdateJumpUnderBlockReachSpriteStackedOnBlock(sprite, (MonsterSprite)spriteStackedOnBlock, level, visibleSpriteList, spritePopulation, random);
            }
            else if (block.IsDestructible && block.IsAlive)
            {
                if (sprite.IsTiny)
                {
                    SoundManager.PlayHelmetBumpSound();
                    if (block is IBumpable)
                        ((IBumpable)block).BumpCycle.Fire();

                    foreach (AbstractSprite spriteStackedOnBlock in visibleSpriteList)
                        if (spriteStackedOnBlock.IGround == block && spriteStackedOnBlock is MonsterSprite && powerUpSprite != spriteStackedOnBlock)
                            UpdateJumpUnderBlockReachSpriteStackedOnBlock(sprite, (MonsterSprite)spriteStackedOnBlock, level, visibleSpriteList, spritePopulation, random);
                }
                else
                {
                    SoundManager.PlayBricksSound();
                    block.HitCycle.Fire();
                    block.IsAlive = false;
                    block.IsAffectedByGravity = true;
                }
            }
            else
            {
                SoundManager.PlayHelmetBumpSound();
            }
        }

        /// <summary>
        /// Reach sprite stacked on block by jumping under the block
        /// </summary>
        /// <param name="jumper">jumper</param>
        /// <param name="monsterSprite">reached sprite</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <param name="spritePopulation">all the sprites in the level</param>
        /// <param name="random">random number generator</param>
        private void UpdateJumpUnderBlockReachSpriteStackedOnBlock(AbstractSprite jumper, MonsterSprite monsterSprite, Level level, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, Random random)
        {
            if (monsterSprite.IsEnableJumpOnConversion)
            {
                AbstractSprite jumpedOnConvertedSprite = monsterSprite.GetConverstionSprite(random);
                if (jumpedOnConvertedSprite != null)
                    PerformSpriteConversion(jumper, monsterSprite, jumpedOnConvertedSprite, spritePopulation);
            }
            else 
            {
                monsterSprite.HitCycle.Fire();
                monsterSprite.JumpingCycle.Fire();
                monsterSprite.CurrentJumpAcceleration = monsterSprite.StartingJumpAcceleration;
                monsterSprite.CurrentWalkingSpeed = monsterSprite.WalkingAcceleration;
                monsterSprite.CurrentDamageReceiving = jumper.AttackStrengthCollision;
                monsterSprite.IsNoAiDefaultDirectionWalkingRight = jumper.XPositionPrevious < jumper.XPosition;
            }
        }

        /// <summary>
        /// Spirte jumps on block's side
        /// </summary>
        /// <param name="sprite">jumper sprite</param>
        /// <param name="block">block</param>
        private void UpdateJumpOnBlockSide(AbstractSprite sprite, StaticSprite block)
        {
            //Side collision
            if (sprite.XPosition < block.XPosition)
                sprite.RightBoundKeepPrevious = block.LeftBound;// - 0.1;
            else if (sprite.XPosition > block.XPosition)
                sprite.LeftBoundKeepPrevious = block.RightBound;// + 0.1;
            sprite.CurrentWalkingSpeed = 0;
            sprite.IGround = null;
        }

        /// <summary>
        /// Player touches mushroom and get health
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="mushroomSprite">mushroom</param>
        private void UpdateTouchMushroom(PlayerSprite playerSprite, MushroomSprite mushroomSprite)
        {
            SoundManager.PlayPowerUpSound();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            else
                playerSprite.PowerUpAnimationCycle.Fire();
            playerSprite.Health -= mushroomSprite.AttackStrengthCollision;
            playerSprite.IsTiny = false;
            mushroomSprite.IsAlive = false;
            mushroomSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Sprite touches peyote
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="peyoteSprite">peyote sprite</param>
        private void UpdateTouchPeyote(PlayerSprite playerSprite, PeyoteSprite peyoteSprite)
        {
            SoundManager.PlayPowerUpSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            playerSprite.IsTiny = false;
            playerSprite.IsRasta = false;
            playerSprite.IsDoped = true;
            peyoteSprite.IsAlive = false;
            peyoteSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Touch rasta hat
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        /// <param name="rastaHatSprite">rasta hat</param>
        private void UpdateTouchRastaHat(PlayerSprite playerSprite, RastaHatSprite rastaHatSprite)
        {
            SoundManager.PlayReggaeSound();
            playerSprite.PowerUpAnimationCycle.Fire();
            if (playerSprite.IsTiny)
                playerSprite.ChangingSizeAnimationCycle.Fire();
            playerSprite.IsTiny = false;
            playerSprite.IsDoped = false;
            playerSprite.IsRasta = true;
            rastaHatSprite.IsAlive = false;
            rastaHatSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Sprite touches music note
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="musicNoteSprite">music note</param>
        private void UpdateTouchMusicNote(PlayerSprite playerSprite, MusicNoteSprite musicNoteSprite)
        {
            SoundManager.PlayCoinSound();
            musicNoteSprite.IsAlive = false;
            musicNoteSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

        /// <summary>
        /// Sprite touches whisky and becomes invincible
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="whiskySprite">whisky</param>
        private void UpdateTouchWhisky(PlayerSprite playerSprite, WhiskySprite whiskySprite)
        {
            SoundManager.PlayDrinkSound();
            playerSprite.InvincibilityCycle.Fire();
            playerSprite.HitCycle.StopAndReset();
            whiskySprite.IsAlive = false;
            whiskySprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
        }

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
        private void UpdateDirectCollision(PlayerSprite sprite, MonsterSprite monsterSprite, Level level, double timeDelta)
        {
            if (sprite.HitCycle.IsFired || monsterSprite.KickedHelmetCycle.IsFired || sprite.FromVortexCycle.IsFired)
                return;

            if (sprite.InvincibilityCycle.IsFired)
            {
                SoundManager.PlayHitSound();
                monsterSprite.IsAlive = false;
                monsterSprite.JumpingCycle.Fire();
            }
            else if (monsterSprite.IsCanDoDamageToPlayerWhenTouched)
            {
                SoundManager.PlayHit2Sound();
                sprite.HitCycle.Fire();
                if (sprite is PlayerSprite && !sprite.IsTiny)
                    ((PlayerSprite)sprite).ChangingSizeAnimationCycle.Fire();

                if (sprite.IsDoped)
                    sprite.IsDoped = false;
                if (sprite.IsRasta)
                    sprite.IsRasta = false;
                sprite.IsTiny = true;

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
                            else 
                            {
                                if (sprite is PlayerSprite && ((PlayerSprite)sprite).InvincibilityCycle.IsFired)
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
