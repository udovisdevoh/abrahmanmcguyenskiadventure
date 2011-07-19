using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Instrument track
    /// </summary>
    internal class InstrumentTrack
    {
        #region Fields and parts
        private InstrumentType instrumentType;

        private AbstractWave pitchWave;

        private AbstractWave velocityWave;

        private int midiInstrument;

        private bool isAllowedTernary;

        private int channel;
        #endregion

        #region Constructor
        public InstrumentTrack(InstrumentType instrumentType, bool isAllowedTernary, Random random)
        {
            pitchWave = MusicWaveBuilder.BuildMusicWave(random);
            velocityWave = MusicWaveBuilder.BuildMusicWave(random);
            this.instrumentType = instrumentType;
            this.isAllowedTernary = isAllowedTernary;
            int riffCount = (int)Math.Pow(2.0, (double)random.Next(1,5));

            switch (instrumentType)
            {
                case InstrumentType.Soprano:
                    midiInstrument = random.Next(0, 96);
                    if (midiInstrument >= 88)
                        midiInstrument += 16;
                    channel = 0;
                    break;
                case InstrumentType.Alto:
                    midiInstrument = random.Next(0, 96);
                    if (midiInstrument >= 88)
                        midiInstrument += 16;
                    channel = 1;
                    break;
                case InstrumentType.Tenor:
                    midiInstrument = random.Next(0, 96);
                    if (midiInstrument >= 88)
                        midiInstrument += 16;
                    channel = 2;
                    break;
                case InstrumentType.Bass:
                    midiInstrument = random.Next(32, 40);
                    channel = 3;
                    break;
                case InstrumentType.Pad:
                    midiInstrument = random.Next(88, 96);
                    channel = 4;
                    break;
                case InstrumentType.ChromaticPercussion:
                    midiInstrument = random.Next(112, 119);
                    channel = 5;
                    break;
                case InstrumentType.Drum:
                    midiInstrument = 0;
                    channel = 9;
                    break;
            }
        }
        #endregion

        #region Properties
        public InstrumentType InstrumentType
        {
            get { return instrumentType; }
        }

        public int MidiInstrument
        {
            get { return midiInstrument; }
        }

        public bool IsAllowedTernary
        {
            get { return isAllowedTernary; }
        }

        public int Channel
        {
            get { return channel; }
        }
        #endregion
    }
}
