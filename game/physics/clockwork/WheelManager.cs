using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages wheels
    /// </summary>
    internal class WheelManager
    {
        internal void Update(Wheel wheel, PlayerSprite playerSprite, Level level, double timeDelta)
        {
            double rotationSpeed = timeDelta * wheel.Speed;

            if (!wheel.IsTryingToWalkRight)
                rotationSpeed *= -1;

            wheel.RotationCycle.Increment(rotationSpeed);
           

            if (wheel.IsBoundToGroundForever && wheel.IGround != null)
            {
                #region We manage "walking" physics
                double walkingSpeed = wheel.Speed / 17;
                wheel.IsTryingToWalk = false;
                wheel.MaxWalkingSpeed = 0.17;

                if (wheel.IsTryingToWalkRight)
                {
                    wheel.XPosition += walkingSpeed;
                }
                else
                {
                    wheel.XPosition -= walkingSpeed;
                }


                Ground path = (Ground)(wheel.IGround);

                if (path.IsHoleAt(wheel.XPosition) || wheel.XPosition >= level.RightBound || wheel.XPosition <= level.LeftBound)
                {
                    if (wheel.IsTryingToWalkRight)
                    {
                        wheel.XPosition -= walkingSpeed;
                    }
                    else
                    {
                        wheel.XPosition += walkingSpeed;
                    }

                    wheel.IsTryingToWalkRight = !wheel.IsTryingToWalkRight;
                }
                else
                {
                    wheel.YPosition = wheel.IGround[wheel.XPosition] + 0.25;
                    if (playerSprite.IGround == wheel)
                    {
                        playerSprite.YPositionKeepPrevious = wheel.TopBound;

                        if (wheel.IsTryingToWalkRight)
                        {
                            playerSprite.XPositionKeepPrevious += walkingSpeed;
                        }
                        else
                        {
                            playerSprite.XPositionKeepPrevious -= walkingSpeed;
                        }
                    }
                }
                #endregion
            }

            double counter = 0;

            #region We move the child linkages
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
            #endregion
        }
    }
}
