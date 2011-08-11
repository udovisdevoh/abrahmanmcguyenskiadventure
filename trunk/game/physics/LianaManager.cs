using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages lianas
    /// </summary>
    internal class LianaManager
    {
        internal void UpdateLiana(LianaSprite lianaSprite, PlayerSprite playerSpriteReference, double timeDelta)
        {
            if (lianaSprite.MovementCycle.IsFired)
                lianaSprite.MovementCycle.Increment(timeDelta);

            if (playerSpriteReference.IClimbingOn == lianaSprite)
            {
                UpdateXPositionOnMovingLiana(playerSpriteReference, lianaSprite);
                lianaSprite.MovementCycle.IsFired = true;
            }
        }

        internal void UpdateXPositionOnMovingLiana(PlayerSprite playerSprite, LianaSprite lianaSprite)
        {
            double yOnLiana = playerSprite.YPosition - (lianaSprite.YPosition - lianaSprite.Height);
            playerSprite.XPosition = lianaSprite.GetXPositionAt(yOnLiana) + lianaSprite.XPosition;
        }

        internal void ForeceLeaveLianaRange(AbstractSprite sprite, LianaSprite lianaSprite)
        {
            if (sprite.IsTryingToWalkRight)
            {
                sprite.XPosition += 2.0;
            }
            else
            {
                sprite.XPosition -= 2.0;
            }
        }
    }
}
