using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Color theme for a level
    /// </summary>
    internal class ColorTheme
    {
        #region Fields
        /// <summary>
        /// Hue: from 0 to 255
        /// </summary>
        public int hue;

        /// <summary>
        /// Saturation: from 0 to 255
        /// </summary>
        public int saturation;

        /// <summary>
        /// Lightness: from 0 to 255
        /// </summary>
        public int lightness;

        /// <summary>
        /// How much the saturation changes when changing layers
        /// </summary>
        public int saturationShiftRate;

        /// <summary>
        /// How much the lightness changes when changing layers
        /// </summary>
        public int lightnessShiftRate;

        /// <summary>
        /// How much the hue changes when changing layers
        /// </summary>
        public int hueShiftRate;

        /// <summary>
        /// List of color (one per layer)
        /// </summary>
        private List<Color> colorList;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a color theme
        /// </summary>
        /// <param name="random">random number generator</param>
        public ColorTheme(Random random)
        {
            colorList = new List<Color>();
            hue = random.Next(0, 256);
            saturation = random.Next(0, 256);
            lightness = random.Next(128, 192);

            hueShiftRate = random.Next(-24, 24);
            saturationShiftRate = random.Next(-32, -16);
            lightnessShiftRate = random.Next(-64, -32);

            int startingColorCount = random.Next(2, 7); //Some other color may appear later, and some color may not be used in level
            for (int i = 0; i < startingColorCount; i++)
                BuildColor(i);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Get color at layer index
        /// </summary>
        /// <param name="themeColorId">layer index</param>
        /// <returns>color at layer index</returns>
        internal Color GetColor(int themeColorId)
        {
            if (colorList.Count - 1 < themeColorId)
                BuildColor(themeColorId);
            return colorList[themeColorId];
        }
        
        /// <summary>
        /// Create color from hue saturation lightness (value)
        /// </summary>
        /// <param name="hue">hue</param>
        /// <param name="saturation">saturation</param>
        /// <param name="value">lightness (value)</param>
        /// <returns>color from hue saturation lightness (value)</returns>
        internal static Color ColorFromHSV(double hue, double saturation, double value)
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
        /// <summary>
        /// Build color at layer index
        /// </summary>
        /// <param name="themeColorId">layer index</param>
        private void BuildColor(int themeColorId)
        {
            if (colorList.Count < themeColorId)
                BuildColor(themeColorId - 1);

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

        #region Properties
        /// <summary>
        /// How many colors
        /// </summary>
        public int Count
        {
            get { return colorList.Count; }
        }
        #endregion
    }
}
