using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackDrumChromaticTerminator : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffDrumChromaticTerminator());
            metaRiffList.Add(new MetaRiffDrumChromaticTerminator());
            return metaRiffList;
        }
    }
}
