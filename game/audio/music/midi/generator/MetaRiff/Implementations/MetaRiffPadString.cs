using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffPadString : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return random.Next(48, 50);
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 50;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 50;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return random.Next(50, 70);
        }

        public override int BuildPreferedRadius(Random random)
        {
            return random.Next(6, 16);
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.MajorPentatonic);
            scaleChooser.Add(Scales.MinorPentatonic);
            return scaleChooser;
        }

        public override AbstractWave BuildPitchOrVelocityWave(Random random)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();


            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction3 = WaveFunctions.GetRandomWaveFunction(random);

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble(), 0.125 * random.Next(1, 3), phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble(), 0.25 * random.Next(1, 3), phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble(), 0.0625 * random.Next(1, 3), phase3, waveFunction3));
            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MinimumNoteLength *= 4.0;
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 16.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 8;

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("pad");
            descriptionTagList.Add("string");
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
