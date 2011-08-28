using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// All the sprites in a level
    /// </summary>
    internal class SpritePopulation
    {
        #region Fields and parts
        /// <summary>
        /// Bucket list
        /// </summary>
        private Dictionary<int, Bucket> bucketList = new Dictionary<int,Bucket>();

        /// <summary>
        /// List of currently visible sprites
        /// </summary>
        private HashSet<SideScrollerSprite> visibleSpriteList = new HashSet<SideScrollerSprite>();

        /// <summary>
        /// List of currently visible sprites + some other sprites that are not visible yet but close
        /// </summary>
        private HashSet<SideScrollerSprite> __toUpdateSpriteList = new HashSet<SideScrollerSprite>();

        /// <summary>
        /// List of all the sprites
        /// </summary>
        private HashSet<SideScrollerSprite> __allSpriteList = new HashSet<SideScrollerSprite>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Add sprite to population
        /// </summary>
        /// <param name="sprite">sprite to add</param>
        internal void Add(SideScrollerSprite sprite)
        {
            sprite.ParentSpriteCollection = this;
            RemoveSpatialHashing(sprite);
            SetSpatialHashing(sprite);
        }

        /// <summary>
        /// Remove sprite from population
        /// </summary>
        /// <param name="sprite">sprite to remove</param>
        internal void Remove(SideScrollerSprite sprite)
        {
            RemoveSpatialHashing(sprite);
            sprite.ParentSpriteCollection = null;
        }

        /// <summary>
        /// Do not use directly
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void SetSpatialHashing(SideScrollerSprite sprite)
        {
            int leftMostBucketId = GetLeftMostBucketId(sprite);
            int rightMostBucketId = GetRightMostBucketId(sprite);

            for (int bucketId = leftMostBucketId; bucketId <= rightMostBucketId; bucketId++)
            {
                Bucket bucket;
                if (!bucketList.TryGetValue(bucketId, out bucket))
                {
                    bucket = new Bucket();
                    bucketList.Add(bucketId, bucket);
                }
                bucket.Add(sprite);
                sprite.ParentBucketList.Add(bucket);
            }
        }

        /// <summary>
        /// Do not use directly
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void RemoveSpatialHashing(SideScrollerSprite sprite)
        {
            foreach (Bucket bucket in sprite.ParentBucketList)
                bucket.Remove(sprite);
            sprite.ParentBucketList.Clear();
        }

        /// <summary>
        /// Get list of currently visible sprites
        /// </summary>
        /// <param name="viewOffsetX">view offset on X coordinates</param>
        /// <param name="viewOffsetY">view offset on Y coordinates</param>
        /// <param name="toUpdateSpriteList">List of currently visible sprites + some other sprites that are not visible yet but close</param>
        /// <returns>List of currently visible sprites</returns>
        internal HashSet<SideScrollerSprite> GetVisibleSpriteList(double viewOffsetX, double viewOffsetY, out HashSet<SideScrollerSprite> toUpdateSpriteList)
        {
            int leftMostViewableBucketId = ((int)Math.Floor(viewOffsetX)) / Program.spatialHashingBucketWidth;
            int rightMostViewableBucketId = ((int)Math.Ceiling(viewOffsetX + Program.tileColumnCount)) / Program.spatialHashingBucketWidth;

            visibleSpriteList.Clear();
            if (Program.isBroadRangeUpdateSprite)
                __toUpdateSpriteList.Clear();

            for (int bucketId = leftMostViewableBucketId; bucketId <= rightMostViewableBucketId; bucketId++)
            {
                Bucket bucket = this[bucketId];
                foreach (SideScrollerSprite sprite in bucket)
                {
                    if (sprite.YPosition >= viewOffsetY && sprite.YPosition <= viewOffsetY + (double)Program.tileRowCount + sprite.Height + 2.0)
                        visibleSpriteList.Add(sprite);
                    if (Program.isBroadRangeUpdateSprite)
                        __toUpdateSpriteList.Add(sprite);
                }
            }

            if (Program.isBroadRangeUpdateSprite)
            {
                #region We add buckets out of the screen (-1 screen to +1 screen to toUpdateSpriteList
                toUpdateSpriteList = __toUpdateSpriteList;

                int broadLeftBound = leftMostViewableBucketId - Program.tileColumnCount;
                int broadRightBound = rightMostViewableBucketId + Program.tileColumnCount;

                for (int bucketId = broadLeftBound; bucketId < leftMostViewableBucketId; bucketId++)
                {
                    Bucket bucket = this[bucketId];
                    foreach (SideScrollerSprite sprite in bucket)
                    {
                        toUpdateSpriteList.Add(sprite);
                    }
                }

                for (int bucketId = rightMostViewableBucketId + 1; bucketId <= broadRightBound; bucketId++)
                {
                    Bucket bucket = this[bucketId];
                    foreach (SideScrollerSprite sprite in bucket)
                    {
                        toUpdateSpriteList.Add(sprite);
                    }
                }
                #endregion
            }
            else
            {
                toUpdateSpriteList = visibleSpriteList;
            }

            return visibleSpriteList;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get index of bucket at the leftmost for buckets that will contain sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <returns>index of bucket at the leftmost for buckets that will contain sprite</returns>
        private int GetLeftMostBucketId(SideScrollerSprite sprite)
        {
            return ((int)Math.Floor(sprite.XPosition - sprite.Width / 2.0)) / Program.spatialHashingBucketWidth;
        }

        /// <summary>
        /// Get index of bucket at the rightmost for buckets that will contain sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <returns>index of bucket at the rightmost for buckets that will contain sprite</returns>
        private int GetRightMostBucketId(SideScrollerSprite sprite)
        {
            return ((int)Math.Ceiling(sprite.XPosition + sprite.Width / 2.0)) / Program.spatialHashingBucketWidth;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get bucket at index
        /// </summary>
        /// <param name="bucketId">index</param>
        /// <returns>bucket at index</returns>
        public Bucket this[int bucketId]
        {
            get
            {
                Bucket bucket;
                if (!bucketList.TryGetValue(bucketId, out bucket))
                {
                    bucket = new Bucket();
                    bucketList.Add(bucketId, bucket);
                }
                return bucket;
            }
        }

        public HashSet<SideScrollerSprite> AllSpriteList
        {
            get
            {
                __allSpriteList.Clear();
                foreach (Bucket bucket in bucketList.Values)
                    foreach (SideScrollerSprite sprite in bucket)
                        __allSpriteList.Add(sprite);
                return __allSpriteList;
            }
        }
        #endregion
    }
}
