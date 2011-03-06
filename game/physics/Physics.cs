using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// For physic operations
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

        /// <summary>
        /// Manages walking logic
        /// </summary>
        private WalkingManager walkingManager = new WalkingManager();
        #endregion

        #region Public Instance Methods
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta)
        {
            walkingManager.Update(sprite, level, timeDelta);
        	gravityManager.ApplyGravity(sprite, level, timeDelta);
            jumpingManager.Update(sprite, timeDelta);
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// To detect collision from sprite to level
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="xDesiredPosition">desired x position for sprite</param>
        /// <param name="level">level to look into</param>
        /// <param name="isConsiderFallingCollision">whether we consider vertical (falling collisions</param>
        /// <returns>Whether collision was detected</returns>
        internal static bool IsDetectCollision(AbstractSprite sprite, double xDesiredPosition, Level level, bool isConsiderFallingCollision)
        {
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
        	
        	if (sprite.IsTryingToWalkRight)
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
        #endregion
    }
}