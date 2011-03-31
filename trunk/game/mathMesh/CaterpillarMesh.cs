using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.mathMesh
{
    /// <summary>
    /// Representation of a caterpillar
    /// </summary>
    class CaterpillarMesh : MathMesh
    {
        #region Fields and parts
        /// <summary>
        /// Represents the catterpillar's main shape
        /// </summary>
        private AbstractWave shapeWave;

        /// <summary>
        /// Represents the catterpillar's texture
        /// </summary>
        private Texture texture;
        #endregion

        #region Constructors
        /// <summary>
        /// Builds a caterpillar's mesh
        /// </summary>
        /// <param name="random">random number generator</param>
        public CaterpillarMesh(Random random, double width, double height)
        {
            shapeWave = BuildShapeWave(random, width, height);
            texture = new Texture(random);
        }

        /// <summary>
        /// Build a caterpillar's shape's wave
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>caterpillar's shape's wave</returns>
        private AbstractWave BuildShapeWave(Random random, double width, double height)
        {
            WavePack shapeWavePack = new WavePack();
            int waveCount = random.Next(1, 11);
            for (int i = 0; i < waveCount; i++)
            {
                double amplitude = random.NextDouble();
                double waveLength = random.NextDouble() * (width * 0.7) + (width * 0.125);
                double phase = random.NextDouble() * 2.0 - 1.0;
                Wave currentWave = new Wave(amplitude, waveLength, phase, WaveFunctions.Sine);
                shapeWavePack.Add(currentWave);
            }
            shapeWavePack.Normalize();
            return shapeWavePack;
        }
        #endregion
    }
}
