using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages lianas
    /// </summary>
    internal class LianaManager
    {
        private static LianaSprite ninjaRope = null;

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

        internal static void ResetNinjaRope()
        {
            ninjaRope = null;
        }

        internal void TryThrowNinjaRope(PlayerSprite playerSprite, Level level, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList, Random random)
        {
            if (ninjaRope != null)
            {
                spritePopulation.Remove(ninjaRope);
                if (playerSprite.IClimbingOn == ninjaRope)
                    playerSprite.IClimbingOn = null;
            }

            IGround attachedGround = IGroundHelper.GetLowestVisibleIGroundAboveSprite(playerSprite, level, visibleSpriteList, true);

            if (attachedGround == null)
                return;

            double yPosition = attachedGround[playerSprite.XPosition];

            if (Math.Abs(playerSprite.YPosition - yPosition) > 20)
                return;

            ninjaRope = new LianaSprite(playerSprite.XPosition, yPosition, random);

            if (playerSprite.IsTryingToWalkRight)
                ninjaRope.MovementCycle.CurrentValue = ninjaRope.MovementCycle.TotalTimeLength * 0.375;
            else
            {
                ninjaRope.MovementCycle.CurrentValue = ninjaRope.MovementCycle.TotalTimeLength * 0.625;
                ninjaRope.MovementCycle.Reverse();
            }

            spritePopulation.Add(ninjaRope);

            ninjaRope.YPosition += ninjaRope.Height;

            if (attachedGround is StaticSprite)
                ninjaRope.YPosition += ((StaticSprite)attachedGround).Height;

            if (ninjaRope.YPosition > playerSprite.TopBound)
            {
                playerSprite.IGround = null;
                playerSprite.IClimbingOn = ninjaRope;
                if (playerSprite.YPosition > ninjaRope.YPosition)
                    playerSprite.YPositionKeepPrevious = ninjaRope.YPosition;
            }

            SoundManager.PlayThrowSound();
        }
    }
}
