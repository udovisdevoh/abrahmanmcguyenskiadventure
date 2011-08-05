using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Lawyer sprite
    /// </summary>
    internal class LawyerSprite : MonsterSprite, IFlyingOnEqualDistance
    {
        #region Fields and parts
        private static Surface standingRight;

        private static Surface standingLeft;

        private static Surface deadSurface;

        private double safeYDistanceFromPlayer;

        private double flyingSpeed;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Don't let the lawyer sue you!";
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public LawyerSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (standingRight == null)
            {
                standingRight = BuildSpriteSurface("./assets/rendered/lawyer/lawyer.png");
                standingLeft = standingRight.CreateFlippedHorizontalSurface();
                deadSurface = standingRight.CreateFlippedVerticalSurface();
            }

            flyingSpeed = random.NextDouble() * 0.1 + 0.045;
            MaxWalkingSpeed = random.NextDouble() * 0.02 + 0.10;
            safeYDistanceFromPlayer = random.NextDouble() * 1.8 - 0.9;
            IsCrossGrounds = true;
        }
        #endregion

        #region Overrides
        protected override bool BuildIsJumpableOn()
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return false;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsEnableSpontaneousConversion()
        {
            return false;
        }

        protected override bool BuildIsEnableJumpOnConversion()
        {
            return false;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return false;
        }



        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildJumpProbability()
        {
            return 0;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 0.0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 100.0;
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 4;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.02;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.11;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.20;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.15;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 0.5;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;

            if (!IsAlive)
                return deadSurface;

            if (IsTryingToWalkRight)
                return standingRight;
            else
                return standingLeft;
        }
        #endregion

        #region IFlyingOnXWave Members
        public double SafeYDistanceFromPlayer
        {
            get { return safeYDistanceFromPlayer; }
        }

        public double FlyingYSpeed
        {
            get { return flyingSpeed; }
        }

        public bool IsOnlyMoveWhenNotBeingLookedAt
        {
            get { return true; }
        }
        #endregion
    }
}
