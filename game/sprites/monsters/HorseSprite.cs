using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Horse
    /// </summary>
    internal class HorseSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface deadSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public HorseSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (standRight == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/horse/horse1.png");
                standLeft = standRight.CreateFlippedHorizontalSurface();
                walkRight = BuildSpriteSurface("./assets/rendered/horse/horse2.png");
                walkLeft = walkRight.CreateFlippedHorizontalSurface();
                deadSurface = standRight.CreateFlippedVerticalSurface();
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
            return 0.013;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.19;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.39;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 18.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 3.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.5;
        }

        protected override double BuildMaxHealth()
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
            return random.Next(0, 3) == 1;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
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
            return true;
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

                if (cycleDivision == 1 || cycleDivision == 3)
                {
                    if (IsTryingToWalkRight)
                        return walkRight;
                    else
                        return walkLeft;
                }
            }

            if (IsTryingToWalkRight)
                return standRight;
            else
                return standLeft;
        }
        #endregion
    }
}
