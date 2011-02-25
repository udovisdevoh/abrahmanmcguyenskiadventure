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

        private Texture texture;
        
        private bool isTransparent;
        #endregion

        #region Constructors
        public Ground(IWave terrainWave, Random random, Color color)
        {
            texture = new Texture(random, color);
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

        public Texture Texture
        {
            get { return texture; }
        }
        
        public bool IsTransparent
        {
        	get{return isTransparent;}
        }
        #endregion
    }
}
