using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Wheel or seesaw
    /// </summary>
    interface IWheel
    {
        bool IsShowCircumference
        {
            get;
        }

        double Radius
        {
            get;
        }
    }
}
