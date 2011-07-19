using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// MetaSong builder
    /// </summary>
    class MetaSongBuilder
    {
        #region Fields
        /// <summary>
        /// Modulation strength: 0: none (pop 1: full (modern jazz)
        /// </summary>
        private double modulationStrength = -1.0;

        /// <summary>
        /// MetaRiff pack
        /// </summary>
        private MetaRiffPack metaRiffPack = null;

        /// <summary>
        /// To build key modulators
        /// </summary>
        private ModulatorBuilder modulatorBuilder = new ModulatorBuilder();

        /// <summary>
        /// To build time frames
        /// </summary>
        private TimeFrameBuilder timeFrameBuilder = new TimeFrameBuilder();

        /// <summary>
        /// How many bars in total
        /// </summary>
        private int barCount = -1;
        #endregion

        #region Public Methods
        /// <summary>
        /// Build metaSong
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="generator">pre defined generator</param>
        /// <param name="metaRiffPack">meta riff pack</param>
        /// <returns>MetaSong</returns>
        public MetaSong Build(Random random, PredefinedGenerator generator, MetaRiffPack metaRiffPack)
        {
            if (modulationStrength == -1.0)
                throw new MetaSongException("Invalid modulation strenght, set ModulationStrength value to 0 to 1");
            if (metaRiffPack == null)
                throw new MetaSongException("Invalid metaRiffPack, set MetaRiffPack property");
            if (barCount == -1)
                throw new MetaSongException("BarCount must be set before");


            TimeFrame timeFrame = timeFrameBuilder.Build(generator, metaRiffPack);

            RiffPackBuilder riffPackBuilder = metaRiffPack.BuildRiffPackBuilder(random, timeFrame.Count);
            Modulator modulator = modulatorBuilder.Build(random, modulationStrength);

            MetaSong metaSong = new MetaSong(riffPackBuilder, modulator, timeFrame);

            return metaSong;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Desired key modulation strength: 0: pop (none) 1: classic/jazz full
        /// </summary>
        public double ModulationStrength
        {
            get
            {
                return modulationStrength;
            }
            set
            {
                modulationStrength = value;
            }
        }

        /// <summary>
        /// MetaRiffPack to use to create MetaSongs
        /// </summary>
        public MetaRiffPack MetaRiffPack
        {
            get { return metaRiffPack; }
            set { metaRiffPack = value; }
        }

        /// <summary>
        /// How many bar you want
        /// </summary>
        public int BarCount
        {
            get { return barCount; }
            set { barCount = value; }
        }
        #endregion
    }
}
