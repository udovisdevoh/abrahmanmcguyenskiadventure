using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    internal static class BlockDispatcher
    {
        #region Constants
        internal const double anarchyBlockProbability = 0.2;

        internal const double hiddenAnarchyBlockProbability = 0.1;

        internal const double indestructibleBlockProbability = 0.2;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Dispatch blocks
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static AddedBlockMemory DispatchBlocks(Level level, SpritePopulation spritePopulation, Random random)
        {
            AddedBlockMemory addedBlockMemory = new AddedBlockMemory();
            foreach (Ground ground in level)
            {
                if (random.Next(0,5) == 1)
                    BlockDispatcherTotems.DispatchBlocks(ground, level, spritePopulation, addedBlockMemory, random);
                else
                    BlockDispatcherWave.DispatchBlocks(ground, level, spritePopulation, addedBlockMemory, random);
            }

            return addedBlockMemory;
        }

        /// <summary>
        /// Whether Y position is higher than a ground in level which is higher than provided ground
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <param name="ground">Ground</param>
        /// <param name="level">Level</param>
        /// <returns>Whether Y position is higher than a ground in level which is higher than provided ground</returns>
        internal static bool IsHigherThanHigherGroundThan(double xPosition, double yPosition, Ground ground, Level level)
        {
            double groundHeight = ground[xPosition];
            foreach (Ground otherGround in level)
            {
                double otherGroundHeight = otherGround[xPosition];
                if (otherGroundHeight < groundHeight)
                {
                    if (yPosition < otherGroundHeight)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// For block dispatcher, build wave for probability of having blocks (0 = yes)
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>For block dispatcher, build wave for probability of having blocks (0 = yes)</returns>
        internal static AbstractWave BuildDensityWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(4, 16, 0, 1, random, false, true));
            } while (random.Next(0, 5) != 0);
            wavePack.Normalize();

            return wavePack;
        }
        #endregion
    }
}
