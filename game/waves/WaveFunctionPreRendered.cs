using AbrahmanAdventure.level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.game.waves
{
    /// <summary>
    /// Prerendered math functions (for faster but less accurate trigonometry)
    /// </summary>
    class WaveFunctionPreRendered
    {
        private const int cacheSize = 2048;

        private double[] preRenderedValues;

        public WaveFunctionPreRendered(WaveFunction innerFunction)
        {
            preRenderedValues = new double[cacheSize];

            for (int index = 0; index < cacheSize; index++)
            {
                double x = GetXFromIndex(index);
                preRenderedValues[index] = innerFunction(x);
            }
        }

        public double GetValue(double x)
        {
            int index = GetIndexFromX(x);
            return preRenderedValues[index];
        }

        private int GetIndexFromX(double x)
        {
            while (x > (Math.PI * 2.0))
            {
                x -= (Math.PI * 2.0);
            }
            while (x < 0)
            {
                x += (Math.PI * 2.0);
            }

            double indexDouble = x / (Math.PI * 2.0) * ((double)cacheSize);
            return (int)indexDouble;
        }

        private double GetXFromIndex(int index)
        {
            double indexDouble = (double)index;

            double x = indexDouble / (double)cacheSize;
            x *= Math.PI * 2.0;

            while (x > (Math.PI * 2.0))
            {
                x -= (Math.PI * 2.0);
            }
            while (x < 0)
            {
                x += (Math.PI * 2.0);
            }

            return x;
        }
    }
}
