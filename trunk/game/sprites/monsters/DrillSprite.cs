using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    class DrillSprite : MonsterSprite, IUpDownCycleMove
    {
        #region Constants
        private const double upDownCycleLength = 125;

        private const double upDownCycleMaxOffset = 4.00;
        #endregion

        #region Static parts
        private static Surface black1;

        private static Surface black2;

        private static Surface black3;

        private static Surface white1;

        private static Surface white2;

        private static Surface white3;

        private static Surface black1d;

        private static Surface black2d;

        private static Surface black3d;

        private static Surface white1d;

        private static Surface white2d;

        private static Surface white3d;
        #endregion

        #region Instance fields and parts
        /// <summary>
        /// Black drills don't wait for player not to be over them to move up
        /// </summary>
        private bool isBlack = false;

        private bool isUpSide = true;

        private double upDownCycleHalfLength;

        private double alwaysActiveRangeCycleStart;

        private double alwaysActiveRangeCycleStop;

        private Cycle drillCycle = new Cycle(3.0, true);

        private Cycle upDownCycle;
        #endregion

        #region Constructor
        public DrillSprite(double xPosition, double yPosition, Random random)
            : this(xPosition, yPosition, random.Next(0, 2) == 1, true, random)
        {
        }
        public DrillSprite(double xPosition, double yPosition, bool isBlack, bool isUpSide, Random random)
            : base(xPosition, yPosition, random)
        {
            this.isBlack = isBlack;
            this.isUpSide = isUpSide;

            upDownCycle = new Cycle(upDownCycleLength, true);

            IsAffectedByGravity = false;
            IsVulnerableToPunch = false;
            upDownCycleHalfLength = upDownCycleLength / 2.0;
            alwaysActiveRangeCycleStart = upDownCycleLength * 0.45;
            alwaysActiveRangeCycleStop = upDownCycleLength * 0.55;
            ZIndex = -1;

            if (black1 == null)
            {
                black1 = BuildSpriteSurface("./assets/rendered/drill/drill1b.png");
                black2 = BuildSpriteSurface("./assets/rendered/drill/drill2b.png");
                black3 = BuildSpriteSurface("./assets/rendered/drill/drill3b.png");
                white1 = BuildSpriteSurface("./assets/rendered/drill/drill1a.png");
                white2 = BuildSpriteSurface("./assets/rendered/drill/drill2a.png");
                white3 = BuildSpriteSurface("./assets/rendered/drill/drill3a.png");
                black1d = black1.CreateFlippedVerticalSurface();
                black2d = black2.CreateFlippedVerticalSurface();
                black3d = black3.CreateFlippedVerticalSurface();
                white1d = white1.CreateFlippedVerticalSurface();
                white2d = white2.CreateFlippedVerticalSurface();
                white3d = white3.CreateFlippedVerticalSurface();
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

        protected override bool BuildIsJumpableOnEvenByBeaver()
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

        protected override double BuildSubjectiveOccurenceProbability()
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
            return 1.65;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            drillCycle.Increment(0.3);
            int cycleDivision = drillCycle.GetCycleDivision(3.0);

            Surface surface;

            if (isUpSide)
            {
                if (isBlack)
                {
                    switch (cycleDivision)
                    {
                        case 1:
                            surface = black1;
                            break;
                        case 2:
                            surface = black2;
                            break;
                        default:
                            surface = black3;
                            break;
                    }
                }
                else
                {
                    switch (cycleDivision)
                    {
                        case 1:
                            surface = white1;
                            break;
                        case 2:
                            surface = white2;
                            break;
                        default:
                            surface = white3;
                            break;
                    }
                }
            }
            else
            {
                if (isBlack)
                {
                    switch (cycleDivision)
                    {
                        case 1:
                            surface = black1d;
                            break;
                        case 2:
                            surface = black2d;
                            break;
                        default:
                            surface = black3d;
                            break;
                    }
                }
                else
                {
                    switch (cycleDivision)
                    {
                        case 1:
                            surface = white1d;
                            break;
                        case 2:
                            surface = white2d;
                            break;
                        default:
                            surface = white3d;
                            break;
                    }
                }
            }

            /*double currentUpDownCycleHeightOffset = GetCurrentUpDownCycleHeightOffset();
            if (currentUpDownCycleHeightOffset != 0)
            {
                int oldSurfaceHeight = surface.GetHeight();
                int newSurfaceHeight = oldSurfaceHeight - (int)(currentUpDownCycleHeightOffset * (double)Program.tileSize * 4.0);
                newSurfaceHeight = Math.Max(newSurfaceHeight, 0);
                newSurfaceHeight = Math.Min(oldSurfaceHeight, newSurfaceHeight);

                int cacheKey = newSurfaceHeight;
                if (isBlack && cacheKey != 0)
                    cacheKey += 1000000;
                cacheKey += (cycleDivision * 2000);

                Surface scaledSurface;
                if (!scaledSurfaceCache.TryGetValue(cacheKey, out scaledSurface))
                {
                    Rectangle rectangle = new Rectangle(0, 0, surface.GetWidth(), newSurfaceHeight);
                    scaledSurface = new Surface(rectangle);
                    scaledSurface.Transparent = true;
                    scaledSurface.Blit(surface, new Point(0, 0), rectangle);
                    scaledSurfaceCache.Add(cacheKey, scaledSurface);
                }

                surface = scaledSurface;
            }*/
            return surface;
        }
        #endregion

        #region IUpDownCycleMove Membres
        public Cycle UpDownCycle
        {
            get { return upDownCycle; }
        }

        public double DontMoveUpDistance
        {
            get
            {
                if (isBlack)
                    return 1.5;
                else
                    return 2.0;
            }
        }

        public double GetCurrentUpDownCycleHeightOffset()
        {
            double scalar = upDownCycle.CurrentValue / upDownCycleLength;

            scalar *= 2.0;

            if (scalar > 1.0)
                scalar = 2.0 - scalar;

            scalar *= upDownCycleMaxOffset * 1.333;

            scalar -= upDownCycleMaxOffset * 0.2;

            scalar = Math.Max(0, scalar);
            scalar = Math.Min(2.45, scalar);

            if (!isUpSide)
                scalar *= -1;

            return scalar;
        }

        public double UpDownCycleHalfLength
        {
            get { return upDownCycleHalfLength; }
        }

        public double AlwaysActiveRangeCycleStart
        {
            get { return alwaysActiveRangeCycleStart; }
        }

        public double AlwaysActiveRangeCycleStop
        {
            get { return alwaysActiveRangeCycleStop; }
        }
        #endregion
    }
}
