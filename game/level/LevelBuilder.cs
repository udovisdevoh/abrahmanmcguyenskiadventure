using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.waves;

namespace AbrahmanAdventure.level
{
    internal class LevelBuilder
    {
        private WaveBuilder waveBuilder = new WaveBuilder();

        public Level Build(Random random)
        {
            Level level = new Level(random);

            do
            {
                level.AddTerrainWave(waveBuilder.Build(random));
            } while (random.Next(0, 3) != 0);

            return level;
        }
    }
}
