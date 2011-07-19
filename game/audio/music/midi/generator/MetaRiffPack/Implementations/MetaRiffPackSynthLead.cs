using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackSynthLead : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffSynthLeadRom());
            metaRiffList.Add(new MetaRiffSynthLeadRom());
            metaRiffList.Add(new MetaRiffSynthLeadRom());
            return metaRiffList;
        }
    }
}
