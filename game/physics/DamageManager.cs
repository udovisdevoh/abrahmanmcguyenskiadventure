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
            sprite.HitCycle.Increment(timeDelta);
            sprite.PunchedCycle.Increment(timeDelta);
            if (sprite is MonsterSprite)
            ((MonsterSprite)sprite).KickedHelmetCycle.Increment(timeDelta);

            if (sprite.IsAlive && sprite.HitCycle.IsFired && sprite.CurrentDamageReceiving > 0)
            {
                double decrease = (sprite.CurrentDamageReceiving * timeDelta / sprite.TotalHitTime) * 5.0;

                if (sprite.Health - sprite.CurrentDamageReceiving < 0.05)
                {
                    //Instant death, no progressive decrease of health because health < damage
                    sprite.IsAlive = false;
                }
                else
                {
                    if (sprite.CurrentDamageReceiving - decrease < 0)
                        decrease -= Math.Abs(sprite.CurrentDamageReceiving - decrease);

                    sprite.Health -= decrease;
                    sprite.CurrentDamageReceiving -= decrease;
                }

                if (!sprite.IsAlive && sprite is PlayerSprite)
                    SoundManager.PlayKo2Sound();
            }
        }
    }
}
