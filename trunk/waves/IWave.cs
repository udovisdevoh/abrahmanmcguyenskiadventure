using System;

namespace AbrahmanAdventure.waves
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
