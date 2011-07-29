using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Creates riff with stored construction settings
    /// </summary>
    public abstract class MetaRiff
    {
        #region Parts
        /// <summary>
        /// Builds rythm patterns by splitting notes
        /// </summary>
        protected RythmPatternBuilderTimeSplit rythmPatternBuilderTimeSplit = new RythmPatternBuilderTimeSplit();
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Build prefered midi instrument
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>selected midi instrument number</returns>
        public abstract int BuildPreferedMidiInstrument(Random random);

        /// <summary>
        /// Build Minimum velocity
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Minimum velocity</returns>
        public abstract int BuildMinimumVelocity(Random random);

        /// <summary>
        /// Build Maximum velocity
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Maximum velocity</returns>
        public abstract int BuildMaximumVelocity(Random random);

        /// <summary>
        /// Build prefered mid pitch
        /// </summary>
        /// <param name="random">Random number generator</param>
        /// <returns>prefered mid pitch (0 to 127)</returns>
        public abstract int BuildPreferedMidPitch(Random random);

        /// <summary>
        /// Build prefered radius
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>prefered radius</returns>
        public abstract int BuildPreferedRadius(Random random);

        /// <summary>
        /// Build prefered tempo
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>prefered tempo</returns>
        public abstract int BuildPreferedTempo(Random random);

        /// <summary>
        /// Returns scale chooser
        /// </summary>
        /// <returns>scale chooser</returns>
        public abstract ScaleChooser BuildScaleChooser();

        /// <summary>
        /// Builds pitch waves or velocity waves
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Pitch wave or velocity wave</returns>
        public abstract AbstractWave BuildPitchOrVelocityWave(Random random);

        /// <summary>
        /// Builds a rythm pattern
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>rythm pattern</returns>
        public abstract RythmPattern BuildRythmPattern(Random random);

        /// <summary>
        /// Build a list of description tags
        /// </summary>
        /// <returns>list of description tags</returns>
        public abstract IEnumerable<string> GetDescriptionTagList();
        
        /// <summary>
        /// Whether this metaRiff is used for drums
        /// </summary>
        public abstract bool IsDrum
        {
            get;
        }

        /// <summary>
        /// Whether this metaRiff is used for ultra rigid drums (snare, kick etc...)
        /// </summary>
        public abstract bool IsUltraRigidDrum
        {
            get;
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Join a metaRiff with a metaRiff Pack
        /// </summary>
        /// <param name="metaRiff1">metaRiff</param>
        /// <param name="metaRiffPack2">metaRiff Pack</param>
        /// <returns>Joined metaRiff Pack</returns>
        public static MetaRiffPack operator +(MetaRiff metaRiff1, MetaRiffPack metaRiffPack2)
        {
            MetaRiffPack metaRiffPack1 = new CompositeMetaRiffPack(metaRiff1);
            return new CompositeMetaRiffPack(metaRiffPack1, metaRiffPack2);
        }

        /// <summary>
        /// Join a metaRiff pack with a metaRiff
        /// </summary>
        /// <param name="metaRiffPack2">metaRiff pack</param>
        /// <param name="metaRiff1">metaRiff</param>
        /// <returns>Joined metaRiff Pack</returns>
        public static MetaRiffPack operator +(MetaRiffPack metaRiffPack2, MetaRiff metaRiff1)
        {
            MetaRiffPack metaRiffPack1 = new CompositeMetaRiffPack(metaRiff1);
            return new CompositeMetaRiffPack(metaRiffPack1, metaRiffPack2);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a riff builder that can be used to create riffs in a deterministic way
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>riff builder that can be used to create riffs in a deterministic way</returns>
        public RiffBuilder Build(Random random)
        {
            RiffBuilder riffBuilder = new RiffBuilder();
            riffBuilder.IsDrum = IsDrum;
            riffBuilder.MidPitch = BuildPreferedMidPitch(random);
            riffBuilder.Radius = BuildPreferedRadius(random);
            riffBuilder.DesiredRiffLength = 2;
            riffBuilder.PitchWave = BuildPitchOrVelocityWave(random);
            riffBuilder.RythmPattern = BuildRythmPattern(random);
            riffBuilder.Scale1 = BuildScaleChooser().BuildPreferedScale(random);
            riffBuilder.MidiInstrument = BuildPreferedMidiInstrument(random);
            riffBuilder.VelocityWave = BuildPitchOrVelocityWave(random);
            riffBuilder.MinimumVelocity = BuildMinimumVelocity(random);
            riffBuilder.MaximumVelocity = BuildMaximumVelocity(random);
            riffBuilder.Tempo = BuildPreferedTempo(random);
            riffBuilder.IsUltraRigidDrum = IsUltraRigidDrum;

            if (IsDrum)
                riffBuilder.DefaultKey = 0;
            else
                riffBuilder.DefaultKey = riffBuilder.DefaultKey;

            return riffBuilder;
        }
        #endregion
    }
}
