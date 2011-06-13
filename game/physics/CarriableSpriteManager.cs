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

        internal void UpdateCarriedSprite(AbstractSprite carrier, AbstractSprite carried, Level level, double timeDelta)
        {
            carried.YPosition = carrier.YPosition - carrier.Height / 4.0;

            if (carrier.IsTryingToWalkRight)
            {
                carried.XPosition = carrier.RightBound;
            }
            else
            {
                carried.XPosition = carrier.LeftBound;
            }

            if (!carrier.IsRunning)
            {
                if (carried is MonsterSprite)
                {
                    carried.IGround = carrier.IGround;
                    spriteCollisionManager.KickOrStopHelmet(carrier, (MonsterSprite)carried, level, timeDelta);
                    ((MonsterSprite)carried).IsNoAiDefaultDirectionWalkingRight = carrier.IsTryingToWalkRight;
                    carried.CurrentWalkingSpeed = Math.Max(carried.MaxWalkingSpeed, carrier.CurrentWalkingSpeed);

                    if (carried.IGround == null)
                    {
                        carried.JumpingCycle.Fire();
                        carried.CurrentJumpAcceleration = carried.StartingJumpAcceleration / 2.0;
                    }
                }
                carrier.CarriedSprite = null;
            }
        }
    }
}
