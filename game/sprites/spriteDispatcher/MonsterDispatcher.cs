using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Monster dispatcher
    /// </summary>
    internal static class MonsterDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch sprite on level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchMonsters(Level level, int skillLevel, Random random)
        {
            double monsterSkillDensity = random.NextDouble() * Math.Sqrt(((double)skillLevel + 1.0) / 10.0) + 0.05 * ((double)skillLevel + 1.0);

            monsterSkillDensity = Math.Min(monsterSkillDensity, 1.0);
            monsterSkillDensity = Math.Max(monsterSkillDensity, 0.0);

            double monsterSkillMass = monsterSkillDensity * level.Size;
        }
        #endregion
    }
}
