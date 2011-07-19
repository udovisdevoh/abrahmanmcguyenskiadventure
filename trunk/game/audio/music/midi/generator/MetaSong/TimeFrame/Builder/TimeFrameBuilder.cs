using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class TimeFrameBuilder
    {
        #region Parts
        private TrackFinder trackFinder = new TrackFinder();
        #endregion

        #region Fields
        private VersePeakBuilder versePeakBuilder = new VersePeakBuilder();

        private ExtrapolationBuilder extrapolationBuilder = new ExtrapolationBuilder();
        #endregion

        #region Public Methods
        public TimeFrame Build(Random random, int verseCount, int riffTrackCount, int barCountForEachVerse)
        {
            if (riffTrackCount < 1)
                throw new TimeFrameException("Not enough riff track count");
            if (verseCount < 1)
                throw new TimeFrameException("Not enough verse count");
            
            BarBlock fullBlocks = new BarBlock();
            for (int i = 0; i < riffTrackCount; i++)
                fullBlocks.Add(i);

            TimeFrame timeFrame = new TimeFrame();
            for (int verseCounter = 0; verseCounter < verseCount; verseCounter++)
                for (int barCounter = 0; barCounter < barCountForEachVerse; barCounter++)
                    timeFrame.Add(fullBlocks);

            return timeFrame;
        }

        public TimeFrame Build(PredefinedGenerator generator, MetaRiffPack metaRiffPack)
        {
            TimeFrame timeFrame = new TimeFrame();
            for (int barIndex = 0; barIndex < generator.BarCount; barIndex++)
            {
                BarBlock barBlocks = new BarBlock();
                foreach (PredefinedGeneratorTrack track in generator)
                {
                    if (track[barIndex])
                    {
                        List<int> availableTrackListFromMetaRiff = trackFinder.GetAvailableTrackList(metaRiffPack, "MetaRiffPack" + track.MetaRiffPackName);
                        foreach (int trackId in availableTrackListFromMetaRiff)
                            barBlocks.Add(trackId);
                    }
                }
                timeFrame.Add(barBlocks);
            }

            timeFrame.Trim();
            return timeFrame;
        }
        #endregion

        #region Private Methods
        private BarBlock BuildUniqueBlock(Random random, int riffTrackCount)
        {
            int value = random.Next(0, riffTrackCount);
            return new BarBlock(value);
        }

        private BarBlock BuildUniqueBlock(Random random, int riffTrackCount, BarBlock startingBlock)
        {
            if (riffTrackCount == 1)
                return new BarBlock(0);

            int value;
            do
            {
                value = random.Next(0, riffTrackCount);
            } while (value == startingBlock[0]);
            return new BarBlock(value);
        }

        private List<BarBlock> BuildListVersePeaks(Random random, int verseCount, int riffTrackCount)
        {
            List<BarBlock> listVersePeaks = new List<BarBlock>();

            for (int i = 0; i < verseCount - 1; i++)
                listVersePeaks.Add(versePeakBuilder.BuildVersePeak(random, listVersePeaks, riffTrackCount, verseCount));

            //we build the last peak with everything in it
            BarBlock lastPeak = new BarBlock();
            for (int i = 0; i < riffTrackCount; i++)
                lastPeak.Add(i);

            listVersePeaks.Add(lastPeak);

            return listVersePeaks;
        }
        #endregion
    }
}
