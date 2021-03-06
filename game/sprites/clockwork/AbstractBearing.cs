﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    internal abstract class AbstractBearing : AbstractLinkage
    {
        #region Private members
        protected static Surface bearingSurface;

        private List<AbstractLinkage> childList = new List<AbstractLinkage>();

        private Color frameColor;

        private bool isContainSubStructure = false;
        #endregion

        #region Override
        protected override double BuildWidth(Random random)
        {
            return 0.5;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.5;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return bearingSurface;
        }
        #endregion

        #region Public Methods
        public void AddChild(AbstractLinkage childComponent)
        {
            childComponent.IsAffectedByGravity = false;
            childList.Add(childComponent);
            childComponent._ParentNode = this;

            if (childComponent is AbstractBearing)
                isContainSubStructure = true;
        }

        internal void RemoveChild(AbstractLinkage childComponent)
        {
            childComponent.IsAffectedByGravity = true;
            childList.Remove(childComponent);
            childComponent._ParentNode = null;

            if (childComponent is AbstractBearing)
                isContainSubStructure = true;
        }

        public List<AbstractLinkage> ChildList
        {
            get { return childList; }
        }

        public static void ClearBearingSurface()
        {
            bearingSurface = null;
        }

        #region Public methods
        public void GenerateColoredPlatformSurface(Random random)
        {
            GenerateColoredPlatformSurface(null, random);
        }

        public void GenerateColoredPlatformSurface(Surface coloredPlatformSurface, Random random)
        {
            if (coloredPlatformSurface == null)
            {
                if (Platform.Surface == null)
                {
                    Platform platformForCachedSurface = new Platform(0, 0, random, false, 0, false, 0, 0);
                }

                coloredPlatformSurface = new Surface(Platform.Surface.Width, Platform.Surface.Height);
                coloredPlatformSurface.Fill(new ColorHsl(random.Next(0, 256), random.Next(192, 256), random.Next(128, 256)).GetColor());
                coloredPlatformSurface.Blit(Platform.Surface);
            }

            foreach (AbstractLinkage childLinkage in childList)
            {
                if (childLinkage is Platform)
                    ((Platform)childLinkage).ColoredSurface = coloredPlatformSurface;
                else if (childLinkage is AbstractBearing)
                    ((AbstractBearing)childLinkage).GenerateColoredPlatformSurface(coloredPlatformSurface, random);
            }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public AbstractBearing(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (bearingSurface == null)
            {
                if (Program.screenHeight > 720)
                    bearingSurface = BuildSpriteSurface("./assets/rendered/1080/clockwork/Bearing.png");
                else if (Program.screenHeight > 480)
                    bearingSurface = BuildSpriteSurface("./assets/rendered/720/clockwork/Bearing.png");
                else
                    bearingSurface = BuildSpriteSurface("./assets/rendered/480/clockwork/Bearing.png");
            }

            

            frameColor = new ColorHsl(random.Next(0, 256), random.Next(0, 256), random.Next(128, 256)).GetColor();
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAffectedByGravity">whether wheel is affected by gravity (default: false)</param>
        /// <param name="supportHeight">support's height (default: 0)</param>
        public AbstractBearing(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : this(xPosition, yPosition, random)
        {
            this.IsAffectedByGravity = isAffectedByGravity;
            this.IsCrossGrounds = !isAffectedByGravity;
            this.SupportHeight = supportHeight;
        }
        #endregion

        #region Properties
        public Color FrameColor
        {
            get { return frameColor; }
            set { frameColor = value; }
        }

        public bool IsContainSubStructure
        {
            get { return isContainSubStructure; }
        }
        #endregion
    }
}
