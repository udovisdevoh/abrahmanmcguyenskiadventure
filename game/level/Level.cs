using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AbrahmanAdventure.physics;

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
        /// Celing of the level, can be null
        /// </summary>
        private Ground ceiling = null;

        /// <summary>
        /// Some levels have a path for platforms and wheels (can be null)
        /// </summary>
        private Ground path = null;

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
        /// Skill level
        /// </summary>
        private int skillLevel;

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
        public Level(Random random, ColorTheme colorTheme, int seed, int skillLevel, bool isWater, AbstractGameMode gameMode)
        {
            this.skillLevel = skillLevel;
            groundList = new List<Ground>();

            int waveCount = random.Next(3, 6);

            leftBound = -30;//-BuildLevelBound(random, skillLevel);
            rightBound = BuildLevelBound(random, skillLevel) * gameMode.LevelSizeMultiplicator;

            if (gameMode.IsBoundAlwaysWall)
            {
                leftBoundType = LevelBoundType.Wall;
                rightBoundType = LevelBoundType.Wall;
            }
            else
            {
                leftBoundType = BuildBoundType(random);
                rightBoundType = BuildBoundType(random);
            }

            double levelWidth = rightBound - leftBound;

            holeSet = new HoleSet(random, skillLevel, levelWidth, gameMode);

            int groundId;
            for (groundId = 0; groundId < waveCount; groundId++)
                AddGround(new Ground(BuildGroundWave(random, false, gameMode.IsCurvyWaveOnly), random, colorTheme.GetColor(waveCount - groundId - 1), holeSet, false, false, seed, groundId, leftBound, rightBound, leftBoundType, rightBoundType));


            if (Program.isEnableParallelTextureRendering)
            {
                ParallelTextureRenderer parallelTextureRenderer = new ParallelTextureRenderer();
                parallelTextureRenderer.Render(groundList);
            }

            #region We determine whether there will be a ceiling in the level
            //For water levels: 2/3, for no water levels: 1/2
            bool isCeiling = (isWater) ? (random.Next(0, 3) != 1) : (random.Next(0, 2) == 1);
            if (!Program.isAllowCeiling || skillLevel == 0)
                isCeiling = false;
            else if (Program.isAlwaysCeiling)
                isCeiling = true;
            #endregion

            if (isCeiling)
            {
                double highestXPoint = IGroundHelper.GetHighestXPoint(this, leftBound, rightBound);
                ceiling = new Ground(BuildGroundWave(random, false, false), random, colorTheme.GetRandomColor(random), holeSet, true, false, seed, groundId, leftBound, rightBound, leftBoundType, rightBoundType);
                double lowestXPoint = IGroundHelper.GetLowestXPoint(ceiling, this, leftBound, rightBound, Program.addedDistanceBetweenHighestGroundAndCeilingIfAboveHole);
                ceiling.CeilingHeight = Math.Abs(highestXPoint - lowestXPoint) + (double)random.Next(2, 5);
            }

            #region We add a path for platforms and wheels
            if (random.NextDouble() > 0.76)
                path = new Ground(BuildGroundWave(random, true, false), random, Color.White, new HoleSet(random, 0, levelWidth, true, gameMode), false, true, seed, groundId, leftBound, rightBound, leftBoundType, rightBoundType);
            #endregion
        }
        #endregion

        #region IEnumerable<AbstractWave> Members
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
        /// Build level bound
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="skillLevel">skill level</param>
        /// <returns>level bound</returns>
        private double BuildLevelBound(Random random, int skillLevel)
        {
            return random.Next(0, 100 * (skillLevel + 1)) + 60;
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

        /// <summary>
        /// Build a ground's (or ceiling's) wave pacl
        /// </summary>
        /// <param name="hillWaveComplexityMultiplicator">default: 1.0</param>
        /// <returns>wave pack</returns>
        private AbstractWave BuildGroundWave(Random random, bool isPathOnly, bool isCurvyWaveOnly)
        {
            AbstractWave wave = WaveBuilder.BuildWavePack(random, isPathOnly, isCurvyWaveOnly);
            double normalizationFactor = (random.NextDouble() * 20) + 4;

            if (isPathOnly)
                normalizationFactor *= ((random.NextDouble() * 1.5) + 1.0);
            
            wave.Normalize(normalizationFactor, false);

            double highestJumpingStep = wave.GetHighestJumpingStep(leftBound, rightBound);
            if (highestJumpingStep > Program.maximumAllowedJumpingStep)
                wave.Normalize(normalizationFactor / highestJumpingStep * Program.maximumAllowedJumpingStep);
            return wave;
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
        /// Skill level
        /// </summary>
        public int SkillLevel
        {
            get { return skillLevel; }
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
        /// Ceiling ground (can be null)
        /// </summary>
        public Ground Ceiling
        {
            get { return ceiling; }
        }

        /// <summary>
        /// Path for platforms and wheels (can be null)
        /// </summary>
        public Ground Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Hole set
        /// </summary>
        public HoleSet HoleSet
        {
            get { return holeSet; }
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
