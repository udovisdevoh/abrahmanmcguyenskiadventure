using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Vortex dispatcher
    /// </summary>
    internal static class VortexDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch vortexes on level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchVortexes(Level level, SpritePopulation spritePopulation, Random random)
        {
            /*bool isExtraVortex = false;
            if (level.Size > Program.minSizeForExtraVortex)
            {
                isExtraVortex = random.Next(0, 6) == 1;
            }

            */

            AddVortexToNextLevel(level, spritePopulation, random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add vortex to next level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        private static void AddVortexToNextLevel(Level level, SpritePopulation spritePopulation, Random random)
        {
            bool isIncrementSkill = random.Next(0, 5) == 1;

            double xPosition = level.RightBound - 2.0;
            /*double yPosition;

            do
            {
                Ground ground = SpriteDispatcher.GetRandomVisibleGround(level,random,xPosition);
                yPosition = ground[xPosition];
                xPosition -= 0.1;
            } while (yPosition >= Program.holeHeight && xPosition > level.LeftBound);
            VortexSprite vortexSprite = new VortexSprite(xPosition, yPosition, random, true);*/

            VortexSprite vortexSprite = new VortexSprite(xPosition, Program.totalHeightTileCount / -2, random, true);
            vortexSprite.IsFullGravityOnNextFrame = true;

            if (isIncrementSkill)
                vortexSprite.IncrementSkillOffset = 1;

            spritePopulation.Add(vortexSprite);
        }
        #endregion
    }
}
