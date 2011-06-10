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
        #endregion

        #region Properties
        /// <summary>
        /// Hue
        /// </summary>
        public int Hue
        {
            get { return hue; }
        }

        /// <summary>
        /// Saturation
        /// </summary>
        public int Saturation
        {
            get { return saturation; }
        }

        /// <summary>
        /// Lightness
        /// </summary>
        public int Lightness
        {
            get { return lightness; }
        }
        #endregion
    }
}
