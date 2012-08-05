using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a pendulum
    /// </summary>
    internal class Pendulum : AbstractBearing
    {
        #region Fields and parts
        private double ropeLength;

        private double speed;

        private double amplitude;

        private Cycle movingCycle = new Cycle(50, true, true, false);
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
            return "Play with that pendulum, it's fun!\nBe careful when there are flail balls attached to it.";
        }

        public override double BuildSupportHeight()
        {
            return 0;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="random"></param>
        /// <param name="ropeLength"></param>
        /// <param name="speed"></param>
        /// <param name="amplitude">max 1.8 (scaled on rope's length)</param>
        public Pendulum(double xPosition, double yPosition, Random random, double ropeLength, double speed, double amplitude)
            : base(xPosition, yPosition, random)
        {
            this.ropeLength = ropeLength;
            this.speed = speed;
            
            this.amplitude = amplitude;
            this.amplitude = Math.Min(this.amplitude, 1.8);
            this.amplitude *= this.ropeLength;

            movingCycle.Fire();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="random"></param>
        /// <param name="ropeLength"></param>
        /// <param name="speed"></param>
        /// <param name="amplitude">max 1.8 (scaled on rope's length)</param>
        /// <param name="isAffectedByGravity"></param>
        /// <param name="supportHeight"></param>
        public Pendulum(double xPosition, double yPosition, Random random, double ropeLength, double speed, double amplitude, bool isAffectedByGravity, double supportHeight)
            : base(xPosition, yPosition, random, isAffectedByGravity, supportHeight)
        {
            this.ropeLength = ropeLength;
            this.speed = speed;
            
            this.amplitude = amplitude;
            this.amplitude = Math.Min(this.amplitude, 1.8);
            this.amplitude *= this.ropeLength;

            movingCycle.Fire();
        }
        #endregion

        #region Properties
        public double Speed
        {
            get { return speed; }
        }

        public double RopeLength
        {
            get { return ropeLength; }
        }

        public double Amplitude
        {
            get { return amplitude; }
        }

        public Cycle MovingCycle
        {
            get { return movingCycle; }
        }
        #endregion
    }
}
