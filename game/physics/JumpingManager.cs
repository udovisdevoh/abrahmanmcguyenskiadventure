using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    internal class JumpingManager
    {
        internal void Update(AbstractSprite sprite, double timeDelta)
        {
            if (sprite.IsTryingToJump)
                StartOrContinueJump(sprite, timeDelta);

            sprite.JumpingCycle.Increment(timeDelta / Math.Max(sprite.MaximumWalkingHeight, sprite.CurrentWalkingSpeed));
        }

        private void StartOrContinueJump(AbstractSprite sprite, double timeDelta)
        {
            if (!sprite.IsNeedToJumpAgain)
            {
                if (sprite.Ground != null)
                {
                    sprite.JumpingCycle.Reset();
                    sprite.CurrentJumpAcceleration = sprite.StartingJumpAcceleration;
                    sprite.Ground = null;
                }

                if (sprite.CurrentJumpAcceleration < 0)
                {
                    sprite.IsNeedToJumpAgain = true;
                }
            }
        }

    }
}
