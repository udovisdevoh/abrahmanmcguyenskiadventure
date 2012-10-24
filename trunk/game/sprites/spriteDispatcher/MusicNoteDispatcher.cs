using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Dispatches music notes
    /// </summary>
    internal static class MusicNoteDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch music notes
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchMusicNotes(Level level, SpritePopulation spritePopulation, AbstractGameMode gameMode, Random random)
        {
            double musicNoteDensity = (random.NextDouble() * 0.06 + 0.014) * gameMode.MusicNoteDensityMultiplicator;
            int musicNoteCount = (int)(musicNoteDensity * level.Size);

            double normalizationFactor = 1.0 + random.NextDouble() * 1.5;
            AbstractWave musicNoteHeightFromGroundWave = BuildMusicNoteHeightFromGroundWave(random, normalizationFactor);

            double xPosition, yPosition;
            for (int i = 0; i < musicNoteCount; i++)
            {
                xPosition = random.NextDouble() * level.Size + level.LeftBound;

                Ground ground = SpriteDispatcher.GetRandomVisibleGround(level, random, xPosition);
                yPosition = ground[xPosition] - (musicNoteHeightFromGroundWave[xPosition] + normalizationFactor);

                MusicNoteSprite musicNoteSprite = new MusicNoteSprite(xPosition, yPosition, random);
                spritePopulation.Add(musicNoteSprite);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Wave for height of music notes over ground
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Wave for height of music notes ground</returns>
        private static AbstractWave BuildMusicNoteHeightFromGroundWave(Random random, double normalizationFactor)
        {
            WavePack wavePack = new WavePack();

            int waveCount = random.Next(1, 5);

            for (int waveId = 0; waveId < waveCount; waveId++)
            {
                double waveLength = random.NextDouble() * 15.0 + 4.0;
                double amplitude = random.NextDouble();
                double phase = random.NextDouble() * 2.0 - 1.0;

                WaveFunction waveFunction = WaveFunctions.Square;

                wavePack.Add(new Wave(amplitude, waveLength, phase, waveFunction));
            }

            wavePack.Normalize(normalizationFactor);

            return wavePack;
        }
        #endregion
    }
}
