using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages growth of vines and stuff like that
    /// </summary>
    internal class ClimbableManager
    {
        internal void Update(IClimbable climbable, PlayerSprite playerSpriteReference, double timeDelta)
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
    }
}
