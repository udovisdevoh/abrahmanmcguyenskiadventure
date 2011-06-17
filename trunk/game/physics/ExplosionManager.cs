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
                    ExplosionSprite explosionSprite = new ExplosionSprite(((AbstractSprite)spriteToUpdate).XPosition, ((AbstractSprite)spriteToUpdate).YPosition + 1.0, random);
                    ((AbstractSprite)spriteToUpdate).IsAlive = false;
                    ((AbstractSprite)spriteToUpdate).YPosition = Program.totalHeightTileCount + 1.0;
                    spritePopulation.Add(explosionSprite);
                }
            }
        }

        /// <summary>
        /// Update explosion sprite
        /// </summary>
        /// <param name="explosionSprite">explosion sprite</param>
        /// <param name="visibleSpriteList">list of visible sprite list</param>
        /// <param name="timeDelta">time delta</param>
        internal void UpdateExplosion(ExplosionSprite explosionSprite, HashSet<AbstractSprite> visibleSpriteList, double timeDelta)
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

            foreach (AbstractSprite otherMonster in visibleSpriteList)
            {
                if (otherMonster != explosionSprite && otherMonster is MonsterSprite)
                {
                    if (Physics.IsDetectCollision(explosionSprite, otherMonster))
                    {
                        if (!otherMonster.HitCycle.IsFired)
                        {
                            otherMonster.HitCycle.Fire();
                            //3 x the strength for damage on monsters
                            otherMonster.CurrentDamageReceiving = otherMonster.AttackStrengthCollision * 3.0;
                        }
                    }
                }
            }
        }
    }
}
