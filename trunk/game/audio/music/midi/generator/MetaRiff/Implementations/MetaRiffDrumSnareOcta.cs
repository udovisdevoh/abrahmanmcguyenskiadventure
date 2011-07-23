using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    internal class MetaRiffDrumSnareOcta : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 0;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 0;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 105;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return random.Next(38, 41);
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
            wavePack.Add(new Wave(1.0, 16, phase, waveFunction));
            wavePack.Normalize(1.0, true, 0.1, 16.0);

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            RythmPattern rythmPattern = new RythmPattern();
            rythmPattern.Add(0.03125);
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("drum");
            descriptionTagList.Add("snare");
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
