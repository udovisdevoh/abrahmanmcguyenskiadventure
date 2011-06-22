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
        /// Whether player currently needs to release and press attack to attack again
        /// </summary>
        private bool isNeedToAttackAgain = false;

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
        /// Max falling speed
        /// </summary>
        private float maxFallingSpeed = float.PositiveInfinity;

        /// <summary>
        /// When bouncing on this sprite
        /// 0: nothing, 1: regular jump
        /// </summary>
        private float bounciness;

        /// <summary>
        /// X position
        /// </summary>
        private float xPosition;

        /// <summary>
        /// Y position
        /// </summary>
        private float yPosition;

        /// <summary>
        /// Height
        /// </summary>
        private float height;

        /// <summary>
        /// Width
        /// </summary>
        private float width;

        /// <summary>
        /// Current jump or falling acceleration
        /// </summary>
        private float currentJumpAcceleration;

        /// <summary>
        /// Previous value of yPosition so we can know if the sprite is falling/jumping up or down
        /// </summary>
        private float yPositionPrevious;

        /// <summary>
        /// Previous value of xPosition so we can know if the sprite moved left or right
        /// </summary>
        private float xPositionPrevious;

        /// <summary>
        /// Jumping acceleration at begining of jump
        /// </summary>
        private float startingJumpAcceleration;

        /// <summary>
        /// Maximum walking speed
        /// </summary>
        private float maxWalkingSpeed;

        /// <summary>
        /// Walking acceleration (incrementation of speed when walking)
        /// </summary>
        private float walkingAcceleration;

        /// <summary>
        /// Maximum running speed
        /// </summary>
        private float maxRunningSpeed;

        /// <summary>
        /// Maximum health
        /// </summary>
        private float maxHealth;

        /// <summary>
        /// Current health
        /// </summary>
        private float health;

        /// <summary>
        /// Giving damage to other sprite on collision
        /// </summary>
        private float attackStrengthCollision;

        /// <summary>
        /// Time during which a sprite is blinking with invulneability after getting hit
        /// </summary>
        private float totalHitTime;

        /// <summary>
        /// Current walking speed
        /// </summary>
        private float currentWalkingSpeed = 0f;

        /// <summary>
        /// Damage currently receiving
        /// </summary>
        private float currentDamageReceiving = 0f;

        /// <summary>
        /// Distance to reference sprite
        /// </summary>
        private float distanceToReferenceSprite;
        #endregion

        #region Constructor
        /// <summary>
        /// Create abstract sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public AbstractSprite(float xPosition, float yPosition, Random random)
        {
            maxHealth = BuildMaxHealth();
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            width = BuildWidth(random);
            height = BuildHeight(random);
            health = maxHealth;
            startingJumpAcceleration = BuildStartingJumpAcceleration();
            __parentBucketList = new HashSet<Bucket>();
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
            walkingCycle = new Cycle(BuildWalkingCycleLength(),true);
            jumpingCycle = new Cycle(BuildJumpingTime(), false);
            attackingCycle = new Cycle(BuildAttackingTime(), false);
            totalHitTime = BuildHitTime();
            hitCycle = new Cycle(totalHitTime, false);
            punchedCycle = new Cycle(totalHitTime, false);
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
        /// Maximum health
        /// </summary>
        /// <returns>Maximum health</returns>
        protected abstract float BuildMaxHealth();

        /// <summary>
        /// Jumping time
        /// </summary>
        /// <returns>Jumping time</returns>
        protected abstract float BuildJumpingTime();

        /// <summary>
        /// Walking animation cycle length (time)
        /// </summary>
        /// <returns>Walking animation cycle length (time)</returns>
        protected abstract float BuildWalkingCycleLength();

        /// <summary>
        /// Walking acceleration (speed incrementation when walking)
        /// </summary>
        /// <returns>Walking acceleration (speed incrementation when walking)</returns>
        protected abstract float BuildWalkingAcceleration();

        /// <summary>
        /// Maximum walking speed
        /// </summary>
        /// <returns>Maximum walking speed</returns>
        protected abstract float BuildMaxWalkingSpeed();

        /// <summary>
        /// Maximum running speed
        /// </summary>
        /// <returns>Maximum running speed</returns>
        protected abstract float BuildMaxRunningSpeed();

        /// <summary>
        /// Jump acceleration at begining of jump
        /// </summary>
        /// <returns>Jump acceleration at begining of jump</returns>
        protected abstract float BuildStartingJumpAcceleration();

        /// <summary>
        /// Timespan of attack
        /// </summary>
        /// <returns></returns>
        protected abstract float BuildAttackingTime();

        /// <summary>
        /// Timespan of getting hit
        /// </summary>
        /// <returns>Timespan of getting hit</returns>
        protected abstract float BuildHitTime();

        /// <summary>
        /// Damage giving to other sprite on collision
        /// </summary>
        /// <returns>Damage giving to other sprite on collision</returns>
        protected abstract float BuildAttackStrengthCollision();

        /// <summary>
        /// sprite's width (1.5 = player's width)
        /// </summary>
        /// <returns>sprite's width (1.5 = player's width)</returns>
        protected abstract float BuildWidth(Random random);

        /// <summary>
        /// sprite's height (2.0 = player's height)
        /// </summary>
        /// <returns>sprite's height (2.0 = player's height)</returns>
        protected abstract float BuildHeight(Random random);

        /// <summary>
        /// When bouncing on this sprite
        /// 0: nothing, 1: regular jump
        /// </summary>
        protected abstract float BuildBounciness();

        /// <summary>
        /// Maximum speed when falling
        /// </summary>
        /// <returns>Maximum speed when falling</returns>
        protected abstract float BuildMaxFallingSpeed();
        #endregion

        #region Public Abstract Methods
        /// <summary>
        /// Get sprite's surface
        /// </summary>
        /// <param name="xOffset">x Offset (1.0 = 1 tile)</param>
        /// <param name="yOffset">y Offset (1.0 = 1 tile)</param>
        /// <returns>Get current surface</returns>
        public abstract Surface GetCurrentSurface(out float xOffset, out float yOffset);
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

            if (Program.screenHeight != 480)
            {
                float zoom = (float)Program.screenHeight / 480.0f;
                spriteSurface = spriteSurface.CreateScaledSurface(zoom);
            }

            return spriteSurface;
        }
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
        public float XPosition
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
        public float XPositionKeepPrevious
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
        public float YPosition
        {
            get { return yPosition; }
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
        public float YPositionKeepPrevious
        {
            get { return yPosition; }
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
        public float YPositionPrevious
        {
            get { return yPositionPrevious; }
        }

        /// <summary>
        /// Previous X position (so we can know if the sprite is moving left or right)
        /// </summary>
        public float XPositionPrevious
        {
            get { return xPositionPrevious; }
        }

        /// <summary>
        /// Last X distance of sprite move
        /// </summary>
        public float LastDistanceX
        {
            get{return xPosition - xPositionPrevious;}
        }

        /// <summary>
        /// Last Y distance of sprite move
        /// </summary>
        public float LastDistanceY
        {
            get { return yPosition - yPositionPrevious; }
        }

        /// <summary>
        /// Width
        /// </summary>
        public float Width
        {
            get
            {
                if (this is PlayerSprite && ((PlayerSprite)this).IsBeaver)
                    return width * 2.0f;
                else
                    return width;
            }
        }

        /// <summary>
        /// Height
        /// </summary>
        public float Height
        {
            get
            {
                if (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver))
                    return height / 2.0f;
                else
                    return height;
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
        /// Current jump or falling acceleration
        /// </summary>
        public float CurrentJumpAcceleration
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
                if (isAlive || value == null)
                {
                    iGround = value;
                }
            }
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
                currentDamageReceiving = 0f;
                isAlive = value;
                if (isAlive)
                {
                    health = maxHealth;
                }
                else
                {
                    health = 0f;
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
        /// Acceleration at begining of jump
        /// </summary>
        public float StartingJumpAcceleration
        {
            get { return startingJumpAcceleration; }
        }

        /// <summary>
        /// Maximum walking speed
        /// </summary>
        public float MaxWalkingSpeed
        {
            get { return maxWalkingSpeed; }
        }

        /// <summary>
        /// Maximum running speed
        /// </summary>
        public float MaxRunningSpeed
        {
            get { return maxRunningSpeed; }
        }

        /// <summary>
        /// Current walking speed
        /// </summary>
        public float CurrentWalkingSpeed
        {
            get { return currentWalkingSpeed; }
            set { currentWalkingSpeed = value; }
        }

        /// <summary>
        /// Speed incrementation when walking
        /// </summary>
        public float WalkingAcceleration
        {
            get { return walkingAcceleration; }
        }

        /// <summary>
        /// Sprite's top bound
        /// </summary>
        public float TopBound
        {
        	get
        	{
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
        			return yPosition - height / 2.0f;
        		else
        			return yPosition - height;
        	}
            set
            {
                yPositionPrevious = yPosition;
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    yPosition = value + height / 2.0f;
                else
                    yPosition = value + height;
            }
        }

        /// <summary>
        /// Sprite's previous top bound
        /// </summary>
        public float TopBoundPrevious
        {
            get
            {
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    return yPositionPrevious - height / 2.0f;
                else
                    return yPositionPrevious - height;
            }
        }

        /// <summary>
        /// Sprite's top bound
        /// </summary>
        public float TopBoundKeepPrevious
        {
            get
            {
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    return yPosition - height / 2.0f;
                else
                    return yPosition - height;
            }
            set
            {
                if (isCrouch || (isTiny && (!(this is PlayerSprite) || !((PlayerSprite)this).IsBeaver)))
                    yPosition = value + height / 2.0f;
                else
                    yPosition = value + height;
            }
        }

        /// <summary>
        /// Sprite's right bound
        /// </summary>
        public float RightBound
        {
            get { return xPosition + Width / 2.0f; }
        }

        /// <summary>
        /// Sprite's previous right bound
        /// </summary>
        public float RightBoundPrevious
        {
            get { return xPositionPrevious + Width / 2.0f; }
        }

        /// <summary>
        /// Sprite's right bound
        /// </summary>
        public float RightBoundKeepPrevious
        {
            get { return xPosition + Width / 2.0f; }
            set { xPosition = value - Width / 2.0f; }
        }

        /// <summary>
        /// Sprite's left bound
        /// </summary>
        public float LeftBound
        {
            get { return xPosition - Width / 2.0f; }
        }

        /// <summary>
        /// Sprite's left bound
        /// </summary>
        public float LeftBoundPrevious
        {
            get { return xPositionPrevious - Width / 2.0f; }
        }

        /// <summary>
        /// Sprite's left bound
        /// </summary>
        public float LeftBoundKeepPrevious
        {
            get { return xPosition - Width / 2.0f; }
            set { xPosition = value + Width / 2.0f; }
        }

        /// <summary>
        /// Maximum height of step to climb
        /// </summary>
        public float MaximumWalkingHeight
        {
            get { return height / 4.0f; }
        }

        /// <summary>
        /// Minimum height of step to fall
        /// </summary>
        public float MinimumFallingHeight
        {
            get { return height / 3.0f; }
        }

        /// <summary>
        /// Current health
        /// </summary>
        public float Health
        {
            get { return health; }
            set
            {
                health = value;

                if (health < 0.05f)
                    health = 0f;

                if (health > maxHealth)
                    health = maxHealth;

                if (health <= 0f)
                {
                    health = 0f;
                    currentDamageReceiving = 0f;
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
        public float MaxHealth
        {
            get { return maxHealth; }
        }

        /// <summary>
        /// Damage giving to other sprite when collisions occur
        /// </summary>
        public float AttackStrengthCollision
        {
            get { return attackStrengthCollision; }
        }

        /// <summary>
        /// Current damage receiving (progressive decrease of health bar)
        /// </summary>
        public float CurrentDamageReceiving
        {
            get { return currentDamageReceiving; }
            set { currentDamageReceiving = value; }
        }

        /// <summary>
        /// Timespan of blinking with invulnerability after getting hit
        /// </summary>
        public float TotalHitTime
        {
            get { return totalHitTime; }
        }

        /// <summary>
        /// Right bound when punching right
        /// </summary>
        public float RightPunchBound
        {
            get { return RightBound + 1.2f; }
        }

        /// <summary>
        /// Left bound when punching left
        /// </summary>
        public float LeftPunchBound
        {
            get { return LeftBound - 1.2f; }
        }

        /// <summary>
        /// When bouncing on this sprite
        /// 0: nothing, 1: regular jump
        /// </summary>
        public float Bounciness
        {
            get { return bounciness; }
            set { bounciness = value; }
        }

        /// <summary>
        /// Distance to reference sprite
        /// </summary>
        public float DistanceToReferenceSprite
        {
            get { return distanceToReferenceSprite; }
            set { distanceToReferenceSprite = value; }
        }

        /// <summary>
        /// Maximum falling speed
        /// </summary>
        public float MaxFallingSpeed
        {
            get { return maxFallingSpeed; }
            set { maxFallingSpeed = value; }
        }

        /// <summary>
        /// Top bound
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <returns>Top bound</returns>
        public float this[float xPosition]
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
        #endregion

        #region IComparable<AbstractSprite> Membres
        public int CompareTo(AbstractSprite other)
        {
            return (int)(distanceToReferenceSprite * 100f - other.distanceToReferenceSprite * 100f);
        }
        #endregion
    }
}
