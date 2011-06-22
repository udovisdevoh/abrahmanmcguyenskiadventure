using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Music note sprite (equivalent to coin)
    /// </summary>
    internal class MusicNoteSprite : StaticSprite
    {
        #region Fields and parts
        private static Surface surface1;

        private static Surface surface2;

        private static Surface surface3;
        
        private static Surface surface4;

        private static Cycle spinCycle;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MusicNoteSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            spinCycle = new Cycle(64, true);
            if (surface1 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/powerups/musicNote1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/powerups/musicNote2.png");
                surface3 = surface2.CreateFlippedHorizontalSurface();
                surface4 = surface1.CreateFlippedHorizontalSurface();
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return true;
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
            return 0.65;
        }

        protected override float BuildHeight(Random random)
        {
            return 0.94;
        }

        protected override float BuildBounciness()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            
            spinCycle.Increment(1);

            int cycleDivision = spinCycle.GetCycleDivision(6.0);

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
                    return surface3;
                default:
                    return surface2;
            }
        }
        #endregion
    }
}
