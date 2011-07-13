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
    /// Manages collsions between helmets and monsters
    /// </summary>
    internal class HelmetCollisionManager
    {
        #region Fields and parts
        /// <summary>
        /// Manages block collision and stuff like open, close, break
        /// </summary>
        private BlockManager blockManager = new BlockManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Manages collsions between helmets and monsters
        /// </summary>
        /// <param name="helmet">helmet sprite</param>
        /// <param name="level">level</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        /// <param name="spritePopulation">all the sprites in game state</param>
        /// <param name="random">random number generator</param>
        internal void Update(HelmetSprite helmet, AbstractSprite playerSpriteReference, Level level, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList, Random random)
        {
            if (!helmet.IsWalkEnabled && !helmet.IsCurrentlyInFreeFallX && helmet != playerSpriteReference.CarriedSprite)
                return;

            foreach (AbstractSprite otherSprite in visibleSpriteList)
            {
                if (helmet != otherSprite && !(otherSprite is PlayerSprite) && !otherSprite.HitCycle.IsFired && !(otherSprite is FireBallSprite))
                {
                    if (otherSprite is MonsterSprite && !(otherSprite is BeaverSprite))
                    {
                        if (Physics.IsDetectCollision(helmet, otherSprite))
                        {
                            helmet.IsCurrentlyInFreeFallX = false;
                            helmet.IsCurrentlyInFreeFallY = false;
                            SoundManager.PlayHitSound();
                            otherSprite.HitCycle.Fire();
                            otherSprite.CurrentDamageReceiving = helmet.AttackStrengthCollision * 2.0;//Yes, twice damage to monsters

                            if (helmet == playerSpriteReference.CarriedSprite)
                            {
                                helmet.IsAlive = false;
                                helmet.CurrentWalkingSpeed = playerSpriteReference.CurrentWalkingSpeed;
                                helmet.IsCurrentlyInFreeFallX = true;
                                helmet.IsNoAiDefaultDirectionWalkingRight = playerSpriteReference.IsTryingToWalkRight;
                                helmet.IsTryingToWalkRight = playerSpriteReference.IsTryingToWalkRight;
                                playerSpriteReference.CarriedSprite = null;
                            }
                        }
                    }
                    else if (otherSprite is StaticSprite && helmet.IGround != null && otherSprite.IsImpassable && helmet != playerSpriteReference.CarriedSprite)
                    {
                        double virtualX = helmet.XPosition + ((helmet.IsTryingToWalkRight) ? helmet.CurrentWalkingSpeed : -helmet.CurrentWalkingSpeed);
                        double virtualY = helmet.IGround[virtualX];
                        if (Physics.IsDetectCollision(helmet, virtualX, virtualY, 1.0, otherSprite))
                        {
                            blockManager.TryOpenOrBreakBlock(helmet, (StaticSprite)otherSprite, spritePopulation, visibleSpriteList, level, random);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
