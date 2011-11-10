using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages sprites like drills (piranha plant behavior)
    /// </summary>
    internal class UpDownCycleMoveManager
    {
        internal void update(IUpDownCycleMove upDownMovingSprite, AbstractSprite playerSprite, double timeDelta)
        {
            if (upDownMovingSprite.UpDownCycle.CurrentValue < upDownMovingSprite.AlwaysActiveRangeCycleStart)
                upDownMovingSprite.UpDownCycle.Increment(timeDelta);
            else if (upDownMovingSprite.UpDownCycle.CurrentValue > upDownMovingSprite.AlwaysActiveRangeCycleStop)
                upDownMovingSprite.UpDownCycle.Increment(timeDelta);
            else if (Math.Abs(upDownMovingSprite.XPosition - playerSprite.XPosition) > upDownMovingSprite.DontMoveUpDistance)
                    upDownMovingSprite.UpDownCycle.Increment(timeDelta);
        }
    }
}
