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
        /// Manages fist/kick fight logic
        /// </summary>
        private BattleManager battleManager = new BattleManager();

        /// <summary>
        /// Manages damage logic
        /// </summary>
        private DamageManager damageManager = new DamageManager();

        /// <summary>
        /// Manages death logic
        /// </summary>
        private DeathManager deathManager = new DeathManager();

        /// <summary>
        /// Manages cases where sprites are spontaneously converted when they have stoped moving for too long
        /// </summary>
        private SpontaneousConversionManager spontaneousConversionManager = new SpontaneousConversionManager();

        /// <summary>
        /// Manages sprite collision
        /// </summary>
        private SpriteCollisionManager spriteCollisionManager = new SpriteCollisionManager();

        /// <summary>
        /// Manages collisions between helmet and monsters
        /// </summary>
        private HelmetToMonsterCollisionManager helmetToMonsterCollisionManager = new HelmetToMonsterCollisionManager();

        /// <summary>
        /// Manages player's projectiles
        /// </summary>
        private PlayerProjectileManager playerProjectileManager = new PlayerProjectileManager();

        /// <summary>
        /// Manages collisions between fireball and monsters
        /// </summary>
        private FireBallToMonsterCollisionManager fireBallToMonsterCollisionManager = new FireBallToMonsterCollisionManager();

        /// <summary>
        /// Manages explosive sprites
        /// </summary>
        private ExplosionManager explosionManager = new ExplosionManager();
        #endregion

        #region Public Instance Methods
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="program">the program itself</param>
        /// <param name="gameMetaState">game meta state</param>
        /// <param name="gameState">game state</param>
        /// <param name="random">random number generator</param>
        internal void Update(AbstractSprite spriteToUpdate, AbstractSprite playerSpriteReference, Level level, Program program, double timeDelta, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, GameMetaState gameMetaState, GameState gameState, Random random)
        {
            walkingManager.Update(spriteToUpdate, level, timeDelta, visibleSpriteList);
            gravityManager.Update(spriteToUpdate, level, timeDelta, visibleSpriteList);
            jumpingManager.Update(spriteToUpdate, timeDelta);
            damageManager.Update(spriteToUpdate, timeDelta);
            deathManager.Update(spriteToUpdate, timeDelta, spritePopulation, visibleSpriteList, gameMetaState, gameState);

            if (spriteToUpdate is PlayerSprite || spriteToUpdate is MonsterSprite)
            {
                spriteCollisionManager.Update(spriteToUpdate, level, timeDelta, visibleSpriteList, spritePopulation, program, gameMetaState, gameState, random);

                if (spriteToUpdate is PlayerSprite)
                {
                    battleManager.Update(spriteToUpdate, level, timeDelta, visibleSpriteList);
                    if (((PlayerSprite)spriteToUpdate).PowerUpAnimationCycle.IsFired)
                        ((PlayerSprite)spriteToUpdate).PowerUpAnimationCycle.Increment(timeDelta);

                    if (((PlayerSprite)spriteToUpdate).ChangingSizeAnimationCycle.IsFired)
                        ((PlayerSprite)spriteToUpdate).ChangingSizeAnimationCycle.Increment(timeDelta);

                    if (((PlayerSprite)spriteToUpdate).ThrowBallCycle.IsFired)
                        ((PlayerSprite)spriteToUpdate).ThrowBallCycle.Increment(timeDelta);

                    if (((PlayerSprite)spriteToUpdate).InvincibilityCycle.IsFired)
                        ((PlayerSprite)spriteToUpdate).InvincibilityCycle.Increment(timeDelta);

                    if (((PlayerSprite)spriteToUpdate).FromVortexCycle.IsFired)
                        ((PlayerSprite)spriteToUpdate).FromVortexCycle.Increment(timeDelta);

                    playerProjectileManager.Update((PlayerSprite)spriteToUpdate, visibleSpriteList, spritePopulation, random);
                }
            }
            
            if (spriteToUpdate is HelmetSprite)
            {
                helmetToMonsterCollisionManager.Update((HelmetSprite)spriteToUpdate, level, visibleSpriteList);
            }
            else if (spriteToUpdate is FireBallSprite)
            {
                fireBallToMonsterCollisionManager.Update((FireBallSprite)spriteToUpdate, level, visibleSpriteList);
            }

            if (spriteToUpdate is MonsterSprite && ((MonsterSprite)spriteToUpdate).IsEnableSpontaneousConversion)
                spontaneousConversionManager.Update((MonsterSprite)spriteToUpdate, spritePopulation, timeDelta, random);

            if (spriteToUpdate is IGrowable && ((IGrowable)spriteToUpdate).GrowthCycle.IsFired)
                ((IGrowable)spriteToUpdate).GrowthCycle.Increment(timeDelta);

            if (spriteToUpdate is IExplodable)
                explosionManager.UpdateExplodable((IExplodable)spriteToUpdate, playerSpriteReference, spritePopulation, timeDelta, random);
            else if (spriteToUpdate is ExplosionSprite && spriteToUpdate.IsAlive)
                explosionManager.UpdateExplosion((ExplosionSprite)spriteToUpdate, visibleSpriteList, timeDelta);
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
        internal static bool IsDetectCollision(AbstractSprite sprite, double xDesiredPosition, Level level, HashSet<AbstractSprite> visibleSpriteList)
        {
            IGround referenceGround;

            if (sprite.IGround == null)
            {
                referenceGround = IGroundHelper.GetHighestVisibleIGroundBelowSprite(sprite, level, visibleSpriteList);
                if (referenceGround == null)
                    return false;
                //if (isConsiderFallingCollision)
                return referenceGround[xDesiredPosition] < sprite.YPosition;
            }
            else
                referenceGround = sprite.IGround;
        	
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


            #region We test collision with impassable sprites
            double yDesiredPosition = sprite.YPosition;
            if (sprite.IGround is Ground)
                yDesiredPosition = sprite.IGround[xDesiredPosition];
            foreach (AbstractSprite otherSprite in visibleSpriteList)
                if (sprite != otherSprite && otherSprite.IsImpassable)
                    if (Physics.IsDetectCollision(sprite, xDesiredPosition, yDesiredPosition, 0.46, otherSprite))
                        return true;
            #endregion

            if (sprite.IGround is Ground)
            {
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
        internal static double GetSlopeRatio(AbstractSprite sprite, IGround ground, double walkingDistance, bool isRight)
        {
            if (ground is AbstractSprite)
                return 0.0;

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
        /// <returns>Whether sprites are in collision</returns>
        internal static bool IsDetectCollision(AbstractSprite sprite1, AbstractSprite sprite2)
        {
            bool isHorizontalCollision = (sprite1.RightBound > sprite2.LeftBound && sprite1.LeftBound < sprite2.LeftBound)
                                      || (sprite2.LeftBound < sprite1.RightBound && sprite2.RightBound > sprite1.RightBound)
                                      || (sprite2.RightBound > sprite1.LeftBound && sprite2.LeftBound < sprite2.LeftBound)
                                      || (sprite1.LeftBound < sprite2.RightBound && sprite1.RightBound > sprite2.RightBound);
            isHorizontalCollision |= sprite1.RightBound == sprite2.RightBound || sprite1.LeftBound == sprite2.LeftBound;
            isHorizontalCollision |= sprite1.LeftBound <= sprite2.LeftBound && sprite1.RightBound >= sprite2.RightBound;
            isHorizontalCollision |= sprite2.LeftBound <= sprite1.LeftBound && sprite2.RightBound >= sprite1.RightBound;

            if (!isHorizontalCollision)
                return false;

            bool isVerticalCollision =  (sprite1.YPosition > sprite2.TopBound && sprite1.YPosition < sprite2.YPosition)
                                     || (sprite2.YPosition > sprite1.TopBound && sprite2.YPosition < sprite1.YPosition);
            isVerticalCollision |= (sprite1.YPosition == sprite2.YPosition || sprite1.TopBound == sprite2.TopBound);
            isVerticalCollision |= (sprite1.YPosition >= sprite2.YPosition && sprite1.TopBound <= sprite2.TopBound);
            isVerticalCollision |= (sprite2.YPosition >= sprite1.YPosition && sprite2.TopBound <= sprite1.TopBound);

            return isHorizontalCollision && isVerticalCollision;
        }

        /// <summary>
        /// Detect collision from sprite with virtual position to other sprite with real position
        /// </summary>
        /// <param name="sprite1">sprite 1</param>
        /// <param name="virtualX">virtual X</param>
        /// <param name="virtualY">virtual Y</param>
        /// <param name="sprite1WidthMultiplicator">virtual width multiplicator for sprite 1 (we simulate a different width</param>
        /// <param name="sprite2">sprite 2</param>
        /// <returns>Whether there is collision from sprite with virtual position to other sprite with real position</returns>
        internal static bool IsDetectCollision(AbstractSprite sprite1, double virtualX, double virtualY, double sprite1WidthMultiplicator, AbstractSprite sprite2)
        {
            double sprite1RightBound = virtualX + sprite1.Width / 2.0 * sprite1WidthMultiplicator;
            double sprite1LeftBound = virtualX - sprite1.Width / 2.0 * sprite1WidthMultiplicator;
            double sprite1YPosition = virtualY;
            double sprite1TopBound = sprite1.TopBound - sprite1.YPosition + virtualY;

            bool isHorizontalCollision =    (sprite1RightBound > sprite2.LeftBound && sprite1LeftBound < sprite2.LeftBound)
                                     ||     (sprite2.LeftBound < sprite1RightBound && sprite2.RightBound > sprite1RightBound)
                                     ||     (sprite2.RightBound > sprite1LeftBound && sprite2.LeftBound < sprite2.LeftBound)
                                     ||     (sprite1LeftBound < sprite2.RightBound && sprite1RightBound > sprite2.RightBound);
            isHorizontalCollision |= sprite1RightBound == sprite2.RightBound || sprite1LeftBound == sprite2.LeftBound;
            isHorizontalCollision |= sprite1LeftBound <= sprite2.LeftBound && sprite1RightBound >= sprite2.RightBound;
            isHorizontalCollision |= sprite2.LeftBound <= sprite1LeftBound && sprite2.RightBound >= sprite1RightBound;

            if (!isHorizontalCollision)
                return false;

            bool isVerticalCollision = (sprite1YPosition > sprite2.TopBound && sprite1YPosition < sprite2.YPosition)
                                     || (sprite2.YPosition > sprite1TopBound && sprite2.YPosition < sprite1YPosition);
            isVerticalCollision |= (sprite1YPosition == sprite2.YPosition || sprite1TopBound == sprite2.TopBound);
            isVerticalCollision |= (sprite1.YPosition >= sprite2.YPosition && sprite1.TopBound <= sprite2.TopBound);
            isVerticalCollision |= (sprite2.YPosition >= sprite1.YPosition && sprite2.TopBound <= sprite1.TopBound);

            return isHorizontalCollision && isVerticalCollision;
        }

        /// <summary>
        /// Whether sprite 1 is punching or kicking sprite 2
        /// </summary>
        /// <param name="sprite1">sprite 1</param>
        /// <param name="sprite2">sprite 2</param>
        /// <returns>Whether sprite 1 is punching or kicking sprite 2</returns>
        internal static bool IsDetectCollisionPunchOrKick(AbstractSprite sprite1, AbstractSprite sprite2)
        {
            if (sprite1.AttackingCycle.IsFired)
            {
                int attackCycleDivision = sprite1.AttackingCycle.GetCycleDivision(8);
                if (attackCycleDivision >= 4)
                {
                    bool isHorizontalCollision, isVerticalCollision;

                    if (sprite1.IsTryingToWalkRight)
                    {
                        isHorizontalCollision = (sprite1.RightPunchBound > sprite2.LeftBound && sprite1.LeftBound < sprite2.LeftBound);
                    }
                    else
                    {
                        isHorizontalCollision = (sprite1.LeftPunchBound < sprite2.RightBound && sprite1.RightBound > sprite2.RightBound);
                    }

                    double sprite1TopBound, sprite1BottomBound;

                    if (sprite1.IsTiny)
                    {
                        sprite1TopBound = sprite1.TopBound;
                        sprite1BottomBound = sprite1.YPosition;
                    }
                    else if (sprite1.IGround == null || sprite1.IsCrouch)
                    {
                        sprite1TopBound = sprite1.YPosition - sprite1.Height / 2.0;
                        sprite1BottomBound = sprite1.YPosition;
                    }
                    else
                    {
                        sprite1TopBound = sprite1.TopBound;
                        sprite1BottomBound = sprite1.YPosition - sprite1.Height / 2.0;
                    }

                    isVerticalCollision = (sprite1TopBound > sprite2.TopBound && sprite1.TopBound < sprite2.YPosition)
                                        || (sprite1BottomBound > sprite2.TopBound && sprite1.TopBound < sprite2.YPosition);

                    return isHorizontalCollision && isVerticalCollision;
                }
            }
            return false;
        }

        /// <summary>
        /// Get angle between two points (in degrees)
        /// </summary>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        /// <returns>angle between two points (in degrees)</returns>
        internal static double GetAngleDegree(double x1, double y1, double x2, double y2)
        {
            double angle = Math.Atan2(y1 - y2, x1 - x2) * 180 / Math.PI;
            while (angle < 0)
                angle += 360;
            while (angle > 360)
                angle -= 360;
            return angle;
        }
        #endregion
    }
}