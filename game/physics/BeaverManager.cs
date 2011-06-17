using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manage jumping on beaver and other beaver stuff
    /// </summary>
    internal class BeaverManager
    {
        /// <summary>
        /// Jumping on beaver
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="beaverSprite">beaver</param>
        internal void UpdateJumpOnBeaver(PlayerSprite playerSprite, BeaverSprite beaverSprite)
        {
            SoundManager.PlayBeaverUpSound();
            playerSprite.IsBeaver = true;
            beaverSprite.IsAlive = false;
            beaverSprite.YPosition = Program.totalHeightTileCount + 1.0;
        }
    }
}
