using System;
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
        private int cycleCount;

        private List<double> listCycleLength;

        private List<double> listHoleWidth;
        #endregion

        #region Constructor
        /// <summary>
        /// Build wave modelization of holes for a level
        /// </summary>
        /// <param name="random">random number generator</param>
        public HoleSet(Random random)
        {
            cycleCount = 1;
            listCycleLength = new List<double>();
            listHoleWidth = new List<double>();

            for (int i = 0; i < cycleCount; i++)
            {
                listCycleLength.Add(random.NextDouble() * 30.0 + 4.0);
                listHoleWidth.Add(random.NextDouble() * 8.0 + 1.0);
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
                //if ((int)(xPosition) % 10 > 7)

                for (int i = 0; i < cycleCount; i++)
                    if (Math.Abs(xPosition) % listCycleLength[i] <= listCycleLength[i] - listHoleWidth[i])
                        return false;

                return true;

                /*if (Math.Abs(xPosition) % 10 > 10 - 3)
                    return true;
                else
                    return false;*/
            }
        }
        #endregion
    }
}
