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

        private bool isBounceBack;

        private bool isFired;

        private bool isBackwards = false;

        private bool isAllowGoBeyond = false;
        #endregion

        #region Constructor
        public Cycle(double totalTimeLength, bool isAutoReset): this(totalTimeLength, isAutoReset, false)
        {
        }

        public Cycle(double totalTimeLength, bool isAutoReset, bool isBounceBack)
            :this(totalTimeLength,isAutoReset, isBounceBack, false)
        {
        }

        public Cycle(double totalTimeLength, bool isAutoReset, bool isBounceBack, bool isAllowGoBeyond)
        {
            this.isBounceBack = isBounceBack;
            this.totalTimeLength = totalTimeLength;
            this.isAutoReset = isAutoReset;
            this.isAllowGoBeyond = isAllowGoBeyond;
            currentValue = 0;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns true if cycle has reseted
        /// </summary>
        /// <param name="incrementTime">incrementation time</param>
        /// <returns>whether cycle has reseted</returns>
        public bool Increment(double incrementTime)
        {
            bool isReseted = false;
            if (isBackwards)
            {
                currentValue -= incrementTime;
                if (currentValue < 0)
                {
                    currentValue = Math.Abs(currentValue);
                    isBackwards = false;
                }
            }
            else
                currentValue += incrementTime;

            if (isAutoReset && totalTimeLength != 0)
            {
                while (!isAllowGoBeyond && currentValue > totalTimeLength)
                {
                    if (isBounceBack)
                    {
                        currentValue = totalTimeLength - (currentValue - totalTimeLength);
                        isBackwards = true;
                        break;
                    }
                    else
                    {
                        currentValue -= totalTimeLength;
                    }
                    isReseted = true;
                }
            }
            else if (currentValue >= totalTimeLength && !isAllowGoBeyond)
                isFired = false;

            return isReseted;
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

        internal void StopAndReset()
        {
            isFired = false;
            currentValue = 0;
        }
        #endregion

        #region Properties
        public double CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
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
            set { isFired = value; }
        }

        public double TotalTimeLength
        {
            get { return totalTimeLength; }
            set { totalTimeLength = value; }
        }

        internal void Reverse()
        {
            isBackwards = !isBackwards;
        }
        #endregion
    }
}
