using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Fire ball or shuriken
    /// </summary>
    interface IPlayerProjectile
    {
        bool IsNoAiDefaultDirectionWalkingRight
        {
            set;
        }

        bool IsCurrentlyInFreeFallX
        {
            set;
        }
        
        bool IsCurrentlyInFreeFallY
        {
            set;
        }

        double CurrentWalkingSpeed
        {
            set;
        }

        double CurrentJumpAcceleration
        {
            set;
        }

        double MaxWalkingSpeed
        {
            get;
            set;
        }
    }
}
