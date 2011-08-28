using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Gypsy girl
    /// </summary>
    internal class GypsySprite : MonsterSprite, IProjectileShooter
    {
        #region Fields and parts
        private Cycle shootingCycle;

        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface deadSurface;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Beware the astrologist.\nAstrology is pure bullshit.";
        #endregion

        #region Constructor
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public GypsySprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            shootingCycle = new Cycle(MaxShootingTimeBetween, false);
            shootingCycle.Fire();
            if (standRight == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/gypsy/GypsyStand.png");
                standLeft = standRight.CreateFlippedHorizontalSurface();

                walkRight = BuildSpriteSurface("./assets/rendered/gypsy/GypsyWalk.png");
                walkLeft = walkRight.CreateFlippedHorizontalSurface();

                deadSurface = walkRight.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return true;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return true;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsEnableSpontaneousConversion()
        {
            return false;
        }

        protected override bool BuildIsEnableJumpOnConversion()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsVulnerableToInvincibility()
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

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildJumpProbability()
        {
            return 0.10;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 0.0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        public override SideScrollerSprite GetConverstionSprite(Random random)
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
            return 0.5;
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 2.5;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.01;
            //return 0.02;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.50;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.75;
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
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 10.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;

            if (!IsAlive)
                return deadSurface;

            if (CurrentJumpAcceleration != 0)
            {
                if (IsTryingToWalkRight)
                    return walkRight;
                else
                    return walkLeft;
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                if (cycleDivision == 1)
                {
                    if (IsTryingToWalkRight)
                        return walkRight;
                    else
                        return walkLeft;
                }
                else if (cycleDivision == 3)
                {
                    if (IsTryingToWalkRight)
                        return walkRight;
                    else
                        return walkLeft;
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return standRight;
                    else
                        return standLeft;
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                    return standRight;
                else
                    return standLeft;
            }
        }
        #endregion

        #region IProjectileShooter Members
        public SideScrollerSprite GetProjectile(Random random)
        {
            return new CrystalBallSprite(XPosition, TopBound, random);
        }

        public Cycle ShootingCycle
        {
            get { return shootingCycle; }
        }

        public Type ProjectileType
        {
            get { return typeof(CrystalBallSprite); }
        }

        public int MaxProjectileCountPerScreen
        {
            get { return 0; }
        }

        public double MinShootingTimeBetween
        {
            get { return 50.0; }
        }

        public double MaxShootingTimeBetween
        {
            get { return 100.0; }
        }

        public double MaxShootingDistance
        {
            get { return 15.0; }
        }
        #endregion
    }
}
