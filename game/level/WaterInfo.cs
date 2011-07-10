using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Information about water in level
    /// </summary>
    internal class WaterInfo
    {
        #region Fields and parts
        /// <summary>
        /// Height of water
        /// </summary>
        private double height;

        /// <summary>
        /// Color of water
        /// </summary>
        private Color color;
        #endregion

        #region Constructor
        /// <summary>
        /// Create information about water
        /// </summary>
        /// <param name="colorHsl">sky's color</param>
        /// <param name="random">random number generator</param>
        public WaterInfo(ColorHsl colorHsl, Random random)
        {
            height = random.NextDouble() * (double)Program.totalHeightTileCount - ((double)Program.totalHeightTileCount / 2.0);
            color = colorHsl.GetColor();
            color = Color.FromArgb(64, color);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Height of water
        /// </summary>
        public double Height
        {
            get { return height; }
        }

        /// <summary>
        /// Color of water
        /// </summary>
        public Color Color
        {
            get { return color; }
        }
        #endregion
    }
}
