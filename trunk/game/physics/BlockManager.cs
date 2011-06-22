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
    /// Manages blocks (open, close, break etc), collisions etc
    /// </summary>
    internal class BlockManager
    {
        #region Fields and parts
        /// <summary>
        /// To convert sprites
        /// </summary>
        private SpriteConversionManager spriteConverter = new SpriteConversionManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Try to open a block
        /// </summary>
        /// <param name="sprite">opener</param>
        /// <param name="block">block</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <param name="level">level</param>
        /// <param name="random">random number generator</param>
        internal void TryOpenOrBreakBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList, Level level, Random random)
        {
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
        /// Sprite jumps under a block
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="block">block</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">list of currently visible sprites</param>
        /// <param name="random">random number generator</param>
        internal void UpdateJumpUnderBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, Level level, HashSet<AbstractSprite> visibleSpriteList, Random random)
        {
            if (sprite.YPosition < block.YPosition)
                return;

            sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration / -4.0f;

            //Only expell the sprite if it doesn't make force the sprite to go lower than the ground
            IGround highestVisibleGroundBelowSprite = IGroundHelper.GetHighestVisibleIGroundBelowSprite(sprite, level, visibleSpriteList);
            if (highestVisibleGroundBelowSprite == null || highestVisibleGroundBelowSprite[sprite.XPosition] > block.YPosition + sprite.Height + 0.01)
                sprite.TopBoundKeepPrevious = block.YPosition + 0.01f;

            if (!(sprite is PlayerSprite) && !(sprite is HelmetSprite))
                return;

            sprite.IsNeedToJumpAgain = true;
            TryOpenOrBreakBlock(sprite, block, spritePopulation, visibleSpriteList, level, random);
        }

        /// <summary>
        /// Player jumps towards impassable block. It may output something from it (mushroom, flower, etc)
        /// </summary>
        /// <param name="sprite">jumper</param>
        /// <param name="anarchyBlockSprite">block</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal void UpdateJumpOnBlock(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, Level level, HashSet<AbstractSprite> visibleSpriteList, Random random)
        {
            float angleFromSpritePreviousPositionToBlock = Physics.GetAngleDegree(sprite.XPositionPrevious, sprite.TopBoundPrevious + block.Height, block.XPosition, block.YPosition);
            sprite.IsCurrentlyInFreeFallX = false;
            sprite.IsCurrentlyInFreeFallY = false;
            if (angleFromSpritePreviousPositionToBlock >= 45 && angleFromSpritePreviousPositionToBlock <= 135)
            {
                UpdateJumpUnderBlock(sprite, block, spritePopulation, level, visibleSpriteList, random);
            }
            else
            {
                ManageBlockSideCollision(sprite, block, spritePopulation, visibleSpriteList, level, random);
            }
        }
        #endregion

        #region Private Methods
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
                    spriteConverter.PerformSpriteConversion(jumper, monsterSprite, jumpedOnConvertedSprite, spritePopulation);
            }
            else
            {
                monsterSprite.HitCycle.Fire();
                monsterSprite.JumpingCycle.Fire();
                monsterSprite.CurrentJumpAcceleration = monsterSprite.StartingJumpAcceleration;
                monsterSprite.CurrentWalkingSpeed = monsterSprite.WalkingAcceleration;
                monsterSprite.CurrentDamageReceiving = jumper.AttackStrengthCollision;
                monsterSprite.IsNoAiDefaultDirectionWalkingRight = jumper.XPositionPrevious < jumper.XPosition;
                monsterSprite.IGround = null;
            }
        }

        /// <summary>
        /// Spirte jumps on block's side
        /// </summary>
        /// <param name="sprite">jumper sprite</param>
        /// <param name="block">block</param>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="visibleSpriteList">list of visible sprites</param>
        /// <param name="random">random number generator</param>
        private void ManageBlockSideCollision(AbstractSprite sprite, StaticSprite block, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList, Level level, Random random)
        {
            //Side collision
            if (sprite.XPosition < block.XPosition)
                sprite.RightBoundKeepPrevious = block.LeftBound;// - 0.1;
            else if (sprite.XPosition > block.XPosition)
                sprite.LeftBoundKeepPrevious = block.RightBound;// + 0.1;
            sprite.CurrentWalkingSpeed = 0;
            sprite.IGround = null;

            if (sprite is HelmetSprite)
                TryOpenOrBreakBlock(sprite, block, spritePopulation, visibleSpriteList, level, random);
        }
        #endregion
    }
}
