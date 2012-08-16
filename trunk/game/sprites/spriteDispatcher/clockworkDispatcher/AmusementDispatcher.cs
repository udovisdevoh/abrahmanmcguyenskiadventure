using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatches floating wheels, seesaws and pendulums
    /// </summary>
    internal static class AmusementDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch wheels
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="random">random number generator</param>
        internal static void Dispatch(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, Random random)
        {
            double density = random.NextDouble() * 0.15;
            density *= Program.amusementDensityAdjustment;

            double speed = random.NextDouble() * 1.5 + 1.2;

            int amusementCount = (int)((double)(level.Size * density));

            for (int i = 0; i < amusementCount; i++)
                AddAmusement(level, spritePopulation, waterInfo, speed, random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add amusement
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="waterInfo">info about water</param>
        /// <param name="speed">speed</param>
        /// <param name="random">random number generator</param>
        private static void AddAmusement(Level level, SpritePopulation spritePopulation, WaterInfo waterInfo, double speed, Random random)
        {
            #warning Implement AddAmusement()
        }
        #endregion
    }
}
