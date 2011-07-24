using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffMelody : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            int instrument = random.Next(0, 96);
            if (instrument > 87)
                instrument += 16;
            return instrument;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 10;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 90;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 63;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return random.Next(7, 24);
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

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase4 *= -1.0;

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 2 * random.Next(1, 3), phase1, WaveFunctions.Sine));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 3 * random.Next(1, 3), phase2, WaveFunctions.Sine));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 4, random.NextDouble(), WaveFunctions.Sine));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 8 * random.Next(1, 3), phase3, WaveFunctions.Sine));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 16 * random.Next(1, 3), phase4, WaveFunctions.Sine));

            wavePack.Normalize(1.0, true, 0.001, 2.0);

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 4.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.5;

            if (random.Next(0, 1) == 0)
            {
                rythmPatternBuilderTimeSplit.DesiredRythmLength *= 2;
                if (random.Next(0, 1) == 0)
                {
                    rythmPatternBuilderTimeSplit.DesiredRythmLength *= 2;
                    if (random.Next(0, 1) == 0)
                    {
                        rythmPatternBuilderTimeSplit.DesiredRythmLength *= 2;
                    }
                }
            }

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = random.Next(0, 3) == 1;
            rythmPatternBuilderTimeSplit.TernaryProbability = 0.333;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("melody");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return false; }
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(70, 140);
        }

        public override bool IsUltraRigidDrum
        {
            get { return false; }
        }
    }
}
