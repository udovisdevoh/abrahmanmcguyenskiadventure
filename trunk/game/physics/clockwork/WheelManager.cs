using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages wheels
    /// </summary>
    internal class WheelManager
    {
        internal void Update(Wheel wheel, PlayerSprite playerSprite, double timeDelta)
        {
            wheel.RotationCycle.Increment(timeDelta * wheel.Speed);

            double counter = 0;

            foreach (AbstractLinkage childLinkage in wheel.ChildList)
            {
                double hypotenus = wheel.Radius;

                if (childLinkage is Wheel && ((Wheel)childLinkage).IsRadiusDistanceFromParentWheel)
                    hypotenus += ((Wheel)childLinkage).Radius;

                double angle = (wheel.RotationCycle.CurrentValue / wheel.RotationCycle.TotalTimeLength);

                double angleOffset = counter / (double)wheel.ChildList.Count;

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

                childLinkage.XPosition = wheel.XPosition + xOffset;
                childLinkage.YPosition = wheel.YPosition + yOffset + childLinkage.SupportHeight;

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
