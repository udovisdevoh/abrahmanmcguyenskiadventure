using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    enum CloudSidePosition { Right, Left, Bilateral, None }

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

            double cloudHeightOffset = (double)random.Next(-2, 7);

            foreach (AnarchyBlockSprite block in blocksContainingVine)
            {
                CloudSidePosition cloudSidePosition = (CloudSidePosition)random.Next(0, 4);
                
                #warning Remove forced bilateral
                cloudSidePosition = CloudSidePosition.Bilateral;

                int minSegmentWidth = random.Next(1, 7);
                int maxSegmentWidth = Math.Max(minSegmentWidth, random.Next(7, 20));

                double absoluteVineHeigth = block.YPosition - block.VineHeight;

                Ground groundBelowVineTop = (Ground)IGroundHelper.GetHighestVisibleIGroundBelowSprite(block, level, null, false);

                if (groundBelowVineTop == null)
                    continue;

                if (cloudSidePosition == CloudSidePosition.Right || cloudSidePosition == CloudSidePosition.Bilateral)
                    DispatchCloudsOnSideOfVine(true, level, block.XPosition, absoluteVineHeigth + cloudHeightOffset, spritePopulation, addedBlockMemory, yDistaceFromVineTopWave, groundBelowVineTop, minSegmentWidth, maxSegmentWidth, random);
                
                if (cloudSidePosition == CloudSidePosition.Left || cloudSidePosition == CloudSidePosition.Bilateral)
                    DispatchCloudsOnSideOfVine(false, level, block.XPosition, absoluteVineHeigth + cloudHeightOffset, spritePopulation, addedBlockMemory, yDistaceFromVineTopWave, groundBelowVineTop, minSegmentWidth, maxSegmentWidth, random);
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
        private static void DispatchCloudsOnSideOfVine(bool isOnRightSide, Level level, double vineX, double absoluteVineHeigthPlusCloudHeightOffset, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, AbstractWave yDistaceFromVineTopWave, Ground groundBelowVineTop, int minSegmentWidth, int maxSegmentWidth, Random random)
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

                y = Math.Round(yDistaceFromVineTopWave[x] + absoluteVineHeigthPlusCloudHeightOffset);
                TryDispatchSingleCloud(level, x, y, spritePopulation, addedBlockMemory, groundBelowVineTop, random);

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
        private static void TryDispatchSingleCloud(Level level, double x, double y, SpritePopulation spritePopulation, AddedBlockMemory addedBlockMemory, Ground groundBelowVineTop, Random random)
        {
            bool isCouldAdd = true;

            if (addedBlockMemory.Contains((int)x, (int)y))
                return;
            else if (BlockDispatcher.IsHigherThanHigherGroundThan(x, y - 1.0, groundBelowVineTop, level))
                return;
            else if (level.Ceiling != null && y - 1 <= level.Ceiling[x])
                return;

            StaticSprite blockSprite;
            /*if (random.NextDouble() < BlockDispatcher.anarchyBlockProbability)
                blockSprite = new AnarchyBlockSprite(x, y, random, false);
            else if (random.NextDouble() < BlockDispatcher.hiddenAnarchyBlockProbability)
                blockSprite = new AnarchyBlockSprite(x, y, random, true);
            else if (random.NextDouble() < BlockDispatcher.indestructibleBlockProbability)*/
                blockSprite = new BrickSprite(x, y, random, false);
            /*else
                blockSprite = new BrickSprite(x, y, random, true);*/

            spritePopulation.Add(blockSprite);

            IGround groundBelowBlock = IGroundHelper.GetHighestVisibleIGroundBelowSprite(blockSprite, level, null, false);

            if (groundBelowBlock == null || groundBelowBlock[x] - blockSprite.YPosition < Program.maxCloudHeightFromGround)
                isCouldAdd = false;

            if (isCouldAdd)
                addedBlockMemory.Add((int)x, (int)y);
            else
                spritePopulation.Remove(blockSprite);
        }

        /// <summary>
        /// Get blocks that contain vines
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns>blocks that contain vines</returns>
        private static List<AnarchyBlockSprite> GetBlocksThatContainVine(SpritePopulation spritePopulation)
        {
            List<AnarchyBlockSprite> blockList = new List<AnarchyBlockSprite>();
            foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
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
