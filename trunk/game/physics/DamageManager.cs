using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

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
        /// <param name="random">random number generator</param>
        internal void Update(AbstractSprite sprite, double timeDelta)
        {
            /*if (sprite is MonsterSprite && sprite.Health < sprite.CurrentDamageReceiving)
            {
                sprite.IsAlive = false;
            }*/

            sprite.HitCycle.Increment(timeDelta);
            sprite.PunchedCycle.Increment(timeDelta);

            if (sprite.IsAlive && sprite.HitCycle.IsFired && sprite.HitCycle.CurrentValue <= sprite.HitCycle.TotalTimeLength / 3.0)
            {
                sprite.Health -= (sprite.CurrentDamageReceiving * timeDelta / sprite.TotalHitTime) * 3.0;

                if (!sprite.IsAlive)
                {
                    if (sprite is PlayerSprite)
                        SoundManager.PlayKo2Sound();
                }
            }
        }
    }
}
