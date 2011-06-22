using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Sorts sprites by distance
    /// </summary>
    internal static class SpriteDistanceSorter
    {
        #region Fields and parts
        /// <summary>
        /// List of sprites sorted by distance to last used sprite
        /// </summary>
        private static List<AbstractSprite> __sortedListSprite = new List<AbstractSprite>();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get list of visible sprites sorted by distance from sprite (closest to farthest)
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="unsortedSpriteList">unsorted list of sprites</param>
        /// <returns></returns>
        internal static List<AbstractSprite> Sort(AbstractSprite sprite, HashSet<AbstractSprite> unsortedSpriteList)
        {
            __sortedListSprite.Clear();

            foreach (AbstractSprite otherSprite in unsortedSpriteList)
            {
                otherSprite.DistanceToReferenceSprite = GetHorizontalDistance(sprite, otherSprite);
                __sortedListSprite.Add(otherSprite);
            }
            __sortedListSprite.Sort();

            return __sortedListSprite;
        }
        #endregion

        #region Private Methods
        private static float GetHorizontalDistance(AbstractSprite sprite, AbstractSprite otherSprite)
        {
            return Math.Abs(sprite.XPosition - otherSprite.XPosition);
        }

        private static float GetApproximateDistance(AbstractSprite sprite, AbstractSprite otherSprite)
        {
            return Math.Max(Math.Abs(sprite.XPosition - otherSprite.XPosition), Math.Abs(sprite.YPosition - otherSprite.YPosition));
        }

        private static int GetExactDistance(AbstractSprite sprite, AbstractSprite otherSprite)
        {
            return (int)(Math.Sqrt(Math.Pow(sprite.XPosition - otherSprite.XPosition, 2.0) + Math.Pow(sprite.YPosition - otherSprite.YPosition, 2.0)) * 32.0);
        }
        #endregion
    }
}
