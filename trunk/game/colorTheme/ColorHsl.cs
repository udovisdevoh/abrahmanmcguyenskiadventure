using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a HSL color set
    /// </summary>
    internal class ColorHsl
    {
        #region Fields
        /// <summary>
        /// Hue
        /// </summary>
        private int hue;

        /// <summary>
        /// Saturation
        /// </summary>
        private int saturation;

        /// <summary>
        /// Lightness
        /// </summary>
        private int lightness;
        #endregion

        #region Constructor
        /// <summary>
        /// Build HSL color
        /// </summary>
        /// <param name="random">random number generator</param>
        public ColorHsl(Random random)
        {
            hue = random.Next(0, 256);
            saturation = random.Next(24, 224);
            lightness = random.Next(32, 256);
        }

        /// <summary>
        /// Create HSL color
        /// </summary>
        /// <param name="hue">Hue</param>
        /// <param name="saturation">Saturation</param>
        /// <param name="lightness">Lightness</param>
        public ColorHsl(int hue, int saturation, int lightness)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.lightness = lightness;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get color
        /// </summary>
        /// <returns>color</returns>
        internal Color GetColor()
        {
            return ColorTheme.ColorFromHSV(hue, saturation / 256.0, lightness / 256.0);
        }

        /// <summary>
        /// Clone HSL color
        /// </summary>
        /// <returns>Clone HSL color</returns>
        internal ColorHsl Clone()
        {
            return new ColorHsl(hue, saturation, lightness);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Hue
        /// </summary>
        public int Hue
        {
            get { return hue; }
            set { hue = value; }
        }

        /// <summary>
        /// Saturation
        /// </summary>
        public int Saturation
        {
            get { return saturation; }
            set { saturation = value; }
        }

        /// <summary>
        /// Lightness
        /// </summary>
        public int Lightness
        {
            get { return lightness; }
            set { lightness = value; }
        }
        #endregion
    }
}
