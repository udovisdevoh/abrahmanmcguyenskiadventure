using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite not moving, not affected by gravity
    /// </summary>
    abstract class StaticSprite : AbstractSprite
    {
        public StaticSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
        }
    }
}
