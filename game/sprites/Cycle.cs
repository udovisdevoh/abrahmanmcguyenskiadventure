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
        #endregion

        #region Constructor
        public Cycle(double totalTimeLength, bool isAutoReset): this(totalTimeLength, isAutoReset, false)
        {
        }

        public Cycle(double totalTimeLength, bool isAutoReset, bool isBounceBack)
        {
            this.isBounceBack = isBounceBack;
            this.totalTimeLength = totalTimeLength;
            this.isAutoReset = isAutoReset;
            currentValue = 0;
        }
        #endregion

        #region Public Methods
        public void Increment(double incrementTime)
        {
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
                while (currentValue > totalTimeLength)
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
                }
            }
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
