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
    #warning Eventually remove abstract keyword
    internal abstract class Wheel : AbstractLinkage
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
            return "Play with that ferris wheel, it's fun! Be careful when there are flail balls attached to it.";
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
