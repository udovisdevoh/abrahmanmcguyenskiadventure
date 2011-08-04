using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a vine
    /// </summary>
    class VineSprite : StaticSprite, IClimbable
    {
        #region Constants
        /// <summary>
        /// Growth speed
        /// </summary>
        private const double growthSpeed = 0.25;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Maximum growing height
        /// </summary>
        private double maxHeight;

        /// <summary>
        /// Whether vine is currently growing
        /// </summary>
        private bool isGrowing;

        /// <summary>
        /// Component surface
        /// </summary>
        private static Surface componentSurface;

        private static Dictionary<int, Surface> compositeSurfaceCache = new Dictionary<int, Surface>();

        private static Dictionary<int, Surface> microAdjustedSurfaceCache = new Dictionary<int, Surface>();
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public VineSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            isGrowing = true;
            if (componentSurface == null)
            {
                componentSurface = BuildSpriteSurface("./assets/rendered/staticSprites/vine.png");
            }
            maxHeight = (double)random.Next(7, 32);
        }
        #endregion

        #region Internal Methods
        internal static void ClearCompositeSurfaces()
        {
            compositeSurfaceCache.Clear();
            microAdjustedSurfaceCache.Clear();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Maximum height
        /// </summary>
        public double MaxHeight
        {
            get { return maxHeight; }
        }
        #endregion

        #region Override Methods
        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        public bool IsGrowing
        {
            get { return isGrowing; }
            set { isGrowing = value; }
        }

        protected override double BuildMaxHealth()
        {
            return 100.0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 5.0;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        protected override string BuildTutorialComment()
        {
            return "You can climb this vine.";
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            int heightInt = (int)Math.Ceiling(Height);
            Surface currentSurface = GetCompositeSurface(heightInt);

            if (Height != (double)heightInt)
            {
                int preciseHeight = (int)(Height * Program.tileSize);

                preciseHeight /= 4;
                preciseHeight *= 4;

                Surface microAdjustedSurface;

                if (!microAdjustedSurfaceCache.TryGetValue(preciseHeight, out microAdjustedSurface))
                {
                    microAdjustedSurface = currentSurface.CreateResizedSurface(new Size(Program.tileSize, preciseHeight));
                    microAdjustedSurfaceCache.Add(preciseHeight, microAdjustedSurface);
                }

                currentSurface = microAdjustedSurface;
            }

            return currentSurface;
        }

        public double GrowthSpeed
        {
            get { return growthSpeed; }
        }
        #endregion

        #region Private Methods
        private Surface GetCompositeSurface(int heightInt)
        {
            if (heightInt < 2)
                return componentSurface;

            Surface surface;
            if (!compositeSurfaceCache.TryGetValue(heightInt, out surface))
            {
                surface = BuildCompositeSurface(heightInt);
                compositeSurfaceCache.Add(heightInt, surface);
            }
            return surface;
        }

        private Surface BuildCompositeSurface(int heightInt)
        {
            Surface compositeSurface = new Surface(Program.tileSize, Program.tileSize * heightInt);
            compositeSurface.Transparent = true;
            compositeSurface.Blit(GetCompositeSurface(heightInt - 1), new Point(0, Program.tileSize), new Rectangle(0, 0, Program.tileSize, Program.tileSize * (heightInt - 1)));
            compositeSurface.Blit(componentSurface, new Point(0, 0), new Rectangle(0, 0, Program.tileSize, Program.tileSize));
            return compositeSurface;
        }
        #endregion
    }
}
