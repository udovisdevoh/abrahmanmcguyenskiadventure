using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Generate random music
    /// </summary>
    internal static class RandomSongGenerator
    {
        /// <summary>
        /// Generate a random song
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random song</returns>
        internal static IRiff BuildSong(Random random)
        {
            MetaRiff metaRiff = new MetaRiffPianoClassic();
            RiffBuilder builder = metaRiff.Build(new Random());
            builder.ForcedModulationOffset = 0;
            builder.IsOverrideKey = true;
            builder.DefaultKey = 3;
            Riff riff = builder.Build(0);
            SongPlayer player = new SongPlayer();
            player.IRiff = riff;
            player.Play(new object());
            return riff;
        }
    }
}
