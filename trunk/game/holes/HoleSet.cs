﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents wave modelization of holes in a level
    /// </summary>
    internal class HoleSet
    {
        #region Fields and parts
        /// <summary>
        /// Odd number for key: start of a hole -> is a hole
        /// Even number for key: end of a hole -> is not a hole
        /// </summary>
        private List<double> holeIntervals;

        /// <summary>
        /// Length of a full cycle of hole patterns
        /// </summary>
        private double cycleLength;
        #endregion

        #region Constructor
        /// <summary>
        /// Build wave modelization of holes for a level
        /// </summary>
        /// <param name="random">random number generator</param>
        public HoleSet(Random random)
        {
            cycleLength = 40.0;
            holeIntervals = new List<double>();
            holeIntervals.Add(0);
            holeIntervals.Add(6.0);
            holeIntervals.Add(10.0);
            holeIntervals.Add(12.0);
            holeIntervals.Add(14.0);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Find key of provided value using binary search. If can't find exact value, takes the immediate lower value
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="collection">collection to look into</param>
        /// <param name="minKeyIncl">minimum key (including itself)</param>
        /// <param name="maxKeyExcl">maximum key (excluding itself)</param>
        /// <returns>key of provided value using binary search. If can't find exact value, takes the immediate lower value</returns>
        private int BinarySearchValueGetKey(double value, List<double> collection, int minKeyIncl, int maxKeyExcl)
        {
            int pivotKey = (minKeyIncl + maxKeyExcl) / 2;

            if (minKeyIncl == pivotKey)
            {
                return pivotKey;
            }
            else if (value < collection[pivotKey])
            {
                return BinarySearchValueGetKey(value, collection, minKeyIncl, pivotKey);
            }
            else
            {
                return BinarySearchValueGetKey(value, collection, pivotKey, maxKeyExcl);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// True: there's a hole, false: there's no hole
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <returns>True: there's a hole, false: there's no hole</returns>
        public bool this[double xPosition, double yPosition]
        {
            get
            {
                return BinarySearchValueGetKey(Math.Abs(xPosition) % cycleLength, holeIntervals, 0, holeIntervals.Count) % 2 == 1;
            }
        }
        #endregion
    }
}
