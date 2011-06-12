using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To cache textures for later use
    /// </summary>
    internal static class TextureCache
    {
        #region Internal Methods
        internal static bool TryGetCachedSurface(int seed, int groundId, bool isTop, int screenWidth, int screenHeight, out Surface surface)
        {
            string fileNameStub = BuildFileNameStub(seed, groundId, isTop, screenWidth, screenHeight);
            if (File.Exists(fileNameStub + ".bmp"))
            {
                surface = new Surface(fileNameStub + ".bmp");
                return true;
            }
            surface = null;
            return false;
        }

        internal static void AddSurfaceToCache(int seed, int groundId, bool isTop, int screenWidth, int screenHeight, Surface surface)
        {
            string fileNameStub = BuildFileNameStub(seed, groundId, isTop, screenWidth, screenHeight);
            if (!File.Exists(fileNameStub + ".bmp"))
            {
                surface.SaveBmp(fileNameStub + ".bmp");
            }
        }
        #endregion

        #region Private Methods
        private static string BuildFileNameStub(int seed, int groundId, bool isTop, int screenWidth, int screenHeight)
        {
            return "./assets/tmp/" + seed + "_" + groundId + "_" + isTop + "_" + screenWidth + "_" + screenHeight;
        }
        #endregion
    }
}
