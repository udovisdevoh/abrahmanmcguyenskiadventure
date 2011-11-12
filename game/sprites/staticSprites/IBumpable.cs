﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Bumpable sprite
    /// </summary>
    interface IBumpable
    {
        Cycle BumpCycle
        {
            get;
        }
    }
}
