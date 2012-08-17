using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class CarSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface walking1LeftSurface;

        private static Surface walking1RightSurface;

        private static Surface walking2LeftSurface;

        private static Surface walking2RightSurface;

        private static Surface walking3LeftSurface;

        private static Surface walking3RightSurface;

        private static Surface deadSurface;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Oh no! A douchebag.";
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public CarSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (walking1RightSurface == null)
            {
                if (Program.screenHeight > 720)
                {
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/1080/car/Car1.png");
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/1080/car/Car2.png");
                    walking3RightSurface = BuildSpriteSurface("./assets/rendered/1080/car/Car3.png");
                }
                else if (Program.screenHeight > 480)
                {
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/720/car/Car1.png");
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/720/car/Car2.png");
                    walking3RightSurface = BuildSpriteSurface("./assets/rendered/720/car/Car3.png");
                }
                else
                {
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/480/car/Car1.png");
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/480/car/Car2.png");
                    walking3RightSurface = BuildSpriteSurface("./assets/rendered/480/car/Car3.png");
                }

                walking1LeftSurface = walking1RightSurface.CreateFlippedHorizontalSurface();
                walking2LeftSurface = walking2RightSurface.CreateFlippedHorizontalSurface();
                walking3LeftSurface = walking3RightSurface.CreateFlippedHorizontalSurface();

                deadSurface = walking1RightSurface.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Override Methods
        protected override double BuildJumpingTime()
        {
            return 0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 1;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.017;
            //return 0.004;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.30;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.45;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 10.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 4.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.9375;
        }

        protected override double BuildMaxHealth()
        {
            return 0.5;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
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

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
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

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
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

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
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
            xOffset = 0;
            yOffset = 0;

            if (!IsAlive)
                return deadSurface;

            if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(3.0);

                if (cycleDivision == 0)
                {
                    if (IsTryingToWalkRight)
                        return walking1RightSurface;
                    else
                        return walking1LeftSurface;
                }
                else if (cycleDivision == 1)
                {
                    if (IsTryingToWalkRight)
                        return walking2RightSurface;
                    else
                        return walking2LeftSurface;
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return walking3RightSurface;
                    else
                        return walking3LeftSurface;
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                    return walking1RightSurface;
                else
                    return walking1LeftSurface;
            }
        }
        #endregion
    }
}