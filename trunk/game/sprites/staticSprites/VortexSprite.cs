﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// A vortex (to change environment)
    /// </summary>
    internal class VortexSprite : StaticSprite
    {
        #region Fields and parts
        /// <summary>
        /// Surface 1
        /// </summary>
        private static Surface surface1;

        /// <summary>
        /// Surface 2
        /// </summary>
        private static Surface surface2;

        /// <summary>
        /// Surface 3
        /// </summary>
        private static Surface surface3;

        /// <summary>
        /// Surface 1 b
        /// </summary>
        private static Surface surface1b;

        /// <summary>
        /// Surface 2 b
        /// </summary>
        private static Surface surface2b;

        /// <summary>
        /// Surface 3 b
        /// </summary>
        private static Surface surface3b;

        /// <summary>
        /// Rotation cycle
        /// </summary>
        private Cycle rotateCycle;

        /// <summary>
        /// Seed for destination world
        /// </summary>
        private int destinationSeed;

        /// <summary>
        /// True: going in, False: warp back
        /// </summary>
        private bool isGoingIn;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public VortexSprite(double xPosition, double yPosition, Random random, bool isGoingIn)
            : base(xPosition, yPosition, random)
        {
            this.isGoingIn = isGoingIn;
            destinationSeed = random.Next();
            IsFullGravityOnNextFrame = true;
            rotateCycle = new Cycle(20, true);
            rotateCycle.Fire();
            if (surface1 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex3.png");
                surface1b = BuildSpriteSurface("./assets/rendered/staticSprites/vortex1b.png");
                surface2b = BuildSpriteSurface("./assets/rendered/staticSprites/vortex2b.png");
                surface3b = BuildSpriteSurface("./assets/rendered/staticSprites/vortex3b.png");
            }
        }

        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAffectedByGravity">default true</param>
        /// <param name="destinationSeed">default: random using local seed</param>
        public VortexSprite(double xPosition, double yPosition, Random random, int destinationSeed, bool isGoingIn)
            : base(xPosition, yPosition, random)
        {
            this.isGoingIn = isGoingIn;
            this.destinationSeed = destinationSeed;
            IsFullGravityOnNextFrame = true;
            rotateCycle = new Cycle(20, true);
            rotateCycle.Fire();
            if (surface1 == null)
            {
                surface1 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex1.png");
                surface2 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex2.png");
                surface3 = BuildSpriteSurface("./assets/rendered/staticSprites/vortex3.png");
                surface1b = BuildSpriteSurface("./assets/rendered/staticSprites/vortex1b.png");
                surface2b = BuildSpriteSurface("./assets/rendered/staticSprites/vortex2b.png");
                surface3b = BuildSpriteSurface("./assets/rendered/staticSprites/vortex3b.png");
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

        protected override double BuildMaxHealth()
        {
            return 1000;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0;
        }

        protected override double BuildWidth(Random random)
        {
            return 3;
        }

        protected override double BuildHeight(Random random)
        {
            return 3;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        public override SdlDotNet.Graphics.Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0.1;
            int cycleDivision = rotateCycle.GetCycleDivision(3);

            rotateCycle.Increment(1.0);

            if (isGoingIn)
            {
                if (cycleDivision == 1)
                {
                    return surface1;
                }
                else if (cycleDivision == 2)
                {
                    return surface2;
                }
                else
                {
                    return surface3;
                }
            }
            else
            {
                if (cycleDivision == 1)
                {
                    return surface1b;
                }
                else if (cycleDivision == 2)
                {
                    return surface2b;
                }
                else
                {
                    return surface3b;
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Seed for destination world
        /// </summary>
        internal int DestinationSeed
        {
            get { return destinationSeed; }
        }

        /// <summary>
        /// True: going in, False: warp back
        /// </summary>
        internal bool IsGoingIn
        {
            get { return isGoingIn; }
        }
        #endregion
    }
}
