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

            int waveCount = random.Next(3, 6);
            for (int i = 0; i < waveCount; i++)
                level.AddTerrainWave(waveBuilder.Build(random));

            return level;
        }
    }
}
