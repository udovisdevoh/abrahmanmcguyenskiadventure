using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AbrahmanAdventure.level
{
    internal class Level : IEnumerable<Ground>
    {
        #region Fields and parts
        private List<Ground> groundList;

        private Sky sky;

        public ColorTheme colorTheme;
        #endregion

        #region Constructor
        /// <summary>
        /// Use level generator instead
        /// </summary>
        /// <param name="random">random number generator</param>
        public Level(Random random)
        {
            sky = new Sky(random);
            colorTheme = new ColorTheme(random);
            groundList = new List<Ground>();
        }
        #endregion

        #region IEnumerable<IWave> Members
        public IEnumerator<Ground> GetEnumerator()
        {
            return groundList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return groundList.GetEnumerator();
        }
        #endregion

        #region Internal Methods
        internal void AddTerrainWave(Ground ground)
        {
            groundList.Add(ground);
        }

        internal void BuildNewGround(IWave wave, Random random, Color color)
        {
            groundList.Add(new Ground(wave, random, color));
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return groundList.Count; }
        }

        public Sky Sky
        {
            get { return sky; }
        }

        public Ground this[int index]
        {
            get { return groundList[index]; }
        }
        #endregion
    }
}
