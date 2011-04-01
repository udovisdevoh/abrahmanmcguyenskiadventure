using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
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

        #region Properties
        /// <summary>
        /// Texture
        /// </summary>
        public Texture Texture
        {
            get { return texture; }
        }
        #endregion

        #region Public Methods
        internal Surface BuildSpriteFrameAt(double xInput, double width, double height)
        {
            int surfaceWidth = (int)(width * Program.tileSize);
            int surfaceHeight = (int)(height * Program.tileSize);

            Surface surface = new Surface(surfaceWidth, surfaceHeight, Program.bitDepth);

            for (int x = 0; x < surfaceWidth; x++)
            {
                int destinationY = 0;
                do
                {
                    int destinationHeight = texture.Surface.Height;

                    if (destinationY + destinationHeight > surfaceHeight)
                        destinationHeight -= (destinationY + destinationHeight) - surfaceHeight;

                    Rectangle destinationRectangle = new Rectangle(x, destinationY, 1, texture.Surface.Height);
                    Rectangle sourceRectangle = new Rectangle(x % texture.Surface.Width, 0, 1, texture.Surface.Height);
                    surface.Blit(texture.Surface, destinationRectangle, sourceRectangle);
                    
                    destinationY += texture.Surface.Height;
                } while (destinationY < surfaceHeight);
            }

            return surface;
        }
        #endregion
    }
}
