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
        private static IOrderedEnumerable<AbstractSprite> __sortedIOrderedEnumerableListSprite;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get list of visible sprites sorted by distance from sprite (closest to farthest)
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="spritePopulation">unsortedSpriteList</param>
        /// <returns></returns>
        internal static IOrderedEnumerable<AbstractSprite> Sort(AbstractSprite sprite, HashSet<AbstractSprite> unsortedSpriteList)
        {
            __sortedIOrderedEnumerableListSprite = from otherSprite in unsortedSpriteList orderby GetApproximateDistance(sprite, otherSprite) descending select otherSprite;
            return __sortedIOrderedEnumerableListSprite;
        }

        private static double GetApproximateDistance(AbstractSprite sprite, AbstractSprite otherSprite)
        {
            return Math.Max(Math.Abs(sprite.XPosition - otherSprite.XPosition), Math.Abs(sprite.YPosition - otherSprite.YPosition));
        }
        #endregion
    }
}
