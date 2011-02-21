using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.colorTheme
{
    internal class ColorTheme
    {
        #region Fields
        private int hue;

        private int saturation;

        private int lightness;

        private int saturationShiftRate;

        private int lightnessShiftRate;

        private int hueShiftRate;
        #endregion

        #region Constructor
        public ColorTheme(Random random)
        {
            hue = random.Next(0, 256);
            saturation = random.Next(0, 192);
            lightness = random.Next(128, 192);

            hueShiftRate = random.Next(-24, 24);
            saturationShiftRate = random.Next(-64, -4);
            lightnessShiftRate = random.Next(-64, -4);
        }
        #endregion
    }
}
