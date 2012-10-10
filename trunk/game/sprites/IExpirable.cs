using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprites that expire after some time if expiration cycle is fired
    /// </summary>
    interface IExpirable
    {
        Cycle ExpirationCycle
        {
            get;
        }
    }
}