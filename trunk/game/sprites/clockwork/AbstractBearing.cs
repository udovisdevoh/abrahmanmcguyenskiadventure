using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    internal abstract class AbstractBearing : AbstractLinkage
    {
        #region Private members
        protected static Surface bearingSurface;

        private List<AbstractLinkage> childList = new List<AbstractLinkage>();
        #endregion

        protected override double BuildWidth(Random random)
        {
            return 0.5;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.5;
        }

        #region Public Methods
        public void AddChild(AbstractLinkage childComponent)
        {
            childComponent.IsAffectedByGravity = false;
            childList.Add(childComponent);
            childComponent._ParentNode = this;
        }

        public List<AbstractLinkage> ChildList
        {
            get { return childList; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Do not use that constructor
        /// </summary>
        public AbstractBearing()
        {
        }

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
    }
}
