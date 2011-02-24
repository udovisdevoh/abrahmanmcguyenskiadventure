using System;

namespace AbrahmanAdventure.level
{
	/// <summary>
	/// Represent a wave or a wave pack
	/// </summary>
	public interface IWave : IEquatable<IWave>
	{
        /// <summary>
        /// Normalize the wave
        /// </summary>
        /// <returns>Normalized wave</returns>
        void Normalize();

		/// <summary>
        /// Normalize the wave
        /// </summary>
        /// <returns>Normalized wave</returns>
        void Normalize(double maxValue);

        /// <summary>
        /// Normalize the wave
        /// </summary>
        /// <param name="maxValue">max value</param>
        /// <param name="isIncreaseToo">true: can increase normalization factor, not just decrease</param>
        void Normalize(double maxValue, bool isIncreaseToo);
        
        /// <summary>
        /// Get amplitude momentum Y value at X
        /// </summary>
        /// <param name="x">x coordinates</param>
        /// <returns>amplitude momentum Y value at X</returns>
        double this[double x]
        {
            get;
        }
    }
}
