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
            if (platform.IsBoundToGroundForever && platform.IGround != null) //if platform is a wagon
            {
                #region Manage wagon physics
                platform.IsTryingToWalk = false;

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
                bool isHorizontalAlignWithPlayer = (playerSprite.LeftBound <= platform.RightBound && playerSprite.LeftBound >= platform.LeftBound) || (platform.LeftBound <= playerSprite.RightBound && platform.LeftBound >= playerSprite.LeftBound);
                bool wasPlayerHigherThanPlatform = playerSprite.YPosition < platform.TopBound || playerSprite.YPositionPrevious < platform.TopBound;

                platform.ElevatorCycle.Increment(platform.ElevatorSpeed * timeDelta / 10);
                platform.YPosition = platform.OriginalYPosition + platform.ElevatorCycle.CurrentValue - (platform.ElevatorCycle.TotalTimeLength / 2.0);

                if (wasPlayerHigherThanPlatform && isHorizontalAlignWithPlayer && playerSprite.IGround == null && playerSprite.YPosition >= platform.TopBound)
                    playerSprite.IGround = platform;//to prevent crossing ground when falling to fast while platform is moving up

                if (playerSprite.IGround == platform)
                    playerSprite.YPositionKeepPrevious = platform.TopBound;
            }
        }
    }
}
