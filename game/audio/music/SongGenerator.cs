using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.midi.generator;

namespace AbrahmanAdventure.audio
{
    internal static class SongGenerator
    {
        #region Internal Methods
        internal static IRiff BuildSong(int seed)
        {
            Random random = new Random(seed);

            MetaRiff metaRiff = new MetaRiffPianoClassic();
            RiffBuilder builder = metaRiff.Build(random);
            builder.ForcedModulationOffset = 0;
            builder.IsOverrideKey = true;
            builder.DefaultKey = 3;
            builder.Tempo = random.Next(80, 160);
            Riff riff = builder.Build(0);

            return riff;
        }
        #endregion
    }
}
