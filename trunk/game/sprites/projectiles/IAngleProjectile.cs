using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    interface IAngleProjectile
    {
        byte AngleIndex
        {
            get;
            set;
        }
    }
}
