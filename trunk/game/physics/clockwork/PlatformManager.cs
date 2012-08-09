using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

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
        internal void Update(Platform platform, PlayerSprite playerSprite, double timeDelta)
        {
            if (platform.ElevatorCycle != null) //if platform is an elevator
            {
                platform.ElevatorCycle.Increment(platform.ElevatorSpeed * timeDelta / 10);
                platform.YPosition = platform.OriginalYPosition + platform.ElevatorCycle.CurrentValue - (platform.ElevatorCycle.TotalTimeLength / 2.0);

                if (playerSprite.IGround == platform)
                    playerSprite.YPositionKeepPrevious = platform.TopBound;
            }

            if (platform.IsAffectedByGravity && platform.IGround != null)
            {
                platform.IsTryingToWalk = true;
                platform.MaxWalkingSpeed = 0.17;
                
                if (platform.CurrentWalkingSpeed == 0)
                    platform.IsTryingToWalkRight = !platform.IsTryingToWalkRight;
            }
        }
    }
}
