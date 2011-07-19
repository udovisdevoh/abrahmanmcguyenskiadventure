using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackDrumTernaryTwo : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffDrumQuad());
            metaRiffList.Add(new MetaRiffDrumQuad());
            metaRiffList.Add(new MetaRiffDrumTernary());
            metaRiffList.Add(new MetaRiffDrumTernary());
            return metaRiffList;
        }
    }
}
