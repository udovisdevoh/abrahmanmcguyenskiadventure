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
        #endregion

        #region Private Methods
        private int GetLeftMostBucketId(AbstractSprite sprite)
        {
            return (int)Math.Floor(sprite.XPosition - sprite.Width / 2.0);
        }

        private int GetRightMostBucketId(AbstractSprite sprite)
        {
            return (int)Math.Ceiling(sprite.XPosition + sprite.Width / 2.0);
        }
        #endregion
    }
}
