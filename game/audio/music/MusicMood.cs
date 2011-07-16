using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Represents the time signature
    /// </summary>
    enum TimeSignature { Quaternary, Ternary }

    /// <summary>
    /// Represents a music theme with high level guide lines
    /// </summary>
    internal class MusicMood
    {
        #region Fields
        /// <summary>
        /// Tempo
        /// </summary>
        private int tempo;

        /// <summary>
        /// Represents the time signature
        /// </summary>
        private TimeSignature timeSignature;

        /// <summary>
        /// 1st Melodic instrument
        /// </summary>
        private int sopranoInstrument;

        /// <summary>
        /// 2nd melodic instrument
        /// </summary>
        private int altoInstrument;

        /// <summary>
        /// 3rd instrument
        /// </summary>
        private int tenorInstrument;

        /// <summary>
        /// Bass instrument
        /// </summary>
        private int bassInstrument;

        /// <summary>
        /// Pad instrument
        /// </summary>
        private int padInstrument;

        /// <summary>
        /// Chromatic percussion instrument
        /// </summary>
        private int chromaticPercussionInstrument;

        /// <summary>
        /// 1: all instrument at once all the time, 0: only one instrument at once
        /// </summary>
        private double orchestrationLevel;

        /// <summary>
        /// 1: a lot of drum, 0: no drum
        /// </summary>
        private double drumPercusivity;

        /// <summary>
        /// 1: a lot of chromatic percussions, 0: no chromatic percussion
        /// </summary>
        private double chromaticPercusivity;
        #endregion

        #region Constructor
        /// <summary>
        /// Create music mood
        /// </summary>
        /// <param name="random">random number generator</param>
        public MusicMood(Random random)
        {
            tempo = random.Next(80, 160);

            if (random.Next(1, 31) <= 11)
                timeSignature = TimeSignature.Ternary;
            else
                timeSignature = TimeSignature.Quaternary;

            sopranoInstrument = random.Next(0, 96);
            if (sopranoInstrument >= 88)
                sopranoInstrument += 16;

            altoInstrument = random.Next(0, 96);
            if (altoInstrument >= 88)
                altoInstrument += 16;

            tenorInstrument = random.Next(0, 96);
            if (tenorInstrument >= 88)
                tenorInstrument += 16;

            bassInstrument = random.Next(32, 40);
            padInstrument = random.Next(88, 96);
            chromaticPercussionInstrument = random.Next(112, 119);

            orchestrationLevel = random.NextDouble();
            drumPercusivity = random.NextDouble();
            chromaticPercusivity = random.NextDouble();
        }
        #endregion
    }
}
