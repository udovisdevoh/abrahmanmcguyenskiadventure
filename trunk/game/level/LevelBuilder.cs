using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    internal class LevelBuilder
    {
        private WaveBuilder waveBuilder = new WaveBuilder();

        public Level Build(Random random)
        {
            Level level = new Level(random);

            int waveCount = random.Next(3, 12);
            for (int i = 0; i < waveCount; i++)
            {
                IWave wave = waveBuilder.BuildWavePack(random);
                //IWave wave = waveBuilder.BuildWaveTree(random,32);
                wave.Normalize(32);

                level.AddTerrainWave(new Ground(wave));
            }

            /*WavePack terrainWave = new WavePack(); //To test rendering
            terrainWave.Add(new Wave(40, 400, 0, WaveFunctions.Saw));
            level.AddTerrainWave(terrainWave);*/

            return level;
        }
    }
}
