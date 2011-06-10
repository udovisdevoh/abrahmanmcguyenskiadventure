using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// A vortex (to change environment)
    /// </summary>
    internal class VortexSprite : StaticSprite
    {
        #region Fields and parts
        /// <summary>
        /// Surface 1
        /// </summary>
        private static Surface surface1;

        /// <summary>
        /// Surface 2
        /// </summary>
        private static Surface surface2;

        /// <summary>
        /// Surface 3
        /// </summary>
        private static Surface surface3;

        /// <summary>
        /// Rotation cycle
        /// </summary>
        private Cycle rotateCycle;

        /// <summary>
        /// Seed for destination world
        /// </summary>
        private int destinationSeed;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public VortexSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            destinationSeed = random.Next();
            rotateCycle = new Cycle(20, true);
            rotateCycle.Fire();
            if (surface1 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex3.png");
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return true;
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
            return 0;
        }

        protected override double BuildWidth(Random random)
        {
            return 4;
        }

        protected override double BuildHeight(Random random)
        {
            return 4;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        public override SdlDotNet.Graphics.Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            int cycleDivision = rotateCycle.GetCycleDivision(3);

            rotateCycle.Increment(1.0);

            if (cycleDivision == 1)
            {
                return surface1;
            }
            else if (cycleDivision == 2)
            {
                return surface2;
            }
            else
            {
                return surface3;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Seed for destination world
        /// </summary>
        internal int DestinationSeed
        {
            get { return destinationSeed; }
        }
        #endregion
    }
}
