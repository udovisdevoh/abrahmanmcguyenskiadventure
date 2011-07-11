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
        #region Internal Methods
        /// <summary>
        /// Dispatch blocks
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchBlocks(Level level, SpritePopulation spritePopulation, Random random)
        {
            HashSet<int> addedBlockMemory = new HashSet<int>();
            foreach (Ground ground in level)
            {
                BlockDispatcherWave.DispatchBlocks(ground, level, spritePopulation, addedBlockMemory, random);
                //BlockDispatcherSegmentWave.DispatchBlocks(ground, level, spritePopulation, addedBlockMemory, random);
            }
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

        /// <summary>
        /// For block dispatcher, build wave for block segment distance from ground
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave for block segment distance from ground</returns>
        internal static AbstractWave BuildBlockYDistanceFromGroundWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(4, 32, 2, 8, random, false, true));
            } while (random.Next(0, 2) != 0);
            wavePack.Normalize((double)random.Next(2, 8));

            return wavePack;
        }

        /// <summary>
        /// For block dispatcher, build wave for probability of having a visible anarchy block
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>For block dispatcher, wave for probability of having a visible anarchy block</returns>
        internal static AbstractWave BuildSpecialBlockTypeProbabilityWave(Random random)
        {
            WavePack wavePack = new WavePack();
            do
            {
                wavePack.Add(WaveBuilder.BuildIndividualWave(0.5, 8, 0, 1, random, false, true));
            } while (random.Next(0, 7) != 0);
            double normalizationFactor = random.NextDouble() * 0.2 + 1.4;
            wavePack.Normalize(normalizationFactor);

            return wavePack;
        }
        #endregion
    }
}
