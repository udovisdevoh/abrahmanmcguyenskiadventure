using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    interface IUpDownCycleMove
    {
        Cycle UpDownCycle
        {
            get;
        }

        double DontMoveUpDistance
        {
            get;
        }

        double XPosition
        {
            get;
        }

        double UpDownCycleHalfLength
        {
            get;
        }

        double AlwaysActiveRangeCycleStart
        {
            get;
        }

        double AlwaysActiveRangeCycleStop
        {
            get;
        }

        double GetCurrentUpDownCycleHeightOffset();

        IGround IGround
        {
            get;
        }
    }
}
