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
        float MinDistanceFromPlayerToStartCountDown
        {
            get;
        }

        Cycle CountDownCycle
        {
            get;
        }
    }
}
