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
            double heightBeforeMovement = -1;//Warning, must not keep this value
            if (lianaSprite.MovementCycle.IsFired)
            {
                heightBeforeMovement = lianaSprite.GetAdjustedHeight();
                lianaSprite.MovementCycle.Increment(timeDelta * 1.5);
            }

            if (playerSpriteReference.IClimbingOn == lianaSprite)
            {
                if (!lianaSprite.MovementCycle.IsFired)
                {
                    lianaSprite.MovementCycle.IsFired = true;
                    UpdateXPositionOnMovingLiana(playerSpriteReference, lianaSprite);
                    playerSpriteReference.JumpingCycle.StopAndReset();
                    playerSpriteReference.CurrentJumpAcceleration = 0.0;
                    playerSpriteReference.CurrentWalkingSpeed = 0.0;
                    playerSpriteReference.IsNeedToJumpAgain = true;
                    return;
                }

                UpdateXPositionOnMovingLiana(playerSpriteReference, lianaSprite);
                double heightAfterMovement = lianaSprite.GetAdjustedHeight();
                double fluctuationOffset = (-heightBeforeMovement + heightAfterMovement);
                double fluctuationRatio = (playerSpriteReference.YPosition - (lianaSprite.YPosition - lianaSprite.Height) - playerSpriteReference.Height / 2.0) / lianaSprite.Height;
                playerSpriteReference.YPosition += fluctuationOffset * fluctuationRatio;
            }
        }

        internal void UpdateXPositionOnMovingLiana(PlayerSprite playerSprite, LianaSprite lianaSprite)
        {
            double yOnLiana = playerSprite.YPosition - (lianaSprite.YPosition - lianaSprite.Height) - playerSprite.Height / 2.0;
            playerSprite.XPosition = lianaSprite.GetXPositionAt(yOnLiana) + lianaSprite.XPosition;
        }

        internal void ForeceLeaveLianaRange(AbstractSprite sprite, LianaSprite lianaSprite)
        {
            if (sprite.IsTryingToWalkRight)
            {
                sprite.XPosition += (sprite.Width + 1.0);
            }
            else
            {
                sprite.XPosition -= (sprite.Width + 1.0);
            }
        }
    }
}
