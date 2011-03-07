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
    internal class Ground
    {
        #region Fields and parts
        /// <summary>
        /// Terrain's wave
        /// </summary>
        private IWave terrainWave;

        /// <summary>
        /// Top texture
        /// </summary>
        private Texture topTexture;

        /// <summary>
        /// Base texture
        /// </summary>
        private Texture bottomTexture;
        
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
        /// <param name="holeSet">hole set</param>
        public Ground(IWave terrainWave, Random random, Color color, HoleSet holeSet)
        {
            topTexture = new Texture(random, color, 1.5, true);

            isUseBottomTexture = random.Next(0, 2) == 0;

            if (Program.isUseBottomTexture && isUseBottomTexture)
                bottomTexture = new Texture(random, color, 16, 0.75, false);
            
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
        #endregion

        #region Properties
        /// <summary>
        /// Y value at X position
        /// </summary>
        /// <param name="xPosition">X position</param>
        /// <returns>Y value at X position</returns>
        public double this[double xPosition]
        {
            get { return terrainWave[xPosition]; }
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
        #endregion
    }
}
