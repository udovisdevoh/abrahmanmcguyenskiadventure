using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

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
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchSprites(Level level, SpritePopulation spritePopulation, int skillLevel, Random random)
        {
            MonsterDispatcher.DispatchMonsters(level, spritePopulation, skillLevel, random);
            VortexDispatcher.DispatchVortexes(level, spritePopulation, skillLevel, random);
            TrampolineDispatcher.DispatchTrampolines(level, spritePopulation, random);
            MusicNoteDispatcher.DispatchMusicNotes(level, spritePopulation, random);
            AddedBlockMemory addedBlockMemory = BlockDispatcher.DispatchBlocks(level, spritePopulation, random);
            PipeDispatcher.DispatchPipes(level, spritePopulation, random);
        }

        /// <summary>
        /// Get random ground
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="random">random number generator</param>
        /// <param name="xPosition">x position to be visible</param>
        /// <returns>random ground</returns>
        internal static Ground GetRandomVisibleGround(Level level, Random random, double xPosition)
        {
            Ground ground;

            int tryCount = 0;
            do
            {
                ground = level[random.Next(level.Count)];
                tryCount++;
            } while ((!IGroundHelper.IsGroundVisible(ground,level,xPosition) || ground[xPosition] >= Program.holeHeight) && tryCount < 20);

            if (tryCount >= 20)
                ground = IGroundHelper.GetHighestGround(level, xPosition);

            return ground;
        }
        #endregion
    }
}
