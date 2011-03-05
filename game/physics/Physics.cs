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
        #region Public Methods
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta)
        {
        	ApplyGravity(sprite, level, timeDelta);
            sprite.JumpingCycle.Increment(timeDelta / Math.Max(sprite.MaximumWalkingHeight,sprite.CurrentWalkingSpeed));
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

        internal void StartOrContinueJump(AbstractSprite sprite, double timeDelta)
        {
            if (!sprite.IsNeedToJumpAgain)
            {
                if (sprite.Ground != null)
                {
                    sprite.JumpingCycle.Reset();
                    sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                    sprite.Ground = null;
                }

                if (sprite.CurrentJumpAcceleration < 0)
                {
                    sprite.IsNeedToJumpAgain = true;
                }
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
        	
        	#warning for transparent grounds and maybe for all grounds, must use texture's width for collision detection (should not block if player is under texture) (consider crouching height too)
            Ground referenceGround;

            if (sprite.Ground == null)
            {
                referenceGround = GetHighestVisibleGroundBelowSprite(sprite, level);
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
        
        /// <summary>
        /// Apply gravity to sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        private void ApplyGravity(AbstractSprite sprite, Level level, double timeDelta)
        {
            #warning Must prevent sprite from faling on the tip of a sharp surface and get stucked on it, or half on a clif and stucked on it
        	if (sprite.Ground != null)
            {
                sprite.CurrentJumpAcceleration = 0;
            }
            else
            {
                Ground closestDownGround = GetHighestVisibleGroundBelowSprite(sprite, level);
                if (closestDownGround == null)
                {
                    sprite.Ground = GetLowestVisibleGround(sprite, level);
                    sprite.YPosition = sprite.Ground.TerrainWave[sprite.XPosition];
                    sprite.CurrentJumpAcceleration = 0;
                }
                else
                {
                    double closestDownGroundHeight = closestDownGround.TerrainWave[sprite.XPosition];
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

        /// <summary>
        /// Highest ground below sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>Highest ground below sprite</returns>
        internal Ground GetHighestVisibleGroundBelowSprite(AbstractSprite sprite, Level level)
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
                        if (IsGroundVisible(ground, level,sprite.XPosition))
                        {
                            highestHeight = currentHeight;
                            highestGroundBelowSprite = ground;
                        }
                    }
                }
            }
            return highestGroundBelowSprite;
        }

        /// <summary>
        /// Whether ground is visible at X Position
        /// </summary>
        /// <param name="ground">ground</param>
        /// <param name="level">level</param>
        /// <param name="xPosition">X Position</param>
        /// <returns>Whether ground is visible at X Position</returns>
        private bool IsGroundVisible(Ground ground, Level level, double xPosition)
        {
            double yPosition = ground.TerrainWave[xPosition];

            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];
                if (currentGround == ground)
                    break;

                if (currentGround.TerrainWave[xPosition] < yPosition)
                    return false;
            }

            return true;
        }

        private Ground GetLowestVisibleGround(AbstractSprite sprite, Level level)
        {
            Ground lowestGround = null;
            double lowestHeight = double.NegativeInfinity;

            foreach (Ground ground in level)
            {
                double currentHeight = ground.TerrainWave[sprite.XPosition];

                if (currentHeight > lowestHeight)
                {
                    if (IsGroundVisible(ground, level, sprite.XPosition))
                    {
                        lowestHeight = currentHeight;
                        lowestGround = ground;
                    }
                }
            }
            return lowestGround;
        }
        #endregion
    }
}