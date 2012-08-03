using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a wheel and its bearing
    /// </summary>
    internal class Wheel : AbstractBearing
    {
        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override string BuildTutorialComment()
        {
            return "Play with that ferris wheel, it's fun!\nBe careful when there are flail balls attached to it.";
        }

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return bearingSurface;
        }
        #endregion

        #region Constructors
        public Wheel(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
        }

        public Wheel(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : base(xPosition, yPosition, random, isAffectedByGravity, supportHeight)
        {
        }
        #endregion
    }
}
