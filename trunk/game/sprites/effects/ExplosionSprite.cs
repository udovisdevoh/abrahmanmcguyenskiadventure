using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents an explosion
    /// </summary>
    internal class ExplosionSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface surface1;

        private static Surface surface2;

        private static Surface surface3;

        private Cycle explosionCycle;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public ExplosionSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            explosionCycle = new Cycle(15, false);
            explosionCycle.Fire();
            IsAffectedByGravity = false;
            if (surface1 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/effects/explosion1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/effects/explosion2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/effects/explosion3.png");
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
            return false;
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
            return false;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
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

        protected override bool BuildIsJumpableOn()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 1.0;
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
            return 100;
        }

        protected override double BuildJumpingTime()
        {
            return 0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 0.1;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 0;
        }

        protected override double BuildAttackingTime()
        {
            return 0;
        }

        protected override double BuildHitTime()
        {
            return 0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildWidth(Random random)
        {
            return 4.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 4.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            int cycleDivision = ExplosionCycle.GetCycleDivision(100) % 3;

            if (cycleDivision == 1)
            {
                return surface1;
            }
            else if (cycleDivision == 2)
            {
                return surface2;
            }
            else
            {
                return surface3;
            }
        }
        #endregion

        #region Properties
        public Cycle ExplosionCycle
        {
            get { return explosionCycle; }
        }
        #endregion
    }
}
