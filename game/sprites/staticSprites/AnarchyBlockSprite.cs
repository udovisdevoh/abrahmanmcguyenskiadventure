using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Anarchy block sprite
    /// </summary>
    internal class AnarchyBlockSprite : StaticSprite
    {
        #region Fields and parts
        private Surface surface1;

        private Surface surface2;

        private Surface surface3;

        private Cycle blinkCycle;

        private Cycle bumpCycle;

        private bool isFinalized;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public AnarchyBlockSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            isFinalized = false;

            if (surface1 == null || surface2 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/staticSprites/anarchyBlock1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/staticSprites/anarchyBlock2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/staticSprites/anarchyBlock3.png");
            }

            bumpCycle = new Cycle(10, false);
            blinkCycle = new Cycle(100, true);
            blinkCycle.Fire();
        }
        #endregion

        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0;
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
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            blinkCycle.Increment(1);
            bumpCycle.Increment(1);
            xOffset = 0;
            yOffset = 0;

            if (bumpCycle.IsFired)
                yOffset = bumpCycle.CurrentValue / -20.0;

            if (isFinalized)
                return surface3;

            if (blinkCycle.GetCycleDivision(2) == 0)
                return surface1;
            else
                return surface2;
        }
        #endregion

        #region Internal Methods
        internal AbstractSprite GetPowerUpSprite(AbstractSprite playerSprite, Random random)
        {
            if (playerSprite.Health == playerSprite.MaxHealth)
            {
                PeyoteSprite peyoteSprite = new PeyoteSprite(XPosition, TopBound, random);
                return peyoteSprite;
            }
            else
            {
                MushroomSprite mushroomSprite = new MushroomSprite(XPosition, TopBound, random);
                mushroomSprite.IsNoAiDefaultDirectionWalkingRight = playerSprite.IsTryingToWalkRight;
                return mushroomSprite;
            }
        }
        #endregion

        #region Properties
        public Cycle BumpCycle
        {
            get { return bumpCycle; }
        }

        public bool IsFinalized
        {
            get { return isFinalized; }
            set { isFinalized = value; }
        }
        #endregion
    }
}
