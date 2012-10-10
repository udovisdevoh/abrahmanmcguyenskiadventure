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
    internal abstract class AbstractSprite : IGround,  IComparable<AbstractSprite>
    {
        #region Fields and parts
        /// <summary>
        /// Walking cycle
        /// </summary>
        private Cycle walkingCycle;

        /// <summary>
        /// Jumping cycle
        /// </summary>
        private Cycle jumpingCycle;

        /// <summary>
        /// Attacking cycle
        /// </summary>
        private Cycle attackingCycle;

        /// <summary>
        /// "Getting hit / slammed, jumped on, touched and harmed" cycle
        /// </summary>
        private Cycle hitCycle;

        /// <summary>
        /// "Getting punched" cycle
        /// </summary>
        private Cycle punchedCycle;

        /// <summary>
        /// Ignore this climbable until touch ground
        /// </summary>
        private IClimbable ignoreThisIClimbable = null;

        /// <summary>
        /// To which sprite collection the sprite belongs
        /// </summary>
        private SpritePopulation __parentSpriteCollection;

        /// <summary>
        /// List of bucket that contain this sprite
        /// </summary>
        private HashSet<Bucket> __parentBucketList;

        /// <summary>
        /// Current IGround attached to sprite
        /// </summary>
        private IGround iGround;

        /// <summary>
        /// Sprite carried by this sprite
        /// </summary>
        private AbstractSprite carriedSprite;

        /// <summary>
        /// Whether sprite is currently tiny
        /// </summary>
        private bool isTiny = false;

        /// <summary>
        /// Whether sprite annihilate on exit screen
        /// </summary>
        private bool isAnnihilateOnExitScreen;

        /// <summary>
        /// Whether sprite is impassable
        /// </summary>
        private bool isImpassable;

        /// <summary>
        /// True: face left, False: face right
        /// </summary>
        private bool isPointingLeft;

        /// <summary>
        /// Whether player currently needs to release and press jump to jump again
        /// </summary>
        private bool isNeedToJumpAgain = false;

        /// <summary>
        /// Whether sprite is not drawn if dead
        /// </summary>
        private bool isInvisibleIfDead = false;

        /// <summary>
        /// Whether sprite is on vine
        /// </summary>
        private IClimbable iClimbingOn = null;

        /// <summary>
        /// Whether player currently needs to release and press attack to attack again
        /// </summary>
        private bool isNeedToAttackAgain = false;

        /// <summary>
        /// Whether player currently needs to release and press throw ninja rope again
        /// </summary>
        private bool isNeedToPressThrowNinjaRopeAgain;

        /// <summary>
        /// Whether sprite is currently running
        /// </summary>
        private bool isRunning = false;

        /// <summary>
        /// Whether sprite is currently trying to walk
        /// </summary>
        private bool isTryingToWalk = false;

        /// <summary>
        /// Whether sprite's walking direction is "right"
        /// </summary>
        private bool isTryingToWalkRight = false;

        /// <summary>
        /// Whether sprite is currently trying to move up (like entering door or pipe going up)
        /// </summary>
        private bool isTryToWalkUp = false;

        /// <summary>
        /// Whether sprite is trying to dig ground
        /// </summary>
        private bool isTryDigGround = false;

        /// <summary>
        /// Whether sprite is currently trying to jump
        /// </summary>
        private bool isTryingToJump = false;

        /// <summary>
        /// Whether sprite is currently crouching
        /// </summary>
        private bool isCrouch = false;

        /// <summary>
        /// Whether sprite is currently in water
        /// </summary>
        private bool isInWater = false;

        /// <summary>
        /// Whether sprite is currntly trying to slide
        /// </summary>
        private bool isTryingToSlide = false;

        /// <summary>
        /// Whether sprite is currntly alive
        /// </summary>
        private bool isAlive = true;

        /// <summary>
        /// Whether sprite is affected by gravity
        /// </summary>
        private bool isAffectedByGravity;

        /// <summary>
        /// Whether sprite can cross grounds when falling
        /// </summary>
        private bool isCrossGrounds;

        /// <summary>
        /// Whether we must apply full gravity force on next frame
        /// </summary>
        private bool isFullGravityOnNextFrame;

        /// <summary>
        /// Whether sprite is currently in free fall (walking speed)
        /// </summary>
        private bool isCurrentlyInFreeFallX;

        /// <summary>
        /// Whether sprite is currently in free fall (vertical falling speed)
        /// </summary>
        private bool isCurrentlyInFreeFallY;

        /// <summary>
        /// Whether sprite is vulnerable to punch
        /// </summary>
        private bool isVulnerableToPunch;

        /// <summary>
        /// Whether sprite is bound to ground and cannot leave it
        /// </summary>
        private bool isBoundToGroundForever;

        /// <summary>
        /// Max falling speed
        /// </summary>
        private double maxFallingSpeed = double.PositiveInfinity;

        /// <summary>
        /// When bouncing on this sprite
        /// 0: nothing, 1: regular jump
        /// </summary>
        private double bounciness;

        /// <summary>
        /// X position
        /// </summary>
        private double xPosition;

        /// <summary>
        /// Y position
        /// </summary>
        private double yPosition;

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
        /// Previous value of yPosition so we can know if the sprite is falling/jumping up or down
        /// </summary>
        private double yPositionPrevious;

        /// <summary>
        /// Previous value of xPosition so we can know if the sprite moved left or right
        /// </summary>
        private double xPositionPrevious;

        /// <summary>
        /// Jumping acceleration at begining of jump
        /// </summary>
        private double startingJumpAcceleration;

        /// <summary>
        /// Maximum walking speed
        /// </summary>
        private double maxWalkingSpeed;

        /// <summary>
        /// Walking acceleration (incrementation of speed when walking)
        /// </summary>
        private double walkingAcceleration;

        /// <summary>
        /// Maximum running speed
        /// </summary>
        private double maxRunningSpeed;

        /// <summary>
        /// Maximum health
        /// </summary>
        private double maxHealth;

        /// <summary>
        /// Current health
        /// </summary>
        private double health;

        /// <summary>
        /// Giving damage to other sprite on collision
        /// </summary>
        private double attackStrengthCollision;

        /// <summary>
        /// Time during which a sprite is blinking with invulneability after getting hit
        /// </summary>
        private double totalHitTime;

        /// <summary>
        /// Current walking speed
        /// </summary>
        private double currentWalkingSpeed = 0;

        /// <summary>
        /// Damage currently receiving
        /// </summary>
        private double currentDamageReceiving = 0.0;

        /// <summary>
        /// Sorting index
        /// </summary>
        private int sortingIndex;

        /// <summary>
        /// Z index (big numbers: frontmost)
        /// </summary>
        private int zIndex;

        /// <summary>
        /// Info about sprite
        /// </summary>
        private string tutorialComment;
        #endregion

        #region Constructor
        /// <summary>
        /// Create empty abstract sprite (don't use this constructor)
        /// </summary>
        public AbstractSprite()
        {
        }

        /// <summary>
        /// Create abstract sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public AbstractSprite(double xPosition, double yPosition, Random random)
        {
            maxHealth = BuildMaxHealth();
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            width = BuildWidth(random);
            height = BuildHeight(random);
            health = maxHealth;
            startingJumpAcceleration = BuildStartingJumpAcceleration();
            __parentBucketList = new HashSet<Bucket>();
            tutorialComment = BuildTutorialComment();
            maxWalkingSpeed = BuildMaxWalkingSpeed();
            maxRunningSpeed = BuildMaxRunningSpeed();
            walkingAcceleration = BuildWalkingAcceleration();
            attackStrengthCollision = BuildAttackStrengthCollision();
            isAffectedByGravity = BuildIsAffectedByGravity();
            bounciness = BuildBounciness();
            maxFallingSpeed = BuildMaxFallingSpeed();
            isImpassable = BuildIsImpassable();
            isAnnihilateOnExitScreen = BuildIsAnnihilateOnExitScreen();
            isCrossGrounds = BuildIsCrossGrounds();
            zIndex = BuildZIndex();
            isVulnerableToPunch = BuildIsVulnerableToPunch();
            isBoundToGroundForever = BuildIsBoundToGroundForever();
            walkingCycle = new Cycle(BuildWalkingCycleLength(),true);
            jumpingCycle = new Cycle(BuildJumpingTime(), false);
            attackingCycle = new Cycle(BuildAttackingTime(), false, false, true);
            totalHitTime = BuildHitTime();
            hitCycle = new Cycle(totalHitTime, false);
            punchedCycle = new Cycle(totalHitTime, false);

            if (this is IMovingGround)
            {
                Bounciness = 0.0;
                IsImpassable = true;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Build sprite's surface from file name
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>sprite's surface</returns>
        protected Surface BuildSpriteSurface(string fileName)
        {
            Surface spriteSurface = new Surface(fileName);

            if (Program.screenHeight == 1080)
            {
                //don't resize
            }
            else if (Program.screenHeight == 720)
            {
                //don't resize
            }
            else if (Program.screenHeight == 480)
            {
                //don't resize
            }
            else if (Program.screenHeight > 720)
            {
                double zoom = (double)Program.screenHeight / 1080.0;
                spriteSurface = spriteSurface.CreateScaledSurface(zoom);
            }
            else if (Program.screenHeight > 480)
            {
                double zoom = (double)Program.screenHeight / 720.0;
                spriteSurface = spriteSurface.CreateScaledSurface(zoom);
            }
            else //smaller than 480
            {
                double zoom = (double)Program.screenHeight / 480.0;
                spriteSurface = spriteSurface.CreateScaledSurface(zoom);
            }


            return spriteSurface;
        }
        #endregion

        #region Protected Abstract Methods
        /// <summary>
        /// Whether sprite can cross grounds when falling
        /// </summary>
        /// <returns>Whether sprite can cross grounds when falling</returns>
        protected abstract bool BuildIsCrossGrounds();

        /// <summary>
        /// Whether sprite is impassable
        /// </summary>
        /// <returns>Whether sprite is impassable</returns>
        protected abstract bool BuildIsImpassable();

        /// <summary>
        /// Whether sprite is affected by gravity
        /// </summary>
        /// <returns>Whether sprite is affected by gravity</returns>
        protected abstract bool BuildIsAffectedByGravity();

        /// <summary>
        /// Whether sprite will be annihilated when exiting screen
        /// </summary>
        /// <returns>Whether sprite will be annihilated when exiting screen</returns>
        protected abstract bool BuildIsAnnihilateOnExitScreen();

        /// <summary>
        /// Whether sprite is vulnerable to punch
        /// </summary>
        /// <returns>Whether sprite is vulnerable to punch</returns>
        protected abstract bool BuildIsVulnerableToPunch();

        /// <summary>
        /// Whether sprite is locked on ground forever once affected to it
        /// </summary>
        /// <returns>Whether sprite is locked on ground forever once affected to it</returns>
        protected abstract bool BuildIsBoundToGroundForever();

        /// <summary>
        /// Maximum health
        /// </summary>
        /// <returns>Maximum health</returns>
        protected abstract double BuildMaxHealth();

        /// <summary>
        /// Jumping time
        /// </summary>
        /// <returns>Jumping time</returns>
        protected abstract double BuildJumpingTime();

        /// <summary>
        /// Walking animation cycle length (time)
        /// </summary>
        /// <returns>Walking animation cycle length (time)</returns>
        protected abstract double BuildWalkingCycleLength();

        /// <summary>
        /// Walking acceleration (speed incrementation when walking)
        /// </summary>
        /// <returns>Walking acceleration (speed incrementation when walking)</returns>
        protected abstract double BuildWalkingAcceleration();

        /// <summary>
        /// Maximum walking speed
        /// </summary>
        /// <returns>Maximum walking speed</returns>
        protected abstract double BuildMaxWalkingSpeed();

        /// <summary>
        /// Maximum running speed
        /// </summary>
        /// <returns>Maximum running speed</returns>
        protected abstract double BuildMaxRunningSpeed();

        /// <summary>
        /// Jump acceleration at begining of jump
        /// </summary>
        /// <returns>Jump acceleration at begining of jump</returns>
        protected abstract double BuildStartingJumpAcceleration();

        /// <summary>
        /// Timespan of attack
        /// </summary>
        /// <returns></returns>
        protected abstract double BuildAttackingTime();

        /// <summary>
        /// Timespan of getting hit
        /// </summary>
        /// <returns>Timespan of getting hit</returns>
        protected abstract double BuildHitTime();

        /// <summary>
        /// Damage giving to other sprite on collision
        /// </summary>
        /// <returns>Damage giving to other sprite on collision</returns>
        protected abstract double BuildAttackStrengthCollision();

        /// <summary>
        /// sprite's width (1.5 = player's width)
        /// </summary>
        /// <returns>sprite's width (1.5 = player's width)</returns>
        protected abstract double BuildWidth(Random random);

        /// <summary>
        /// sprite's height (2.0 = player's height)
        /// </summary>
        /// <returns>sprite's height (2.0 = player's height)</returns>
        protected abstract double BuildHeight(Random random);

        /// <summary>
        /// When bouncing on this sprite
        /// 0: nothing, 1: regular jump
        /// </summary>
        protected abstract double BuildBounciness();

        /// <summary>
        /// Maximum speed when falling
        /// </summary>
        /// <returns>Maximum speed when falling</returns>
        protected abstract double BuildMaxFallingSpeed();

        /// <summary>
        /// Z Index for sprite (big numbers: frontmost)
        /// </summary>
        /// <returns>Z Index for sprite</returns>
        protected abstract int BuildZIndex();

        /// <summary>
        /// Info about sprite
        /// </summary>
        /// <returns>Info about sprite</returns>
        protected abstract string BuildTutorialComment();
        #endregion

        #region Public Abstract Methods
        /// <summary>
        /// Get sprite's surface
        /// </summary>
        /// <param name="xOffset">x Offset (1.0 = 1 tile)</param>
        /// <param name="yOffset">y Offset (1.0 = 1 tile)</param>
        /// <returns>Get current surface</returns>
        public abstract Surface GetCurrentSurface(out double xOffset, out double yOffset);
        #endregion

        #region Properties
        /// <summary>
        /// Sprite carried by this sprite
        /// </summary>
        public AbstractSprite CarriedSprite
        {
            get { return carriedSprite; }
            set { carriedSprite = value; }
        }

        /// <summary>
        /// X position
        /// </summary>
        public double XPosition
        {
            get { return xPosition; }
            set
            {
                xPositionPrevious = xPosition;
                __parentSpriteCollection.RemoveSpatialHashing(this);
                xPosition = value;
                __parentSpriteCollection.SetSpatialHashing(this);
            }
        }

        /// <summary>
        /// X position, keep previous position when setting this value
        /// </summary>
        public double XPositionKeepPrevious
        {
            get { return xPosition; }
            set
            {
                __parentSpriteCollection.RemoveSpatialHashing(this);
                xPosition = value;
                __parentSpriteCollection.SetSpatialHashing(this);
            }
        }

        /// <summary>
        /// Y position
        /// </summary>
        public double YPosition
        {
            get
            {
                if (this is IUpDownCycleMove && this.IsAlive)
                    return yPosition + ((IUpDownCycleMove)this).GetCurrentUpDownCycleHeightOffset();

                return yPosition;
            }
            set
            {
                yPositionPrevious = yPosition;
                __parentSpriteCollection.RemoveSpatialHashing(this);
                yPosition = value;
                __parentSpriteCollection.SetSpatialHashing(this);
            }
        }

        /// <summary>
        /// Y position
        /// </summary>
        public double YPositionKeepPrevious
        {
            get { return YPosition; }
            set
            {
                __parentSpriteCollection.RemoveSpatialHashing(this);
                yPosition = value;
                __parentSpriteCollection.SetSpatialHashing(this);
            }
        }

        /// <summary>
        /// Previous Y position (so we can know if the sprite is falling/jumping up or down
        /// </summary>
        public double YPositionPrevious
        {
            get
            {
                if (this is IUpDownCycleMove && this.IsAlive)
                    return yPositionPrevious + ((IUpDownCycleMove)this).GetCurrentUpDownCycleHeightOffset();

                return yPositionPrevious;
            }
        }

        /// <summary>
        /// Previous X position (so we can know if the sprite is moving left or right)
        /// </summary>
        public double XPositionPrevious
        {
            get { return xPositionPrevious; }
        }

        /// <summary>
        /// Last X distance of sprite move
        /// </summary>
        public double LastDistanceX
        {
            get{return xPosition - xPositionPrevious;}
        }

        /// <summary>
        /// Last Y distance of sprite move
        /// </summary>
        public double LastDistanceY
        {
            get { return yPosition - yPositionPrevious; }
        }

        /// <summary>
        /// Width
        /// </summary>
        public double Width
        {
            get
            {
                if (this is PlayerSprite && ((PlayerSprite)this).IsBeaver)
                    return width * 2.0;
                else
                    return width;
            }
            set
            {
                width = value;
            }
        }

        /// <summary>
        /// Height
        /// </summary>
        public double Height
        {
            get
            {
                if (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver))
                    return height / 2.0;
                else
                    return height;
            }

            set
            {
                if (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver))
                    height = value * 2.0;
                else
                    height = value;
            }
        }

        /// <summary>
        /// Whether sprite is currently running
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }

        /// <summary>
        /// Whether sprite's walking speed is constant
        /// </summary>
        public bool IsCurrentlyInFreeFallX
        {
            get { return isCurrentlyInFreeFallX; }
            set { isCurrentlyInFreeFallX = value; }
        }

        /// <summary>
        /// Whether sprite's vertical falling speed
        /// </summary>
        public bool IsCurrentlyInFreeFallY
        {
            get { return isCurrentlyInFreeFallY; }
            set { isCurrentlyInFreeFallY = value; }
        }

        /// <summary>
        /// Whether sprite is bound to ground and cannot leave it once affected to it
        /// </summary>
        public bool IsBoundToGroundForever
        {
            get { return isBoundToGroundForever; }
            set { isBoundToGroundForever = value; }
        }

        /// <summary>
        /// To which sprite collection the sprite belongs
        /// </summary>
        public SpritePopulation ParentSpriteCollection
        {
            get { return __parentSpriteCollection; }
            set { __parentSpriteCollection = value; }
        }

        /// <summary>
        /// List of buckets that contain sprite
        /// </summary>
        public HashSet<Bucket> ParentBucketList
        {
            get { return __parentBucketList; }
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
        /// Whether sprite is on vine
        /// </summary>
        public IClimbable IClimbingOn
        {
            get { return iClimbingOn; }
            set { iClimbingOn = value; }
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
        public IGround IGround
        {
            get { return iGround; }
            set
            {
                if (isBoundToGroundForever && iGround != null)
                    return;

                if (isAlive || value == null)
                {
                    iGround = value;
                }
            }
        }

        /// <summary>
        /// Ignore this climbable until touch ground
        /// </summary>
        public IClimbable IgnoreThisIClimbable
        {
            get { return ignoreThisIClimbable; }
            set { ignoreThisIClimbable = value; }
        }

        /// <summary>
        /// Walking animation cycle
        /// </summary>
        public Cycle WalkingCycle
        {
            get { return walkingCycle; }
        }

        /// <summary>
        /// Jumping cycle
        /// </summary>
        public Cycle JumpingCycle
        {
            get { return jumpingCycle; }
        }

        /// <summary>
        /// Attacking cycle
        /// </summary>
        public Cycle AttackingCycle
        {
            get { return attackingCycle; }
        }

        /// <summary>
        /// "Getting hit" cycle
        /// </summary>
        public Cycle HitCycle
        {
            get { return hitCycle; }
        }

        /// <summary>
        /// "Getting punched" cycle
        /// </summary>
        public Cycle PunchedCycle
        {
            get { return punchedCycle; }
        }

        /// <summary>
        /// Whether need to release and press jump again to jump
        /// </summary>
        public bool IsNeedToJumpAgain
        {
            get { return isNeedToJumpAgain; }
            set { isNeedToJumpAgain = value; }
        }

        /// <summary>
        /// Whether need to release and press attack again to attack
        /// </summary>
        public bool IsNeedToAttackAgain
        {
            get { return isNeedToAttackAgain; }
            set { isNeedToAttackAgain = value; }
        }

        /// <summary>
        /// Whether player currently needs to release and press throw ninja rope again
        /// </summary>
        public bool IsNeedToPressThrowNinjaRopeAgain
        {
            get { return isNeedToPressThrowNinjaRopeAgain; }
            set { isNeedToPressThrowNinjaRopeAgain = value; }
        }

        /// <summary>
        /// Whether is currently trying to walk
        /// </summary>
        public bool IsTryingToWalk
        {
            get { return isTryingToWalk; }
            set { isTryingToWalk = value; }
        }

        /// <summary>
        /// Whether current walking direction is "right", not "left"
        /// </summary>
        public bool IsTryingToWalkRight
        {
            get { return isTryingToWalkRight; }
            set { isTryingToWalkRight = value; }
        }

        /// <summary>
        /// Whether sprite is currently trying to jump
        /// </summary>
        public bool IsTryingToJump
        {
            get { return isTryingToJump; }
            set { isTryingToJump = value; }
        }

        /// <summary>
        /// Whether sprite is currently trying to move up (to enter a door or a pipe going up)
        /// </summary>
        public bool IsTryToWalkUp
        {
            get { return isTryToWalkUp; }
            set { isTryToWalkUp = value; }
        }

        /// <summary>
        /// Whether sprite is trying to dig ground
        /// </summary>
        public bool IsTryDigGround
        {
            get { return isTryDigGround; }
            set { isTryDigGround = value; }
        }

        /// <summary>
        /// Whether sprite is currently crouched
        /// </summary>
        public bool IsCrouch
        {
            get { return isCrouch; }
            set { isCrouch = value; }
        }

        /// <summary>
        /// Whether sprite is currently trying to slide
        /// </summary>
        public bool IsTryingToSlide
        {
            get { return isTryingToSlide; }
            set { isTryingToSlide = value; }
        }

        /// <summary>
        /// Whether sprite is currently alive
        /// </summary>
        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                currentDamageReceiving = 0.0;
                isAlive = value;
                if (isAlive)
                {
                    health = maxHealth;
                }
                else
                {
                    health = 0.0;
                    iGround = null;
                }
            }
        }

        /// <summary>
        /// Whether sprite is affected by gravity
        /// </summary>
        public bool IsAffectedByGravity
        {
            get { return isAffectedByGravity; }
            set { isAffectedByGravity = value; }
        }

        /// <summary>
        /// Whether sprite can cross grounds
        /// </summary>
        public bool IsCrossGrounds
        {
            get { return isCrossGrounds; }
            set { isCrossGrounds = value; }
        }

        /// <summary>
        /// Whether sprite is vulnerable to punch
        /// </summary>
        public bool IsVulnerableToPunch
        {
            get { return isVulnerableToPunch; }
            set { isVulnerableToPunch = value; }
        }

        /// <summary>
        /// Whether sprite is impassible
        /// </summary>
        public bool IsImpassable
        {
            get { return isImpassable; }
            set { isImpassable = value; }
        }

        /// <summary>
        /// Whether sprite annihilate on exit screen
        /// </summary>
        public bool IsAnnihilateOnExitScreen
        {
            get { return isAnnihilateOnExitScreen; }
        }

        /// <summary>
        /// Whether we must apply full gravity force on next frame
        /// </summary>
        public bool IsFullGravityOnNextFrame
        {
            get { return isFullGravityOnNextFrame; }
            set { isFullGravityOnNextFrame = value; }
        }

        /// <summary>
        /// Whether sprite is not drawn if dead
        /// </summary>
        public bool IsInvisibleIfDead
        {
            get { return isInvisibleIfDead; }
            set { isInvisibleIfDead = value; }
        }

        /// <summary>
        /// Acceleration at begining of jump
        /// </summary>
        public double StartingJumpAcceleration
        {
            get { return startingJumpAcceleration; }
            set { startingJumpAcceleration = value; }
        }

        /// <summary>
        /// Maximum walking speed
        /// </summary>
        public double MaxWalkingSpeed
        {
            get { return maxWalkingSpeed; }
            set { maxWalkingSpeed = value; }
        }

        /// <summary>
        /// Maximum running speed
        /// </summary>
        public double MaxRunningSpeed
        {
            get { return maxRunningSpeed; }
            set { maxRunningSpeed = value; }
        }

        /// <summary>
        /// Current walking speed
        /// </summary>
        public double CurrentWalkingSpeed
        {
            get { return currentWalkingSpeed; }
            set { currentWalkingSpeed = value; }
        }

        /// <summary>
        /// Speed incrementation when walking
        /// </summary>
        public double WalkingAcceleration
        {
            get { return walkingAcceleration; }
            set { walkingAcceleration = value; }
        }

        /// <summary>
        /// Sprite's top bound
        /// </summary>
        public double TopBound
        {
        	get
        	{
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
        			return YPosition - height / 2.0;
        		else
        			return YPosition - height;
        	}
            set
            {
                yPositionPrevious = yPosition;
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    yPosition = value + height / 2.0;
                else
                    yPosition = value + height;
            }
        }

        /// <summary>
        /// Sprite's previous top bound
        /// </summary>
        public double TopBoundPrevious
        {
            get
            {
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    return yPositionPrevious - height / 2.0;
                else
                    return yPositionPrevious - height;
            }
        }

        /// <summary>
        /// Sprite's top bound
        /// </summary>
        public double TopBoundKeepPrevious
        {
            get
            {
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    return yPosition - height / 2.0;
                else
                    return yPosition - height;
            }
            set
            {
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    yPosition = value + height / 2.0;
                else
                    yPosition = value + height;
            }
        }

        /// <summary>
        /// Sprite's right bound
        /// </summary>
        public double RightBound
        {
            get { return xPosition + Width / 2.0; }
        }

        /// <summary>
        /// Sprite's previous right bound
        /// </summary>
        public double RightBoundPrevious
        {
            get { return xPositionPrevious + Width / 2.0; }
        }

        /// <summary>
        /// Sprite's right bound
        /// </summary>
        public double RightBoundKeepPrevious
        {
            get { return xPosition + Width / 2.0; }
            set { xPosition = value - Width / 2.0; }
        }

        /// <summary>
        /// Sprite's left bound
        /// </summary>
        public double LeftBound
        {
            get { return xPosition - Width / 2.0; }
        }

        /// <summary>
        /// Sprite's left bound
        /// </summary>
        public double LeftBoundPrevious
        {
            get { return xPositionPrevious - Width / 2.0; }
        }

        /// <summary>
        /// Sprite's left bound
        /// </summary>
        public double LeftBoundKeepPrevious
        {
            get { return xPosition - Width / 2.0; }
            set { xPosition = value + Width / 2.0; }
        }

        /// <summary>
        /// Maximum height of step to climb
        /// </summary>
        public double MaximumWalkingHeight
        {
            get { return height / 4.0; }
        }

        /// <summary>
        /// Minimum height of step to fall
        /// </summary>
        public double MinimumFallingHeight
        {
            get { return height / 3.0; }
        }

        /// <summary>
        /// Current health
        /// </summary>
        public double Health
        {
            get { return health; }
            set
            {
                health = value;

                if (health < 0.05)
                    health = 0.0;

                if (health > maxHealth)
                    health = maxHealth;

                if (health <= 0)
                {
                    health = 0.0;
                    currentDamageReceiving = 0.0;
                    isAlive = false;
                    iGround = null;
                }
                else
                {
                    isAlive = true;
                }
            }
        }

        /// <summary>
        /// Maximum health
        /// </summary>
        public double MaxHealth
        {
            get { return maxHealth; }
        }

        /// <summary>
        /// Damage giving to other sprite when collisions occur
        /// </summary>
        public double AttackStrengthCollision
        {
            get { return attackStrengthCollision; }
            set { attackStrengthCollision = value; }
        }

        /// <summary>
        /// Current damage receiving (progressive decrease of health bar)
        /// </summary>
        public double CurrentDamageReceiving
        {
            get { return currentDamageReceiving; }
            set { currentDamageReceiving = value; }
        }

        /// <summary>
        /// Timespan of blinking with invulnerability after getting hit
        /// </summary>
        public double TotalHitTime
        {
            get { return totalHitTime; }
        }

        /// <summary>
        /// Right bound when punching right
        /// </summary>
        public double RightPunchBound
        {
            get
            {
                if (this is PlayerSprite && ((PlayerSprite)this).IsNinja && !(((PlayerSprite)this).IsBeaver))
                {
                    if (((PlayerSprite)this).IsTryUseNunchaku)
                        return RightBound + Program.nunchakuRange;
                    else
                        return RightBound + Program.swordRange;
                }
                else
                    return RightBound + Program.punchRange;
            }
        }

        /// <summary>
        /// Left bound when punching left
        /// </summary>
        public double LeftPunchBound
        {
            get
            {
                if (this is PlayerSprite && ((PlayerSprite)this).IsNinja && !(((PlayerSprite)this).IsBeaver))
                {
                    if (((PlayerSprite)this).IsTryUseNunchaku)
                        return LeftBound - Program.nunchakuRange;
                    else
                        return LeftBound - Program.swordRange;
                }
                else
                    return LeftBound - Program.punchRange;
            }
        }

        /// <summary>
        /// When bouncing on this sprite
        /// 0: nothing, 1: regular jump
        /// </summary>
        public double Bounciness
        {
            get { return bounciness; }
            set { bounciness = value; }
        }

        /// <summary>
        /// Sorting index
        /// </summary>
        public int SortingIndex
        {
            get { return sortingIndex; }
            set { sortingIndex = value; }
        }

        /// <summary>
        /// Maximum falling speed
        /// </summary>
        public double MaxFallingSpeed
        {
            get { return maxFallingSpeed; }
            set { maxFallingSpeed = value; }
        }

        /// <summary>
        /// Top bound
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <returns>Top bound</returns>
        public double this[double xPosition]
        {
            get { return TopBound; }
        }

        /// <summary>
        /// Whether sprite is currently tiny
        /// </summary>
        public bool IsTiny
        {
            get { return isTiny; }
            set { isTiny = value; }
        }

        /// <summary>
        /// Whether sprite is currently in water
        /// </summary>
        public bool IsInWater
        {
            get { return isInWater; }
            set { isInWater = value; }
        }

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        public string TutorialComment
        {
            get { return tutorialComment; }
        }

        /// <summary>
        /// Z index (big numbers: frontmost)
        /// </summary>
        public int ZIndex
        {
            get { return zIndex; }
            set { zIndex = value; }
        }
        #endregion

        #region IComparable<AbstractSprite> Membres
        public int CompareTo(AbstractSprite other)
        {
            return (int)(sortingIndex * 100.0 - other.sortingIndex * 100.0);
        }
        #endregion
    }
}
