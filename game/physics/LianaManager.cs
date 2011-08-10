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

            if (playerSpriteReference.ClimbingOn == lianaSprite)
            {
                double yOnLiana = playerSpriteReference.YPosition - (lianaSprite.YPosition - lianaSprite.Height);
                playerSpriteReference.XPosition = lianaSprite.GetXPositionAt(yOnLiana) + lianaSprite.XPosition;
                lianaSprite.MovementCycle.IsFired = true;
            }
        }
    }
}
