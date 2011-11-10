using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Ronald
    /// </summary>
    internal class RonaldSprite : MonsterSprite, IProjectileShooter
    {
        #region Fields and parts
        private static Surface standRight;

        private static Surface standLeft;

        private static Surface dead;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private Cycle shootingCycle;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Beware of the evil clown.\nHe will try to sell you rotten meat of\nmistreated industrially raised cows feeded\nwith hormons and antibiotics\nwhile they swim in 3 feet deep of their own feces.\nI'm lovin' it.";
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public RonaldSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            shootingCycle = new Cycle(MaxShootingTimeBetween, false);
            shootingCycle.Fire();
            if (standRight == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/ronald/ronaldStand.png");
                standLeft = standRight.CreateFlippedHorizontalSurface();

                walkRight = BuildSpriteSurface("./assets/rendered/ronald/ronaldWalk.png");
                walkLeft = walkRight.CreateFlippedHorizontalSurface();

                hitRight = BuildSpriteSurface("./assets/rendered/ronald/ronaldHit.png");
                hitLeft = hitRight.CreateFlippedHorizontalSurface();

                dead = hitRight.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Override Methods
        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 5;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.25;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.55;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.2;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.92;
        }

        protected override double BuildMaxHealth()
        {
            return 1.5;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        protected override double BuildJumpProbability()
        {
            return 0.2;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 2.0;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return true;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return true;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
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

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;

            if (!IsAlive)
                return dead;

            if (HitCycle.IsFired)
            {
                if (IsTryingToWalkRight)
                    return hitRight;
                else
                    return hitLeft;
            }

            if (CurrentJumpAcceleration != 0)
            {
                if (IsTryingToWalkRight)
                    return walkRight;
                else
                    return walkLeft;
            }

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);
            if (cycleDivision == 1 || cycleDivision == 3)
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
        #endregion

        #region IProjectileShooter Membres
        public AbstractSprite GetProjectile(Random random)
        {
            return new HamburgerSprite(XPosition, TopBound, random);
        }

        public Cycle ShootingCycle
        {
            get { return shootingCycle; }
        }

        public Type ProjectileType
        {
            get { return typeof(HamburgerSprite); }
        }

        public int MaxProjectileCountPerScreen
        {
            get { return 3; }
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
            get { return 18.0; }
        }
        #endregion
    }
}
