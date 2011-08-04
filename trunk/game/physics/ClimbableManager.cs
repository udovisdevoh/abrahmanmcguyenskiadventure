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
        internal void Update(IClimbable climbable, Level level, PlayerSprite playerSpriteReference, double timeDelta)
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
                climbable.Height = climbable.XPosition - level.Ceiling[climbable.XPosition];
                climbable.IsGrowing = false;
            }

            if (playerSpriteReference.ClimbingOn == climbable)
            {
                playerSpriteReference.YPosition -= (climbable.Height - previousHeight);
            }
        }
    }
}
