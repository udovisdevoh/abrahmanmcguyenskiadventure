using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Makes player invincible
    /// </summary>
    class WhiskySprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface surface0;

        private static Surface surface1;

        private static Surface surface2;

        private static Surface surface3;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public WhiskySprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (surface0 == null)
            {
                surface0 = BuildSpriteSurface("./assets/rendered/powerups/bottle4.png");
                surface1 = BuildSpriteSurface("./assets/rendered/powerups/bottle1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/powerups/bottle2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/powerups/bottle3.png");
            }
        }
        #endregion

        #region Overrides
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
            return true;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
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
            return 100;
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 10;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.15;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.35;
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

        protected override double BuildAttackStrengthCollision()
        {
            return 1.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 0.25;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.25;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

            if (cycleDivision == 1)
                return surface1;
            else if (cycleDivision == 2)
                return surface2;
            else if (cycleDivision == 3)
                return surface3;
            else
                return surface0;
        }
        #endregion
    }
}
