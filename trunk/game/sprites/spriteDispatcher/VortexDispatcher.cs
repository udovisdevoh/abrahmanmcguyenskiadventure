using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

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
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchVortexes(Level level, SpritePopulation spritePopulation, int skillLevel, Random random)
        {
            bool isAddExtraVortex = false;
            if (level.Size > Program.minSizeForExtraVortex)
                isAddExtraVortex = random.Next(0, 6) == 1;

            bool isIncrementSkill = random.Next(0, skillLevel +1) == 0;

            AddVortexToNextLevel(level, spritePopulation, random, isIncrementSkill);

            if (isAddExtraVortex)
                AddExtraVortex(level, spritePopulation, random, isIncrementSkill);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add vortex to next level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        /// <param name="isIncrementSkill">whether we will increment skill when going into this vortex</param>
        private static void AddVortexToNextLevel(Level level, SpritePopulation spritePopulation, Random random, bool isIncrementSkill)
        {
            double xPosition = level.RightBound - 2.0;

            while (IGroundHelper.GetHighestGround(level, xPosition)[xPosition] >= Program.holeHeight / 2.0)
                xPosition -= 1.0;

            VortexSprite vortexSprite = new VortexSprite(xPosition, Program.totalHeightTileCount / -2, random, true);
            vortexSprite.IsFullGravityOnNextFrame = true;

            if (isIncrementSkill)
                vortexSprite.IncrementSkillOffset = 1;

            spritePopulation.Add(vortexSprite);
        }

        /// <summary>
        /// Add extra vortext (to fork)
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        /// <param name="isIncrementSkill">whether we will increment skill when going into this vortex</param>
        private static void AddExtraVortex(Level level, SpritePopulation spritePopulation, Random random, bool isIncrementSkill)
        {
            double xPosition;
            double yPosition;
            int tryCount = 0;
            do
            {
                xPosition = random.NextDouble() * level.Size + level.LeftBound;
                Ground ground = SpriteDispatcher.GetRandomVisibleGround(level,random,xPosition);
                yPosition = ground[xPosition];
            } while (yPosition >= Program.holeHeight && tryCount < 20);
            VortexSprite vortexSprite = new VortexSprite(xPosition, yPosition, random, true);

            if (isIncrementSkill)
                vortexSprite.IncrementSkillOffset = 1;

            spritePopulation.Add(vortexSprite);
        }
        #endregion
    }
}
