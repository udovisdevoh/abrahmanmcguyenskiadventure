using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    internal class Cycle
    {
        #region Fields
        private double totalTimeLength;

        private double currentValue;
        #endregion

        #region Constructor
        public Cycle(double totalTimeLength)
        {
            this.totalTimeLength = totalTimeLength;
            currentValue = 0;
        }
        #endregion

        #region Public Methods
        public void Increment(double incrementTime)
        {
            currentValue += incrementTime;
            while (currentValue > totalTimeLength)
                currentValue -= totalTimeLength;
        }

        public void Reset()
        {
            currentValue = 0;
        }

        public int GetCycleDivision(double divisor)
        {
            double otherDivisor = totalTimeLength / divisor;
            return (int)(currentValue / otherDivisor);
        }
        #endregion

        #region Properties
        public double CurrentValue
        {
            get { return currentValue; }
        }
        #endregion
    }
}
