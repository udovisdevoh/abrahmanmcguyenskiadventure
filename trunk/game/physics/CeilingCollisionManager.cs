using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages ceiling collisions
    /// </summary>
    internal class CeilingCollisionManager
    {
        /// <summary>
        /// Update sprite to ceiling collisions
        /// </summary>
        /// <param name="spriteToUpdate">sprite</param>
        /// <param name="ceiling">ceiling</param>
        internal void Update(AbstractSprite sprite, Ground ceiling)
        {
            double ceilingHeight = ceiling[sprite.XPosition];
            if (sprite.TopBound < ceilingHeight)
            {
                double angleFromSpritePreviousPositionToBlock = Physics.GetAngleDegree(sprite.XPositionPrevious, sprite.TopBoundPrevious, sprite.XPosition, ceilingHeight);

                if (angleFromSpritePreviousPositionToBlock >= 45 && angleFromSpritePreviousPositionToBlock <= 135 && sprite.XPositionKeepPrevious != sprite.XPositionPrevious)
                {
                    sprite.TopBoundKeepPrevious = ceiling[sprite.XPosition];
                    if (sprite.CurrentJumpAcceleration > 0)
                        sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration / -4.0;
                }
                else
                {
                    sprite.XPositionKeepPrevious = sprite.XPositionPrevious;
                    ceilingHeight = ceiling[sprite.XPosition];
                    if (sprite.TopBound < ceilingHeight)
                    {
                        sprite.TopBoundKeepPrevious = ceiling[sprite.XPosition];
                        if (sprite.CurrentJumpAcceleration > 0)
                            sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration / -4.0;
                    }
                    else
                    {
                        sprite.CurrentWalkingSpeed = 0.0;
                    }
                }
            }
        }
    }
}
