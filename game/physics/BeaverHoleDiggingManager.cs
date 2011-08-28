using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages beaver hole digging
    /// </summary>
    internal class BeaverHoleDiggingManager
    {
        #region Fields and parts
        /// <summary>
        /// Walking manager
        /// </summary>
        private WalkingManager walkingManager = new WalkingManager();
        #endregion

        /// <summary>
        /// Player tries to dig a hole
        /// </summary>
        /// <param name="playerSprite">playerSprite</param>
        /// <param name="level">level</param>
        /// <param name="levelViewer">level viewer</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(PlayerSprite playerSprite, Level level, ILevelViewer levelViewer, HashSet<SideScrollerSprite> visibleSpriteList)
        {
            if (!(playerSprite.IGround is Ground))
                return;

            double holeXPosition;
            double distance;

            if (playerSprite.IsTryingToWalkRight)
            {
                double desiredDistance = playerSprite.RightPunchBound - playerSprite.XPosition;
                distance = walkingManager.GetFarthestWalkingDistanceNoCollision(playerSprite, desiredDistance, level, visibleSpriteList) + 0.25;
                distance = Math.Min(distance, desiredDistance);
            }
            else
            {
                double desiredDistance = playerSprite.LeftPunchBound - playerSprite.XPosition;
                distance = walkingManager.GetFarthestWalkingDistanceNoCollision(playerSprite, desiredDistance, level, visibleSpriteList) - 0.25;
                distance = Math.Max(distance, desiredDistance);
            }

            holeXPosition = playerSprite.XPosition + distance;

            double holeYPosition = playerSprite.IGround[holeXPosition];

            if (holeYPosition > playerSprite.YPosition + playerSprite.Height / 4.0)
                return;
            /*else if (holeYPosition < playerSprite.YPosition - playerSprite.Height)
                return;*/

            SoundManager.PlayBeaverAttackSound();
            ((Ground)playerSprite.IGround).DigHole(holeXPosition);

            double xLeftBoundClearCache = holeXPosition - Program.beaverHoleDiameter;
            double xRightBoundClearCache = holeXPosition + Program.beaverHoleDiameter;
            double yTopBoundClearCache = holeYPosition;
            double yBottomBoundClearCache = holeYPosition + Program.beaverHoleDepth;

            levelViewer.ClearCacheAtRange(xLeftBoundClearCache, xRightBoundClearCache, yTopBoundClearCache, yBottomBoundClearCache);
        }
    }
}
