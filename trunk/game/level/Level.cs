﻿using System;
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

        /// <summary>
        /// Left bound
        /// </summary>
        private double leftBound;

        /// <summary>
        /// Right bound
        /// </summary>
        private double rightBound;

        /// <summary>
        /// Left bound type
        /// </summary>
        private LevelBoundType leftBoundType;

        /// <summary>
        /// Right bound type
        /// </summary>
        private LevelBoundType rightBoundType;
        #endregion

        #region Constructor
        /// <summary>
        /// Use level generator instead
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="seed">seed</param>
        /// <param name="skillLevel">skill level</param>
        public Level(Random random, ColorTheme colorTheme, int seed, int skillLevel)
        {
            groundList = new List<Ground>();

            int waveCount = random.Next(3, 6);

            leftBound = -30;//-BuildLevelBound(random, skillLevel);
            rightBound = BuildLevelBound(random, skillLevel);
            leftBoundType = BuildBoundType(random);
            rightBoundType = BuildBoundType(random);

            double levelWidth = rightBound - leftBound;

            holeSet = new HoleSet(random, skillLevel, levelWidth);

            for (int i = 0; i < waveCount; i++)
            {
                AbstractWave wave;
                wave = WaveBuilder.BuildWavePack(random);

                double normalizationFactor = (random.NextDouble() * 20) + 4;
                wave.Normalize(normalizationFactor, false);

                BuildNewGround(wave, random, colorTheme.GetColor(waveCount - i - 1), holeSet, seed, i, leftBound, rightBound, leftBoundType, rightBoundType);
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

        /// <summary>
        /// Remove beaver's holes
        /// </summary>
        internal void ClearBeaverDestruction()
        {
            foreach (Ground ground in this)
                ground.ClearBeaverDestruction();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build new ground (internally)
        /// </summary>
        /// <param name="wave">wave</param>
        /// <param name="random">random number generator</param>
        /// <param name="color">color</param>
        private void BuildNewGround(AbstractWave wave, Random random, Color color, HoleSet holeSet, int seed, int groundId, double leftBound, double rightBound, LevelBoundType leftBoundType, LevelBoundType rightBoundType)
        {
            AddGround(new Ground(wave, random, color, holeSet, seed, groundId, leftBound, rightBound, leftBoundType, rightBoundType));
        }

        /// <summary>
        /// Build level bound
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="skillLevel">skill level</param>
        /// <returns>level bound</returns>
        private double BuildLevelBound(Random random, int skillLevel)
        {
            return random.Next(0, 370 * (skillLevel + 1)) + 60;
        }

        /// <summary>
        /// Build bound type
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Bound type</returns>
        private LevelBoundType BuildBoundType(Random random)
        {
            return (LevelBoundType)random.Next(0, 4);
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

        /// <summary>
        /// Left bound
        /// </summary>
        public double LeftBound
        {
            get { return leftBound; }
        }

        /// <summary>
        /// Right bound
        /// </summary>
        public double RightBound
        {
            get { return rightBound; }
        }

        /// <summary>
        /// Level's size (width)
        /// </summary>
        public double Size
        {
            get { return rightBound - leftBound; }
        }
        #endregion
    }
}
