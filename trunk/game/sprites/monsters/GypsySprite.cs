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
        #endregion

        #region Constructor
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public GypsySprite(float xPosition, float yPosition, Random random)
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

        protected override float BuildJumpProbability()
        {
            return 0.10f;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 0.0f;
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
            return 1.0f;
        }

        protected override float BuildJumpingTime()
        {
            return 10.0f;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 2.5f;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.02f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.50f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.75f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 25.0f;
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
            return 0.5f;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0f;
        }

        protected override float BuildHeight(Random random)
        {
            return 2.0f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 10.0f;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
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
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0f);

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
        public AbstractSprite GetProjectile(Random random)
        {
            return new CrystalBallSprite(XPosition, TopBound, random);
        }

        public Cycle ShootingCycle
        {
            get { return shootingCycle; }
        }

        public float MinShootingTimeBetween
        {
            get { return 50.0f; }
        }

        public float MaxShootingTimeBetween
        {
            get { return 100.0f; }
        }

        public float MaxShootingDistance
        {
            get { return 15.0f; }
        }
        #endregion
    }
}
