﻿using System;
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

            int waveCount = random.Next(3, 6);
            for (int i = 0; i < waveCount; i++)
            {
                IWave wave;
                if (random.Next(0,4) != 0)
                    wave = waveBuilder.BuildWavePack(random);
                else
                    wave = waveBuilder.BuildWaveTree(random,16);

                //wave = new Wave(10, 10, 0, WaveFunctions.AbsSin);

                double normalizationFactor = (random.NextDouble() * 20) + 4;
                wave.Normalize(normalizationFactor, false);

                //level.AddTerrainWave(new Ground(wave, random));
                level.BuildNewGround(wave, random, level.colorTheme.GetColor(waveCount - i - 1));
            }

            /*WavePack terrainWave = new WavePack(); //To test rendering
            terrainWave.Add(new Wave(40, 400, 0, WaveFunctions.Saw));
            level.AddTerrainWave(terrainWave);*/

            return level;
        }
    }
}
