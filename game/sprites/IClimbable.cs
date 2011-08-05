using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprites on which we can walk up and down
    /// </summary>
    interface IClimbable
    {
        bool IsGrowing
        {
            get;
            set;
        }

        double Height
        {
            get;
            set;
        }

        double MaxHeight
        {
            get;
        }

        double GrowthSpeed
        {
            get;
        }

        double XPosition
        {
            get;
        }

        double YPosition
        {
            get;
        }

        double TopBound
        {
            get;
        }
    }
}
