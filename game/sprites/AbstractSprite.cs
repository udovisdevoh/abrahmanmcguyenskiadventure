using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a sprite
    /// </summary>
    internal abstract class AbstractSprite
    {
        #region Fields and parts
        /// <summary>
        /// X position
        /// </summary>
        protected double xPosition;

        /// <summary>
        /// Y position
        /// </summary>
        protected double yPosition;

        /// <summary>
        /// Mass
        /// </summary>
        protected double mass;

        /// <summary>
        /// Height
        /// </summary>
        protected double height;

        /// <summary>
        /// Width
        /// </summary>
        protected double width;
        #endregion

        #region Constructor
        /// <summary>
        /// Create abstract sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public AbstractSprite(double xPosition, double yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            width = BuildWidth();
            height = BuildHeight();
            mass = BuildMass();
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// sprite's width (0.5 = player's width)
        /// </summary>
        /// <returns>sprite's width (0.5 = player's width)</returns>
        protected abstract double BuildWidth();

        /// <summary>
        /// sprite's height (1.0 = player's height)
        /// </summary>
        /// <returns>sprite's height (1.0 = player's height)</returns>
        protected abstract double BuildHeight();

        /// <summary>
        /// sprite's mass (1.0 = player's mass)
        /// </summary>
        /// <returns>sprite's mass (1.0 = player's mass)</returns>
        protected abstract double BuildMass();
        #endregion

        #region Properties
        /// <summary>
        /// X position
        /// </summary>
        public double XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        /// <summary>
        /// Y position
        /// </summary>
        public double YPosition
        {
            get { return yPosition; }
            set { yPosition = value; }
        }

        /// <summary>
        /// Mass
        /// </summary>
        public double Mass
        {
            get { return mass; }
        }

        /// <summary>
        /// Width
        /// </summary>
        public double Width
        {
            get { return width; }
        }

        /// <summary>
        /// Height
        /// </summary>
        public double Height
        {
            get { return height; }
        }
        #endregion
    }
}
