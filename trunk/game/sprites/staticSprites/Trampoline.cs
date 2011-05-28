using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a trampoline
    /// </summary>
    class Trampoline : StaticSprite
    {
        #region Fields and parts
        private Surface surface;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public Trampoline(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            surface = BuildSpriteSurface("./assets/rendered/staticSprites/trampoline.png");
        }
        #endregion

        #region Override Methods
        protected override bool BuildIsAffectedByGravity()
        {
            return true;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
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
            return 1.0;
        }

        protected override double BuildBounciness()
        {
            return 1.5;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0.1;
            return surface;
        }
        #endregion
    }
}
