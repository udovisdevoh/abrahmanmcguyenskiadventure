using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a sprite
    /// </summary>
    internal abstract class AbstractSprite
    {
        #region Fields and parts
        /// <summary>
        /// X position
        /// </summary>
        private double xPosition;

        /// <summary>
        /// Y position
        /// </summary>
        private double yPosition;

        /// <summary>
        /// Mass
        /// </summary>
        private double mass;

        /// <summary>
        /// Height
        /// </summary>
        private double height;

        /// <summary>
        /// Width
        /// </summary>
        private double width;

        /// <summary>
        /// Current jump or falling acceleration
        /// </summary>
        private double currentJumpAcceleration;

        /// <summary>
        /// To which sprite collection the sprite belongs
        /// </summary>
        private SpritePopulation parentSpriteCollection;

        /// <summary>
        /// List of bucket that contain this sprite
        /// </summary>
        private HashSet<Bucket> parentBucketList;

        /// <summary>
        /// True: face left, False: face right
        /// </summary>
        private bool isPointingLeft;

        /// <summary>
        /// Current ground attached to sprite
        /// </summary>
        private Ground ground;

        private bool isNeedToJumpAgain = false;

        private bool isNeedToAttackAgain = false;

        /// <summary>
        /// Previous value of yPosition so we can know if the sprite is falling/jumping up or down
        /// </summary>
        private double yPositionPrevious;

        private double startingJumpAcceleration;

        private double maxWalkingSpeed;

        private double currentWalkingSpeed = 0;

        private double walkingAcceleration;

        private double maxRunningSpeed;

        private bool isRunning = false;

        private bool isTryingToWalk = false;

        private bool isTryingToWalkRight = false;

        private bool isTryingToJump = false;

        private bool isCrouch = false;

        private bool isTryingToSlide = false;

        private Cycle walkingCycle;

        private Cycle jumpingCycle;

        private Cycle attackingCycle;
        #endregion

        #region Constructor
        /// <summary>
        /// Create abstract sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public AbstractSprite(double xPosition, double yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            width = BuildWidth();
            height = BuildHeight();
            mass = BuildMass();
            startingJumpAcceleration = BuildStartingJumpAcceleration();
            parentBucketList = new HashSet<Bucket>();
            maxWalkingSpeed = BuildMaxWalkingSpeed();
            maxRunningSpeed = BuildMaxRunningSpeed();
            walkingAcceleration = BuildWalkingAcceleration();
            walkingCycle = new Cycle(BuildWalkingCycleLength(),true);
            jumpingCycle = new Cycle(BuildJumpingTime(), false);
            attackingCycle = new Cycle(BuildAttackingTime(), false);
        }
        #endregion

        #region Abstract Methods
        protected abstract double BuildJumpingTime();

        protected abstract double BuildWalkingCycleLength();

        protected abstract double BuildWalkingAcceleration();

        protected abstract double BuildMaxWalkingSpeed();

        protected abstract double BuildMaxRunningSpeed();

        protected abstract double BuildStartingJumpAcceleration();

        protected abstract double BuildAttackingTime();

        /// <summary>
        /// sprite's width (1.5 = player's width)
        /// </summary>
        /// <returns>sprite's width (1.5 = player's width)</returns>
        protected abstract double BuildWidth();

        /// <summary>
        /// sprite's height (2.0 = player's height)
        /// </summary>
        /// <returns>sprite's height (2.0 = player's height)</returns>
        protected abstract double BuildHeight();

        /// <summary>
        /// sprite's mass (1.0 = player's mass)
        /// </summary>
        /// <returns>sprite's mass (1.0 = player's mass)</returns>
        protected abstract double BuildMass();

        /// <summary>
        /// Sprite's surface
        /// </summary>
        /// <returns>Sprite's surface</returns>
        public abstract Surface GetCurrentSurface();
        #endregion

        #region Properties
        /// <summary>
        /// X position
        /// </summary>
        public double XPosition
        {
            get { return xPosition; }
            set
            {
                parentSpriteCollection.RemoveSpatialHashing(this);
                xPosition = value;
                parentSpriteCollection.SetSpatialHashing(this);
            }
        }

        /// <summary>
        /// Y position
        /// </summary>
        public double YPosition
        {
            get { return yPosition; }
            set
            {
                yPositionPrevious = yPosition;
                parentSpriteCollection.RemoveSpatialHashing(this);
                yPosition = value;
                parentSpriteCollection.SetSpatialHashing(this);
            }
        }

        /// <summary>
        /// Previous Y position (so we can know if the sprite is falling/jumping up or down
        /// </summary>
        public double YPositionPrevious
        {
            get { return yPositionPrevious; }
        }

        /// <summary>
        /// Mass
        /// </summary>
        public double Mass
        {
            get { return mass; }
        }

        /// <summary>
        /// Width
        /// </summary>
        public double Width
        {
            get { return width; }
        }

        /// <summary>
        /// Height
        /// </summary>
        public double Height
        {
            get { return height; }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }
        
        /// <summary>
        /// To which sprite collection the sprite belongs
        /// </summary>
        public SpritePopulation ParentSpriteCollection
        {
            get { return parentSpriteCollection; }
            set { parentSpriteCollection = value; }
        }

        public HashSet<Bucket> ParentBucketList
        {
            get { return parentBucketList; }
        }

        /// <summary>
        /// True: face left, False: face right
        /// </summary>
        public bool IsPointingLeft
        {
            get { return isPointingLeft; }
            set { isPointingLeft = value; }
        }

        /// <summary>
        /// Current jump or falling acceleration
        /// </summary>
        public double CurrentJumpAcceleration
        {
            get { return currentJumpAcceleration; }
            set { currentJumpAcceleration = value; }
        }

        /// <summary>
        /// Current ground attached to sprite
        /// </summary>
        public Ground Ground
        {
            get { return ground; }
            set { ground = value; }
        }

        public double RightBound
        {
            get { return xPosition + width / 2.0; }
        }

        public double LeftBound
        {
            get { return xPosition - width / 2.0; }
        }

        public double MaximumWalkingHeight
        {
            get { return height / 5.0; }
        }

        public double MinimumFallingHeight
        {
            get { return height / 3.0; }
        }

        public bool IsNeedToJumpAgain
        {
            get { return isNeedToJumpAgain; }
            set { isNeedToJumpAgain = value; }
        }

        public bool IsNeedToAttackAgain
        {
            get { return isNeedToAttackAgain; }
            set { isNeedToAttackAgain = value; }
        }

        public double StartingJumpAcceleration
        {
            get { return startingJumpAcceleration; }
        }

        public double MaxWalkingSpeed
        {
            get { return maxWalkingSpeed; }
        }

        public double MaxRunningSpeed
        {
            get { return maxRunningSpeed; }
        }

        public double CurrentWalkingSpeed
        {
            get { return currentWalkingSpeed; }
            set { currentWalkingSpeed = value; }
        }

        public double WalkingAcceleration
        {
            get { return walkingAcceleration; }
        }

        public bool IsTryingToWalk
        {
            get { return isTryingToWalk; }
            set { isTryingToWalk = value; }
        }

        public bool IsTryingToWalkRight
        {
            get { return isTryingToWalkRight; }
            set { isTryingToWalkRight = value; }
        }

        public bool IsTryingToJump
        {
            get { return isTryingToJump; }
            set { isTryingToJump = value; }
        }

        public bool IsCrouch
        {
            get { return isCrouch; }
            set { isCrouch = value; }
        }

        public bool IsTryingToSlide
        {
            get { return isTryingToSlide; }
            set { isTryingToSlide = value; }
        }

        public Cycle WalkingCycle
        {
            get { return walkingCycle; }
        }

        public Cycle JumpingCycle
        {
            get { return jumpingCycle; }
        }

        public Cycle AttackingCycle
        {
            get { return attackingCycle; }
        }
        #endregion
    }
}
