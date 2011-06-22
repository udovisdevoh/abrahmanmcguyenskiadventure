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
        internal void Update(PlayerSprite playerSprite, Level level, ILevelViewer levelViewer, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (!(playerSprite.IGround is Ground))
                return;

            float holeXPosition;
            float distance;

            if (playerSprite.IsTryingToWalkRight)
            {
                float desiredDistance = playerSprite.RightPunchBound - playerSprite.XPosition;
                distance = walkingManager.GetFarthestWalkingDistanceNoCollision(playerSprite, desiredDistance, level, visibleSpriteList) + 0.25f;
                distance = Math.Min(distance, desiredDistance);
            }
            else
            {
                float desiredDistance = playerSprite.LeftPunchBound - playerSprite.XPosition;
                distance = walkingManager.GetFarthestWalkingDistanceNoCollision(playerSprite, desiredDistance, level, visibleSpriteList) - 0.25f;
                distance = Math.Max(distance, desiredDistance);
            }

            holeXPosition = playerSprite.XPosition + distance;

            float holeYPosition = playerSprite.IGround[holeXPosition];

            if (holeYPosition > playerSprite.YPosition + playerSprite.Height / 4.0f)
                return;
            /*else if (holeYPosition < playerSprite.YPosition - playerSprite.Height)
                return;*/

            SoundManager.PlayBeaverAttackSound();
            ((Ground)playerSprite.IGround).DigHole(holeXPosition);

            float xLeftBoundClearCache = holeXPosition - Program.beaverHoleDiameter;
            float xRightBoundClearCache = holeXPosition + Program.beaverHoleDiameter;
            float yTopBoundClearCache = holeYPosition;
            float yBottomBoundClearCache = holeYPosition + Program.beaverHoleDepth;

            levelViewer.ClearCacheAtRange(xLeftBoundClearCache, xRightBoundClearCache, yTopBoundClearCache, yBottomBoundClearCache);
        }
    }
}
