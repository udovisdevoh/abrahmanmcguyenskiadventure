﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// To get the ninja's armor
    /// </summary>
    internal class BandanaSprite : StaticSprite, IGrowable
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
        private const string tutorialComment = "Get the bandana and become a ninja.\nPress Alt or 3rd button to throw your grappler up.\nPress attack to use your katana and throw shuriken.\nKeep attack pressed while standing still\nto use your nunchaku.";
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BandanaSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
            {
                if (Program.screenHeight > 720)
                    surface = BuildSpriteSurface("./assets/rendered/1080/powerups/bandana.png");
                else if (Program.screenHeight > 480)
                    surface = BuildSpriteSurface("./assets/rendered/720/powerups/bandana.png");
                else
                    surface = BuildSpriteSurface("./assets/rendered/480/powerups/bandana.png");
            }
        }
        #endregion

        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override double BuildMaxHealth()
        {
            return 100;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildBounciness()
        {
            return 0;
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
