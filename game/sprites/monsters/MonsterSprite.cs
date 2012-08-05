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
    abstract class MonsterSprite : AbstractSprite, IEvilSprite
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
        /// small: easy monster, big: tough monster
        /// </summary>
        private double skillDispatchRatio;
        
        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        private double jumpProbability;

        /// <summary>
        /// When AI is enabled, keep safe distance to player
        /// </summary>
        private double safeDistanceAi;

        /// <summary>
        /// Some monsters (raptors and raptorJesus) can do no damage when they hit you from the bottom in their dead zone
        /// </summary>
        private double bottomHitCollisionDeadZoneHeight = 0.0;

        /// <summary>
        /// Some monsters (raptors and raptorJesus) can do no damage when they hit you from the bottom in their dead zone,
        /// BUT they have a foot in the middle and can give you damage anyways. [Width of the foot]
        /// </summary>
        private double bottomHitCollisionDeadZoneExceptionRadius = 0.0;

        /// <summary>
        /// For sprite dispatcher
        /// </summary>
        private double subjectiveOccurenceProbability;

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
        /// Whether we can jump on this sprite as a beaver
        /// </summary>
        private bool isJumpableOnEvenByBeaver;

        /// <summary>
        /// Whether sprite is vulnerable to invincibility
        /// </summary>
        private bool isVulnerableToInvincibility;

        /// <summary>
        /// Whether sprite makes metal sound when touching ground
        /// </summary>
        private bool isMakeSoundWhenTouchGround;

        /// <summary>
        /// Whether sprite can do damage when in free fall
        /// </summary>
        private bool isCanDoDamageWhenInFreeFall;

        /// <summary>
        /// Whether sprite has a dead zone collision at the bottom of it (ie raptors and raptorJesuses)
        /// </summary>
        private bool isUseBottomHitCollisionDeadZone;

        /// <summary>
        /// Some monsters (raptors and raptorJesus) can do no damage when they hit you from the bottom in their dead zone,
        /// BUT they have a foot in the middle and can give you damage anyways. [Width of the foot]
        /// </summary>
        private bool isUseBottomHitCollisionDeadZoneExceptionRadius;

        /// <summary>
        /// Whether sprite is vulnerable to katana but not vulnerable to punch
        /// </summary>
        private bool isVulnerableToKatanaButNotPunch;

        /// <summary>
        /// Whether sprite can resist fireballs and shurikens
        /// </summary>
        private bool isResistantToPlayerProjectile;
        #endregion

        #region Constructors
        /// <summary>
        /// Create monster sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MonsterSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            isWalkEnabled = true;
            kickedHelmetCycle = new Cycle(16.0,false);
            spontaneousTransformationCycle = new Cycle(256, false);
            changeDirectionNoAiCycle = new Cycle(BuildChangeDirectionNoAiCycleLength(),true);
            safeDistanceAi = BuildSafeDistanceAi();
            jumpProbability = BuildJumpProbability();
            subjectiveOccurenceProbability = BuildSubjectiveOccurenceProbability();
            isCanJump = BuildIsCanJump(random);
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
            isJumpableOnEvenByBeaver = BuildIsJumpableOnEvenByBeaver();
            isNoAiChangeDirectionByCycle = BuildIsNoAiChangeDirectionByCycle();
            isDieOnTouchGround = BuildIsDieOnTouchGround();
            isVulnerableToInvincibility = BuildIsVulnerableToInvincibility();
            isMakeSoundWhenTouchGround = BuildIsMakeSoundWhenTouchGround();
            isCanDoDamageWhenInFreeFall = BuildIsCanDoDamageWhenInFreeFall();
            skillDispatchRatio = BuildSkillDispatchRatio();
            isVulnerableToKatanaButNotPunch = false;
            isResistantToPlayerProjectile = false;
            if (isNoAiChangeDirectionByCycle)
                changeDirectionNoAiCycle.Fire();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Small: easy, big: tough
        /// </summary>
        /// <returns>Ratio of a monster's skill level</returns>
        private double BuildSkillDispatchRatio()
        {
            double skillDispatchRatio = 0;

            if (!isVulnerableToKatanaButNotPunch && !isResistantToPlayerProjectile && MaxHealth < 10.0)
                skillDispatchRatio += MaxHealth;
            if (isCanDoDamageToPlayerWhenTouched && AttackStrengthCollision < 10.0)
                skillDispatchRatio += AttackStrengthCollision;
            skillDispatchRatio += MaxWalkingSpeed;

            if (isAiEnabled)
                skillDispatchRatio += 0.5;
            if (!isJumpableOn)
                skillDispatchRatio += 0.5;
            if (this is IProjectileShooter)
                skillDispatchRatio += 0.5;
            if (this is IFluctuatingSafeDistance)
                skillDispatchRatio += 0.5;
            if (isAvoidFall)
                skillDispatchRatio += 0.5;
            if (isToggleWalkWhenJumpedOn)
                skillDispatchRatio += 0.333;
            if (this is IExplodable)
                skillDispatchRatio += 1.5;
            if (IsCrossGrounds)
                skillDispatchRatio += 0.5;
            if (!isJumpableOn)
                skillDispatchRatio += 0.5;
            if (isResistantToPlayerProjectile)
                skillDispatchRatio += 1.0;
            if (isVulnerableToKatanaButNotPunch)
                skillDispatchRatio += 1.0;

            return skillDispatchRatio;
        }
        #endregion

        #region Abstract methods
        /// <summary>
        /// Whether we can jump on this sprite
        /// </summary>
        /// <returns>Whether we can jump on this sprite</returns>
        protected abstract bool BuildIsJumpableOn();

        /// <summary>
        /// Whether we can jump on this sprite as a beaver
        /// </summary>
        /// <returns>Whether we can jump on this sprite as a beaver</returns>
        protected abstract bool BuildIsJumpableOnEvenByBeaver();

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
        /// Whether sprite can do damage when in free fall
        /// </summary>
        /// <returns>Whether sprite can do damage when in free fall</returns>
        protected abstract bool BuildIsCanDoDamageWhenInFreeFall();

        /// <summary>
        /// Build subjective occurence probability
        /// </summary>
        /// <returns>Build subjective occurence probability</returns>
        protected abstract double BuildSubjectiveOccurenceProbability();

        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        /// <returns>Probability of jumping (from 0 to 1)</returns>
        protected abstract double BuildJumpProbability();

        /// <summary>
        /// Some sprites change direction automatically
        /// </summary>
        /// <returns>Some sprites change direction automatically</returns>
        protected abstract double BuildChangeDirectionNoAiCycleLength();

        /// <summary>
        /// Distance to keep from player (if AI is on)
        /// </summary>
        /// <returns>Distance to keep from player (if AI is on)</returns>
        protected abstract double BuildSafeDistanceAi();

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

        protected override bool BuildIsVulnerableToPunch()
        {
            return true;
        }

        protected override double BuildBounciness()
        {
            return 1.0;
        }

        protected override double BuildMaxFallingSpeed()
        {
            return double.PositiveInfinity;
        }

        protected override int BuildZIndex()
        {
            return 1;
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
            set { isCanJump = value; }
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
            set { isAiEnabled = value; }
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
            set { isFullSpeedAfterBounceNoAi = value; }
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
            set { isCanDoDamageToPlayerWhenTouched = value; }
        }

        /// <summary>
        /// Whether sprite will change direction if no AI, when stucked
        /// </summary>
        public bool IsNoAiChangeDirectionWhenStucked
        {
            get { return isNoAiChangeDirectionWhenStucked; }
            set { isNoAiChangeDirectionWhenStucked = value; }
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
        /// Whether we can jump on this sprite as a beaver
        /// </summary>
        public bool IsJumpableOnEvenByBeaver
        {
            get { return isJumpableOnEvenByBeaver; }
        }

        /// <summary>
        /// Whether sprite dies when touch ground
        /// </summary>
        public bool IsDieOnTouchGround
        {
            get { return isDieOnTouchGround; }
            set { isDieOnTouchGround = value; }
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
        /// Whether sprite can do damage when in free fall
        /// </summary>
        public bool IsCanDoDamageWhenInFreeFall
        {
            get { return isCanDoDamageWhenInFreeFall; }
        }

        /// <summary>
        /// Whether sprite has a dead zone collision at the bottom of it (ie raptors and raptorJesuses)
        /// </summary>
        public bool IsUseBottomHitCollisionDeadZone
        {
            get { return isUseBottomHitCollisionDeadZone; }
            set { isUseBottomHitCollisionDeadZone = value; }
        }

        /// <summary>
        /// Some monsters (raptors and raptorJesus) can do no damage when they hit you from the bottom in their dead zone,
        /// BUT they have a foot in the middle and can give you damage anyways. [Width of the foot]
        /// </summary>
        public bool IsUseBottomHitCollisionDeadZoneExceptionRadius
        {
            get { return isUseBottomHitCollisionDeadZoneExceptionRadius; }
            set { isUseBottomHitCollisionDeadZoneExceptionRadius = value; }
        }

        /// <summary>
        /// Whether sprite is vulnerable to katana but not vulnerable to punch
        /// </summary>
        public bool IsVulnerableToKatanaButNotPunch
        {
            get { return isVulnerableToKatanaButNotPunch; }
            set { isVulnerableToKatanaButNotPunch = value; }
        }

        /// <summary>
        /// Whether sprite can resist fireballs and shurikens
        /// </summary>
        public bool IsResistantToPlayerProjectile
        {
            get { return isResistantToPlayerProjectile; }
            set { isResistantToPlayerProjectile = value; }
        }

        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        public double JumpProbability
        {
            get { return jumpProbability; }
        }

        /// <summary>
        /// When AI is enabled, keep safe distance to player
        /// </summary>
        public double SafeDistanceAi
        {
            get { return safeDistanceAi; }
            set { safeDistanceAi = value; }
        }

        /// <summary>
        /// Some monsters (raptors and raptorJesus) can do no damage when they hit you from the bottom in their dead zone
        /// </summary>
        public double BottomHitCollisionDeadZoneHeight
        {
            get { return bottomHitCollisionDeadZoneHeight; }
            set { bottomHitCollisionDeadZoneHeight = value; }
        }

        /// <summary>
        /// Some monsters (raptors and raptorJesus) can do no damage when they hit you from the bottom in their dead zone,
        /// BUT they have a foot in the middle and can give you damage anyways. [Width of the foot]
        /// </summary>
        public double BottomHitCollisionDeadZoneExceptionRadius
        {
            get { return bottomHitCollisionDeadZoneExceptionRadius; }
            set { bottomHitCollisionDeadZoneExceptionRadius = value; }
        }

        /// <summary>
        /// small: easy monster, big: tough monster
        /// burger: 1.17
        /// jew: 2.85
        /// raptor: 3.45
        /// riot1: 2.17
        /// riot2: 1.67
        /// mormon: 2.85
        /// farmer: 2.61
        /// </summary>
        public double SkillDispatchRatio
        {
            get { return skillDispatchRatio; }
        }

        /// <summary>
        /// For sprite dispatcher
        /// </summary>
        public double SubjectiveOccurenceProbability
        {
            get { return subjectiveOccurenceProbability; }
        }
        #endregion
    }
}