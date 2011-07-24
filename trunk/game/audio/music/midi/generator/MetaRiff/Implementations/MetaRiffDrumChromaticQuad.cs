using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffDrumChromaticQuad : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            int instrument = random.Next(115, 120);
            if (instrument == 118)
                instrument = 127;
            if (instrument == 119)
                instrument = 47;

            return instrument;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 5;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 70;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 50;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 10;
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.Dodecaphonic);
            return scaleChooser;
        }

        public override AbstractWave BuildPitchOrVelocityWave(Random random)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();
            double phase4 = random.NextDouble();
            double phase5 = random.NextDouble();
            double phase6 = random.NextDouble();

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase4 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase5 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase6 *= -1.0;

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 1, phase1, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 6, phase2, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 4 * Math.Sqrt(2.0), phase3, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 8, phase4, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 16, phase5, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 32, phase6, WaveFunctions.GetRandomWaveFunction(random)));

            wavePack.Normalize(1.0, true, 0.001, 2.0);

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MinimumNoteLength /= 2.0;
            rythmPatternBuilderTimeSplit.MaximumNoteLength /= 2.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.25;
            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("chromatic");
            descriptionTagList.Add("percussive");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return false; }
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(73, 146);
        }

        public override bool IsUltraRigidDrum
        {
            get { return false; }
        }
    }
}
