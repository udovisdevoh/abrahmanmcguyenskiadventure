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

        private bool isAutoReset;

        private bool isFired;
        #endregion

        #region Constructor
        public Cycle(double totalTimeLength, bool isAutoReset)
        {
            this.totalTimeLength = totalTimeLength;
            this.isAutoReset = isAutoReset;
            currentValue = 0;
        }
        #endregion

        #region Public Methods
        public void Increment(double incrementTime)
        {
            currentValue += incrementTime;
            if (isAutoReset)
                while (currentValue > totalTimeLength)
                    currentValue -= totalTimeLength;
            else if (currentValue >= totalTimeLength)
                isFired = false;
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

        internal void Fire()
        {
            isFired = true;
            currentValue = 0;
        }
        #endregion

        #region Properties
        public double CurrentValue
        {
            get { return currentValue; }
        }

        public bool IsFinished
        {
            get { return currentValue >= totalTimeLength; }
        }

        public bool IsReadyToFire
        {
            get
            {
                return currentValue == 0 || currentValue >= totalTimeLength;
            }
        }

        public bool IsFired
        {
            get { return isFired; }
        }

        public double TotalTimeLength
        {
            get { return totalTimeLength; }
        }
        #endregion
    }
}
