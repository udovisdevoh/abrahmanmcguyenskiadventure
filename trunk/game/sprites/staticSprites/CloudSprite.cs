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
    internal class CloudSprite : StaticSprite
    {
        #region Fields
        private static Surface surface;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public CloudSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            IsDestructible = false;
            if (surface == null)
            {
                surface = BuildSpriteSurface("./assets/rendered/staticSprites/cloud.png");
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

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return surface;
        }
        #endregion
    }
}