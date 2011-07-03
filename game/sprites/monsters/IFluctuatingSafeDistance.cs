using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite with fluctuating distance
    /// </summary>
    interface IFluctuatingSafeDistance
    {
        /// <summary>
        /// Fluctuating safe distance
        /// </summary>
        Cycle FluctuatingSafeDistanceCycle
        {
            get;
        }

        double GetCurrentSafeDistance();
    }
}