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
                if (sprite == otherSprite || !Physics.IsDetectCollision(sprite, otherSprite))
                    continue;

                if (sprite is PlayerSprite && otherSprite is MushroomSprite && otherSprite.IsAlive)
                {
                    UpdateTouchMushroom((PlayerSprite)sprite, (MushroomSprite)otherSprite);
                }
                else if (sprite is PlayerSprite && otherSprite is ShishaSprite && otherSprite.IsAlive)
                {
                    UpdateTouchShisha((PlayerSprite)sprite, (ShishaSprite)otherSprite);
                }
                else if (otherSprite is StaticSprite && otherSprite.IsImpassable && otherSprite.IsAlive && sprite.IGround == null)
                {
                    UpdateJumpOnBlock(sprite, (StaticSprite)otherSprite, spritePopulation,random);
                }
                else if (sprite.IGround == null && sprite.YPosition < otherSprite.YPosition) //Player IS jumping on the monster
                {
                    UpdateJumpOnSprite(sprite, otherSprite, level, spritePopulation, timeDelta, random);
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && ((MonsterSprite)otherSprite).IsToggleWalkWhenJumpedOn && !((MonsterSprite)otherSprite).IsWalkEnabled) //Start/stop (for helmets)
                {
                    KickOrStopHelmet(sprite, (MonsterSprite)otherSprite, level, timeDelta);
                }
                else if (sprite is PlayerSprite && otherSprite is MonsterSprite && otherSprite.IsAlive)
                {
                    UpdateDirectCollision(sprite, (MonsterSprite)otherSprite, level, timeDelta);
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
        private void UpdateJumpOnBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, Random random)
        {
            /*bool wasInHorizontalCollision = sprite.RightBoundPrevious >= block.LeftBound && sprite.LeftBoundPrevious <= block.LeftBound;
            wasInHorizontalCollision |= sprite.LeftBoundPrevious <= block.RightBound && sprite.RightBoundPrevious >= block.RightBound;
            wasInHorizontalCollision |= sprite.LeftBoundPrevious == block.LeftBound;
            wasInHorizontalCollision |= sprite.RightBoundPrevious == block.RightBound;
            wasInHorizontalCollision |= sprite.RightBoundPrevious < block.RightBound && sprite.LeftBoundPrevious > block.LeftBound;
            wasInHorizontalCollision |= sprite.RightBoundPrevious > block.RightBound && sprite.LeftBoundPrevious < block.LeftBound;

            bool isInHorizontalCollision = sprite.RightBound >= block.LeftBound && sprite.LeftBound <= block.LeftBound;
            isInHorizontalCollision |= sprite.LeftBound <= block.RightBound && sprite.RightBound >= block.RightBound;
            isInHorizontalCollision |= sprite.LeftBound == block.LeftBound;
            isInHorizontalCollision |= sprite.RightBound == block.RightBound;
            isInHorizontalCollision |= sprite.RightBound < block.RightBound && sprite.LeftBound > block.LeftBound;
            isInHorizontalCollision |= sprite.RightBound > block.RightBound && sprite.LeftBound < block.LeftBound;*/

            double angleFromSpritePreviousPositionToBlock = Physics.GetAngleDegree(sprite.XPositionPrevious, sprite.TopBoundPrevious + block.Height, block.XPosition, block.YPosition);

            if (angleFromSpritePreviousPositionToBlock >= 45 && angleFromSpritePreviousPositionToBlock <= 135)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, random);
            }
            else
            {
                UpdateJumpOnBlockSide(sprite, block);
            }


            /*if (wasInHorizontalCollision == isInHorizontalCollision && sprite.YPosition < sprite.YPositionPrevious)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, random);
            }
            else if (sprite.XPosition > block.LeftBound + 0.025 && sprite.XPosition < block.RightBound - 0.025 && sprite.TopBound > block.YPosition - 0.125)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, random);
            }
            else if (sprite.XPosition > block.LeftBound + 0.05 && sprite.XPosition < block.RightBound - 0.05 && sprite.TopBound > block.YPosition - 0.25)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, random);
            }
            else if (sprite.XPosition > block.LeftBound + 0.1 && sprite.XPosition < block.RightBound - 0.1 && sprite.TopBound > block.YPosition - 0.5)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, random);
            }
            else
            {
                UpdateJumpOnBlockSide(sprite, block);
            }*/
            
            /*if (Math.Abs(sprite.LastDistanceX) > Math.Abs(sprite.LastDistanceY))
            {
                UpdateJumpOnBlockSide(sprite, block);
            }
            else
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, random);
            }*/
        }

        private void UpdateJumpUnderBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, Random random)
        {
            if (sprite.YPosition < block.YPosition)
                return;

            sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration / -4.0;

            //sprite.YPositionKeepPrevious += 0.01;

            //sprite.TopBoundKeepPrevious = block.YPosition + 0.1;

            if (!(sprite is PlayerSprite))
                return;

            sprite.IsNeedToJumpAgain = true;

            //Must be well centered, or else, don't open the block
            if (Math.Abs(sprite.XPosition - block.XPosition) > block.Width / 2.0)
                return;

            if (block is AnarchyBlockSprite && !((AnarchyBlockSprite)block).IsFinalized)
            {
                SoundManager.PlayGrowSound();
                ((AnarchyBlockSprite)block).BumpCycle.Fire();
                ((AnarchyBlockSprite)block).IsFinalized = true;

                AbstractSprite powerUpSprite = ((AnarchyBlockSprite)block).GetPowerUpSprite(sprite, random);
                if (powerUpSprite is IGrowable)
                    ((IGrowable)powerUpSprite).GrowthCycle.Fire();
                spritePopulation.Add(powerUpSprite);
            }
            else if (block.IsDestructible && block.IsAlive)
            {
                SoundManager.PlayBricksSound();
                block.HitCycle.Fire();
                block.IsAlive = false;
                block.IsAffectedByGravity = true;
            }
            else
            {
                SoundManager.PlayHelmetBumpSound();
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
                sprite.RightBoundKeepPrevious = block.LeftBound - 0.1;
            else if (sprite.XPosition > block.XPosition)
                sprite.LeftBoundKeepPrevious = block.RightBound + 0.1;
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
            if (sprite.HitCycle.IsFired || monsterSprite.KickedHelmetCycle.IsFired)
                return;

            SoundManager.PlayHit2Sound();
            sprite.HitCycle.Fire();
            if (sprite is PlayerSprite)
                ((PlayerSprite)sprite).IsDoped = false;
            sprite.CurrentDamageReceiving = monsterSprite.AttackStrengthCollision;
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
