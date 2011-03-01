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
        
        internal void TryMakeWalk(AbstractSprite sprite, bool isRight, double timeDelta, Level level)
        {
        	if (isRight)
        		sprite.XPosition += timeDelta;
        	else
        		sprite.XPosition -= timeDelta;
        	
        	if (sprite.Ground != null)
        	{        		
        		Ground frontestGroundAccessibleWalkingHeightForSprite = GetFrontestGroundAccessibleWalkingHeightForSprite(sprite, sprite.Ground, level);        		
        		
        		//If a ground is obstructing current ground, and it is accessible for sprite, use that ground instead
        		if (frontestGroundAccessibleWalkingHeightForSprite != null)
        			sprite.Ground = frontestGroundAccessibleWalkingHeightForSprite;
        			
        		sprite.YPosition = sprite.Ground.TerrainWave[sprite.XPosition];
        	}
        }
        
        private Ground GetFrontestGroundAccessibleWalkingHeightForSprite(AbstractSprite sprite, Ground ground, Level level)
        {
        	#warning: must not consider ground that are too high above ground from which sprite originates
        	for (int groundId = level.Count -1; groundId >= 0; groundId--)
        	{
        		Ground currentGround = level[groundId];
        		
        		if (currentGround == ground)
        			break;
    			
        		if (currentGround.TerrainWave[sprite.XPosition] < ground.TerrainWave[sprite.XPosition])
    				return currentGround;
        	}
        	return null;
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