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
        internal void Update(IFlyingOnEqualDistance flyingSprite, SideScrollerSprite playerSprite, double timeDelta)
        {
            #region We manage the behavior of a boo that stops when being looked at
            if (flyingSprite.IsOnlyMoveWhenNotBeingLookedAt)
            {
                if ((playerSprite.IsTryingToWalkRight && flyingSprite.XPosition > playerSprite.XPosition) || (!playerSprite.IsTryingToWalkRight && flyingSprite.XPosition < playerSprite.XPosition))
                {
                    return;
                }
            }
            #endregion

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
