using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    class DrillSprite : MonsterSprite, IUpDownCycleMove
    {
        #region Constants
        private const double upDownCycleLength = 100;

        private const double upDownCycleMaxOffset = 1.65;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Black drills don't wait for player not to be over them to move up
        /// </summary>
        private bool isBlack = false;

        private static Surface black1;

        private static Surface black2;

        private static Surface black3;

        private static Surface white1;

        private static Surface white2;

        private static Surface white3;

        private Cycle drillCycle = new Cycle(3.0, true);

        private Cycle upDownCycle;
        #endregion

        #region Constructor
        public DrillSprite(double xPosition, double yPosition, Random random)
            : this(xPosition, yPosition, random.Next(0, 2) == 1, random)
        {
        }
        public DrillSprite(double xPosition, double yPosition, bool isBlack, Random random)
            : base(xPosition, yPosition, random)
        {
            this.isBlack = isBlack;

            upDownCycle = new Cycle(upDownCycleLength, true);

            IsAffectedByGravity = false;
            IsFullGravityOnNextFrame = true;

            if (black1 == null)
            {
                black1 = BuildSpriteSurface("./assets/rendered/drill/drill1b.png");
                black2 = BuildSpriteSurface("./assets/rendered/drill/drill2b.png");
                black3 = BuildSpriteSurface("./assets/rendered/drill/drill3b.png");
                white1 = BuildSpriteSurface("./assets/rendered/drill/drill1a.png");
                white2 = BuildSpriteSurface("./assets/rendered/drill/drill2a.png");
                white3 = BuildSpriteSurface("./assets/rendered/drill/drill3a.png");
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

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
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
            return 0.0;
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
            return 0.5;
        }

        protected override double BuildJumpingTime()
        {
            return 0.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 2;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.0;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.0;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.0;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 0.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildHitTime()
        {
            return 16;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildWidth(Random random)
        {
            return 0.5;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.1;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            drillCycle.Increment(0.3);
            int cycleDivision = drillCycle.GetCycleDivision(3.0);
            if (isBlack)
            {
                switch (cycleDivision)
                {
                    case 1:
                        return black1;
                    case 2:
                        return black2;
                    default:
                        return black3;
                }
            }
            else
            {
                switch (cycleDivision)
                {
                    case 1:
                        return white1;
                    case 2:
                        return white2;
                    default:
                        return white3;
                }
            }
        }
        #endregion

        #region IUpDownCycleMove Membres
        public Cycle UpDownCycle
        {
            get { return upDownCycle; }
        }

        public bool IsUseDontMoveUpDistance
        {
            get { return !isBlack; }
        }

        public double DontMoveUpDistance
        {
            get { return 1.5; }
        }

        public double GetCurrentUpDownCycleHeightOffset()
        {
            double scalar = upDownCycle.CurrentValue / upDownCycleLength;

            scalar *= 2.0;

            if (scalar > 1.0)
                scalar = 2.0 - scalar;

            return scalar * upDownCycleMaxOffset;
        }
        #endregion
    }
}
