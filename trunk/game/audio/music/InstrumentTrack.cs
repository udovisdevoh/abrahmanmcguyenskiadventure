using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Instrument track
    /// </summary>
    internal class InstrumentTrack : IEnumerable<Riff>
    {
        #region Fields and parts
        private InstrumentType instrumentType;

        private int midiInstrument;

        private bool isAllowedTernary;

        private int channel;

        private double length;

        private double riffLength;

        private List<Riff> riffList;
        #endregion

        #region Constructor
        public InstrumentTrack(InstrumentType instrumentType, bool isAllowedTernary, Random random)
        {
            this.instrumentType = instrumentType;
            this.isAllowedTernary = isAllowedTernary;
            riffLength = 1.0;
            int riffCount = (int)Math.Pow(2.0, (double)random.Next(1,5));

            switch (instrumentType)
            {
                case InstrumentType.Soprano:
                    midiInstrument = random.Next(0, 96);
                    if (midiInstrument >= 88)
                        midiInstrument += 16;
                    riffLength = Math.Pow(2.0, (double)random.Next(2,6));
                    channel = 0;
                    break;
                case InstrumentType.Alto:
                    midiInstrument = random.Next(0, 96);
                    if (midiInstrument >= 88)
                        midiInstrument += 16;
                    riffLength = Math.Pow(2.0, (double)random.Next(2, 6));
                    channel = 1;
                    break;
                case InstrumentType.Tenor:
                    midiInstrument = random.Next(0, 96);
                    if (midiInstrument >= 88)
                        midiInstrument += 16;
                    riffLength = Math.Pow(2.0, (double)random.Next(1, 6));
                    channel = 2;
                    break;
                case InstrumentType.Bass:
                    midiInstrument = random.Next(32, 40);
                    riffLength = Math.Pow(2.0, (double)random.Next(0, 5));
                    channel = 3;
                    break;
                case InstrumentType.Pad:
                    midiInstrument = random.Next(88, 96);
                    riffLength = Math.Pow(2.0, (double)random.Next(3, 6));
                    channel = 4;
                    break;
                case InstrumentType.ChromaticPercussion:
                    midiInstrument = random.Next(112, 119);
                    riffLength = Math.Pow(2.0, (double)random.Next(0, 6));
                    channel = 5;
                    break;
                case InstrumentType.Drum:
                    midiInstrument = 0;
                    riffLength = Math.Pow(2.0, (double)random.Next(0, 5));
                    channel = 9;
                    break;
            }
            
            length = (double)riffCount * riffLength;
            riffList = new List<Riff>();
            for (int i = 0; i < riffCount; i++)
                riffList.Add(new Riff(random, riffLength, isAllowedTernary, instrumentType));
        }
        #endregion

        #region Internal Methods
        internal static double GetMaxLength(List<InstrumentTrack> listInstrumentTrack)
        {
            double maxLength = -1;
            foreach (InstrumentTrack instrumentTrack in listInstrumentTrack)
                if (maxLength == -1 || instrumentTrack.Length > maxLength)
                    maxLength = instrumentTrack.Length;
            return maxLength;
        }

        internal Riff GetRiffAtTime(double timePointerAbsolute, double timePointerPreviousAbsolute, out double timePointerRelativeToRiff, out double timePointerPreviousRelativeToRiff)
        {
            Riff riff = riffList[(int)(timePointerAbsolute / riffLength) % riffList.Count];
            timePointerRelativeToRiff = timePointerAbsolute % riffLength;
            timePointerPreviousRelativeToRiff = timePointerPreviousAbsolute % riffLength;

            if (timePointerPreviousRelativeToRiff > timePointerRelativeToRiff)
                timePointerPreviousRelativeToRiff = 0;

            return riff;
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

        public double Length
        {
            get { return length; }
        }
        #endregion

        #region IEnumerable<Riff> Members
        public IEnumerator<Riff> GetEnumerator()
        {
            return riffList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return riffList.GetEnumerator();
        }
        #endregion
    }
}
