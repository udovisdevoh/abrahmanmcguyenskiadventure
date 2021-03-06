﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a mushroom
    /// </summary>
    class MushroomSprite : MonsterSprite, IGrowable
    {
        #region Fields and parts
        private static Surface surface;

        /// <summary>
        /// Cycle of growth
        /// </summary>
        private Cycle growthCycle;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Eat that psilocybin hallucinogenic mushroom.\nIt's 100% natural and it's good!";
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MushroomSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
            {
                if (Program.screenHeight > 720)
                    surface = BuildSpriteSurface("./assets/rendered/1080/powerups/mushroom.png");
                else if (Program.screenHeight > 480)
                    surface = BuildSpriteSurface("./assets/rendered/720/powerups/mushroom.png");
                else
                    surface = BuildSpriteSurface("./assets/rendered/480/powerups/mushroom.png");
            }
        }
        #endregion

        #region Override
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
            return 0;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.016;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.17;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.17;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 20;
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
            return -0.5;
        }

        protected override double BuildWidth(Random random)
        {
            return 0.725;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
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
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
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

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override double BuildJumpProbability()
        {
            return 0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return surface;
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
