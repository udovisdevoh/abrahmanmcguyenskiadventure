using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a seesaw
    /// </summary>
    internal class SeeSaw : AbstractBearing, IWheel
    {
        #region Fields and parts
        private double radius;

        private double speed;

        private double rotationAmplitude;

        private double angle;

        private bool isRadiusDistanceFromParentWheel;

        private bool isShowCircumference;
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

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override string BuildTutorialComment()
        {
            return "Play with that seesaw, it's fun!\nBe careful when there are flail balls attached to it.";
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return bearingSurface;
        }
        #endregion

        #region Constructors
        public SeeSaw(double xPosition, double yPosition, Random random, double radius, double speed, double rotationAmplitude, bool isAffectedByGravity, bool isRadiusDistanceFromParentWheel, bool isShowCircumference, double supportHeight)
            : base(xPosition, yPosition, random, isAffectedByGravity, supportHeight)
        {
            this.radius = radius;
            this.speed = speed;
            this.rotationAmplitude = rotationAmplitude;
            this.angle = 0;
            this.isShowCircumference = isShowCircumference;
            this.isRadiusDistanceFromParentWheel = isRadiusDistanceFromParentWheel;
        }
        #endregion

        #region Properties
        public double Radius
        {
            get { return radius; }
        }

        public double Speed
        {
            get { return speed; }
        }

        public double RotationAmplitude
        {
            get { return rotationAmplitude; }
        }

        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;

                while (angle > 1.0)
                    angle -= 1.0;

                while (angle < 0)
                    angle += 1.0;
            }
        }

        public bool IsRadiusDistanceFromParentWheel
        {
            get { return isRadiusDistanceFromParentWheel; }
        }

        public bool IsShowCircumference
        {
            get { return isShowCircumference; }
        }
        #endregion
    }
}
