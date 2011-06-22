using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a mormon
    /// </summary>
    internal class MormonSprite : MonsterSprite, IProjectileShooter
    {
        #region Fields and parts
        private Cycle shootingCycle;

        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private static Surface attackRight;

        private static Surface attackLeft;

        private static Surface deadSurface;
        #endregion

        #region Constructor
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MormonSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            shootingCycle = new Cycle(MaxShootingTimeBetween, false);
            shootingCycle.Fire();
            if (standRight == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/mormon/MormonStand.png");
                standLeft = standRight.CreateFlippedHorizontalSurface();

                hitRight = BuildSpriteSurface("./assets/rendered/mormon/MormonHit.png");
                hitLeft = hitRight.CreateFlippedHorizontalSurface();

                walkRight = BuildSpriteSurface("./assets/rendered/mormon/MormonWalk.png");
                walkLeft = walkRight.CreateFlippedHorizontalSurface();

                attackRight = BuildSpriteSurface("./assets/rendered/mormon/MormonAttack.png");
                attackLeft = attackRight.CreateFlippedHorizontalSurface();

                deadSurface = hitRight.CreateFlippedVerticalSurface();
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override float BuildJumpProbability()
        {
            return 0.10;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
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

        protected override float BuildMaxHealth()
        {
            return 1.0;
        }

        protected override float BuildJumpingTime()
        {
            return 10.0;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 5;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.01;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.35;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.65;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override float BuildAttackingTime()
        {
            return 4;
        }

        protected override float BuildHitTime()
        {
            return 32;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override float BuildHeight(Random random)
        {
            return 2.0;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 6.0;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = yOffset = 0;

            if (!IsAlive)
                return deadSurface;

            if (CurrentJumpAcceleration != 0)
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return hitRight;
                    else
                        return hitLeft;
                }

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
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return hitRight;
                        else
                            return hitLeft;
                    }

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
        public AbstractSprite GetProjectile(Random random)
        {
            return new BibleSprite(XPosition, TopBound, random);
        }

        public Cycle ShootingCycle
        {
            get { return shootingCycle; }
        }

        public float MinShootingTimeBetween
        {
            get { return 50.0; }
        }

        public float MaxShootingTimeBetween
        {
            get { return 100.0; }
        }

        public float MaxShootingDistance
        {
            get { return 14.0; }
        }
        #endregion
    }
}
