using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffDrumBassBinary : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 0;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 127;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 127;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return random.Next(35, 37);
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 0;
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(73, 146);
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.Dodecaphonic);
            return scaleChooser;
        }

        public override AbstractWave BuildPitchOrVelocityWave(Random random)
        {
            double phase = -1.0;

            WaveFunction waveFunction = WaveFunctions.Square;

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(1.0, 8, phase, waveFunction));
            wavePack.Normalize(1.0, true, 0.001, 2.0);

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            //rythmPatternBuilderTimeSplit.MaximumNoteLength *= 4.0;
            rythmPatternBuilderTimeSplit.MinimumNoteLength *= 2.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.25 * random.Next(1, 3);

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("drum");
            descriptionTagList.Add("kick");
            descriptionTagList.Add("bass drum");
            descriptionTagList.Add("percusion");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return true; }
        }

        public override bool IsUltraRigidDrum
        {
            get { return true; }
        }
    }
}
