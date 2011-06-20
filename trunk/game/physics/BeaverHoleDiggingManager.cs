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
        /// <summary>
        /// Player tries to dig a hole
        /// </summary>
        /// <param name="playerSprite">playerSprite</param>
        /// <param name="level">level</param>
        /// <param name="levelViewer">level viewer</param>
        internal void Update(PlayerSprite playerSprite, Level level, ILevelViewer levelViewer)
        {
            if (!(playerSprite.IGround is Ground))
                return;

            double holeXPosition = (playerSprite.IsTryingToWalkRight) ? playerSprite.RightPunchBound : playerSprite.LeftPunchBound;

            double holeYPosition = playerSprite.IGround[holeXPosition];

            if (holeYPosition > playerSprite.YPosition + playerSprite.Height)
                return;
            else if (holeYPosition < playerSprite.YPosition - playerSprite.Height)
                return;

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
