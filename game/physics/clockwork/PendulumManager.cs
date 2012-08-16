using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

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
        internal void Update(Pendulum pendulum, PlayerSprite playerSprite, Level level, double timeDelta)
        {
            double distanceFromCenter = Math.Abs(pendulum.MovingCycle.CurrentValue - pendulum.MovingCycle.TotalTimeLength / 2.0);
            double distanceFromSide = pendulum.MovingCycle.TotalTimeLength - distanceFromCenter;

            double currentSpeed = pendulum.Speed * (Math.Pow(distanceFromSide + 0.1, 1.5) / 600);

            if (timeDelta > 10) //to prevent absurd time delta after pausing or stuff like that
                timeDelta = 10;

            pendulum.MovingCycle.Increment(timeDelta * currentSpeed);


            #region Walking pendulum
            if (pendulum.IsBoundToGroundForever && pendulum.IGround != null)
            {
                double walkingSpeed = pendulum.Speed / 14;
                pendulum.IsTryingToWalk = false;

                if (pendulum.IsTryingToWalkRight)
                {
                    pendulum.XPosition += walkingSpeed;
                }
                else
                {
                    pendulum.XPosition -= walkingSpeed;
                }


                Ground path = (Ground)(pendulum.IGround);

                if (path.IsHoleAt(pendulum.XPosition) || pendulum.XPosition >= level.RightBound || pendulum.XPosition <= level.LeftBound)
                {
                    if (pendulum.IsTryingToWalkRight)
                    {
                        pendulum.XPosition -= walkingSpeed;
                    }
                    else
                    {
                        pendulum.XPosition += walkingSpeed;
                    }

                    pendulum.IsTryingToWalkRight = !pendulum.IsTryingToWalkRight;
                }
                else
                {
                    pendulum.YPosition = pendulum.IGround[pendulum.XPosition] + 0.25;
                    if (playerSprite.IGround == pendulum)
                    {
                        playerSprite.YPositionKeepPrevious = pendulum.TopBound;

                        if (pendulum.IsTryingToWalkRight)
                        {
                            playerSprite.XPositionKeepPrevious += walkingSpeed;
                        }
                        else
                        {
                            playerSprite.XPositionKeepPrevious -= walkingSpeed;
                        }
                    }
                }
            }
            #endregion


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
