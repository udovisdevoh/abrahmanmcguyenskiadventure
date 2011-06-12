using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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
            if (File.Exists(fileNameStub + ".jpg"))
            {
                surface = new Surface(fileNameStub + ".jpg");
                return true;
            }
            surface = null;
            return false;
        }

        internal static void AddSurfaceToCache(int seed, int groundId, bool isTop, int screenWidth, int screenHeight, Surface surface)
        {
            string fileNameStub = BuildFileNameStub(seed, groundId, isTop, screenWidth, screenHeight);
            if (!File.Exists(fileNameStub + ".bmp") && !File.Exists(fileNameStub + ".jpg"))
            {
                surface.SaveBmp(fileNameStub + ".bmp");
                using (Image image = Image.FromFile(fileNameStub + ".bmp"))
                {
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                    image.Save(fileNameStub + ".jpg", GetEncoder(ImageFormat.Jpeg), encoderParameters);
                }
                File.Delete(fileNameStub + ".bmp");
            }
        }
        #endregion

        #region Private Methods
        private static string BuildFileNameStub(int seed, int groundId, bool isTop, int screenWidth, int screenHeight)
        {
            return "./assets/tmp/" + seed + "_" + groundId + "_" + isTop + "_" + screenWidth + "_" + screenHeight;
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
            return null;
        }
        #endregion
    }
}
