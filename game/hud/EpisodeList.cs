using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.textGenerator;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// List of episodes
    /// </summary>
    internal static class EpisodeList
    {
        /// <summary>
        /// Random number generator
        /// </summary>
        private static Random random = new Random(0);

        /// <summary>
        /// Internal dictionary
        /// </summary>
        private static Dictionary<uint, string> internalDictionary = new Dictionary<uint, string>();

        /// <summary>
        /// Get episode name from id
        /// </summary>
        /// <param name="episodeId">episode id</param>
        /// <returns>Episode name</returns>
        public static string GetEpisodeName(uint episodeId)
        {
            while (internalDictionary.Count <= episodeId)
            {
                internalDictionary.Add((uint)internalDictionary.Count, textGenerator.LineGenerator.GetRandomLine(random));
            }
            return internalDictionary[episodeId];
        }
    }
}
