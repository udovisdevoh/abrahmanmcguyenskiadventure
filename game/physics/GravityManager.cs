using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages stuff that are falling
    /// </summary>
    internal class GravityManager
    {
        #region Internal Methods
        /// <summary>
        /// Apply gravity to sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void ApplyGravity(AbstractSprite sprite, Level level, double timeDelta)
        {
            if (sprite.Ground != null)
            {
                sprite.CurrentJumpAcceleration = 0;
            }
            else
            {
                Ground closestDownGround = GroundHelper.GetHighestVisibleGroundBelowSprite(sprite, level);
                if (closestDownGround == null)
                {
                    sprite.Ground = GroundHelper.GetLowestVisibleGround(sprite, level);
                    sprite.YPosition = sprite.Ground[sprite.XPosition];
                    sprite.CurrentJumpAcceleration = 0;
                }
                else
                {
                    double closestDownGroundHeight = closestDownGround[sprite.XPosition];
                    sprite.YPosition -= sprite.CurrentJumpAcceleration / 50 * timeDelta;

                    if (!sprite.IsTryingToJump || sprite.JumpingCycle.IsFinished)
                    {
                        sprite.CurrentJumpAcceleration -= 4.0 * timeDelta;
                    }

                    if (sprite.YPosition >= closestDownGroundHeight)
                    {
                        sprite.YPosition = closestDownGroundHeight;
                        sprite.Ground = closestDownGround;
                    }
                }
            }
        }
        #endregion
    }
}
