using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a platform attached to a wheel or any mechanical component
    /// </summary>
    internal class Platform : AbstractLinkage
    {
        #region Private members
        private static Surface surface;

        private static Surface defaultColorSurface;

        private Surface coloredSurface = null;

        private Cycle elevatorCycle = null;

        private double elevatorSpeed = 0;

        private double originalYPosition;
        #endregion

        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override double BuildWidth(Random random)
        {
            return 3.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.5;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        protected override string BuildTutorialComment()
        {
            return "You can jump on moving platforms.";
        }

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override int BuildZIndex()
        {
            return 1;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = -0.0666;
            if (coloredSurface != null)
                return coloredSurface;
            return defaultColorSurface;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public Platform(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (surface == null)
            {
                if (Program.screenHeight > 720)
                    surface = BuildSpriteSurface("./assets/rendered/1080/clockwork/Platform.png");
                else if (Program.screenHeight > 480)
                    surface = BuildSpriteSurface("./assets/rendered/720/clockwork/Platform.png");
                else
                    surface = BuildSpriteSurface("./assets/rendered/480/clockwork/Platform.png");

                defaultColorSurface = new Surface(surface.Width, surface.Height);
                defaultColorSurface.Fill(new ColorHsl(random.Next(0, 256), random.Next(192, 256), random.Next(128, 256)).GetColor());
                defaultColorSurface.Blit(surface);
            }

            originalYPosition = yPosition;
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAffectedByGravity">whether wheel is affected by gravity (default: false)</param>
        /// <param name="supportHeight">support's height (default: 0)</param>
        public Platform(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight, bool isElevator, double elevatorHeight, double elevatorSpeed)
            : this(xPosition, yPosition, random)
        {
            this.IsAffectedByGravity = isAffectedByGravity;
            this.IsCrossGrounds = !isAffectedByGravity;
            this.SupportHeight = supportHeight;
            this.elevatorSpeed = elevatorSpeed;
            IsBoundToGroundForever = !isElevator;

            if (isElevator)
            {
                elevatorCycle = new Cycle(elevatorHeight, true, true, false);
                elevatorCycle.CurrentValue = random.NextDouble() * elevatorCycle.TotalTimeLength;
                elevatorCycle.Fire();
            }
        }
        #endregion

        #region Properties
        public static Surface Surface
        {
            get { return surface; }
        }

        public Surface ColoredSurface
        {
            get { return coloredSurface; }
            set { coloredSurface = value; }
        }

        public Cycle ElevatorCycle
        {
            get { return elevatorCycle; }
        }

        public double ElevatorSpeed
        {
            get { return elevatorSpeed; }
        }

        /// <summary>
        /// Elevator's middle Y position
        /// </summary>
        public double OriginalYPosition
        {
            get { return originalYPosition; }
            set { originalYPosition = value; }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Clear cached surfaces (default color etc)
        /// </summary>
        internal static void ResetDefaultColorSurface()
        {
            surface = null;
            defaultColorSurface = null;
        }
        #endregion
    }
}
