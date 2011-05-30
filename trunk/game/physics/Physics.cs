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
        /// <param name="random">random number generator</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta, HashSet<AbstractSprite> visibleSpriteList, SpritePopulation spritePopulation, Random random)
        {
            walkingManager.Update(sprite, level, timeDelta, visibleSpriteList);
            gravityManager.Update(sprite, level, timeDelta, visibleSpriteList);
            jumpingManager.Update(sprite, timeDelta);
            damageManager.Update(sprite, timeDelta);
            deathManager.Update(sprite, timeDelta, spritePopulation);

            if (sprite is PlayerSprite)
            {
                spriteCollisionManager.Update(sprite, level, timeDelta, visibleSpriteList, spritePopulation, random);
                battleManager.Update(sprite, level, timeDelta, visibleSpriteList);
                if (((PlayerSprite)sprite).PowerUpAnimationCycle.IsFired)
                    ((PlayerSprite)sprite).PowerUpAnimationCycle.Increment(timeDelta);
            }
            else if (sprite is HelmetSprite)
            {
                helmetToMonsterCollisionManager.Update((HelmetSprite)sprite, level, visibleSpriteList);
            }

            if (sprite is MonsterSprite && ((MonsterSprite)sprite).IsEnableSpontaneousConversion)
                spontaneousConversionManager.Update((MonsterSprite)sprite, spritePopulation, timeDelta, random);

            if (sprite is IGrowable && ((IGrowable)sprite).GrowthCycle.IsFired)
                ((IGrowable)sprite).GrowthCycle.Increment(timeDelta);
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
            double yDesiredPosition = referenceGround[xDesiredPosition];
            foreach (AbstractSprite otherSprite in visibleSpriteList)
                if (otherSprite.IsImpassable)
                    if (Physics.IsDetectCollision(sprite, xDesiredPosition, yDesiredPosition, 0.27, otherSprite))
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

            if (!isHorizontalCollision)
                return false;

            bool isVerticalCollision =  (sprite1.YPosition > sprite2.TopBound && sprite1.YPosition < sprite2.YPosition)
                                     || (sprite2.YPosition > sprite1.TopBound && sprite2.YPosition < sprite1.YPosition);
            isVerticalCollision |= (sprite1.YPosition == sprite2.YPosition || sprite1.TopBound == sprite2.TopBound);
            
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

            if (!isHorizontalCollision)
                return false;

            bool isVerticalCollision = (sprite1YPosition > sprite2.TopBound && sprite1YPosition < sprite2.YPosition)
                                     || (sprite2.YPosition > sprite1TopBound && sprite2.YPosition < sprite1YPosition);
            isVerticalCollision |= (sprite1YPosition == sprite2.YPosition || sprite1TopBound == sprite2.TopBound);

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

                    if (sprite1.IGround == null || sprite1.IsCrouch)
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
        #endregion
    }
}