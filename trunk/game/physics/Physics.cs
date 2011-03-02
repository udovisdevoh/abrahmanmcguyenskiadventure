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
        	double walkingDistance;
        	if (isRight)
        		walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite,timeDelta,level);
        	else
        		walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite,-timeDelta,level);
        	
        	if (walkingDistance != 0)
        	    sprite.XPosition += walkingDistance;
        	
        	
        	if (sprite.Ground != null)
        	{        		
        		Ground frontestGroundHavingAccessibleWalkingHeightForSprite = GetFrontestGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.Ground, level);        		
        		
        		//If a ground is obstructing current ground, and it is accessible for sprite, use that ground instead
        		if (frontestGroundHavingAccessibleWalkingHeightForSprite != null)
        			sprite.Ground = frontestGroundHavingAccessibleWalkingHeightForSprite;
        			
        		sprite.YPosition = sprite.Ground.TerrainWave[sprite.XPosition];
        	}
        }
        
        private double GetFarthestWalkingDistanceNoCollision(AbstractSprite sprite, double desiredDistance, Level level)
        {
        	double previousDistance = 0;
        	if (desiredDistance > 0)
        	{
        		for (double currentDistance = 0; currentDistance <= desiredDistance; currentDistance += Program.collisionDetectionResolution)
        		{
                    if (IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, true))
        				return previousDistance;
    				previousDistance = currentDistance;
        		}
        	}
        	else
        	{
        		for (double currentDistance = 0; currentDistance >= desiredDistance; currentDistance -= Program.collisionDetectionResolution)
        		{
                    if (IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, false))
        				return previousDistance;
    				previousDistance = currentDistance;
        		}
        	}
        	return previousDistance;
        }
        
        private bool IsDetectCollision(AbstractSprite sprite, double xDesiredPosition, Level level, bool isWalkingRight)
        {
        	if (sprite.Ground == null)
        		return false;
        	
        	double angleX1;
        	double angleX2;
        	
        	if (isWalkingRight)
        	{
                angleX1 = xDesiredPosition;
        		angleX2 = angleX1 + Program.collisionDetectionResolution;
        	}
        	else 
        	{
        		angleX1 = xDesiredPosition;
        		angleX2 = angleX1 - Program.collisionDetectionResolution;
        	}
        	
        	double angleY1 = sprite.Ground.TerrainWave[angleX1];
        	double angleY2 = sprite.Ground.TerrainWave[angleX2];

            double slope = angleY1 - angleY2;
            return (slope >= sprite.Height / 5.0);
        }
        
        private Ground GetFrontestGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, Ground ground, Level level)
        {
        	#warning: must not consider ground that are too high above ground from which sprite originates
        	double groundHeight = ground.TerrainWave[sprite.XPosition];
        	
        	for (int groundId = level.Count -1; groundId >= 0; groundId--)
        	{
        		Ground currentGround = level[groundId];
        		
        		if (currentGround == ground)
        			break;
        		
        		double currentGroundHeight = currentGround.TerrainWave[sprite.XPosition];
    			
        		if (currentGroundHeight < groundHeight && groundHeight - currentGroundHeight <= sprite.Height / 5)
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