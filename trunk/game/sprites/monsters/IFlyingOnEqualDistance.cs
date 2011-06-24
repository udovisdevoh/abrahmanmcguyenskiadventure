using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// These sprites fly on a horizontal wave
    /// </summary>
    interface IFlyingOnEqualDistance
    {

        /// <summary>
        /// Safe height distance from player
        /// </summary>
        double SafeYDistanceFromPlayer
        {
            get;
        }

        /// <summary>
        /// Y position
        /// </summary>
        double YPosition
        {
            get;
            set;
        }

        /// <summary>
        /// Flying Y speed
        /// </summary>
        double FlyingYSpeed
        {
            get;
        }
    }
}