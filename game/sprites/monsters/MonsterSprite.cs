using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using System.Drawing;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a monster
    /// </summary>
    abstract class MonsterSprite : AbstractSprite
    {
        #region Fields and parts
        /// <summary>
        /// Cycle of kicked sprite (for instance, helmet)
        /// When cycle is fired, no damage is given from touching this sprite
        /// </summary>
        private Cycle kickedHelmetCycle;

        /// <summary>
        /// At the end of the cycle, the sprite will start shaking (if fired)
        /// At the very end, the sprite will be transformed into another sprite (if fired)
        /// </summary>
        private Cycle spontaneousTransformationCycle;

        /// <summary>
        /// Some sprites change direction automatically according to a cycle
        /// </summary>
        private Cycle changeDirectionNoAiCycle;
        
        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        private float jumpProbability;

        /// <summary>
        /// When AI is enabled, keep safe distance to player
        /// </summary>
        private float safeDistanceAi;

        /// <summary>
        /// Whether monster can jump
        /// </summary>
        private bool isCanJump;

        /// <summary>
        /// Whether monster flees when getting attacked
        /// </summary>
        private bool isFleeWhenAttacked;

        /// <summary>
        /// Whether monster is chasing player
        /// </summary>
        private bool isAiEnabled;

        /// <summary>
        /// Whether sprite will change direction if no AI, when stucked
        /// </summary>
        private bool isNoAiChangeDirectionWhenStucked;

        /// <summary>
        /// Whether spritr will change direction (if no AI) using a cycle
        /// </summary>
        private bool isNoAiChangeDirectionByCycle;

        /// <summary>
        /// Whether monster is walking right, not left, when monster is not chasing player
        /// </summary>
        private bool isNoAiDefaultDirectionWalkingRight;

        /// <summary>
        /// Whether monster will try to avoid falling in holes
        /// </summary>
        private bool isAvoidFall;

        /// <summary>
        /// Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)
        /// </summary>
        private bool isFullSpeedAfterBounceNoAi;

        /// <summary>
        /// Whether monster can walk, or is stoped (i.e. stopped helmets)
        /// </summary>
        private bool isWalkEnabled;

        /// <summary>
        /// Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)
        /// </summary>
        private bool isToggleWalkWhenJumpedOn;

        /// <summary>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </summary>
        private bool isInstantKickConvertedSprite;

        /// <summary>
        /// Whether this sprite can be spontaneously transformed into another one when not walking for too long
        /// </summary>
        private bool isEnableSpontaneousConversion;

        /// <summary>
        /// Whether sprite can be converted to another sprite when getting jumped on
        /// </summary>
        private bool isEnableJumpOnConversion;

        /// <summary>
        /// Whether sprite can do damage to player when touched
        /// </summary>
        private bool isCanDoDamageToPlayerWhenTouched;

        /// <summary>
        /// If monster sprite has no AI, die when can't move
        /// </summary>
        private bool isNoAiDieWhenStucked;

        /// <summary>
        /// Sprite dies when touch ground
        /// </summary>
        private bool isDieOnTouchGround;

        /// <summary>
        /// If sprite has no AI, always jump when touch ground
        /// </summary>
        private bool isNoAiAlwaysBounce;

        /// <summary>
        /// Whether we can jump on this sprite
        /// </summary>
        private bool isJumpableOn;

        /// <summary>
        /// Whether sprite is vulnerable to invincibility
        /// </summary>
        private bool isVulnerableToInvincibility;

        /// <summary>
        /// Whether sprite makes metal sound when touching ground
        /// </summary>
        private bool isMakeSoundWhenTouchGround;
        #endregion

        #region Constructors
        /// <summary>
        /// Create monster sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MonsterSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            isWalkEnabled = true;
            kickedHelmetCycle = new Cycle(16.0f,false);
            spontaneousTransformationCycle = new Cycle(256, false);
            changeDirectionNoAiCycle = new Cycle(BuildChangeDirectionNoAiCycleLength(),true);
            safeDistanceAi = BuildSafeDistanceAi();
            isCanJump = BuildIsCanJump(random);
            jumpProbability = BuildJumpProbability();
            isFleeWhenAttacked = BuildIsFleeWhenAttacked(random);
            isAiEnabled = BuildIsAiEnabled();
            isAvoidFall = BuildIsAvoidFall(random);
            isNoAiDefaultDirectionWalkingRight = random.Next(0, 2) == 1;
            isFullSpeedAfterBounceNoAi = BuildIsFullSpeedAfterBounceNoAi();
            isToggleWalkWhenJumpedOn = BuildIsToggleWalkWhenJumpedOn();
            isInstantKickConvertedSprite = BuildIsInstantKickConvertedSprite();
            isEnableSpontaneousConversion = BuildIsEnableSpontaneousConversion();
            isEnableJumpOnConversion = BuildIsEnableJumpOnConversion();
            isCanDoDamageToPlayerWhenTouched = BuildIsCanDoDamageToPlayerWhenTouched();
            isNoAiChangeDirectionWhenStucked = BuildIsNoAiChangeDirectionWhenStucked();
            isNoAiDieWhenStucked = BuildIsNoAiDieWhenStucked();
            isNoAiAlwaysBounce = BuildIsNoAiAlwaysBounce();
            isJumpableOn = BuildIsJumpableOn();
            isNoAiChangeDirectionByCycle = BuildIsNoAiChangeDirectionByCycle();
            isDieOnTouchGround = BuildIsDieOnTouchGround();
            isVulnerableToInvincibility = BuildIsVulnerableToInvincibility();
            isMakeSoundWhenTouchGround = BuildIsMakeSoundWhenTouchGround();
            if (isNoAiChangeDirectionByCycle)
                changeDirectionNoAiCycle.Fire();
        }
        #endregion

        #region Abstract methods
        /// <summary>
        /// Whether we can jump on this sprite
        /// </summary>
        /// <returns>Whether we can jump on this sprite</returns>
        protected abstract bool BuildIsJumpableOn();

        /// <summary>
        /// Whether monster is chasing player
        /// </summary>
        /// <returns>Whether monster is chasing player</returns>
        protected abstract bool BuildIsAiEnabled();

        /// <summary>
        /// Whether monster can jump
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Whether monster can jump</returns>
        protected abstract bool BuildIsCanJump(Random random);

        /// <summary>
        /// Whether monster will try to avoid falling in holes
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Whether monster will try to avoid falling in holes</returns>
        protected abstract bool BuildIsAvoidFall(Random random);

        /// <summary>
        /// Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)
        /// </summary>
        /// <returns>Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)</returns>
        protected abstract bool BuildIsToggleWalkWhenJumpedOn();

        /// <summary>
        /// Whether sprite will flee just after getting attacked
        /// </summary>
        /// <returns>Whether sprite will flee just after getting attacked</returns>
        protected abstract bool BuildIsFleeWhenAttacked(Random random);

        /// <summary>
        /// Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)
        /// </summary>
        /// <returns>Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)</returns>
        protected abstract bool BuildIsFullSpeedAfterBounceNoAi();

        /// <summary>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </summary>
        /// <returns>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </returns>
        protected abstract bool BuildIsInstantKickConvertedSprite();

        /// <summary>
        /// Whether this sprite can be spontaneously transformed into another one when not walking for too long
        /// </summary>
        /// <returns>Whether this sprite can be spontaneously transformed into another one when not walking for too long</returns>
        protected abstract bool BuildIsEnableSpontaneousConversion();

        /// <summary>
        /// Whether sprite can be converted to another sprite when getting jumped on
        /// </summary>
        /// <returns>Whether sprite can be converted to another sprite when getting jumped on</returns>
        protected abstract bool BuildIsEnableJumpOnConversion();

        /// <summary>
        /// Whether sprite can do damage to player
        /// </summary>
        /// <returns>Whether sprite can do damage to player</returns>
        protected abstract bool BuildIsCanDoDamageToPlayerWhenTouched();

        /// <summary>
        /// Whether sprite will change direction if no AI, when stucked
        /// </summary>
        /// <returns>Whether sprite will change direction if no AI, when stucked</returns>
        protected abstract bool BuildIsNoAiChangeDirectionWhenStucked();

        /// <summary>
        /// If monster sprite has no AI, die when can't move
        /// </summary>
        /// <returns>If monster sprite has no AI, die when can't move</returns>
        protected abstract bool BuildIsNoAiDieWhenStucked();

        /// <summary>
        /// If sprite has no AI, always jump when touch ground
        /// </summary>
        /// <returns>If sprite has no AI, always jump when touch ground</returns>
        protected abstract bool BuildIsNoAiAlwaysBounce();

        /// <summary>
        /// Whether spritr will change direction (if no AI) using a cycle
        /// </summary>
        /// <returns>Whether spritr will change direction (if no AI) using a cycle</returns>
        protected abstract bool BuildIsNoAiChangeDirectionByCycle();

        /// <summary>
        /// Whether sprite dies when touches ground
        /// </summary>
        /// <returns>Whether sprite dies when touches ground</returns>
        protected abstract bool BuildIsDieOnTouchGround();

        /// <summary>
        /// Whether sprite makes metal sound when touches ground
        /// </summary>
        /// <returns>Whether sprite makes metal sound when touches ground</returns>
        protected abstract bool BuildIsMakeSoundWhenTouchGround();

        /// <summary>
        /// Whether sprite is vulnerable to invincibility
        /// </summary>
        /// <returns></returns>
        protected abstract bool BuildIsVulnerableToInvincibility();

        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        /// <returns>Probability of jumping (from 0 to 1)</returns>
        protected abstract float BuildJumpProbability();

        /// <summary>
        /// Some sprites change direction automatically
        /// </summary>
        /// <returns>Some sprites change direction automatically</returns>
        protected abstract float BuildChangeDirectionNoAiCycleLength();

        /// <summary>
        /// Distance to keep from player (if AI is on)
        /// </summary>
        /// <returns>Distance to keep from player (if AI is on)</returns>
        protected abstract float BuildSafeDistanceAi();

        /// <summary>
        /// Sprite when converted to another sprite (default: null)
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Sprite when converted to another sprite (default: null)</returns>
        public abstract AbstractSprite GetConverstionSprite(Random random);
        #endregion

        #region Override methods
        protected override bool BuildIsAffectedByGravity()
        {
            return true;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsCrossGrounds()
        {
            return false;
        }

        protected override float BuildBounciness()
        {
            return 1f;
        }

        protected override float BuildMaxFallingSpeed()
        {
            return float.PositiveInfinity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Cycle of kicked sprite (for instance, helmet)
        /// When cycle is fired, no damage is given from touching this sprite
        /// </summary>
        public Cycle KickedHelmetCycle
        {
            get { return kickedHelmetCycle; }
        }

        /// <summary>
        /// At the end of the cycle, the sprite will start shaking (if fired)
        /// At the very end, the sprite will be transformed into another sprite (if fired)
        /// </summary>
        public Cycle SpontaneousTransformationCycle
        {
            get { return spontaneousTransformationCycle; }
        }

        /// <summary>
        /// Change direction no AI cycle
        /// </summary>
        public Cycle ChangeDirectionNoAiCycle
        {
            get { return changeDirectionNoAiCycle; }
        }

        /// <summary>
        /// Whether monster can jump
        /// </summary>
        public bool IsCanJump
        {
            get { return isCanJump; }
        }

        /// <summary>
        /// Whether sprite will flee just after getting attacked
        /// </summary>
        public bool IsFleeWhenAttacked
        {
            get { return isFleeWhenAttacked; }
        }

        /// <summary>
        /// Whether monster is chasing player
        /// </summary>
        public bool IsAiEnabled
        {
            get { return isAiEnabled; }
        }

        /// <summary>
        /// Whether monster is walking right, not left, when monster is not chasing player
        /// </summary>
        public bool IsNoAiDefaultDirectionWalkingRight
        {
            get { return isNoAiDefaultDirectionWalkingRight; }
            set { isNoAiDefaultDirectionWalkingRight = value; }
        }

        /// <summary>
        /// Whether monster will try to avoid falling in holes
        /// </summary>
        public bool IsAvoidFall
        {
            get { return isAvoidFall; }
            set { isAvoidFall = value; }
        }

        /// <summary>
        /// Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)
        /// </summary>
        public bool IsFullSpeedAfterBounceNoAi
        {
            get { return isFullSpeedAfterBounceNoAi; }
        }

        /// <summary>
        /// Whether monster can currently walk, or is stoped (i.e. stopped helmets)
        /// </summary>
        public bool IsWalkEnabled
        {
            get { return isWalkEnabled; }
            set { isWalkEnabled = value; }
        }

        /// <summary>
        /// Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)
        /// </summary>
        public bool IsToggleWalkWhenJumpedOn
        {
            get { return isToggleWalkWhenJumpedOn; }
        }

        /// <summary>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </summary>
        public bool IsInstantKickConvertedSprite
        {
            get { return isInstantKickConvertedSprite; }
        }

        /// <summary>
        /// Whether this sprite can be spontaneously transformed into another one when not walking for too long
        /// </summary>
        public bool IsEnableSpontaneousConversion
        {
            get { return isEnableSpontaneousConversion; }
        }

        /// <summary>
        /// Whether sprite can be converted to another sprite when getting jumped on
        /// </summary>
        public bool IsEnableJumpOnConversion
        {
            get { return isEnableJumpOnConversion; }
        }

        /// <summary>
        /// Whether sprite can do damage to player when touched
        /// </summary>
        public bool IsCanDoDamageToPlayerWhenTouched
        {
            get { return isCanDoDamageToPlayerWhenTouched; }
        }

        /// <summary>
        /// Whether sprite will change direction if no AI, when stucked
        /// </summary>
        public bool IsNoAiChangeDirectionWhenStucked
        {
            get { return isNoAiChangeDirectionWhenStucked; }
        }

        /// <summary>
        /// If monster sprite has no AI, die when can't move
        /// </summary>
        public bool IsNoAiDieWhenStucked
        {
            get { return isNoAiDieWhenStucked; }
        }

        /// <summary>
        /// If sprite has no AI, always jump when touch ground
        /// </summary>
        public bool IsNoAiAlwaysBounce
        {
            get { return isNoAiAlwaysBounce; }
        }

        /// <summary>
        /// Whether spritr will change direction (if no AI) using a cycle
        /// </summary>
        public bool IsNoAiChangeDirectionByCycle
        {
            get { return isNoAiChangeDirectionByCycle; }
        }

        /// <summary>
        /// Whether we can jump on this sprite
        /// </summary>
        public bool IsJumpableOn
        {
            get { return isJumpableOn; }
        }

        /// <summary>
        /// Whether sprite dies when touch ground
        /// </summary>
        public bool IsDieOnTouchGround
        {
            get { return isDieOnTouchGround; }
        }

        /// <summary>
        /// Whether sprite makes metal sound when touching ground
        /// </summary>
        public bool IsMakeSoundWhenTouchGround
        {
            get { return isMakeSoundWhenTouchGround; }
        }

        /// <summary>
        /// Whether sprite is vulnerable to invincibility
        /// </summary>
        public bool IsVulnerableToInvincibility
        {
            get { return isVulnerableToInvincibility; }
        }

        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        public float JumpProbability
        {
            get { return jumpProbability; }
        }

        /// <summary>
        /// When AI is enabled, keep safe distance to player
        /// </summary>
        public float SafeDistanceAi
        {
            get { return safeDistanceAi; }
        }
        #endregion
    }
}