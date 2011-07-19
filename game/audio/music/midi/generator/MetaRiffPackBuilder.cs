using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackBuilder
    {
        #region Parts
        private MetaRiffPackLoader metaRiffPackLoader = new MetaRiffPackLoader();
        #endregion

        #region Public Methods
        public MetaRiffPack Build(PredefinedGenerator currentGenerator)
        {
            CompositeMetaRiffPack compositeMetaRiffPack = new CompositeMetaRiffPack();

            foreach (PredefinedGeneratorTrack track in currentGenerator)
                if (track.MetaRiffPackName != "")
                    compositeMetaRiffPack = new CompositeMetaRiffPack(compositeMetaRiffPack, metaRiffPackLoader.Load("MetaRiffPack" + track.MetaRiffPackName));

            return compositeMetaRiffPack;
        }
        #endregion
    }
}
