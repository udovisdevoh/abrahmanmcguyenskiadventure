using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackMeditativeJamaican : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffSteelDrum());
            metaRiffList.Add(new MetaRiffSteelDrum());
            metaRiffList.Add(new MetaRiffGuitarFolkBinary());
            metaRiffList.Add(new MetaRiffGuitarFolkBinary());
            metaRiffList.Add(new MetaRiffPadAum());
            metaRiffList.Add(new MetaRiffPadAum());
            return metaRiffList;
        }
    }
}
