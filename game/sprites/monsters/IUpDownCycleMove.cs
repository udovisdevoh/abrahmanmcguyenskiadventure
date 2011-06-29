using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    interface IUpDownCycleMove
    {
        Cycle UpDownCycle
        {
            get;
        }

        bool IsUseDontMoveUpDistance
        {
            get;
        }

        double DontMoveUpDistance
        {
            get;
        }
    }
}
