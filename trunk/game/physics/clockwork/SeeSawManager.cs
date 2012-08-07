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

            bool isWalking = false;

            double rotationMovement = 0;

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

                    if (seeSaw.IsAffectedByGravity)
                        isWalking = true;
                }
                else if (playerSprite.IClimbingOn is LianaSprite && ((LianaSprite)playerSprite.IClimbingOn).AttachedToIGround == childLinkage)
                {
                    platformPlayerIsOn = childLinkage;
                    angleOfPlatformPlayerIsOn = angle;
                    if (seeSaw.IsAffectedByGravity)
                        isWalking = true;
                }


                counter += 1.0;
            }


            if (platformPlayerIsOn != null)
            {
                AbstractLinkage forcedPlatformPlayerIsOnParentSeeSaw;
                AbstractLinkage nextParentSeeSaw = GetNextParentSeeSaw(seeSaw, out forcedPlatformPlayerIsOnParentSeeSaw);


                if (Math.Abs(angleOfPlatformPlayerIsOn - 0.25) /*, Math.Abs(angleOfPlatformPlayerIsOn - 0.75)*/ > availablePower / 300)
                {
                    #region if platform is at the top, it will rotate on the side player is standing
                    if (Math.Abs(angleOfPlatformPlayerIsOn - 0.75) < availablePower / 300)
                    {
                        if (Math.Abs(playerSprite.XPosition - platformPlayerIsOn.XPosition) < 0.6)
                        {
                            if (playerSprite.IsTryingToWalkRight)
                            {
                                rotationMovement = availablePower / 200;
                            }
                            else
                            {
                                rotationMovement = -(availablePower / 200);
                            }
                        }
                        else
                        {
                            if (playerSprite.XPosition > platformPlayerIsOn.XPosition)
                            {
                                rotationMovement = availablePower / 200;
                            }
                            else
                            {
                                rotationMovement = -(availablePower / 200);
                            }
                        }
                    }
                    else
                    {
                        if (angleOfPlatformPlayerIsOn >= 0.25 && angleOfPlatformPlayerIsOn <= 0.75)
                        {
                            rotationMovement = -(availablePower / 200);
                        }
                        else
                        {
                            rotationMovement = availablePower / 200;
                        }
                    }
                    #endregion

                    seeSaw.Angle += rotationMovement;
                }

                if (nextParentSeeSaw != null)
                {
                    Update((SeeSaw)nextParentSeeSaw, playerSprite, timeDelta, forcedPlatformPlayerIsOnParentSeeSaw);
                }
            }
            else if (seeSaw.IsTension && (seeSaw.Angle > 0.001 && seeSaw.Angle < 0.999))
            {
                bool isAngleLargerThanHalf = seeSaw.Angle > 0.5;

                if (isAngleLargerThanHalf)
                {
                    rotationMovement = availablePower / 200;
                    seeSaw.Angle += rotationMovement * seeSaw.TensionRatio;
                }
                else
                {
                    rotationMovement = -(availablePower / 200);
                    seeSaw.Angle += rotationMovement * seeSaw.TensionRatio;
                }

                if (isAngleLargerThanHalf != seeSaw.Angle > 0.5)
                    seeSaw.Angle = 0;
            }

            #region Walking See Saw
            if (isWalking && Math.Abs(rotationMovement) > 0.0001)
            {
                seeSaw.IsTryingToWalk = true;
                seeSaw.MaxWalkingSpeed = 0.17 * seeSaw.Radius;
                seeSaw.IsTryingToWalkRight = (rotationMovement > 0);
            }
            else
            {
                seeSaw.IsTryingToWalk = false;
                seeSaw.CurrentWalkingSpeed = 0;
            }
            #endregion
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
