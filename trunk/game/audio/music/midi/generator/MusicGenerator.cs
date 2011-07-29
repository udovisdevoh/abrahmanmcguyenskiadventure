using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Generates random music from presets
    /// </summary>
    public class MusicGenerator
    {
        #region Parts
        private MetaSongBuilder metaSongBuilder;

        private Random random;

        private MetaRiffPackBuilder metaRiffPackBuilder;

        private RiffToSequenceConverter riffToSequenceConverter;
        #endregion

        #region Constructor
        /// <summary>
        /// Create random music generator
        /// </summary>
        /// <param name="random">random number generator</param>
        public MusicGenerator(Random random)
        {
            riffToSequenceConverter = new RiffToSequenceConverter();
            metaSongBuilder = new MetaSongBuilder();
            metaRiffPackBuilder = new MetaRiffPackBuilder();
            this.random = random;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build riff pack from predefined generator
        /// </summary>
        /// <param name="predefinedGenerator">predefined generator</param>
        /// <returns>riff pack</returns>
        public RiffPack BuildSong(PredefinedGenerator predefinedGenerator)
        {
            metaSongBuilder.ModulationStrength = predefinedGenerator.Modulation;
            metaSongBuilder.BarCount = predefinedGenerator.BarCount;
            metaSongBuilder.MetaRiffPack = metaRiffPackBuilder.Build(predefinedGenerator);

            if (predefinedGenerator.IsOverrideScale)
            {
                metaSongBuilder.MetaRiffPack.OverridenScale1 = Scales.GetScale(predefinedGenerator.ScaleName1);
                metaSongBuilder.MetaRiffPack.OverridenScale2 = Scales.GetScale(predefinedGenerator.ScaleName2);
                metaSongBuilder.MetaRiffPack.ScaleCycleLength = predefinedGenerator.ScaleCycleLength;
            }

            metaSongBuilder.ScaleCycleLength = predefinedGenerator.ScaleCycleLength;
            metaSongBuilder.MetaRiffPack.IsOverrideKey = predefinedGenerator.IsOverrideKey;
            if (predefinedGenerator.IsOverrideKey)
                metaSongBuilder.MetaRiffPack.ForcedModulationOffset = predefinedGenerator.ForcedModulationOffset;

            MetaSong metaSong = metaSongBuilder.Build(random, predefinedGenerator, metaSongBuilder.MetaRiffPack);
            RiffPack riffPack = metaSong.BuildSong(random);

            if (predefinedGenerator.IsOverrideTempo)
                riffPack.Tempo = predefinedGenerator.Tempo;

            //return riffToSequenceConverter.Convert(riffPack);
            return riffPack;
        }
        #endregion
    }
}
