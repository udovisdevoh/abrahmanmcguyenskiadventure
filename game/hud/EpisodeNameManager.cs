using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AbrahmanAdventure.textGenerator;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// List of episodes
    /// </summary>
    internal static class EpisodeNameManager
    {
        /// <summary>
        /// Get episode name from id
        /// </summary>
        /// <param name="episodeId">episode id</param>
        /// <returns>Episode name</returns>
        public static string GetEpisodeName(int episodeId)
        {
            Random random = new Random(episodeId);
            return textGenerator.LineGenerator.GetRandomLine(random);
        }

        public static Color GetEpisodeColor(int episodeId)
        {
            Random random = new Random(episodeId);
            ColorHsl colorHsl = new ColorHsl(random.Next(0, 256), random.Next(0, 64), random.Next(128, 256));
            return colorHsl.GetColor();
        }
    }
}
