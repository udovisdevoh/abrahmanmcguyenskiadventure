using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace AbrahmanAdventure
{
    /// <summary>
    /// To get surface's height and width, and remeber it
    /// </summary>
    internal static class SurfaceSizeCache
    {
        #region Fields and parts
        /// <summary>
        /// To remember surface's rectangle
        /// </summary>
        private static Dictionary<Surface, Rectangle> mapTextureToRectangle = new Dictionary<Surface, Rectangle>();

        /// <summary>
        /// To remember surface's height
        /// </summary>
        private static Dictionary<Surface, int> mapTextureToHeight = new Dictionary<Surface, int>();

        /// <summary>
        /// To remember surface's width
        /// </summary>
        private static Dictionary<Surface, int> mapTextureToWidth = new Dictionary<Surface, int>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Get surface's rectangle
        /// </summary>
        /// <param name="surface">surface</param>
        /// <returns>surface's rectangle</returns>
        public static Rectangle GetRectangle(this Surface surface)
        {
            Rectangle rectangle;
            if (!mapTextureToRectangle.TryGetValue(surface, out rectangle))
            {
                rectangle = new Rectangle(0, 0, surface.Width, surface.Height);
                mapTextureToRectangle.Add(surface, rectangle);
            }
            return rectangle;
        }

        /// <summary>
        /// Get surface's height
        /// </summary>
        /// <param name="surface">surface</param>
        /// <returns>height</returns>
        public static int GetHeight(this Surface surface)
        {
            int height;
            if (!mapTextureToHeight.TryGetValue(surface, out height))
            {
                height = surface.Height;
                mapTextureToHeight.Add(surface, height);
            }
            return height;
        }

        /// <summary>
        /// Get surface's width
        /// </summary>
        /// <param name="surface">surface</param>
        /// <returns>width</returns>
        public static int GetWidth(this Surface surface)
        {
            int width;
            if (!mapTextureToWidth.TryGetValue(surface, out width))
            {
                width = surface.Width;
                mapTextureToWidth.Add(surface, width);
            }
            return width;
        }
        #endregion
    }
}
