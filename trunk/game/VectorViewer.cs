using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Graphics.Primitives;
using System.Drawing;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// To view vectors that link sprites
    /// </summary>
    internal class VectorViewer
    {
        #region Fields and parts
        private Surface mainSurface;

        private ClockworkManager clockworkManager;
        #endregion

        #region Public Methods
        public VectorViewer(ClockworkManager clockworkManager, Surface mainSurface)
        {
            this.clockworkManager = clockworkManager;
            this.mainSurface = mainSurface;
        }

        public void Update(double viewOffsetX, double viewOffsetY)
        {
            foreach (AbstractLinkage abstractLinkage in clockworkManager.RootLinkageSpriteList)
            {
                ShowVectors(abstractLinkage, viewOffsetX, viewOffsetY);
            }
        }
        #endregion

        #region Private Methods
        private void ShowVectors(AbstractLinkage abstractLinkage, double viewOffsetX, double viewOffsetY)
        {
            if (abstractLinkage.ParentNode != null)
            {
                //draw link to parent node

                Color color = abstractLinkage.ParentNode.FrameColor;


                Point spriteTopSupportPosition = GetSpritePosition(abstractLinkage, viewOffsetX, viewOffsetY, abstractLinkage.SupportHeight);
                Point parentSpritePosition = GetSpritePosition(abstractLinkage.ParentNode, viewOffsetX, viewOffsetY);

                mainSurface.Draw(new Line(spriteTopSupportPosition, parentSpritePosition), color);

                if (abstractLinkage.SupportHeight != 0)
                {
                    Point spriteLeftJointPosition = GetSpritePosition(abstractLinkage, viewOffsetX, viewOffsetY, 0, true, true);
                    Point spriteRightJointPosition = GetSpritePosition(abstractLinkage, viewOffsetX, viewOffsetY, 0, true, false);

                    mainSurface.Draw(new Line(spriteTopSupportPosition, spriteLeftJointPosition), color);
                    mainSurface.Draw(new Line(spriteTopSupportPosition, spriteRightJointPosition), color);
                }
            }

            if (abstractLinkage is AbstractBearing)
            {
                AbstractBearing abstractBearing = (AbstractBearing)abstractLinkage;

                foreach (AbstractLinkage childLinkage in abstractBearing.ChildList)
                    ShowVectors(childLinkage, viewOffsetX, viewOffsetY);

                if (abstractBearing is Wheel && ((Wheel)abstractBearing).IsShowCircumference && abstractBearing.ChildList.Count > 0)
                    mainSurface.Draw(new Circle(GetSpritePosition(abstractLinkage, viewOffsetX, viewOffsetY/*, abstractBearing.ChildList[0].SupportHeight*/), (short)(((Wheel)abstractBearing).Radius * (double)Program.tileSize)), abstractBearing.FrameColor);
            }
        }

        private Point GetSpritePosition(AbstractLinkage sprite, double viewOffsetX, double viewOffsetY)
        {
            return GetSpritePosition(sprite, viewOffsetX, viewOffsetY, 0);
        }

        private Point GetSpritePosition(AbstractLinkage sprite, double viewOffsetX, double viewOffsetY, double supportHeight)
        {
            return GetSpritePosition(sprite, viewOffsetX, viewOffsetY, supportHeight, false, false);
        }

        /// <summary>
        /// Sprite's position
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="viewOffsetX"></param>
        /// <param name="viewOffsetY"></param>
        /// <param name="supportHeight">default: 0</param>
        /// <param name="isLeftOrRight">whether we return the side extremity of the sprite instead of the center (default: false)</param>
        /// <returns></returns>
        private Point GetSpritePosition(AbstractLinkage sprite, double viewOffsetX, double viewOffsetY, double supportHeight, bool isLeftOrRight, bool isLeft)
        {
            int xBlitPosition = (int)((sprite.XPosition - viewOffsetX) * (double)Program.tileSize);

            if (isLeftOrRight)
            {
                if (isLeft)
                    xBlitPosition -= (int)(sprite.Width / 2.0 * Program.tileSize);
                else
                    xBlitPosition += (int)(sprite.Width / 2.0 * Program.tileSize);
            }

            int yBlitPosition = (int)((sprite.YPosition - supportHeight - viewOffsetY - (sprite.Height / 2.0)) * (double)Program.tileSize);

            return new Point(xBlitPosition, yBlitPosition);
        }
        #endregion
    }
}
