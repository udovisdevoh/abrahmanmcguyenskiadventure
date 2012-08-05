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
        /// <summary>
        /// Update seesaw
        /// </summary>
        /// <param name="seeSaw">see saw</param>
        /// <param name="playerSprite">player's sprite</param>
        /// <param name="timeDelta">time delta</param>
        internal void Update(SeeSaw seeSaw, PlayerSprite playerSprite, double timeDelta)
        {
            double counter = 0;

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

                if (playerSprite.IGround == childLinkage)
                {
                    playerSprite.YPositionKeepPrevious = childLinkage.TopBound;
                    playerSprite.XPosition += xMove;
                }

                counter += 1.0;
            }
        }
    }
}
