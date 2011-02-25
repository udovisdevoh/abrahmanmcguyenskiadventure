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
        #endregion

        #region Constructors
        public Ground(IWave terrainWave, Random random, Color color)
        {
            texture = new Texture(random, color);
            this.terrainWave = terrainWave;
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
        #endregion
    }
}
