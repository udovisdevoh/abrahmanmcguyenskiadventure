using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    internal class PeyoteSprite : StaticSprite, IGrowable
    {
        #region Fields and parts
        private static Surface surface;

        /// <summary>
        /// Cycle of growth
        /// </summary>
        private Cycle growthCycle;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public PeyoteSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
                surface = BuildSpriteSurface("./assets/rendered/powerups/peyote.png");
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

        protected override float BuildMaxHealth()
        {
            return 100;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override float BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override float BuildBounciness()
        {
            return 0;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
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
