using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackGuitarFolkTernary : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffGuitarFolkBinary());
            metaRiffList.Add(new MetaRiffGuitarFolkTernary());
            metaRiffList.Add(new MetaRiffGuitarFolkTernary());
            return metaRiffList;
        }
    }
}
