using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages carriable sprite
    /// </summary>
    internal class CarriableSpriteManager
    {
        internal SpriteCollisionManager spriteCollisionManager = new SpriteCollisionManager();

        internal void UpdateCarriedSprite(AbstractSprite carrier, AbstractSprite carriedItem, Level level, Program program, double timeDelta)
        {
            carriedItem.YPosition = carrier.YPosition - carrier.Height / 4.0;

            if (carrier.IsTryingToWalkRight)
            {
                carriedItem.XPosition = carrier.RightBound;
            }
            else
            {
                carriedItem.XPosition = carrier.LeftBound;
            }

            if (!carrier.IsRunning)
            {
                if (carriedItem is MonsterSprite)
                {
                    carriedItem.IGround = carrier.IGround;

                    if (program.UserInput.isPressDown && carriedItem.IGround != null) //Deposit carried item
                    {
                        ((MonsterSprite)carriedItem).KickedHelmetCycle.Fire();//We don't kick the helmet, but we must prevent further accicdental kick so next kick will wait
                        ((MonsterSprite)carriedItem).SpontaneousTransformationCycle.Fire();

                        if (carrier.IsTryingToWalkRight)
                            ((MonsterSprite)carriedItem).XPosition = carrier.RightBound + 0.5;
                        else
                            ((MonsterSprite)carriedItem).XPosition = carrier.LeftBound - 0.5;

                        carriedItem.YPosition = carriedItem.IGround[carriedItem.XPosition];
                    }
                    else //Throw carried item
                    {
                        spriteCollisionManager.KickOrStopHelmet(carrier, (MonsterSprite)carriedItem, level, timeDelta);
                        ((MonsterSprite)carriedItem).IsNoAiDefaultDirectionWalkingRight = carrier.IsTryingToWalkRight;
                        carriedItem.CurrentWalkingSpeed = Math.Max(carriedItem.MaxWalkingSpeed, carrier.CurrentWalkingSpeed);
                    }

                    if (carriedItem.IGround == null)
                    {
                        carriedItem.JumpingCycle.Fire();
                        carriedItem.CurrentJumpAcceleration = carriedItem.StartingJumpAcceleration / 2.0;
                    }
                }
                carrier.CarriedSprite = null;
            }
        }
    }
}
