using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    internal class Cycle
    {
        #region Fields
        private float totalTimeLength;

        private float currentValue;

        private bool isAutoReset;

        private bool isFired;
        #endregion

        #region Constructor
        public Cycle(float totalTimeLength, bool isAutoReset)
        {
            this.totalTimeLength = totalTimeLength;
            this.isAutoReset = isAutoReset;
            currentValue = 0f;
        }
        #endregion

        #region Public Methods
        public void Increment(float incrementTime)
        {
            currentValue += incrementTime;
            if (isAutoReset && totalTimeLength != 0)
                while (currentValue > totalTimeLength)
                    currentValue -= totalTimeLength;
            else if (currentValue >= totalTimeLength)
                isFired = false;
        }

        public void Reset()
        {
            currentValue = 0f;
        }

        public int GetCycleDivision(float divisor)
        {
            float otherDivisor = totalTimeLength / divisor;
            return (int)(currentValue / otherDivisor);
        }

        internal void Fire()
        {
            isFired = true;
            currentValue = 0f;
        }

        internal void StopAndReset()
        {
            isFired = false;
            currentValue = 0f;
        }
        #endregion

        #region Properties
        public float CurrentValue
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
                return currentValue == 0f || currentValue >= totalTimeLength;
            }
        }

        public bool IsFired
        {
            get { return isFired; }
        }

        public float TotalTimeLength
        {
            get { return totalTimeLength; }
            set { totalTimeLength = value; }
        }
        #endregion
    }
}
