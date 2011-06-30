using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    class FarmerSprite : MonsterSprite, IProjectileShooter, IFlyingOnEqualDistance
    {
        #region Fields and parts
        private Cycle shootingCycle;

        private static Surface standRight;

        private static Surface deadSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public FarmerSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            shootingCycle = new Cycle(MaxShootingTimeBetween, false);
            shootingCycle.Fire();
            if (standRight == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/farmer/farmer.png");
                deadSurface = standRight.CreateFlippedVerticalSurface();
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
            return random.Next(0,2) == 1;
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
            return true;
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
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
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

        protected override double BuildJumpProbability()
        {
            return 0.10;
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
            return 0.5;
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
            return 1.71;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 4.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;

            if (!IsAlive)
                return deadSurface;

            return standRight;
        }
        #endregion

        #region IProjectileShooter Members
        public AbstractSprite GetProjectile(Random random)
        {
            CornSprite cornSprite = new CornSprite(XPosition, TopBound, random);
            cornSprite.IGround = null;
            cornSprite.CurrentJumpAcceleration = 0.25;
            cornSprite.IsCurrentlyInFreeFallX = true;
            cornSprite.CurrentWalkingSpeed = CurrentWalkingSpeed;
            cornSprite.JumpingCycle.Fire();
            return cornSprite;
        }

        public Cycle ShootingCycle
        {
            get { return shootingCycle; }
        }

        public Type ProjectileType
        {
            get { return typeof(CornSprite); }
        }

        public int MaxProjectileCountPerScreen
        {
            get { return 4; }
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
            get { return 30.0; }
        }
        #endregion

        #region IFlyingOnXWave Members
        public double SafeYDistanceFromPlayer
        {
            get { return 4; }
        }

        public double FlyingYSpeed
        {
            get { return 0.05; }
        }
        #endregion
    }
}
