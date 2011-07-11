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
        private int height;

        /// <summary>
        /// Height of water
        /// </summary>
        private double heightInDouble;

        /// <summary>
        /// Color of water
        /// </summary>
        private Color color;

        /// <summary>
        /// Color of the surface
        /// </summary>
        private Color edgeColor;
        #endregion

        #region Constructor
        /// <summary>
        /// Create information about water
        /// </summary>
        /// <param name="colorHsl">sky's color</param>
        /// <param name="random">random number generator</param>
        public WaterInfo(ColorHsl colorHsl, Random random)
        {
            height = random.Next(Program.totalHeightTileCount / -2, Program.totalHeightTileCount / 2);

            heightInDouble = (double)height;

            colorHsl = new ColorHsl(colorHsl.Hue, Math.Min(255, colorHsl.Saturation + 32), Math.Min(255, colorHsl.Lightness + 32));
            color = Color.FromArgb(96, colorHsl.GetColor());

            colorHsl = new ColorHsl(colorHsl.Hue, colorHsl.Saturation, Math.Min(255, colorHsl.Lightness + 128));
            edgeColor = Color.FromArgb(96, Color.White/*colorHsl.GetColor()*/);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Height of water (in tiles)
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        /// <summary>
        /// Height of water (in tiles)
        /// </summary>
        public double HeightInDouble
        {
            get { return heightInDouble; }
        }

        /// <summary>
        /// Color of water
        /// </summary>
        public Color Color
        {
            get { return color; }
        }

        /// <summary>
        /// Color of the surface
        /// </summary>
        public Color EdgeColor
        {
            get { return edgeColor; }
        }
        #endregion
    }
}