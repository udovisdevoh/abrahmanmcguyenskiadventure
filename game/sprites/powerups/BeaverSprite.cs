using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// A beaver (similar behavior to Yoshi)
    /// </summary>
    internal class BeaverSprite : MonsterSprite, IGrowable
    {
        #region Constants
        public const double DefaultMaxWalkingSpeed = 0.35;

        public const double DefaultStartingJumpAcceleration = 5.0;
        #endregion

        #region Fields and parts
        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface walkBRight;

        private static Surface walkBLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private static Surface dead;

        private static Surface standRightNinja;

        private static Surface standLeftNinja;

        private static Surface walkRightNinja;

        private static Surface walkBRightNinja;

        private static Surface walkBLeftNinja;

        private static Surface walkLeftNinja;

        private static Surface hitRightNinja;

        private static Surface hitLeftNinja;

        private static Surface deadNinja;

        /// <summary>
        /// Cycle of growth
        /// </summary>
        private Cycle growthCycle;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "The beaver! Hop on it.\nPress attack to eat things.\nCrouch and press attack to grind the ground.";
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BeaverSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (standLeft == null)
            {
                if (Program.screenHeight > 720)
                {
                    standRight = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverStand.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverWalk.png");
                    walkBRight = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverWalkB.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverHit.png");
                    standRightNinja = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverStandNinja.png");
                    walkRightNinja = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverWalkNinja.png");
                    walkBRightNinja = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverWalkBNinja.png");
                    hitRightNinja = BuildSpriteSurface("./assets/rendered/1080/beaver/BeaverHitNinja.png");
                }
                else if (Program.screenHeight > 480)
                {
                    standRight = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverStand.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverWalk.png");
                    walkBRight = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverWalkB.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverHit.png");
                    standRightNinja = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverStandNinja.png");
                    walkRightNinja = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverWalkNinja.png");
                    walkBRightNinja = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverWalkBNinja.png");
                    hitRightNinja = BuildSpriteSurface("./assets/rendered/720/beaver/BeaverHitNinja.png");
                }
                else
                {
                    standRight = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverStand.png");
                    walkRight = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverWalk.png");
                    walkBRight = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverWalkB.png");
                    hitRight = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverHit.png");
                    standRightNinja = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverStandNinja.png");
                    walkRightNinja = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverWalkNinja.png");
                    walkBRightNinja = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverWalkBNinja.png");
                    hitRightNinja = BuildSpriteSurface("./assets/rendered/480/beaver/BeaverHitNinja.png");
                }

                standLeft = standRight.CreateFlippedHorizontalSurface();
                walkLeft = walkRight.CreateFlippedHorizontalSurface();
                walkBLeft = walkBRight.CreateFlippedHorizontalSurface();
                hitLeft = hitRight.CreateFlippedHorizontalSurface();
                dead = hitRight.CreateFlippedVerticalSurface();
                standLeftNinja = standRightNinja.CreateFlippedHorizontalSurface();
                walkLeftNinja = walkRightNinja.CreateFlippedHorizontalSurface();
                walkBLeftNinja = walkBRightNinja.CreateFlippedHorizontalSurface();
                hitLeftNinja = hitRightNinja.CreateFlippedHorizontalSurface();

                deadNinja = hitRightNinja.CreateFlippedVerticalSurface();
            }
            IsVulnerableToPunch = false;
        }
        #endregion

        #region Override
        protected override double BuildMaxHealth()
        {
            return 100;
        }

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
            return DefaultMaxWalkingSpeed;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.60;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return DefaultStartingJumpAcceleration;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildHitTime()
        {
            return 0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.5;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.9;
        }

        protected override double BuildBounciness()
        {
            return 1.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return false;
        }

        protected override bool BuildIsEnableJumpOnConversion()
        {
            return false;
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

        protected override bool BuildIsJumpableOn()
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

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override double BuildJumpProbability()
        {
            return 0.1;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

            if (IsAiEnabled)
            {
                if (!IsAlive)
                    return deadNinja;

                if (cycleDivision == 1 || cycleDivision == 3)
                {
                    if (IsTryingToWalkRight)
                        return walkBRightNinja;
                    else
                        return walkBLeftNinja;
                }
                else if (cycleDivision == 2)
                {
                    if (IsTryingToWalkRight)
                        return walkRightNinja;
                    else
                        return walkLeftNinja;
                }
                else
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return hitRightNinja;
                        else
                            return hitLeftNinja;
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return standRightNinja;
                        else
                            return standLeftNinja;
                    }
                }
            }
            else
            {
                if (!IsAlive)
                    return dead;

                if (cycleDivision == 1 || cycleDivision == 3)
                {
                    if (IsTryingToWalkRight)
                        return walkBRight;
                    else
                        return walkBLeft;
                }
                else if (cycleDivision == 2)
                {
                    if (IsTryingToWalkRight)
                        return walkRight;
                    else
                        return walkLeft;
                }
                else
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return hitRight;
                        else
                            return hitLeft;
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return standRight;
                        else
                            return standLeft;
                    }
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Cycle of growth
        /// </summary>
        public Cycle GrowthCycle
        {
            get { return growthCycle; }
        }
        #endregion
    }
}
