using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
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
        /// Whether ground's color is transparent
        /// </summary>
        private bool isTransparent;

        /// <summary>
        /// Whether we put a base texture
        /// </summary>
        private bool isUseBottomTexture;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a ground
        /// </summary>
        /// <param name="terrainWave">wave to use for terrain</param>
        /// <param name="random">random number generator</param>
        /// <param name="color">terrain's top most layer's color</param>
        /// <param name="holeSet">represents wave modelization of holes in a level</param>
        public Ground(AbstractWave terrainWave, Random random, Color color, HoleSet holeSet, int seed, int groundId)
        {
            this.holeSet = holeSet;

            topTexture = new Texture(random, color, 1.5, seed,groundId,true);
            beaverDestructionSet = new BeaverDestructionSet();

            if (Program.isAlwaysUseBottomTexture)
                isUseBottomTexture = true;
            else
                isUseBottomTexture = random.Next(0, 2) == 0;

            if (Program.isUseBottomTexture && isUseBottomTexture)
            {
                bottomTexture = new Texture(random, color, 16, 0.75, seed, groundId, false);
            }
            
            this.terrainWave = terrainWave;
            isTransparent = random.Next(0,5) == 0;
        }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Whether ground is higher than other grounds in level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="xInput">x value</param>
        /// <returns>Whether ground is higher than other grounds</returns>
        public bool IsHigherThanOtherGrounds(Ground ground, Level level, double xInput)
        {
        	double yOutput = terrainWave[xInput];
        	
        	foreach (Ground otherGround in level)
        		if (otherGround != this)
        			if (otherGround.terrainWave[xInput] < yOutput)
        				return false;

        	
        	return true;
        }

        /// <summary>
        /// Remove beaver's holes
        /// </summary>
        public void ClearBeaverDestruction()
        {
            beaverDestructionSet.Clear();
        }

        /// <summary>
        /// Dig hole at X position
        /// </summary>
        /// <param name="holeXPosition"></param>
        public void DigHole(double holeXPosition)
        {
            beaverDestructionSet.Dig(holeXPosition);
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
                double yValue = (Program.isUseWaveValueCache) ? terrainWave.GetCachedValue(xPosition) : terrainWave[xPosition];
                if (holeSet[xPosition, yValue])
                    yValue = Program.holeHeight - yValue;

                yValue += beaverDestructionSet[xPosition];

                return yValue;
            }
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
