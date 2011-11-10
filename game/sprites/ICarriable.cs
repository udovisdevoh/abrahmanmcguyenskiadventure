using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprites that can be carried
    /// </summary>
    internal interface ICarriable
    {
        double XPosition
        {
            get;
            set;
        }

        double YPosition
        {
            get;
            set;
        }
    }
}
