using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages seesaws
    /// </summary>
    internal class SeeSawManager
    {
        #region Internal Methods
        /// <summary>
        /// Update seesaw
        /// </summary>
        /// <param name="seeSaw">see saw</param>
        /// <param name="playerSprite">player's sprite</param>
        /// <param name="timeDelta">time delta</param>
        internal void Update(SeeSaw seeSaw, PlayerSprite playerSprite, double timeDelta)
        {
            Update(seeSaw, playerSprite, timeDelta, null);
        }

        /// <summary>
        /// Update seesaw
        /// </summary>
        /// <param name="seeSaw">see saw</param>
        /// <param name="playerSprite">player's sprite</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="forcedPlatformPlayerIsOn">default: null</param>
        internal void Update(SeeSaw seeSaw, PlayerSprite playerSprite, double timeDelta, AbstractLinkage forcedPlatformPlayerIsOn)
        {
            double counter = 0;

            AbstractLinkage platformPlayerIsOn = null;
            double angleOfPlatformPlayerIsOn = 0;

            double availablePower = seeSaw.Speed * timeDelta / seeSaw.Radius;

            foreach (AbstractLinkage childLinkage in seeSaw.ChildList)
            {
                double hypotenus = seeSaw.Radius;

                if (childLinkage is Wheel && ((Wheel)childLinkage).IsRadiusDistanceFromParentWheel)
                    hypotenus += ((Wheel)childLinkage).Radius;

                double angle = seeSaw.Angle;

                double angleOffset = counter / (double)seeSaw.ChildList.Count;

                angle += angleOffset;

                while (angle > 1.0)
                    angle -= 1.0;
                while (angle < 0)
                    angle += 1.0;

                double angleRad = angle * 2.0 * Math.PI;

                double yOffset = Math.Sin(angleRad) * hypotenus;
                double xOffset = Math.Sqrt(Math.Pow(hypotenus, 2.0) - Math.Pow(yOffset, 2.0));

                if (angle > 0.25 && angle <= 0.75)
                    xOffset *= -1;

                childLinkage.XPosition = seeSaw.XPosition + xOffset;
                childLinkage.YPosition = seeSaw.YPosition + yOffset + childLinkage.SupportHeight;

                double xMove = childLinkage.XPosition - childLinkage.XPositionPrevious;
                double yMove = childLinkage.YPosition - childLinkage.YPositionPrevious;

                if (playerSprite.IGround == childLinkage || forcedPlatformPlayerIsOn == childLinkage)
                {
                    if (playerSprite.IGround == childLinkage)
                    {
                        playerSprite.YPositionKeepPrevious = childLinkage.TopBound;
                        playerSprite.XPosition += xMove;
                    }
                    platformPlayerIsOn = childLinkage;

                    angleOfPlatformPlayerIsOn = angle;
                }

                counter += 1.0;
            }

            if (platformPlayerIsOn != null)
            {
                AbstractLinkage forcedPlatformPlayerIsOnParentSeeSaw;
                AbstractLinkage nextParentSeeSaw = GetNextParentSeeSaw(seeSaw, out forcedPlatformPlayerIsOnParentSeeSaw);


                if (Math.Abs(angleOfPlatformPlayerIsOn - 0.25) /*, Math.Abs(angleOfPlatformPlayerIsOn - 0.75)*/ > availablePower / 300)
                {
                    if (angleOfPlatformPlayerIsOn >= 0.25 && angleOfPlatformPlayerIsOn <= 0.75)
                    {
                        seeSaw.Angle -= availablePower / 200;
                    }
                    else
                    {
                        seeSaw.Angle += availablePower / 200;
                    }
                }

                if (nextParentSeeSaw != null)
                {
                    Update((SeeSaw)nextParentSeeSaw, playerSprite, timeDelta, forcedPlatformPlayerIsOnParentSeeSaw);
                }
            }
        }

        private AbstractLinkage GetNextParentSeeSaw(AbstractLinkage linkage, out AbstractLinkage forcedPlatformPlayerIsOnParentSeeSaw)
        {
            if (linkage.ParentNode == null)
            {
                forcedPlatformPlayerIsOnParentSeeSaw = null;
                return null;
            }

            if (linkage.ParentNode is SeeSaw)
            {
                forcedPlatformPlayerIsOnParentSeeSaw = linkage;
                return linkage.ParentNode;
            }
            else
            {
                return GetNextParentSeeSaw(linkage.ParentNode, out forcedPlatformPlayerIsOnParentSeeSaw);
            }
        }
        #endregion
    }
}
