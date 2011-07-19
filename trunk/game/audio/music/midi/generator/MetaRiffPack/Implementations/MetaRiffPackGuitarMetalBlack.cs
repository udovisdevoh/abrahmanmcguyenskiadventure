using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackGuitarMetalBlack : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffGuitarMetalBlack());
            metaRiffList.Add(new MetaRiffGuitarMetalBlack());
            return metaRiffList;
        }
    }
}
