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

        private double minDistanceFromPlayerToStartCountDown;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Beware the member of a religion of peace.\nHe will try to peacefully explode on you.\nYou cannot do damage to this enemy directly.";
        #endregion

        #region Constructor
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MuslimSprite(double xPosition, double yPosition, Random random)
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

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
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

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
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

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildJumpProbability()
        {
            return 0.17;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
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

        protected override double BuildMaxHealth()
        {
            return 100.0;
        }

        protected override double BuildJumpingTime()
        {
            return 10;
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
            return 0.45;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.45;
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

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.0;
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
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
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
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

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
        public double MinDistanceFromPlayerToStartCountDown
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
