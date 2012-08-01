using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a platform attached to a wheel or any mechanical component
    /// </summary>
    #warning Eventually remove abstract keyword
    internal abstract class Platform : AbstractLinkage
    {
        protected override bool BuildIsAffectedByGravity()
        {
            throw new NotImplementedException();
        }

        protected override double BuildWidth(Random random)
        {
            throw new NotImplementedException();
        }

        protected override double BuildHeight(Random random)
        {
            throw new NotImplementedException();
        }

        protected override double BuildBounciness()
        {
            throw new NotImplementedException();
        }

        protected override double BuildMaxFallingSpeed()
        {
            throw new NotImplementedException();
        }

        protected override string BuildTutorialComment()
        {
            return "You can jump on moving platforms.";
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            throw new NotImplementedException();
        }

        public override double BuildSupportHeight()
        {
            throw new NotImplementedException();
        }
    }
}
