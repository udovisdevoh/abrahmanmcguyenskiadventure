using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages growth of vines and stuff like that
    /// </summary>
    internal class IClimbableManager
    {
        #region Fields and Parts
        private LianaManager lianaManager = new LianaManager();
        #endregion

        #region Internal Methods
        internal void UpdateClimber(SideScrollerSprite sprite, SideScrollerSprite potentialClimbable, IClimbable wasClimbingOnAtPreviousFrame, UserInput userInput)
        {
            if (sprite.YPosition >= potentialClimbable.YPosition && userInput.isPressDown)
                return;

            if (!(sprite is IPlayerProjectile) && !(sprite is BeaverSprite) && (!(sprite is PlayerSprite) || !((PlayerSprite)sprite).IsBeaver || potentialClimbable is LianaSprite))
            {
                if (sprite is MonsterSprite || userInput.isPressUp || !((IClimbable)potentialClimbable).IsPlayerNeedToWalkUpToBind || wasClimbingOnAtPreviousFrame == potentialClimbable)
                {
                    if (!((IClimbable)potentialClimbable).IsNeedToBeInAirToBind || sprite.IGround == null)
                    {
                        if (sprite.IgnoreThisIClimbable != potentialClimbable)
                        {
                            if (!(potentialClimbable is LianaSprite) || !sprite.IsInWater)
                            {
                                if (sprite.IGround != null && Math.Abs(sprite.YPosition - sprite.IGround[sprite.XPosition]) >= sprite.MinimumFallingHeight)
                                    sprite.IGround = null;
                                sprite.IClimbingOn = (IClimbable)potentialClimbable;
                            }
                        }
                    }
                }
            }
        }

        internal void UpdateClimbable(IClimbable climbable, PlayerSprite playerSpriteReference, Level level, double timeDelta)
        {            
            if (!climbable.IsGrowing)
                return;

            double previousHeight = climbable.Height;

            climbable.Height += climbable.GrowthSpeed * timeDelta;

            if (climbable.Height >= climbable.MaxHeight)
            {
                climbable.Height = climbable.MaxHeight;
                climbable.IsGrowing = false;
            }
            else if (level.Ceiling != null && climbable.YPosition - climbable.Height <= level.Ceiling[climbable.XPosition])
            {
                climbable.IsGrowing = false;
            }

            if (playerSpriteReference.IClimbingOn == climbable)
            {
                playerSpriteReference.YPosition -= (climbable.Height - previousHeight);
            }
        }

        internal void ClimbUp(PlayerSprite playerSprite)
        {
            playerSprite.YPosition -= playerSprite.MaxWalkingSpeed / 3.0;

            if (playerSprite.YPosition < playerSprite.IClimbingOn.TopBound)
                playerSprite.YPositionKeepPrevious = playerSprite.IClimbingOn.TopBound + 0.01;

            playerSprite.CurrentWalkingSpeed = 0.0;

            if (playerSprite.IClimbingOn is LianaSprite)
                lianaManager.UpdateXPositionOnMovingLiana(playerSprite, (LianaSprite)playerSprite.IClimbingOn);
        }

        internal void ClimbDown(PlayerSprite playerSprite)
        {
            playerSprite.YPosition += playerSprite.MaxWalkingSpeed / 3.0;
            
            if (playerSprite.IClimbingOn is LianaSprite)
                playerSprite.YPositionKeepPrevious = Math.Min(playerSprite.IClimbingOn.YPosition - playerSprite.IClimbingOn.Height + ((LianaSprite)playerSprite.IClimbingOn).GetAdjustedHeight(), playerSprite.YPosition);
            else
                playerSprite.YPositionKeepPrevious = Math.Min(playerSprite.IClimbingOn.YPosition, playerSprite.YPosition);

            playerSprite.CurrentWalkingSpeed = 0.0;
        }
        #endregion
    }
}