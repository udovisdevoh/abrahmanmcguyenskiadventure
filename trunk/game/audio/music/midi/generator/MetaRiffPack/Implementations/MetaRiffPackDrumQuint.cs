using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackDrumQuint : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            //metaRiffList.Add(new QuadDrum());
            metaRiffList.Add(new MetaRiffDrumQuint());
            metaRiffList.Add(new MetaRiffDrumQuint());
            return metaRiffList;
        }
    }
}
