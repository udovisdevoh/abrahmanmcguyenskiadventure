﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

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
        internal void Update(AbstractSprite sprite, double timeDelta)
        {
            if (sprite is MonsterSprite && sprite.Health < sprite.CurrentDamageReceiving)
            {
                sprite.IsAlive = false;
            }

            sprite.HitCycle.Increment(timeDelta);
            
            if (sprite.IsAlive && sprite.HitCycle.IsFired)
                sprite.Health -= sprite.CurrentDamageReceiving * timeDelta / sprite.TotalHitTime;
        }
    }
}