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
        private float tangentNormalizationMultiplicator = 1.0f;

        /// <summary>
        /// Tangent normalization offset
        /// </summary>
        private float tangentNormalizationOffset = 0.0f;
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
        public abstract void Normalize(float maxValue);

        /// <summary>
        /// Normalize the wave
        /// </summary>
        /// <param name="maxValue">max value</param>
        /// <param name="isIncreaseToo">true: can increase normalization factor, not just decrease</param>
        public abstract void Normalize(float maxValue, bool isIncreaseToo);

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
        public abstract float this[float x]
        {
            get;
        }

        /// <summary>
        /// Mostly for wave packs
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>Cached value</returns>
        public abstract float GetCachedValue(float x);
        #endregion

        #region Implementation
        /// <summary>
        /// Normalize the tangent value
        /// </summary>
        /// <param name="resolution">resolution</param>
        /// <param name="desiredMinimum">desired minimum value</param>
        /// <param name="desiredMaximum">desired maximum value</param>
        /// <param name="desiredAverage">desired average value</param>
        public void NormalizeTangent(float resolution, float desiredMinimum, float desiredMaximum, float desiredAverage)
        {
            tangentNormalizationMultiplicator = 1.0f;
            tangentNormalizationOffset = 0.0f;

            float maxY = float.NegativeInfinity;
            float minY = float.PositiveInfinity;

            for (float x = -1024.0f; x < 1024.0f; x += 1f)
            {
                float y = this.GetTangentValue(x, resolution);
                if (y > maxY)
                    maxY = y;

                if (y < minY)
                    minY = y;
            }

            float desiredRange = desiredMaximum - desiredMinimum;
            float currentRange = maxY - minY;

            tangentNormalizationMultiplicator = tangentNormalizationMultiplicator / currentRange * desiredRange;


            float sum = 0.0f;
            for (float x = -1024.0f; x < 1024.0f; x += 1f)
                sum += this.GetTangentValue(x, resolution);

            float average = sum / 2048.0f;
            
            //tangentNormalizationOffset = desiredMinimum - minY; 

            tangentNormalizationOffset = desiredAverage - average; 
        }

        /// <summary>
        /// Get tangent value (difference between Y value of X and Y value of X - resolution
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="resolution">resolution</param>
        /// <returns>tangent value (difference between Y value of X and Y value of X - resolution</returns>
        public float GetTangentValue(float x, float resolution)
        {
            return (this[x - resolution] - this[x]) * tangentNormalizationMultiplicator + tangentNormalizationOffset;
        }
        #endregion
    }
}
