using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class TrackFinder
    {
        #region Parts
        private MetaRiffPackLoader metaRiffPackLoader = new MetaRiffPackLoader();
        #endregion

        #region Public Methods
        public List<int> GetAvailableTrackList(MetaRiffPack largeMetaRiffPack, string metaRiffPackName)
        {
            MetaRiffPack smallMetaRiffPack = metaRiffPackLoader.Load(metaRiffPackName);

            List<int> availableTrackList = new List<int>();

            int trackCounter = 0;
            foreach (MetaRiff currentMetaRiff in largeMetaRiffPack)
            {
                Type type = currentMetaRiff.GetType();
                if (smallMetaRiffPack.ContainsMetaRiffType(type))
                    availableTrackList.Add(trackCounter);

                trackCounter++;
            }
            return availableTrackList;
        }
        #endregion
    }
}
