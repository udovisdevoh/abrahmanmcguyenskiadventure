﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MetaRiffPackDrumBassBinary : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffDrumBassBinary());
            return metaRiffList;
        }
    }
}
