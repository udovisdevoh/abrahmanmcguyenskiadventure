using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Self-exploding monsters
    /// </summary>
    interface IExplodable
    {
        double MinDistanceFromPlayerToStartCountDown
        {
            get;
        }

        Cycle CountDownCycle
        {
            get;
        }
    }
}
