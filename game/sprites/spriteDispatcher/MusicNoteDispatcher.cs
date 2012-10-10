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

            double xPosition, yPosition;
            for (int i = 0; i < musicNoteCount; i++)
            {
                xPosition = random.NextDouble() * level.Size + level.LeftBound;

                if (random.Next(0, 2) == 1)
                {   
                    Ground ground = SpriteDispatcher.GetRandomVisibleGround(level, random, xPosition);
                    yPosition = ground[xPosition];

                    MusicNoteSprite musicNoteSprite = new MusicNoteSprite(xPosition, yPosition, random);
                    spritePopulation.Add(musicNoteSprite);
                }
                else
                {
                    yPosition = Program.totalHeightTileCount / -2;
                    MusicNoteSprite musicNoteSprite = new MusicNoteSprite(xPosition, yPosition, random);
                    spritePopulation.Add(musicNoteSprite);
                    musicNoteSprite.IsFullGravityOnNextFrame = true;
                }
            }
        }
    }
}
