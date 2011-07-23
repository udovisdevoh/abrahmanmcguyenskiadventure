using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffGuitarGypsy : MetaRiff
    {
        public override AbstractWave BuildPitchOrVelocityWave(Random random)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();
            double phase4 = random.NextDouble();
            double phase5 = random.NextDouble();
            double phase6 = random.NextDouble();
            double phase7 = random.NextDouble();

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
            if (random.Next(0, 2) == 1)
                phase7 *= -1.0;


            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 1, phase1, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 2, phase2, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 4, phase3, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 8, phase4, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 16, phase5, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 32, phase6, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 64, phase7, WaveFunctions.GetRandomWaveFunction(random)));

            wavePack.Normalize(1.0, true, 0.1, 16.0);

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MinimumNoteLength /= 2.0;
            rythmPatternBuilderTimeSplit.MaximumNoteLength /= 2.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.25;

            int lengthModifier = random.Next(0, 3);
            if (lengthModifier == 1)
                rythmPatternBuilderTimeSplit.DesiredRythmLength *= 2.0;
            else if (lengthModifier == 2)
                rythmPatternBuilderTimeSplit.DesiredRythmLength /= 2.0;

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.GypsyPentatonic);
            return scaleChooser;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 64;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 16;
        }

        public override int BuildPreferedMidiInstrument(Random random)
        {
            return random.Next(24, 26);
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("guitar");
            descriptionTagList.Add("gypsy");
            descriptionTagList.Add("lead");
            return descriptionTagList;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 85;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 127;
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
