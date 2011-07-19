using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackMeditativeChina : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffShamisenChinese());
            metaRiffList.Add(new MetaRiffShamisenChinese());
            metaRiffList.Add(new MetaRiffPadAum());
            metaRiffList.Add(new MetaRiffPadAum());
            return metaRiffList;
        }
    }
}
