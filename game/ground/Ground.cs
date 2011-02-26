using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    internal class Ground
    {
        #region Fields and parts
        private IWave terrainWave;

        private Texture topTexture;

        private Texture bottomTexture;
        
        private bool isTransparent;

        private bool isUseBottomTexture;
        #endregion

        #region Constructors
        public Ground(IWave terrainWave, Random random, Color color)
        {
            topTexture = new Texture(random, color, 1.5);

            isUseBottomTexture = random.Next(0, 2) == 0;

            if (Program.isUseBottomTexture && isUseBottomTexture)
                bottomTexture = new Texture(random, color, 16, 0.75);
            this.terrainWave = terrainWave;
            isTransparent = random.Next(0,5) == 0;
        }
        #endregion
        
        #region Public Methods
        public bool IsHigherThanOtherGrounds(Level level, double xInput)
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
        public IWave TerrainWave
        {
            get{return terrainWave;}
        }

        public Texture TopTexture
        {
            get { return topTexture; }
        }

        public Texture BottomTexture
        {
            get { return bottomTexture; }
        }
        
        public bool IsTransparent
        {
        	get{return isTransparent;}
        }

        public bool IsUseBottomTexture
        {
            get { return isUseBottomTexture; }
        }
        #endregion
    }
}
