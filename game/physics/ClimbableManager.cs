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
    internal class ClimbableManager
    {
        internal void UpdateClimber(AbstractSprite sprite, AbstractSprite potentialClimbable, IClimbable wasClimbingOnAtPreviousFrame, UserInput userInput)
        {
            if (!(sprite is FireBallSprite) && !(sprite is BeaverSprite) && (!(sprite is PlayerSprite) || !((PlayerSprite)sprite).IsBeaver))
            {
                if (sprite is MonsterSprite || userInput.isPressUp || wasClimbingOnAtPreviousFrame == potentialClimbable)
                {
                    sprite.IGround = null;
                    sprite.ClimbingOn = (IClimbable)potentialClimbable;
                }
            }
        }

        internal void UpdateClimbable(IClimbable climbable, PlayerSprite playerSpriteReference, double timeDelta)
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

            if (playerSpriteReference.ClimbingOn == climbable)
            {
                playerSpriteReference.YPosition -= (climbable.Height - previousHeight);
            }
        }

        internal void ClimbUp(PlayerSprite playerSprite)
        {
            playerSprite.YPosition -= playerSprite.MaxWalkingSpeed / 3.0;

            if (playerSprite.YPosition < playerSprite.ClimbingOn.TopBound)
                playerSprite.YPositionKeepPrevious = playerSprite.ClimbingOn.TopBound + 0.01;

            playerSprite.CurrentWalkingSpeed = 0.0;
        }

        internal void ClimbDown(PlayerSprite playerSprite)
        {
            playerSprite.YPosition += playerSprite.MaxWalkingSpeed / 3.0;
            playerSprite.CurrentWalkingSpeed = 0.0;
        }
    }
}
