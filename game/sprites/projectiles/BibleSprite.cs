using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Bible (projectile)
    /// </summary>
    internal class BibleSprite : MonsterSprite
    {
        #region Fields
        private static Surface surface1;

        private static Surface surface2;

        private static Surface surface3;

        private static Surface surface4;

        private static Surface surface5;

        private static Surface surface6;

        private static Surface surface7;

        private static Surface surface8;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BibleSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (surface1 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/projectiles/bible1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/projectiles/bible2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/projectiles/bible3.png");
                surface4 = surface2.CreateFlippedVerticalSurface();
                surface5 = surface1.CreateFlippedVerticalSurface();
                surface6 = surface4.CreateFlippedHorizontalSurface();
                surface7 = surface3.CreateFlippedHorizontalSurface();
                surface8 = surface2.CreateFlippedHorizontalSurface();
            }
        }
        #endregion

        #region Override Methods
        protected override bool BuildIsJumpableOn()
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

        protected override bool BuildIsDieOnTouchGround()
        {
            return true;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 0.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return true;
        }

        protected override double BuildMaxHealth()
        {
            return 100.0;
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
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.35;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.55;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 20.0;
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
            return 1.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 0.5;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.5;
        }
        
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            int cycleDivision = WalkingCycle.GetCycleDivision(8.0);

            if (!IsNoAiDefaultDirectionWalkingRight)
                cycleDivision = cycleDivision * -1 + 7;

            switch (cycleDivision)
            {
                case 1:
                    return surface1;
                case 2:
                    return surface2;
                case 3:
                    return surface3;
                case 4:
                    return surface4;
                case 5:
                    return surface5;
                case 6:
                    return surface6;
                case 7:
                    return surface7;
                default:
                    return surface8;
            }
        }
        #endregion
    }
}
