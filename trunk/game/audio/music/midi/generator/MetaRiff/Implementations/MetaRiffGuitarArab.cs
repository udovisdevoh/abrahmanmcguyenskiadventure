using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffGuitarArab : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 24;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 40;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 127;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 64;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 9;
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.ArabicPentatonic);
            return scaleChooser;
        }

        public override AbstractWave BuildPitchOrVelocityWave(Random random)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();
            double phase4 = random.NextDouble();
            double phase5 = random.NextDouble();

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

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 2 * random.Next(1, 3), phase1, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 3 * random.Next(1, 3), phase2, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 4, phase3, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 8 * random.Next(1, 3), phase4, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 16 * random.Next(1, 3), phase5, WaveFunctions.GetRandomWaveFunction(random)));

            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 2.0;
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
            descriptionTagList.Add("arab");
            descriptionTagList.Add("guitar");
            descriptionTagList.Add("lead");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return false; }
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(80, 130);
        }

        public override bool IsUltraRigidDrum
        {
            get { return false; }
        }
    }
}
