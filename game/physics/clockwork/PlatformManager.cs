using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages platforms (elevators etc)
    /// </summary>
    internal class PlatformManager
    {
        /// <summary>
        /// Update platform sprite
        /// </summary>
        /// <param name="platform">platform</param>
        /// <param name="playerSprite">player's sprite</param>
        /// <param name="timeDelta">timeDelta</param>
        internal void Update(Platform platform, PlayerSprite playerSprite, Level level, double timeDelta)
        {
            if (platform.IsBoundToGroundForever) //if platform is a wagon
            {
                #region Manage wagon physics
                platform.IsTryingToWalk = false;
                platform.MaxWalkingSpeed = 0.17;

                if (platform.IsTryingToWalkRight)
                {
                    platform.XPosition += platform.ElevatorSpeed;
                }
                else
                {
                    platform.XPosition -= platform.ElevatorSpeed;
                }


                Ground path = (Ground)(platform.IGround);

                if (path.IsHoleAt(platform.XPosition) || platform.XPosition >= level.RightBound || platform.XPosition <= level.LeftBound)
                {
                    if (platform.IsTryingToWalkRight)
                    {
                        platform.XPosition -= platform.ElevatorSpeed;
                    }
                    else
                    {
                        platform.XPosition += platform.ElevatorSpeed;
                    }

                    platform.IsTryingToWalkRight = !platform.IsTryingToWalkRight;
                }
                else
                {
                    platform.YPosition = platform.IGround[platform.XPosition] + 0.25;
                    if (playerSprite.IGround == platform)
                    {
                        playerSprite.YPositionKeepPrevious = platform.TopBound;

                        if (platform.IsTryingToWalkRight)
                        {
                            playerSprite.XPositionKeepPrevious += platform.ElevatorSpeed;
                        }
                        else
                        {
                            playerSprite.XPositionKeepPrevious -= platform.ElevatorSpeed;
                        }
                    }
                }
                #endregion
            }

            if (platform.ElevatorCycle != null) //if platform is an elevator
            {
                platform.ElevatorCycle.Increment(platform.ElevatorSpeed * timeDelta / 10);
                platform.YPosition = platform.OriginalYPosition + platform.ElevatorCycle.CurrentValue - (platform.ElevatorCycle.TotalTimeLength / 2.0);

                if (playerSprite.IGround == platform)
                    playerSprite.YPositionKeepPrevious = platform.TopBound;
            }
        }
    }
}
