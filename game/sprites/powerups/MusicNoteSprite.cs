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
    internal class MusicNoteSprite : StaticSprite, IExpirable
    {
        #region Fields and parts
        private static Surface surface1;

        private static Surface surface2;

        private static Surface surface3;
        
        private static Surface surface4;

        private static Cycle spinCycle = new Cycle(64, true);

        private Cycle expirationCycle;
        
        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private static string tutorialComment = "Get " + Program.musicNoteCountForBodhi + " music notes\nto experience enlightenment.";
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
            expirationCycle = new Cycle(100, false, false, false);

            if (surface1 == null)
            {
                if (Program.screenHeight > 720)
                {
                    surface1 = BuildSpriteSurface("./assets/rendered/1080/powerups/musicNote1.png");
                    surface2 = BuildSpriteSurface("./assets/rendered/1080/powerups/musicNote2.png");
                }
                else if (Program.screenHeight > 480)
                {
                    surface1 = BuildSpriteSurface("./assets/rendered/720/powerups/musicNote1.png");
                    surface2 = BuildSpriteSurface("./assets/rendered/720/powerups/musicNote2.png");
                }
                else
                {
                    surface1 = BuildSpriteSurface("./assets/rendered/480/powerups/musicNote1.png");
                    surface2 = BuildSpriteSurface("./assets/rendered/480/powerups/musicNote2.png");
                }
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
            return 0.65;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.94;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            
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

        #region Properties
        public Cycle ExpirationCycle
        {
            get { return expirationCycle; }
        }
        #endregion

        #region Static
        public static void IncrementSpinCycle()
        {
            spinCycle.Increment(2);
        }
        #endregion
    }
}
