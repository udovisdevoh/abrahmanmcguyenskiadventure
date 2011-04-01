using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.mathMesh;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a caterpillar
    /// </summary>
    class CaterpillarSprite : MonsterSprite
    {
        #region Fields and parts
        /// <summary>
        /// Sprite's math mesh
        /// </summary>
        private CaterpillarMesh catterpillarMesh;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public CaterpillarSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            catterpillarMesh = new CaterpillarMesh(random, Width, Height);
        }
        #endregion

        #region Override Methods
        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 50;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.02;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.45;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.75;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return random.NextDouble() * 2.5 + 1.5;
        }

        protected override double BuildHeight(Random random)
        {
            return random.NextDouble() * 2.0 + 0.5;
        }

        protected override double BuildMass(Random random)
        {
            return random.NextDouble() * 0.5 + 1.0;
        }

        public override Surface GetCurrentSurface()
        {
            return base.GetCurrentSurface();
        }
        #endregion
    }
}