using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a wheel and its bearing
    /// </summary>
    #warning Eventually remove abstract keyword
    internal class Wheel : AbstractLinkage, ILinkageNode
    {
        #region Private members
        private static Surface surface = null;

        private List<AbstractLinkage> childList = new List<AbstractLinkage>();
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

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
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

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return surface;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Do not use that constructor
        /// </summary>
        public Wheel()
        {
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public Wheel(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (surface == null)
            {
                if (Program.screenHeight > 720)
                    surface = BuildSpriteSurface("./assets/rendered/1080/clockwork/Bearing.png");
                else if (Program.screenHeight > 480)
                    surface = BuildSpriteSurface("./assets/rendered/720/clockwork/Bearing.png");
                else
                    surface = BuildSpriteSurface("./assets/rendered/480/clockwork/Bearing.png");
            }
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAffectedByGravity">whether wheel is affected by gravity (default: false)</param>
        /// <param name="supportHeight">support's height (default: 0)</param>
        public Wheel(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : this(xPosition, yPosition, random)
        {
            this.IsAffectedByGravity = isAffectedByGravity;
            this.IsCrossGrounds = !isAffectedByGravity;
            this.SupportHeight = supportHeight;
        }
        #endregion

        #region ILinkageNode
        public void AddChild(AbstractLinkage childComponent)
        {
            childList.Add(childComponent);
            childComponent._ParentNode = this;
        }

        public List<AbstractLinkage> ChildList
        {
            get { return childList; }
        }
        #endregion
    }
}
