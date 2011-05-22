using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class BlobSprite : MonsterSprite
    {
        #region Fields and parts
        /// <summary>
        /// Sprite's math mesh
        /// </summary>
        private Surface defaultSurface;

        private Surface right1Surface;

        private Surface left1Surface;

        private Surface right2Surface;

        private Surface left2Surface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BlobSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
        }
        #endregion

        #region Override Methods
        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 5;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.25;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.55;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            //return 25.0;
            return 5.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildMass(Random random)
        {
            return 0.5;
        }
        
        protected override double BuildMaxHealth()
        {
            return 0.2;
        }

        protected override bool BuildIsCanJump()
        {
            return true;
        }

        protected override double BuildJumpProbability()
        {
            return 1.0;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            int cycleDivision = WalkingCycle.GetCycleDivision(2.0);

            if (cycleDivision == 1)
            {
                if (IsTryingToWalkRight)
                {
                    return GetRight1Surface();
                }
                else
                {
                    return GetLeft1Surface();
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                {
                    return GetRight2Surface();
                }
                else
                {
                    return GetLeft2Surface();
                }
            }
        }
        #endregion

        #region Private Method
        private Surface GetLeft1Surface()
        {
            if (left1Surface == null)
                left1Surface = GetRight1Surface().CreateFlippedHorizontalSurface();

            return left1Surface;
        }

        private Surface GetRight1Surface()
        {
            if (right1Surface == null)
                right1Surface = BuildSpriteSurface("./assets/rendered/blob/blob1.png");

            return right1Surface;
        }

        private Surface GetLeft2Surface()
        {
            if (left2Surface == null)
                left2Surface = GetRight2Surface().CreateFlippedHorizontalSurface();

            return left2Surface;
        }

        private Surface GetRight2Surface()
        {
            if (right2Surface == null)
                right2Surface = BuildSpriteSurface("./assets/rendered/blob/blob2.png");

            return right2Surface;
        }
        #endregion
    }
}
