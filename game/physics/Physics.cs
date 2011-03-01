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
        	ApplyGravity(sprite, level, timeDelta);
        }
        
        internal void TryMakeWalk(AbstractSprite sprite, bool isRight, double timeDelta)
        {
        	if (isRight)
        		sprite.XPosition += timeDelta;
        	else
        		sprite.XPosition -= timeDelta;
        	
        	if (sprite.Ground != null)
        	{        		
        		sprite.YPosition = sprite.Ground.TerrainWave[sprite.XPosition];
        	}
        }
        
        /// <summary>
        /// Apply gravity to sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        private void ApplyGravity(AbstractSprite sprite, Level level, double timeDelta)
        {
        	if (sprite.Ground != null)
            {
                sprite.CurrentJumpAcceleration = 0;
            }
            else
            {
                Ground closestDownGround = GetHighestGroundBelowSprite(sprite, level);
                if (closestDownGround == null)
                {
                    sprite.CurrentJumpAcceleration = 0;
                }
                else
                {
                    double closestDownGroundHeight = closestDownGround.TerrainWave[sprite.XPosition];
                    sprite.YPosition -= sprite.CurrentJumpAcceleration / 200 * timeDelta;
                    sprite.CurrentJumpAcceleration -= 1.0 * timeDelta;

                    if (sprite.YPosition >= closestDownGroundHeight)
                    {
                        sprite.YPosition = closestDownGroundHeight;
                        sprite.Ground = closestDownGround;
                    }
                }
            }
        }

        /// <summary>
        /// Highest ground below sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>Highest ground below sprite</returns>
        private Ground GetHighestGroundBelowSprite(AbstractSprite sprite, Level level)
        {
            Ground highestGroundBelowSprite = null;
            double highestHeight = -1;

            foreach (Ground ground in level)
            {
                double currentHeight = ground.TerrainWave[sprite.XPosition];

                if (sprite.YPosition <= currentHeight)
                {
                    if (highestHeight == -1 || currentHeight < highestHeight)
                    {
                        highestHeight = currentHeight;
                        highestGroundBelowSprite = ground;
                    }
                }
            }
            return highestGroundBelowSprite;
        }
    }
}