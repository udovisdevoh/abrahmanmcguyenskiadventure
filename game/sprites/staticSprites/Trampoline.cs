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
        private static Surface surface;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public Trampoline(float xPosition, float yPosition, Random random)
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

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override float BuildMaxHealth()
        {
            return 1.0f;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.0f;
        }

        protected override float BuildWidth(Random random)
        {
            return 2.0f;
        }

        protected override float BuildHeight(Random random)
        {
            return 0.7f;
        }

        protected override float BuildBounciness()
        {
            return 1.5f;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = 0;
            yOffset = 0.1f;
            return surface;
        }
        #endregion
    }
}
