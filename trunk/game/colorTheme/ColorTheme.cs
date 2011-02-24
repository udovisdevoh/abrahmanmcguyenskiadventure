using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    internal class ColorTheme
    {
        #region Fields
        public int hue;

        public int saturation;

        public int lightness;

        public int saturationShiftRate;

        public int lightnessShiftRate;

        public int hueShiftRate;

        private List<Color> colorList;
        #endregion

        #region Constructor
        public ColorTheme(Random random)
        {
            colorList = new List<Color>();
            hue = random.Next(0, 256);
            saturation = random.Next(0, 256);
            lightness = random.Next(128, 192);

            hueShiftRate = random.Next(-24, 24);
            saturationShiftRate = random.Next(-32, -16);
            lightnessShiftRate = random.Next(-64, -32);
        }
        #endregion

        #region Internal methods
        internal Color GetColor(int themeColorId)
        {
            if (colorList.Count - 1 < themeColorId)
                buildColor(themeColorId);
            return colorList[themeColorId];
        }
        
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        #endregion

        #region Private methods
        private void buildColor(int themeColorId)
        {
            if (colorList.Count < themeColorId)
                buildColor(themeColorId - 1);

            int currentHue = hue;
            int currentSaturation = saturation;
            int currentLightness = lightness;

            currentHue += hueShiftRate * themeColorId;
            currentSaturation += saturationShiftRate * themeColorId;
            currentLightness += lightnessShiftRate * themeColorId;

            currentHue = Math.Max(0, currentHue);
            currentSaturation = Math.Max(0, currentSaturation);
            currentLightness = Math.Max(0, currentLightness);

            currentHue = Math.Min(255, currentHue);
            //currentSaturation = Math.Min(255, currentSaturation);
            while (currentSaturation < 0)
                currentSaturation += 256;

            //currentLightness = Math.Min(255, currentLightness);
            while (currentLightness < 0)
                currentLightness += 256;
            currentLightness = Math.Max(32, currentLightness);

            Color color = ColorFromHSV(currentHue, currentSaturation / 256.0, currentLightness / 256.0);

            colorList.Add(color);
        }
        #endregion
    }
}
