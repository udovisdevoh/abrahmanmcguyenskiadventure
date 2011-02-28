using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// For physics
    /// </summary>
    internal class Physics
    {
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta)
        {
            if (sprite.Ground != null)
            {
                sprite.CurrentJumpAcceleration = 0;
            }
            else
            {
                Ground closestDownGround = GetClosestDownGround(sprite, level);
                if (closestDownGround == null)
                {
                    sprite.CurrentJumpAcceleration = 0;
                }
                else
                {
                    double closestDownGroundHeight = closestDownGround.TerrainWave[sprite.XPosition];
                    sprite.YPosition += sprite.CurrentJumpAcceleration / 500 * timeDelta;
                    sprite.CurrentJumpAcceleration -= 1.0 * timeDelta;

                    if (sprite.YPosition <= closestDownGroundHeight)
                    {
                        sprite.YPosition = closestDownGroundHeight;
                        sprite.Ground = closestDownGround;
                    }
                }
            }
        }

        /// <summary>
        /// Closest down ground
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>Closest down ground</returns>
        private Ground GetClosestDownGround(AbstractSprite sprite, Level level)
        {
            #warning Fix code: find closest ground that is not above sprite

            Ground closestDownGround = null;
            double closestDistance = -1;

            foreach (Ground ground in level)
            {
                double currentHeight = ground.TerrainWave[sprite.XPosition];
                double distance = Math.Abs(sprite.XPosition - currentHeight);

                if (sprite.YPosition >= currentHeight)
                {
                    if (closestDistance == -1 || distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestDownGround = ground;
                    }
                }
            }
            return closestDownGround;
        }
    }
}