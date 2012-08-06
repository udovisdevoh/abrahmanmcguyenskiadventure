using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a wheel and its bearing
    /// </summary>
    internal class Wheel : AbstractBearing, IWheel
    {
        #region Fields and parts
        private double radius;

        private double speed;

        private bool isShowCircumference;

        private bool isRadiusDistanceFromParentWheel;

        private Cycle rotationCycle;
        #endregion

        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        protected override string BuildTutorialComment()
        {
            return "Play with that ferris wheel, it's fun!\nBe careful when there are flail balls attached to it.";
        }

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create wheel
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="random"></param>
        /// <param name="radius"></param>
        /// <param name="firstChildOffset">from 0 to 1.0</param>
        /// <param name="isAffectedByGravity"></param>
        /// <param name="supportHeight"></param>
        /// <param name="speed">speed (can be negative for reverse rotation)</param>
        public Wheel(double xPosition, double yPosition, Random random, double radius, double firstChildOffset, double speed, bool isAffectedByGravity, bool isShowCircumference, bool isRadiusDistanceFromParentWheel, double supportHeight)
            : base(xPosition, yPosition, random, isAffectedByGravity, supportHeight)
        {
            this.radius = radius;
            this.isShowCircumference = isShowCircumference;
            this.speed = speed / radius;
            this.isRadiusDistanceFromParentWheel = isRadiusDistanceFromParentWheel;

            firstChildOffset = Math.Min(1.0, Math.Max(firstChildOffset, 0));

            rotationCycle = new Cycle(100, true);
            rotationCycle.CurrentValue = firstChildOffset * 100;
        }
        #endregion

        #region Properties
        public bool IsShowCircumference
        {
            get { return isShowCircumference; }
        }

        public bool IsRadiusDistanceFromParentWheel
        {
            get { return isRadiusDistanceFromParentWheel; }
        }

        public double Radius
        {
            get { return radius; }
        }

        public double Speed
        {
            get { return speed; }
        }

        public Cycle RotationCycle
        {
            get { return rotationCycle; }
        }
        #endregion
    }
}
