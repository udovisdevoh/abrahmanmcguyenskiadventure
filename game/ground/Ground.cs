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

        private IWave xTextureWave;

        private IWave yTextureWave;

        private bool isTextureMultiply;
        #endregion

        #region Constructors
        public Ground(IWave terrainWave)
        {
            this.terrainWave = terrainWave;
        }
        #endregion

        #region Properties
        public IWave TerrainWave
        {
            get{return terrainWave;}
        }
        #endregion
    }
}
