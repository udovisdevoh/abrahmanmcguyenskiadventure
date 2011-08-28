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
    /// Manages carriable sprite
    /// </summary>
    internal class CarriableSpriteManager
    {
        #region Fields and parts
        /// <summary>
        /// Sprite collision manager
        /// </summary>
        private SpriteCollisionManager spriteCollisionManager = new SpriteCollisionManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Update carried sprite
        /// </summary>
        /// <param name="carrier">carrier</param>
        /// <param name="carriedItem">carried item</param>
        /// <param name="level">level</param>
        /// <param name="program">program</param>
        /// <param name="timeDelta">time delta</param>
        internal void UpdateCarriedSprite(SideScrollerSprite carrier, SideScrollerSprite carriedItem, Level level, Program program, double timeDelta)
        {
            if (!carriedItem.IsAlive)
            {
                carrier.CarriedSprite = null;
                return;
            }

            carriedItem.IGround = null;
            if (program.UserInput.isPressUp)
                carriedItem.YPosition = carrier.YPosition - carrier.Height / 8.0;
            else
                carriedItem.YPosition = carrier.YPosition - carrier.Height / 4.0;

            if (carrier.IsTryingToWalkRight)
            {
                carriedItem.XPosition = carrier.RightBound;
            }
            else
            {
                carriedItem.XPosition = carrier.LeftBound;
            }

            if (!carrier.IsRunning) //Sprite release carried item because running button is not pressed anymore
            {
                if (carriedItem is MonsterSprite)
                {
                    carriedItem.IGround = carrier.IGround;

                    if (program.UserInput.isPressDown && carriedItem.IGround != null && !(carriedItem is IHarvestable)) //Deposit carried item, helmet only
                    {
                        ((MonsterSprite)carriedItem).KickedHelmetCycle.Fire();//We don't kick the helmet, but we must prevent further accicdental kick so next kick will wait
                        ((MonsterSprite)carriedItem).SpontaneousTransformationCycle.Fire();

                        if (carrier.IsTryingToWalkRight)
                            ((MonsterSprite)carriedItem).XPosition = carrier.RightBound + 0.5;
                        else
                            ((MonsterSprite)carriedItem).XPosition = carrier.LeftBound - 0.5;

                        carriedItem.YPosition = carriedItem.IGround[carriedItem.XPosition];
                    }
                    else if (program.UserInput.isPressUp || carriedItem is IHarvestable) //We throw it up (helmets or fat kids)
                    {
                        if (carriedItem is IHarvestable)
                            SoundManager.PlayThrowSound();
                        else
                            SoundManager.PlayHelmetKickSound();

                        ((MonsterSprite)carriedItem).KickedHelmetCycle.Fire();//We don't kick the helmet, but we must prevent further accicdental kick so next kick will wait
                        ((MonsterSprite)carriedItem).SpontaneousTransformationCycle.Fire();

                        ((MonsterSprite)carriedItem).IsNoAiDefaultDirectionWalkingRight = carrier.IsTryingToWalkRight;
                        ((MonsterSprite)carriedItem).IsTryingToWalkRight = carrier.IsTryingToWalkRight;

                        carriedItem.IGround = null;
                        carriedItem.YPosition = carrier.TopBound;
                        carriedItem.JumpingCycle.Fire();
                        carriedItem.IsCurrentlyInFreeFallX = true;
                        
                        if (program.UserInput.isPressUp) //helmet or fat kid will be thrown up
                        {
                            carriedItem.CurrentJumpAcceleration = carriedItem.StartingJumpAcceleration * 2.0;
                            carriedItem.CurrentWalkingSpeed = carrier.CurrentWalkingSpeed;
                        }
                        else //fat kid will be thrown parabolically to the left or right
                        {
                            carriedItem.CurrentJumpAcceleration = carriedItem.StartingJumpAcceleration * 0.666;
                            carriedItem.CurrentWalkingSpeed = carrier.CurrentWalkingSpeed + carrier.MaxWalkingSpeed * 1.5;
                        }
                    }
                    else //We throw it left or right (helmets only)
                    {
                        spriteCollisionManager.KickOrStopHelmet(carrier, (MonsterSprite)carriedItem, level, timeDelta);
                        ((MonsterSprite)carriedItem).IsNoAiDefaultDirectionWalkingRight = carrier.IsTryingToWalkRight;
                        carriedItem.CurrentWalkingSpeed = (carriedItem.MaxWalkingSpeed / 2.0) + carrier.CurrentWalkingSpeed;

                        if (carriedItem.IGround == null)
                        {
                            carriedItem.IsCurrentlyInFreeFallX = true;
                            carriedItem.JumpingCycle.Fire();
                            carriedItem.CurrentJumpAcceleration = carriedItem.StartingJumpAcceleration / 3;
                        }
                    }
                }
                carrier.CarriedSprite = null;
            }
        }
        #endregion
    }
}
