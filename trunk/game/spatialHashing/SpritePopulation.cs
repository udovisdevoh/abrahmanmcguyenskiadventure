using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite collection
    /// </summary>
    internal class SpritePopulation
    {
        #region Fields and parts
        private Dictionary<int, Bucket> bucketList = new Dictionary<int,Bucket>();

        private HashSet<AbstractSprite> visibleSpriteList = new HashSet<AbstractSprite>();

        private HashSet<AbstractSprite> __toUpdateSpriteList = new HashSet<AbstractSprite>();
        #endregion

        #region Public Methods
        internal void Add(AbstractSprite sprite)
        {
            sprite.ParentSpriteCollection = this;
            RemoveSpatialHashing(sprite);
            SetSpatialHashing(sprite);
        }

        internal void Remove(AbstractSprite sprite)
        {
            RemoveSpatialHashing(sprite);
            sprite.ParentSpriteCollection = null;
        }

        /// <summary>
        /// Do not use directly
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void SetSpatialHashing(AbstractSprite sprite)
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
        internal void RemoveSpatialHashing(AbstractSprite sprite)
        {
            foreach (Bucket bucket in sprite.ParentBucketList)
                bucket.Remove(sprite);
            sprite.ParentBucketList.Clear();
        }

        internal HashSet<AbstractSprite> GetVisibleSpriteList(double viewOffsetX, double viewOffsetY, out HashSet<AbstractSprite> toUpdateSpriteList)
        {
            int leftMostViewableBucketId = ((int)Math.Floor(viewOffsetX)) / Program.spatialHashingBucketWidth;
            int rightMostViewableBucketId = ((int)Math.Ceiling(viewOffsetX + Program.tileColumnCount)) / Program.spatialHashingBucketWidth;

            visibleSpriteList.Clear();
            if (Program.isBroadRangeUpdateSprite)
                __toUpdateSpriteList.Clear();

            for (int bucketId = leftMostViewableBucketId; bucketId <= rightMostViewableBucketId; bucketId++)
            {
                Bucket bucket = this[bucketId];
                foreach (AbstractSprite sprite in bucket)
                {
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
                    foreach (AbstractSprite sprite in bucket)
                    {
                        toUpdateSpriteList.Add(sprite);
                    }
                }

                for (int bucketId = rightMostViewableBucketId + 1; bucketId <= broadRightBound; bucketId++)
                {
                    Bucket bucket = this[bucketId];
                    foreach (AbstractSprite sprite in bucket)
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
        private int GetLeftMostBucketId(AbstractSprite sprite)
        {
            return ((int)Math.Floor(sprite.XPosition - sprite.Width / 2.0)) / Program.spatialHashingBucketWidth;
        }

        private int GetRightMostBucketId(AbstractSprite sprite)
        {
            return ((int)Math.Ceiling(sprite.XPosition + sprite.Width / 2.0)) / Program.spatialHashingBucketWidth;
        }
        #endregion

        #region Properties
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
        #endregion
    }
}
