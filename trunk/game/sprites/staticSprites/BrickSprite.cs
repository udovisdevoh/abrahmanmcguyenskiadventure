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
    internal class BrickSprite : StaticSprite, IBumpable
    {
        #region Fields
        private static Surface destructibleSurface;

        private static Surface indestructibleSurface;

        private static Surface destroyedSurface;

        private Cycle bumpCycle;
        #endregion

        #region Constructors
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BrickSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            bumpCycle = new Cycle(10, false);
            if (destructibleSurface == null || indestructibleSurface == null)
            {
                indestructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock2.png");
                destructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock1.png");
                destroyedSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock3.png");
            }
        }

        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="isDestructible">whether sprite is destructible (default: false)</param>
        /// <param name="random">random number generator</param>
        public BrickSprite(float xPosition, float yPosition, Random random, bool isDestructible)
            : base(xPosition, yPosition, random, isDestructible)
        {
            bumpCycle = new Cycle(10, false);
            if (destructibleSurface == null || indestructibleSurface == null)
            {
                indestructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock2.png");
                destructibleSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock1.png");
                destroyedSurface = BuildSpriteSurface("./assets/rendered/staticSprites/brickBlock3.png");
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override float BuildMaxHealth()
        {
            return 1.0;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override float BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override float BuildBounciness()
        {
            return 0;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = yOffset = 0;
            bumpCycle.Increment(1);
            if (bumpCycle.IsFired)
                yOffset = bumpCycle.CurrentValue / -20.0;

            if (!IsAlive)
                return destroyedSurface;
            else if (IsDestructible)
                return destructibleSurface;
            else
                return indestructibleSurface;
        }
        #endregion

        #region IBumpable Members
        public Cycle BumpCycle
        {
            get { return bumpCycle; }
        }
        #endregion
    }
}
