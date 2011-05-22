using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages damage logic
    /// </summary>
    internal class DamageManager
    {
        /// <summary>
        /// Manages damage logic
        /// </summary>
        /// <param name="sprite">Sprite</param>
        /// <param name="timeDelta">Time delta</param>
        internal void Update(AbrahmanAdventure.sprites.AbstractSprite sprite, double timeDelta)
        {
            sprite.HitCycle.Increment(timeDelta);
            
            if (sprite.IsAlive && sprite.HitCycle.IsFired)
                sprite.Health -= sprite.CurrentDamageReceiving * timeDelta / sprite.TotalHitTime;
        }
    }
}
