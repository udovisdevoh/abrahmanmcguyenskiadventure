using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages explosive sprites
    /// </summary>
    internal class ExplosionManager
    {
        /// <summary>
        /// Update sprites that can explode
        /// </summary>
        /// <param name="spriteToUpdate">sprite to update</param>
        /// <param name="playerSpriteReference">player sprite</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="random">random number generator</param>
        internal void Update(IExplodable spriteToUpdate, AbstractSprite playerSpriteReference, SpritePopulation spritePopulation, double timeDelta, Random random)
        {
            double distanceToPlayer = Math.Max(Math.Abs(((AbstractSprite)spriteToUpdate).XPosition - playerSpriteReference.XPosition), Math.Abs(((AbstractSprite)spriteToUpdate).YPosition - playerSpriteReference.YPosition));

            if (distanceToPlayer <= spriteToUpdate.MinDistanceFromPlayerToStartCountDown && !spriteToUpdate.CountDownCycle.IsFired)
            {
                spriteToUpdate.CountDownCycle.Fire();
            }

            if (spriteToUpdate.CountDownCycle.IsFired)
            {
                spriteToUpdate.CountDownCycle.Increment(timeDelta);

                if (spriteToUpdate.CountDownCycle.IsFinished)
                {
                    SoundManager.PlayExplosionSound();
                    ((AbstractSprite)spriteToUpdate).IsAlive = false;
                    ((AbstractSprite)spriteToUpdate).YPosition = Program.totalHeightTileCount + 1.0;
                }
            }
        }
    }
}
