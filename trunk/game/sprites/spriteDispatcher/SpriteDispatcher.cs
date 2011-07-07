using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Manage sprite position in level
    /// </summary>
    internal static class SpriteDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch sprite on level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchSprites(Level level, int skillLevel, Random random)
        {
            MonsterDispatcher.DispatchMonsters(level, skillLevel, random);
        }
        #endregion
    }
}
