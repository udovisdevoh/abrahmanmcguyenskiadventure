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
        private static List<SideScrollerSprite> __sortedListSprite = new List<SideScrollerSprite>();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get list of visible sprites sorted by distance from sprite (closest to farthest)
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="unsortedSpriteList">unsorted list of sprites</param>
        /// <returns>sorted (by distance to sprite) list of sprites</returns>
        internal static List<SideScrollerSprite> SortByDistanceToSprite(SideScrollerSprite sprite, HashSet<SideScrollerSprite> unsortedSpriteList)
        {
            __sortedListSprite.Clear();

            foreach (SideScrollerSprite otherSprite in unsortedSpriteList)
            {
                otherSprite.SortingIndex = (int)(GetHorizontalDistance(sprite, otherSprite) * 32.0);
                __sortedListSprite.Add(otherSprite);
            }
            __sortedListSprite.Sort();

            return __sortedListSprite;
        }

        /// <summary>
        /// Sort sprites by ZIndex (big numbers: frontmost)
        /// </summary>
        /// <param name="unsortedSpriteList">unsorted list of sprites</param>
        /// <returns>sorted (by ZIndex) list of sprites</returns>
        internal static List<SideScrollerSprite> SortByZIndex(HashSet<SideScrollerSprite> unsortedSpriteList)
        {
            __sortedListSprite.Clear();

            foreach (SideScrollerSprite otherSprite in unsortedSpriteList)
            {
                otherSprite.SortingIndex = otherSprite.ZIndex;
                __sortedListSprite.Add(otherSprite);
            }
            __sortedListSprite.Sort();

            return __sortedListSprite;
        }

        /// <summary>
        /// Exact distance between sprites
        /// </summary>
        /// <param name="sprite">sprite 1</param>
        /// <param name="otherSprite">sprite 2</param>
        /// <returns>Exact distance between sprites</returns>
        internal static int GetExactDistancePixel(SideScrollerSprite sprite, SideScrollerSprite otherSprite)
        {
            return (int)(Math.Sqrt(Math.Pow(sprite.XPosition - otherSprite.XPosition, 2.0) + Math.Pow(sprite.YPosition - otherSprite.YPosition, 2.0)) * 32.0);
        }

        /// <summary>
        /// Exact distance between sprites
        /// </summary>
        /// <param name="sprite">sprite 1</param>
        /// <param name="otherSprite">sprite 2</param>
        /// <returns>Exact distance between sprites</returns>
        internal static double GetExactDistanceTile(SideScrollerSprite sprite, SideScrollerSprite otherSprite)
        {
            return Math.Sqrt(Math.Pow(sprite.XPosition - otherSprite.XPosition, 2.0) + Math.Pow(sprite.YPosition - otherSprite.YPosition, 2.0));
        }
        #endregion

        #region Private Methods
        private static double GetHorizontalDistance(SideScrollerSprite sprite, SideScrollerSprite otherSprite)
        {
            return Math.Abs(sprite.XPosition - otherSprite.XPosition);
        }

        private static double GetApproximateDistance(SideScrollerSprite sprite, SideScrollerSprite otherSprite)
        {
            return Math.Max(Math.Abs(sprite.XPosition - otherSprite.XPosition), Math.Abs(sprite.YPosition - otherSprite.YPosition));
        }
        #endregion
    }
}
