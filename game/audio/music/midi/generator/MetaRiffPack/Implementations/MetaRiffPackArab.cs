using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackArab : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffGuitarArab());
            metaRiffList.Add(new MetaRiffGuitarArab());
            metaRiffList.Add(new MetaRiffViolinArab());
            metaRiffList.Add(new MetaRiffViolinArab());
            //metaRiffList.Add(new MetaRiffTrumpetTurkish());
            return metaRiffList;
        }
    }
}
