using System;

namespace AbrahmanAdventure.level
{
	/// <summary>
	/// Represent a wave or a wave pack
	/// </summary>
	public abstract class AbstractWave : IEquatable<AbstractWave>
    {
        #region Fields
        /// <summary>
        /// Tangent normalization multiplicator
        /// </summary>
        private double tangentNormalizationMultiplicator = 1.0;

        /// <summary>
        /// Tangent normalization offset
        /// </summary>
        private double tangentNormalizationOffset = 0.0;
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Normalize the wave
        /// </summary>
        /// <returns>Normalized wave</returns>
        public abstract void Normalize();

		/// <summary>
        /// Normalize the wave
        /// </summary>
        /// <returns>Normalized wave</returns>
        public abstract void Normalize(double maxValue);

        /// <summary>
        /// Normalize the wave
        /// </summary>
        /// <param name="maxValue">max value</param>
        /// <param name="isIncreaseToo">true: can increase normalization factor, not just decrease</param>
        public abstract void Normalize(double maxValue, bool isIncreaseToo);

                /// <summary>
        /// Normalize the wave
        /// </summary>
        /// <param name="maxValue">max value</param>
        /// <param name="isIncreaseToo">true: can increase normalization factor, not just decrease</param>
        /// <param name="incrementation">incrementation</param>
        /// <param name="range">range</param>
        public abstract void Normalize(double maxValue, bool isIncreaseToo, double incrementation, double range);

        /// <summary>
        /// Whether waves are identical
        /// </summary>
        /// <param name="other">other wave</param>
        /// <returns>Whether waves are identical</returns>
        public abstract bool Equals(AbstractWave other);

        /// <summary>
        /// Get amplitude momentum Y value at X
        /// </summary>
        /// <param name="x">x coordinates</param>
        /// <returns>amplitude momentum Y value at X</returns>
        public abstract double this[double x]
        {
            get;
        }

        /// <summary>
        /// Mostly for wave packs
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>Cached value</returns>
        public abstract double GetCachedValue(double x);
        #endregion

        #region Implementation
        /// <summary>
        /// Normalize the tangent value
        /// </summary>
        /// <param name="resolution">resolution</param>
        /// <param name="desiredMinimum">desired minimum value</param>
        /// <param name="desiredMaximum">desired maximum value</param>
        /// <param name="desiredAverage">desired average value</param>
        public void NormalizeTangent(double resolution, double desiredMinimum, double desiredMaximum, double desiredAverage)
        {
            tangentNormalizationMultiplicator = 1.0;
            tangentNormalizationOffset = 0.0;

            double maxY = double.NegativeInfinity;
            double minY = double.PositiveInfinity;

            for (double x = -1024.0; x < 1024.0; x += 1)
            {
                double y = this.GetTangentValue(x, resolution);
                if (y > maxY)
                    maxY = y;

                if (y < minY)
                    minY = y;
            }

            double desiredRange = desiredMaximum - desiredMinimum;
            double currentRange = maxY - minY;

            tangentNormalizationMultiplicator = tangentNormalizationMultiplicator / currentRange * desiredRange;


            double sum = 0.0;
            for (double x = -1024.0; x < 1024.0; x += 1)
                sum += this.GetTangentValue(x, resolution);

            double average = sum / 2048.0;
            
            //tangentNormalizationOffset = desiredMinimum - minY; 

            tangentNormalizationOffset = desiredAverage - average; 
        }

        /// <summary>
        /// Get tangent value (difference between Y value of X and Y value of X - resolution
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="resolution">resolution</param>
        /// <returns>tangent value (difference between Y value of X and Y value of X - resolution</returns>
        public double GetTangentValue(double x, double resolution)
        {
            return (this[x - resolution] - this[x]) * tangentNormalizationMultiplicator + tangentNormalizationOffset;
        }

        internal double GetHighestJumpingStep(double leftBound, double rightBound)
        {
            double highestJumpingStep = 0;
            double walkingResolution = 0.25;
            for (double x = leftBound; x <= rightBound; x += walkingResolution)
            {
                double currentJumpingStep = Math.Abs(this[x] - this[x + walkingResolution]);
                if (currentJumpingStep > highestJumpingStep)
                    highestJumpingStep = currentJumpingStep;
            }

            return highestJumpingStep;
        }
        #endregion
    }
}
