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
        #region Parts
        /// <summary>
        /// Manages jumping
        /// </summary>
        private JumpingManager jumpingManager = new JumpingManager();

        /// <summary>
        /// Manages falling
        /// </summary>
        private GravityManager gravityManager = new GravityManager();
        #endregion

        #region Public Methods
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta)
        {
        	gravityManager.ApplyGravity(sprite, level, timeDelta);
            jumpingManager.Update(sprite, timeDelta);
        }

        internal void TryMakeWalk(AbstractSprite sprite, bool isTryingToWalk, bool isWalkingRight, double timeDelta, Level level)
        {
            double desiredWalkingDistance;
            double walkingDistance;
            double previousWalkingSpeed = sprite.CurrentWalkingSpeed;

            if (isTryingToWalk)
                sprite.CurrentWalkingSpeed += sprite.WalkingAcceleration;

            if (sprite.IsRunning)
                sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxRunningSpeed);
            else
                sprite.CurrentWalkingSpeed = Math.Min(sprite.CurrentWalkingSpeed, sprite.MaxWalkingSpeed);

            if (isWalkingRight)
            {
                desiredWalkingDistance = timeDelta * sprite.CurrentWalkingSpeed;
                walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite, desiredWalkingDistance + sprite.Width / 2.0, level);
                walkingDistance -= sprite.Width / 2.0;
                walkingDistance = Math.Max(0, walkingDistance);
            }
            else
            {
                desiredWalkingDistance = -timeDelta * sprite.CurrentWalkingSpeed;
                walkingDistance = GetFarthestWalkingDistanceNoCollision(sprite, desiredWalkingDistance - sprite.Width / 2.0, level);
                walkingDistance += sprite.Width / 2.0;
                walkingDistance = Math.Min(0, walkingDistance);
            }

            if (walkingDistance != 0)
            {
            	if (sprite.Ground != null)
            	{
	                double slope;
	                
	                if (isWalkingRight)
	                	slope = 1.0 - ((sprite.Ground.TerrainWave[sprite.XPosition + walkingDistance] - sprite.Ground.TerrainWave[sprite.XPosition]) / walkingDistance) / 2.0;
	                else
	                	slope = 1.0 - ((sprite.Ground.TerrainWave[sprite.XPosition] - sprite.Ground.TerrainWave[sprite.XPosition + walkingDistance]) / walkingDistance) / 2.0;
	
	                slope = Math.Sqrt(slope);
	
	                if (slope > 0)
	                    walkingDistance /= slope;
            	}

                sprite.XPosition += walkingDistance;
            }

            #warning Must improve prevent sprite from accelerating while pushing on a collision
            //Must prevent sprite from accelerating while pushing on a collision
            if (sprite.Ground != null && walkingDistance == 0 && previousWalkingSpeed > 0.1)
                sprite.CurrentWalkingSpeed = 0;

        	if (sprite.Ground != null)
        	{
        		Ground frontestGroundHavingAccessibleWalkingHeightForSprite = GetFrontestGroundHavingAccessibleWalkingHeightForSprite(sprite, sprite.Ground, level);        		
        		
        		//If a ground is obstructing current ground, and it is accessible for sprite, use that ground instead
        		if (frontestGroundHavingAccessibleWalkingHeightForSprite != null)
        			sprite.Ground = frontestGroundHavingAccessibleWalkingHeightForSprite;

                double groundHeight = sprite.Ground.TerrainWave[sprite.XPosition];

                //We sometimes make fall the sprite
                if (sprite.YPosition < groundHeight - sprite.MinimumFallingHeight)
                    sprite.Ground = null;
                else
                    sprite.YPosition = groundHeight;
        	}
        }
        #endregion

        #region Private Methods
        private double GetFarthestWalkingDistanceNoCollision(AbstractSprite sprite, double desiredDistance, Level level)
        {
        	double previousDistance = 0;
        	if (desiredDistance > 0)
        	{
        		for (double currentDistance = 0; currentDistance <= desiredDistance; currentDistance += Program.collisionDetectionResolution)
        		{
                    if (IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, true, true))
        				return previousDistance;
    				previousDistance = currentDistance;
        		}
        	}
        	else
        	{
        		for (double currentDistance = 0; currentDistance >= desiredDistance; currentDistance -= Program.collisionDetectionResolution)
        		{
                    if (IsDetectCollision(sprite, sprite.XPosition + currentDistance, level, false, true))
        				return previousDistance;
    				previousDistance = currentDistance;
        		}
        	}
        	return previousDistance;
        }
        
        private bool IsDetectCollision(AbstractSprite sprite, double xDesiredPosition, Level level, bool isWalkingRight, bool isConsiderFallingCollision)
        {
        	#warning for transparent grounds and maybe for all grounds, must use texture's thickness for collision detection (should not block if player is under texture) (consider crouching height too)
            Ground referenceGround;

            if (sprite.Ground == null)
            {
                referenceGround = GroundHelper.GetHighestVisibleGroundBelowSprite(sprite, level);
                if (referenceGround == null)
                    return false;
                if (isConsiderFallingCollision)
                    return referenceGround.TerrainWave[xDesiredPosition] < sprite.YPosition;
            }
            else
                referenceGround = sprite.Ground;
        	
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

            //We check other grounds for collision
            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];
                double angleY1 = referenceGround.TerrainWave[angleX1];
                double angleY2 = referenceGround.TerrainWave[angleX2];

                double slope = angleY1 - angleY2;

                if (slope >= sprite.MaximumWalkingHeight)
                    return true;

                if (currentGround == referenceGround)
                    break;
                else if (isConsiderFallingCollision && currentGround.TerrainWave[sprite.XPosition] < sprite.YPosition)
                    return true;
            }
            return false;
        }
        
        private Ground GetFrontestGroundHavingAccessibleWalkingHeightForSprite(AbstractSprite sprite, Ground ground, Level level)
        {
        	double groundHeight = ground.TerrainWave[sprite.XPosition];
        	
        	for (int groundId = level.Count -1; groundId >= 0; groundId--)
        	{
        		Ground currentGround = level[groundId];
        		
        		if (currentGround == ground)
        			break;
        		
        		double currentGroundHeight = currentGround.TerrainWave[sprite.XPosition];

                if (currentGroundHeight < groundHeight && groundHeight - currentGroundHeight <= sprite.MaximumWalkingHeight)
    				return currentGround;
        	}
        	return null;
        }
        #endregion
    }
}