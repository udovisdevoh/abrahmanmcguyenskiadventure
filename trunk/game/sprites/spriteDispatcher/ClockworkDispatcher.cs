using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Clockwork dispatcher
    /// </summary>
    internal static class ClockworkDispatcher
    {
        internal static void DispatchClockwork(Level level, SpritePopulation spritePopulation, level.WaterInfo waterInfo, Random random)
        {
            Pendulum pendulum = new Pendulum(8, -17, random, false, 0);
            spritePopulation.Add(pendulum);

            FlailBall flailBall = new FlailBall(20, -13, random, false, 0);
            spritePopulation.Add(flailBall);

            Platform platform = new Platform(8, -11, random, false, 0);
            spritePopulation.Add(platform);


            pendulum.AddChild(platform);
        }
    }
}
