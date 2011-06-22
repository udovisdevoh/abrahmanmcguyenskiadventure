using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Muslim
    /// </summary>
    internal class MuslimSprite : MonsterSprite, IExplodable
    {
        #region Fields and parts
        private static Surface standRight;

        private static Surface standLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private static Surface walk1Right;

        private static Surface walk2Right;

        private static Surface walk1Left;

        private static Surface walk2Left;

        private static Surface deadSurface;

        private Cycle countDownCycle;

        private float minDistanceFromPlayerToStartCountDown;
        #endregion

        #region Constructor
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MuslimSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            minDistanceFromPlayerToStartCountDown = 5;
            countDownCycle = new Cycle(125, false);

            if (standRight == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/muslim/muslimStand.png");
                standLeft = standRight.CreateFlippedHorizontalSurface();

                hitRight = BuildSpriteSurface("./assets/rendered/muslim/muslimHit.png");
                hitLeft = hitRight.CreateFlippedHorizontalSurface();

                walk1Right = BuildSpriteSurface("./assets/rendered/muslim/muslimWalk1.png");
                walk1Left = walk1Right.CreateFlippedHorizontalSurface();
                
                walk2Right = BuildSpriteSurface("./assets/rendered/muslim/muslimWalk2.png");
                walk2Left = walk1Right.CreateFlippedHorizontalSurface();

                deadSurface = standRight.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Overrides
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
            //return random.Next(0, 2) == 1;
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsJumpableOn()
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

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return false;
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

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override float BuildJumpProbability()
        {
            return 0.17f;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 10;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override float BuildMaxHealth()
        {
            return 100;
        }

        protected override float BuildJumpingTime()
        {
            return 10;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 5;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.01f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.45f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.45f;
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
            return 0.0f;
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
            return 0.0f;
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
                    return walk1Right;
                else
                    return walk1Left;
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0f);

                if (cycleDivision == 1)
                {
                    if (IsTryingToWalkRight)
                        return walk1Right;
                    else
                        return walk1Left;
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
                        return walk2Right;
                    else
                        return walk2Left;
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

        #region IExplodable Members
        public float MinDistanceFromPlayerToStartCountDown
        {
            get { return minDistanceFromPlayerToStartCountDown; }
        }

        public Cycle CountDownCycle
        {
            get { return countDownCycle; }
        }
        #endregion
    }
}
