using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages pendulum physics
    /// </summary>
    internal class PendulumManager
    {
        /// <summary>
        /// Update pendulum
        /// </summary>
        /// <param name="pendulum">pendulum</param>
        /// <param name="timeDelta">physics</param>
        internal void Update(Pendulum pendulum, PlayerSprite playerSprite, double timeDelta)
        {
            double distanceFromCenter = Math.Abs(pendulum.MovingCycle.CurrentValue - pendulum.MovingCycle.TotalTimeLength / 2.0);
            double distanceFromSide = pendulum.MovingCycle.TotalTimeLength - distanceFromCenter;

            double currentSpeed = pendulum.Speed * (Math.Pow(distanceFromSide + 0.1, 1.5) / 200);


            pendulum.MovingCycle.Increment(timeDelta * currentSpeed);

            if (pendulum.ChildList.Count > 0)
            {
                double childLinkageXPosition = pendulum.XPosition + (pendulum.MovingCycle.CurrentValue - pendulum.MovingCycle.TotalTimeLength / 2) / pendulum.MovingCycle.TotalTimeLength * pendulum.Amplitude;
                double childLinkagePositionPrevious = pendulum.ChildList[0].XPosition;

                double xMove = (childLinkageXPosition - childLinkagePositionPrevious);

                double xDistance = Math.Abs(pendulum.XPosition - pendulum.ChildList[0].XPosition);

                double childLinkageYPositionPrevious = pendulum.ChildList[0].YPosition;

                double childLinkageYPosition = pendulum.YPosition + Math.Sqrt(Math.Pow(pendulum.RopeLength, 2.0) - Math.Pow(xDistance, 2.0));

                double yMove = (childLinkageYPosition - childLinkageYPositionPrevious);

                foreach (AbstractLinkage childLinkage in pendulum.ChildList)
                {
                    childLinkage.XPosition = childLinkageXPosition;


                    childLinkage.YPosition = childLinkageYPosition + childLinkage.SupportHeight;

                    if (playerSprite.IGround == childLinkage)
                    {
                        playerSprite.YPositionKeepPrevious = childLinkage.TopBound;
                        playerSprite.XPosition += xMove;
                    }
                }
            }
        }
    }
}
