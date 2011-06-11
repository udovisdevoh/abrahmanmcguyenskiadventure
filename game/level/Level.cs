using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// A game's level
    /// </summary>
    internal class Level : IEnumerable<Ground>
    {
        #region Fields and parts
        /// <summary>
        /// List of walkable grounds in the level
        /// </summary>
        private List<Ground> groundList;

        /// <summary>
        /// Represents wave modelization of holes in a level
        /// </summary>
        private HoleSet holeSet;
        #endregion

        #region Constructor
        /// <summary>
        /// Use level generator instead
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="colorTheme">color theme</param>
        public Level(Random random, ColorTheme colorTheme, int seed)
        {
            groundList = new List<Ground>();
            holeSet = new HoleSet(random);

            int waveCount = random.Next(3, 6);
            for (int i = 0; i < waveCount; i++)
            {
                AbstractWave wave;
                //if (random.Next(0, 4) != 0)
                    wave = WaveBuilder.BuildWavePack(random);
                //else
                //    wave = WaveBuilder.BuildWaveTree(random, 16);

                //wave = new Wave(10, 10, 0, WaveFunctions.AbsSin);

                double normalizationFactor = (random.NextDouble() * 20) + 4;
                wave.Normalize(normalizationFactor, false);

                //level.AddTerrainWave(new Ground(wave, random));
                BuildNewGround(wave, random, colorTheme.GetColor(waveCount - i - 1), holeSet, seed, i);
            }
        }
        #endregion

        #region IEnumerable<IWave> Members
        /// <summary>
        /// List of grounds
        /// </summary>
        /// <returns>List of grounds</returns>
        public IEnumerator<Ground> GetEnumerator()
        {
            return groundList.GetEnumerator();
        }

        /// <summary>
        /// List of grounds
        /// </summary>
        /// <returns>List of grounds</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return groundList.GetEnumerator();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Add ground to level
        /// </summary>
        /// <param name="ground">ground</param>
        internal void AddGround(Ground ground)
        {
            Ground previousFurther = null;
            if (groundList.Count > 0)
                previousFurther = groundList[groundList.Count - 1];

            if (previousFurther != null)
            {
                previousFurther.NextCloser = ground;
                ground.PreviousFurther = previousFurther;
            }

            groundList.Add(ground);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build new ground (internally)
        /// </summary>
        /// <param name="wave">wave</param>
        /// <param name="random">random number generator</param>
        /// <param name="color">color</param>
        private void BuildNewGround(AbstractWave wave, Random random, Color color, HoleSet holeSet, int seed, int groundId)
        {
            AddGround(new Ground(wave, random, color, holeSet, seed, groundId));
        }
        #endregion

        #region Properties
        /// <summary>
        /// How many grounds
        /// </summary>
        public int Count
        {
            get { return groundList.Count; }
        }

        /// <summary>
        /// Ground at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Ground at index</returns>
        public Ground this[int index]
        {
            get { return groundList[index]; }
        }
        #endregion
    }
}
