using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Pipe sprite
    /// </summary>
    internal class PipeSprite : StaticSprite
    {
        #region Field and parts
        private static Surface upSideSurface;

        private static Surface downSideSurface;

        private Surface coloredSurface = null;

        /// <summary>
        /// Whether the pipe is upside
        /// </summary>
        private bool isUpSide;

        /// <summary>
        /// Sprite linked to it
        /// </summary>
        private PipeSprite linkedPipe = null;

        private DrillSprite linkedDrill = null;
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
            if (upSideSurface == null)
            {
                upSideSurface = BuildSpriteSurface("./assets/rendered/staticSprites/pipe.png");
                downSideSurface = upSideSurface.CreateFlippedVerticalSurface();
            }

            coloredSurface = new Surface(upSideSurface.GetWidth(), upSideSurface.GetHeight());
            coloredSurface.Fill(upSideSurface.GetRectangle(), new ColorHsl(random).GetColor());
            coloredSurface.Transparent = true;

            if (isUpSide)
                coloredSurface.Blit(upSideSurface);
            else
                coloredSurface.Blit(downSideSurface);
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
            return coloredSurface;
        }
        #endregion

        #region Properties
        public PipeSprite LinkedPipe
        {
            get
            {
                return linkedPipe;
            }
            set
            {
                if (value != this && value != null)
                {
                    linkedPipe = value;
                    linkedPipe.linkedPipe = this;
                    linkedPipe.coloredSurface = this.coloredSurface;

                    if (linkedPipe.isUpSide != this.isUpSide)
                        linkedPipe.coloredSurface = linkedPipe.coloredSurface.CreateFlippedVerticalSurface();
                }
            }
        }

        public DrillSprite LinkedDrill
        {
            get { return linkedDrill; }
            set { linkedDrill = value; }
        }

        public Surface ColoredSurface
        {
            get { return coloredSurface; }
            set { coloredSurface = value; }
        }

        public bool IsUpSide
        {
            get { return isUpSide; }
        }
        #endregion
    }
}
