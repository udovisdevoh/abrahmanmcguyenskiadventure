using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    internal enum LevelBoundType { Hole, Wall, MinusExponentialDistance, PlusExponentialDistance }

    /// <summary>
    /// Represents a surface on which a sprite can walk
    /// </summary>
    internal class Ground : IGround
    {
        #region Fields and parts
        /// <summary>
        /// Terrain's wave
        /// </summary>
        private AbstractWave terrainWave;

        /// <summary>
        /// Top texture
        /// </summary>
        private Texture topTexture;

        /// <summary>
        /// Base texture
        /// </summary>
        private Texture bottomTexture;
        
        /// <summary>
        /// Represents wave modelization of holes in a level
        /// </summary>
        private HoleSet holeSet;

        /// <summary>
        /// Stores which part of a ground are destroyed by a beaver
        /// </summary>
        private BeaverDestructionSet beaverDestructionSet;

        /// <summary>
        /// Next ground
        /// </summary>
        private Ground nextCloser = null;

        /// <summary>
        /// Previous ground
        /// </summary>
        private Ground previousFurther = null;

        /// <summary>
        /// Whether ground is celing
        /// </summary>
        private bool isCeiling;

        /// <summary>
        /// Left bound
        /// </summary>
        private double leftBound;

        /// <summary>
        /// Right bound
        /// </summary>
        private double rightBound;

        /// <summary>
        /// Width of a pillar that holds a transparent ground
        /// </summary>
        private double pillarWidth;

        /// <summary>
        /// Height of the ceiling (if it is a ceiling)
        /// </summary>
        private double ceilingHeight = 0.0;

        /// <summary>
        /// Type of level bound (hill, valley, etc) at the westernmost side of the level
        /// </summary>
        private LevelBoundType leftBoundType;

        /// <summary>
        /// Type of level bound (hill, valley, etc) at the easternmost side of the level
        /// </summary>
        private LevelBoundType rightBoundType;

        /// <summary>
        /// Whether ground's color is transparent
        /// </summary>
        private bool isTransparent;

        /// <summary>
        /// Whether we put a base texture
        /// </summary>
        private bool isUseBottomTexture;

        /// <summary>
        /// Whether this ground is only a path (nobody can walk on it except linkages (platforms, wheels, seesaws etc)
        /// Paths have no texture. They are single lines
        /// </summary>
        private bool isPathOnly;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a ground
        /// </summary>
        /// <param name="terrainWave">wave to use for terrain</param>
        /// <param name="random">random number generator</param>
        /// <param name="color">terrain's top most layer's color</param>
        /// <param name="holeSet">represents wave modelization of holes in a level</param>
        /// <param name="isPathOnly">whether ground is only a path for linkages (platforms, wheels etc)</param>
        public Ground(AbstractWave terrainWave, Random random, Color color, HoleSet holeSet, bool isCeiling, bool isPathOnly, int seed, int groundId, double leftBound, double rightBound, LevelBoundType leftBoundType, LevelBoundType rightBoundType)
        {
            this.isCeiling = isCeiling;
            this.holeSet = holeSet;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
            this.leftBoundType = leftBoundType;
            this.rightBoundType = rightBoundType;
            this.isPathOnly = isPathOnly;

            if (!isPathOnly)
            {
                topTexture = new Texture(random, color, 1.5, (!Program.isEnableParallelTextureRendering || isCeiling), seed, groundId, true);
                if (isCeiling)
                    topTexture.Surface = topTexture.Surface.CreateFlippedVerticalSurface();
            }

            beaverDestructionSet = new BeaverDestructionSet();

            if (Program.isAlwaysUseBottomTexture)
                isUseBottomTexture = !isCeiling;
            else
                isUseBottomTexture = /*!isCeiling && */random.Next(0, 2) == 0;

            pillarWidth = Program.collisionDetectionResolution;

            if (Program.isUseBottomTexture && isUseBottomTexture && !isPathOnly)
            {
                bottomTexture = new Texture(random, color, 16, 0.75, (!Program.isEnableParallelTextureRendering || isCeiling), seed, groundId, false);
                if (isCeiling)
                    bottomTexture.Surface = bottomTexture.Surface.CreateFlippedVerticalSurface();
            }
            
            this.terrainWave = terrainWave;
            isTransparent = !isCeiling && random.Next(0,5) == 0;
        }
        #endregion
        
        #region Internal Methods
        /// <summary>
        /// Remove beaver's holes
        /// </summary>
        internal void ClearBeaverDestruction()
        {
            beaverDestructionSet.Clear();
        }

        /// <summary>
        /// Dig hole at X position
        /// </summary>
        /// <param name="holeXPosition"></param>
        internal void DigHole(double holeXPosition)
        {
            beaverDestructionSet.Dig(holeXPosition);
        }

        /// <summary>
        /// Whether there is a hole at X position
        /// </summary>
        /// <param name="xPosition">X position</param>
        /// <returns>Whether there is a hole at X position</returns>
        internal bool IsHoleAt(double xPosition)
        {
            double yValue = (Program.isUseWaveValueCache) ? terrainWave.GetCachedValue(xPosition) : terrainWave[xPosition];
            return holeSet[xPosition, yValue];
        }

        /// <summary>
        /// Get ground height at X position regardless holes
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <returns>height at X position regardless holes</returns>
        internal double GetGroundHeightNoHole(double xPosition)
        {
            return (Program.isUseWaveValueCache) ? terrainWave.GetCachedValue(xPosition) : terrainWave[xPosition];
        }
        #endregion

        #region Properties
        /// <summary>
        /// Y value at X position
        /// </summary>
        /// <param name="xPosition">X position</param>
        /// <returns>Y value at X position</returns>
        public double this[double xPosition]
        {
            get
            {
                if (!isCeiling)
                {
                    if (xPosition < leftBound)
                        return GetOutBoundHeight(xPosition, false);
                    else if (xPosition > rightBound)
                        return GetOutBoundHeight(xPosition, true);
                }

                double yValue = (Program.isUseWaveValueCache) ? terrainWave.GetCachedValue(xPosition) : terrainWave[xPosition];
                if (holeSet[xPosition, yValue])
                {
                    if (isCeiling)
                        yValue = yValue - Program.holeHeight;
                    else
                        yValue = Program.holeHeight - yValue;
                }

                if (isCeiling)
                    yValue -= ceilingHeight;
                else
                    yValue += beaverDestructionSet[xPosition];

                return yValue;
            }
        }

        /// <summary>
        /// Height of the ceiling (if it is a ceiling)
        /// </summary>
        public double CeilingHeight
        {
            get { return ceilingHeight; }
            set { ceilingHeight = value; }
        }

        /// <summary>
        /// Width of a pillar that holds a transparent ground
        /// </summary>
        public double PillarWidth
        {
            get { return pillarWidth; }
        }

        private double GetOutBoundHeight(double xPosition, bool isRightBound)
        {
            double boundXPosition;
            LevelBoundType boundType;
            if (isRightBound)
            {
                boundXPosition = rightBound;
                boundType = rightBoundType;
            }
            else
            {
                boundXPosition = leftBound;
                boundType = leftBoundType;
            }

            if (boundType == LevelBoundType.Hole)
                return Program.holeHeight;
            else if (boundType == LevelBoundType.Wall)
                return -Program.holeHeight;

            double yValue = (Program.isUseWaveValueCache) ? terrainWave.GetCachedValue(xPosition) : terrainWave[xPosition];

            if (boundType == LevelBoundType.MinusExponentialDistance)
                yValue -= Math.Min(Math.Pow(1.1, Math.Abs(xPosition - boundXPosition)), Program.holeHeight);
            else if (boundType == LevelBoundType.PlusExponentialDistance)
                yValue += Math.Min(Math.Pow(1.1, Math.Abs(xPosition - boundXPosition)), Program.holeHeight);
            /*else if (boundType == LevelBoundType.HeightMultiplication)
                yValue *= Math.Abs(xPosition - boundXPosition);
            else if (boundType == LevelBoundType.HeightDivision)
                yValue /= Math.Abs(xPosition - boundXPosition);
            else if (boundType == LevelBoundType.SquareHeight)
                yValue = Math.Pow(yValue,2.0);
            else if (boundType == LevelBoundType.SquareRootHeight)
                yValue = Math.Sqrt(yValue);*/

            if (holeSet[xPosition, yValue])
                yValue = Program.holeHeight - yValue;
            yValue += beaverDestructionSet[xPosition];

            return yValue;
        }

        /// <summary>
        /// Top texture
        /// </summary>
        public Texture TopTexture
        {
            get { return topTexture; }
        }

        /// <summary>
        /// Base texture
        /// </summary>
        public Texture BottomTexture
        {
            get { return bottomTexture; }
        }
        
        /// <summary>
        /// Whether ground's color is transparent
        /// </summary>
        public bool IsTransparent
        {
        	get{return isTransparent;}
        }

        /// <summary>
        /// Whether we use a base texture
        /// </summary>
        public bool IsUseBottomTexture
        {
            get { return isUseBottomTexture; }
        }

        /// <summary>
        /// Whether there will be a scaling effect on top texture's thickness
        /// </summary>
        public bool IsUseTopTextureThicknessScaling
        {
            get { return topTexture.IsUseTopTextureThicknessScaling; }
        }

        /// <summary>
        /// Whether this ground is only a path (nobody can walk on it except linkages (platforms, wheels, seesaws etc)
        /// Paths have no texture. They are single lines
        /// </summary>
        public bool IsPathOnly
        {
            get { return isPathOnly; }
        }

        /// <summary>
        /// Next ground
        /// </summary>
        public Ground NextCloser
        {
            get { return nextCloser; }
            set { nextCloser = value; }
        }

        /// <summary>
        /// Previous ground
        /// </summary>
        public Ground PreviousFurther
        {
            get { return previousFurther; }
            set { previousFurther = value; }
        }
        #endregion
    }
}
