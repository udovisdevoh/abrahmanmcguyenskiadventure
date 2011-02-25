﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public Level(Random random) : this(random, null) { }

        /// <summary>
        /// Use level generator instead
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="firstGround"></param>
        public Level(Random random, Ground firstGround)
        {
            sky = new Sky(random);
            colorTheme = new ColorTheme(random);
            groundList = new List<Ground>();
            if (firstGround != null)
                this.groundList.Add(firstGround);
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
        #endregion
    }
}
