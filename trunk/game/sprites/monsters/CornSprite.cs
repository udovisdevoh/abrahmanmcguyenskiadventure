using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    class CornSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface standSurfaceRight;
        
        private static Surface standSurfaceLeft;

        private static Surface walkSurfaceRight;

        private static Surface walkSurfaceLeft;

        private static Surface rotate1;

        private static Surface rotate2;

        private static Surface rotate3;

        private static Surface rotate4;

        private static Surface rotate5;

        private static Surface rotate6;

        private static Surface rotate7;

        private static Surface rotate8;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public CornSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (standSurfaceRight == null)
            {
                standSurfaceRight = BuildSpriteSurface("./assets/rendered/corn/cornStand.png");
                standSurfaceLeft = standSurfaceRight.CreateFlippedHorizontalSurface();
                walkSurfaceRight = BuildSpriteSurface("./assets/rendered/corn/cornWalk.png");
                walkSurfaceLeft = walkSurfaceRight.CreateFlippedHorizontalSurface();

                rotate1 = BuildSpriteSurface("./assets/rendered/corn/corn1.png");
                rotate2 = BuildSpriteSurface("./assets/rendered/corn/corn2.png");
                rotate3 = BuildSpriteSurface("./assets/rendered/corn/corn3.png");
                rotate4 = rotate2.CreateFlippedHorizontalSurface();
                rotate5 = rotate1.CreateFlippedHorizontalSurface();
                rotate6 = rotate4.CreateFlippedVerticalSurface();
                rotate7 = rotate3.CreateFlippedVerticalSurface();
                rotate8 = rotate2.CreateFlippedVerticalSurface();
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
            return 0.3;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.17;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.55;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            //return 25.0;
            return 5.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildMaxHealth()
        {
            return 0.5;
        }

        protected override double BuildJumpProbability()
        {
            return 1.0;
        }

        protected override double BuildHitTime()
        {
            return 16;
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

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsJumpableOn()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsAvoidFall(Random random)
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

            if (IsCurrentlyInFreeFallX || !IsAlive)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(8.0);

                if (!IsNoAiDefaultDirectionWalkingRight)
                    cycleDivision = cycleDivision * -1 + 7;

                switch (cycleDivision)
                {
                    case 1:
                        return rotate1;
                    case 2:
                        return rotate2;
                    case 3:
                        return rotate3;
                    case 4:
                        return rotate4;
                    case 5:
                        return rotate5;
                    case 6:
                        return rotate6;
                    case 7:
                        return rotate7;
                    default:
                        return rotate8;
                }
            }
            else
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(2.0);

                if (IsTryingToWalkRight)
                {
                    if (cycleDivision == 1)
                        return standSurfaceRight;
                    else
                        return walkSurfaceRight;
                }
                else
                {
                    if (cycleDivision == 1)
                        return standSurfaceLeft;
                    else
                        return walkSurfaceLeft;
                }
            }
        }
        #endregion
    }
}
