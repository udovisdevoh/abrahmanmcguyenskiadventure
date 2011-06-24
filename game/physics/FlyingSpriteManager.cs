using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages flying sprites
    /// </summary>
    internal class FlyingSpriteManager
    {
        internal void Update(IFlyingOnEqualDistance flyingSprite, AbstractSprite playerSprite, double timeDelta)
        {
            if (flyingSprite.YPosition > playerSprite.YPosition - flyingSprite.SafeYDistanceFromPlayer * 0.75)
            {
                flyingSprite.YPosition -= flyingSprite.FlyingYSpeed * timeDelta;
            }
            else if (flyingSprite.YPosition < playerSprite.YPosition - flyingSprite.SafeYDistanceFromPlayer * 1.5)
            {
                flyingSprite.YPosition += flyingSprite.FlyingYSpeed * timeDelta;
            }
        }
    }
}
