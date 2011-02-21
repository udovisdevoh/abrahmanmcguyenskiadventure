using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.waves;
using AbrahmanAdventure.colorTheme;

namespace AbrahmanAdventure.level
{
    internal class Level : IEnumerable<IWave>
    {
        #region Fields and parts
        private List<IWave> terrainWaveList;

        private ColorTheme colorTheme;
        #endregion

        #region Constructor
        public Level(Random random) : this(random, null) { }

        /// <summary>
        /// Warning! Use
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="firstTerrainWave"></param>
        public Level(Random random, IWave firstTerrainWave)
        {
            colorTheme = new ColorTheme(random);
            terrainWaveList = new List<IWave>();
            if (firstTerrainWave != null)
                this.terrainWaveList.Add(firstTerrainWave);
        }
        #endregion

        #region IEnumerable<IWave> Members
        public IEnumerator<IWave> GetEnumerator()
        {
            return terrainWaveList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return terrainWaveList.GetEnumerator();
        }
        #endregion

        #region Internal Methods
        internal void AddTerrainWave(IWave wave)
        {
            terrainWaveList.Add(wave);
        }
        #endregion
    }
}
