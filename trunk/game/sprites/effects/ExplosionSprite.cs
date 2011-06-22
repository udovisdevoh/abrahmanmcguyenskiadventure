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
        public ExplosionSprite(float xPosition, float yPosition, Random random)
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

        protected override float BuildJumpProbability()
        {
            return 0.0f;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 1.0f;
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
            return 0;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 0.1f;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 0;
        }

        protected override float BuildAttackingTime()
        {
            return 0;
        }

        protected override float BuildHitTime()
        {
            return 0;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.5f;
        }

        protected override float BuildWidth(Random random)
        {
            return 4.0f;
        }

        protected override float BuildHeight(Random random)
        {
            return 4.0f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0f;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
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
