using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    enum CloudSidePosition { Right, Left, Bilateral }

    /// <summary>
    /// To dispatch blocks that are only reachable by climbing a vine
    /// </summary>
    internal static class CloudDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch blocks that can only be accessed by climbing a vine
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">all sprites</param>
        /// <param name="addedBlockMemory">memory of added blocks</param>
        /// <param name="blocksContainingVine">blocks that contain vines</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchBlocks(Level level, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, Random random)
        {
            List<AnarchyBlockSprite> blocksContainingVine = GetBlocksThatContainVine(spritePopulation);

            if (blocksContainingVine.Count == 0)
                return;

            AbstractWave yDistaceFromVineTopWave = BlockDispatcherWave.BuildBlockYDistanceFromGroundWave(random);

            double cloudHeightOffset = (double)random.Next(-1, 7);

            foreach (AnarchyBlockSprite block in blocksContainingVine)
            {
                CloudSidePosition cloudSidePosition = (CloudSidePosition)random.Next(0, 3);

                int minSegmentWidth = random.Next(1, 7);
                int maxSegmentWidth = Math.Max(minSegmentWidth, random.Next(7, 20));
                double musicNoteYDistance = (double)random.Next(1, 5);

                double absoluteVineHeigth = block.YPosition - block.VineHeight;

                Ground groundBelowVineTop = (Ground)IGroundHelper.GetHighestVisibleIGroundBelowSprite(block, level, null, false);

                if (groundBelowVineTop == null)
                    continue;

                if (cloudSidePosition == CloudSidePosition.Right || cloudSidePosition == CloudSidePosition.Bilateral)
                    DispatchCloudsOnSideOfVine(true, musicNoteYDistance, level, block.XPosition, absoluteVineHeigth + cloudHeightOffset, spritePopulation, addedBlockMemory, yDistaceFromVineTopWave, groundBelowVineTop, minSegmentWidth, maxSegmentWidth, random);
                
                if (cloudSidePosition == CloudSidePosition.Left || cloudSidePosition == CloudSidePosition.Bilateral)
                    DispatchCloudsOnSideOfVine(false, musicNoteYDistance, level, block.XPosition, absoluteVineHeigth + cloudHeightOffset, spritePopulation, addedBlockMemory, yDistaceFromVineTopWave, groundBelowVineTop, minSegmentWidth, maxSegmentWidth, random);
            }

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Dispatch clouds on side of vine
        /// </summary>
        /// <param name="isOnRightSide">whether it is right side</param>
        /// <param name="level">level</param>
        /// <param name="vineX">x coordinate of vine</param>
        /// <param name="absoluteVineHeigth">height of the vine (absolute)</param>
        /// <param name="spritePopulation"></param>
        /// <param name="addedBlockMemory"></param>
        /// <param name="yDistaceFromVineTopWave"></param>
        /// <param name="groundBelowVineTop">ground below vine's top</param>
        /// <param name="random">random number generator</param>
        private static void DispatchCloudsOnSideOfVine(bool isOnRightSide, double musicNoteYDistance, Level level, double vineX, double absoluteVineHeigthPlusCloudHeightOffset, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, AbstractWave yDistaceFromVineTopWave, Ground groundBelowVineTop, int minSegmentWidth, int maxSegmentWidth, Random random)
        {
            double xDistanceFromVine = (double)random.Next(0, 5);
            double width = (double)random.Next(7, 32);
            double segmentWidth = 0;
            double y = 0;

            double startX, incrementationX;

            if (isOnRightSide)
            {
                startX = vineX + 1 + xDistanceFromVine;
                incrementationX = 1;
                
            }
            else
            {
                startX = vineX - 1 - xDistanceFromVine;
                incrementationX = -1;
            }

            for (double x = startX; (isOnRightSide && x <= vineX + xDistanceFromVine + width) || (!isOnRightSide && x >= vineX - xDistanceFromVine - width); x += incrementationX)
            {
                if (segmentWidth == 0)
                {
                    y = Math.Round(yDistaceFromVineTopWave[x] + absoluteVineHeigthPlusCloudHeightOffset);
                    segmentWidth = (double)random.Next(minSegmentWidth, maxSegmentWidth);
                }

                TryDispatchSingleCloud(level, x, y, musicNoteYDistance, spritePopulation, addedBlockMemory, groundBelowVineTop, random);

                segmentWidth--;
            }
        }

        /// <summary>
        /// Dispatch signle cloud sprite
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="spritePopulation">all sprites</param>
        /// <param name="addedBlockMemory">added blocks</param>
        /// <param name="yDistaceFromVineTopWave">shape of the cloud set</param>
        /// <param name="random">random number generator</param>
        private static void TryDispatchSingleCloud(Level level, double x, double y, double musicNoteYDistance, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, Ground groundBelowVineTop, Random random)
        {
            bool isCouldAdd = true;

            if (addedBlockMemory.Contains((int)x, (int)y))
                return;
            else if (BlockDispatcher.IsHigherThanHigherGroundThan(x, y - 1.0, groundBelowVineTop, level))
                return;
            else if (level.Ceiling != null && y - 1 <= level.Ceiling[x])
                return;
            else if (level.HoleSet[x, y])
                return;

            CloudSprite cloudSprite = new CloudSprite(x, y, random);

            spritePopulation.Add(cloudSprite);

            IGround groundBelowBlock = IGroundHelper.GetHighestVisibleIGroundBelowSprite(cloudSprite, level, null, false);

            if (groundBelowBlock == null || groundBelowBlock[x] - cloudSprite.YPosition < Program.maxCloudHeightFromGround)
                isCouldAdd = false;
            else if (!IGroundHelper.IsHigherThanOtherGrounds((Ground)groundBelowBlock, level, x))
                return;

            if (isCouldAdd)
            {
                addedBlockMemory.Add((int)x, (int)y);

                if (random.Next(0, 3) == 1)
                    TryDispatchPowerup(level, x, y, musicNoteYDistance, spritePopulation, addedBlockMemory, random);
            }
            else
                spritePopulation.Remove(cloudSprite);
        }

        private static void TryDispatchPowerup(Level level, double x, double y, double musicNoteYDistance, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, Random random)
        {
            if (random.Next(0, 8) == 1)
            {
                double bonusYDistance = (double)random.Next(3, 6);
                if (level.Ceiling != null && y - bonusYDistance < level.Ceiling[x])
                    return;

                BlockContent blockContent = (BlockContent)random.Next(1, 5);
                AnarchyBlockSprite anarchyBlock = new AnarchyBlockSprite(x, y - bonusYDistance, random, blockContent, false);
                spritePopulation.Add(anarchyBlock);
            }
            else
            {
                if (level.Ceiling != null && y - musicNoteYDistance < level.Ceiling[x])
                    return;

                MusicNoteSprite musicNoteSprite = new MusicNoteSprite(x, y - musicNoteYDistance, random);
                spritePopulation.Add(musicNoteSprite);
            }
        }

        /// <summary>
        /// Get blocks that contain vines
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns>blocks that contain vines</returns>
        private static List<AnarchyBlockSprite> GetBlocksThatContainVine(SpritePopulation spritePopulation)
        {
            List<AnarchyBlockSprite> blockList = new List<AnarchyBlockSprite>();
            foreach (SideScrollerSprite sprite in spritePopulation.AllSpriteList)
            {
                if (sprite is AnarchyBlockSprite)
                {
                    AnarchyBlockSprite blockSprite = (AnarchyBlockSprite)sprite;
                    if (blockSprite.BlockContent == BlockContent.Vine)
                    {
                        blockList.Add(blockSprite);
                    }
                }
            }
            return blockList;
        }
        #endregion
    }
}
