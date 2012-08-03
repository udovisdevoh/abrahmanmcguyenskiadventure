using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a seesaw
    /// </summary>
    internal class SeeSaw : AbstractBearing
    {
        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
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

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override string BuildTutorialComment()
        {
            return "Play with that seesaw, it's fun!\nBe careful when there are flail balls attached to it.";
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
        public SeeSaw(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
        }

        public SeeSaw(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : base(xPosition, yPosition, random, isAffectedByGravity, supportHeight)
        {
        }
        #endregion
    }
}
