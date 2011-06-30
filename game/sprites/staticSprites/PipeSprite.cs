using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Pipe sprite
    /// </summary>
    internal class PipeSprite : StaticSprite
    {
        #region Field and parts
        private static Surface surface;

        /// <summary>
        /// Whether the pipe is upside
        /// </summary>
        private bool isUpSide;
        #endregion

        #region Constructor
        public PipeSprite(double xPosition, double yPosition, Random random)
            :this(xPosition,yPosition,true,random)
        {
        }

        public PipeSprite(double xPosition, double yPosition, bool isUpSide, Random random)
            : base(xPosition, yPosition, random)
        {
            this.isUpSide = isUpSide;
            if (surface == null)
                surface = BuildSpriteSurface("./assets/rendered/staticSprites/pipe.png");
        }
        #endregion

        #region Override Methods
        protected override bool BuildIsImpassable()
        {
            return true;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 1000;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 2.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.5;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return surface;
        }
        #endregion
    }
}
