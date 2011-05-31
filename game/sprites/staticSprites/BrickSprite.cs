using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.level;
using System.Drawing;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents bricks (breakable or not), unbreakable are darker
    /// </summary>
    internal class BrickSprite : StaticSprite
    {
        #region Fields
        private static Surface destructibleSurface;

        private static Surface indestructibleSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BrickSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (destructibleSurface == null || indestructibleSurface == null)
            {
                indestructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock2.png");
                destructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock1.png");
            }
        }

        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="isDestructible">whether sprite is destructible (default: false)</param>
        /// <param name="random">random number generator</param>
        public BrickSprite(double xPosition, double yPosition, Random random, bool isDestructible)
            : base(xPosition, yPosition, random, isDestructible)
        {
            if (destructibleSurface == null || indestructibleSurface == null)
            {
                indestructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock2.png");
                destructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock1.png");
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
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
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            if (IsDestructible)
                return destructibleSurface;
            else
                return indestructibleSurface;
        }
        #endregion
    }
}
