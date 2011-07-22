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
            /*

            MetaRiff metaRiff = new MetaRiffPianoClassic();
            RiffBuilder builder = metaRiff.Build(random);
            builder.ForcedModulationOffset = 0;
            builder.IsOverrideKey = true;
            builder.DefaultKey = 3;
            builder.Tempo = random.Next(80, 160);
            Riff riff = builder.Build(0);

            return riff;*/

            Random random = new Random(seed);
            PredefinedGenerator predefinedGenerator = new PredefinedGenerator();

            predefinedGenerator.IsOverrideScale = random.Next(0, 7) == 1;
            predefinedGenerator.IsOverrideTempo = true;
            predefinedGenerator.Tempo = random.Next(80, 160);
            predefinedGenerator.Modulation = random.NextDouble() * 0.75 + 0.25;
            predefinedGenerator.ScaleName = Scales.GetRandomScaleName(random);
            predefinedGenerator.IsOverrideKey = false;

            MusicGenerator musicGenerator = new MusicGenerator(random);
            return musicGenerator.BuildSong(predefinedGenerator);
        }
        #endregion
    }
}
