﻿using System;
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

        /// <summary>
        /// Manages damage logic
        /// </summary>
        private DamageManager damageManager = new DamageManager();

        /// <summary>
        /// Manages sprite collision
        /// </summary>
        private SpriteCollisionManager spriteCollisionManager = new SpriteCollisionManager();
        #endregion

        #region Public Instance Methods
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList)
        {
            walkingManager.Update(sprite, level, timeDelta);
        	gravityManager.Update(sprite, level, timeDelta);
            jumpingManager.Update(sprite, timeDelta);
            
            damageManager.Update(sprite, timeDelta);

            if (sprite is PlayerSprite)
                spriteCollisionManager.Update(sprite, level, timeDelta, visibleSpriteList);

            if (sprite.YPosition > Program.totalHeightTileCount)
                sprite.IsAlive = false;
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// To detect collision from sprite to level
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="xDesiredPosition">desired x position for sprite</param>
        /// <param name="level">level to look into</param>
        /// <returns>Whether collision was detected</returns>
        internal static bool IsDetectCollision(AbstractSprite sprite, double xDesiredPosition, Level level)
        {
            Ground referenceGround;

            if (sprite.Ground == null)
            {
                referenceGround = GroundHelper.GetHighestVisibleGroundBelowSprite(sprite, level);
                if (referenceGround == null)
                    return false;
                //if (isConsiderFallingCollision)
                return referenceGround[xDesiredPosition] < sprite.YPosition;
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

        	double angleY1 = Math.Min(referenceGround[angleX1], referenceGround[sprite.XPosition]);
            double angleY2 = referenceGround[angleX2];
            double slope = angleY1 - angleY2;
            if (slope >= sprite.MaximumWalkingHeight)
                return true;


            //We check other grounds for ground collisions
            for (int groundId = level.Count - 1; groundId >= 0; groundId--)
            {
                Ground currentGround = level[groundId];
                if (currentGround == referenceGround)
                    break;
                else if (sprite.YPosition - currentGround[sprite.XPosition] >= sprite.MaximumWalkingHeight)
                    return true;
                else if (sprite.YPosition - currentGround[xDesiredPosition] >= sprite.MaximumWalkingHeight)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get the ratio of a slope at sprite's position going in sprite's current direction
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="ground">ground</param>
        /// <param name="walkingDistance">walking distance (could be negative)</param>
        /// <returns>ratio of a slope at sprite's position going in sprite's current direction. 0: flat, 1: 45% going down, -1: -45% going up</returns>
        internal static double GetSlopeRatio(AbstractSprite sprite, Ground ground, double walkingDistance, bool isRight)
        {
            if (isRight)
                return ((ground[sprite.XPosition + walkingDistance] - ground[sprite.XPosition]) / walkingDistance) / 2.0;
            else
                return ((ground[sprite.XPosition] - ground[sprite.XPosition + walkingDistance]) / walkingDistance) / 2.0;
        }

        /// <summary>
        /// Whether sprites are in collision
        /// </summary>
        /// <param name="sprite1">sprite 1</param>
        /// <param name="sprite2">sprite 2</param>
        /// <returns></returns>
        internal static bool IsDetectCollision(AbstractSprite sprite1, AbstractSprite sprite2)
        {
            bool isHorizontalCollision = (sprite1.RightBound > sprite2.LeftBound && sprite1.LeftBound < sprite1.LeftBound)
                                      || (sprite2.LeftBound < sprite1.RightBound && sprite2.RightBound > sprite1.RightBound)
                                      || (sprite2.RightBound > sprite1.LeftBound && sprite2.LeftBound < sprite2.LeftBound)
                                      || (sprite1.LeftBound < sprite2.RightBound && sprite1.RightBound > sprite2.RightBound);

            bool isVerticalCollision =  (sprite1.YPosition > sprite2.TopBound && sprite1.YPosition < sprite2.YPosition)
                                     || (sprite2.YPosition > sprite1.TopBound && sprite2.YPosition < sprite1.YPosition);
            
            return isHorizontalCollision && isVerticalCollision;
        }
        #endregion
    }
}