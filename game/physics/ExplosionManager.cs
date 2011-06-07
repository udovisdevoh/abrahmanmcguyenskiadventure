﻿using System;
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
        internal void UpdateExplodable(IExplodable spriteToUpdate, AbstractSprite playerSpriteReference, SpritePopulation spritePopulation, double timeDelta, Random random)
        {
            double distanceToPlayer = Math.Max(Math.Abs(((AbstractSprite)spriteToUpdate).XPosition - playerSpriteReference.XPosition), Math.Abs(((AbstractSprite)spriteToUpdate).YPosition - playerSpriteReference.YPosition));

            if (distanceToPlayer <= spriteToUpdate.MinDistanceFromPlayerToStartCountDown && !spriteToUpdate.CountDownCycle.IsFired)
            {
                SoundManager.PlayBombTimerSound();
                spriteToUpdate.CountDownCycle.Fire();
            }

            if (spriteToUpdate.CountDownCycle.IsFired)
            {
                spriteToUpdate.CountDownCycle.Increment(timeDelta);

                if (spriteToUpdate.CountDownCycle.IsFinished)
                {
                    SoundManager.PlayExplosionSound();
                    ExplosionSprite explosionSprite = new ExplosionSprite(((AbstractSprite)spriteToUpdate).XPosition, ((AbstractSprite)spriteToUpdate).YPosition, random);
                    ((AbstractSprite)spriteToUpdate).IsAlive = false;
                    ((AbstractSprite)spriteToUpdate).YPosition = Program.totalHeightTileCount + 1.0;
                    spritePopulation.Add(explosionSprite);
                }
            }
        }

        internal void UpdateExplosion(ExplosionSprite explosionSprite, double timeDelta)
        {
            if (explosionSprite.ExplosionCycle.IsFired)
            {
                explosionSprite.ExplosionCycle.Increment(timeDelta);
            }

            if (explosionSprite.ExplosionCycle.IsFinished)
            {
                explosionSprite.IsAlive = false;
                explosionSprite.YPosition = Program.totalHeightTileCount + 1.0;
            }
        }
    }
}
