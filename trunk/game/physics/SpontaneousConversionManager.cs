using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages cases where sprites are spontaneously converted when they have stoped moving for too long
    /// </summary>
    internal class SpontaneousConversionManager
    {
        /// <summary>
        /// Manages cases where sprites are spontaneously converted when they have stoped moving for too long
        /// </summary>
        /// <param name="monster">monster</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="timeDelta">time delta</param>
        /// <param name="random">random number generator</param>
        internal void Update(MonsterSprite monster, SpritePopulation spritePopulation, double timeDelta, Random random)
        {
            if (!monster.SpontaneousTransformationCycle.IsFired || monster.IsWalkEnabled || monster.SpontaneousTransformationCycle.IsFinished)
                return;

            monster.SpontaneousTransformationCycle.Increment(timeDelta);

            if (monster.SpontaneousTransformationCycle.IsFinished)
            {
                SideScrollerSprite newSprite = monster.GetConverstionSprite(random);
                if (newSprite == null)
                    return;

                monster.IsAlive = false;
                monster.YPosition = Program.totalHeightTileCount + 1.0;
                spritePopulation.Add(newSprite);
            }
        }
    }
}
