using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Wall background
    /// </summary>
    internal class Wall : AbstractBackground
    {
        #region Fields and parts
        /// <summary>
        /// Wall's texture
        /// </summary>
        private Texture texture;
        #endregion

        #region Constructor
        /// <summary>
        /// Build new background
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="colorHsl">HSL color</param>
        public Wall(Random random, ColorHsl colorHsl)
        {
            texture = new Texture(random, colorHsl.GetColor(), 1.0, random.Next(), 0, false);

            int textureWidth = texture.Surface.GetWidth();
            int textureHeight = texture.Surface.GetHeight();


            int width = 0;
            int height = 0;
            while (width < Program.screenWidth)
            {
                height = 0;
                while (height < Program.screenHeight)
                {
                    height += textureHeight;
                }
                width += textureWidth;
            }

            
            
            surface = new Surface(width, height);
            width = 0;
            height = 0;
            while (width < Program.screenWidth)
            {
                height = 0;
                while (height < Program.screenHeight)
                {
                    surface.Blit(texture.Surface, new Point(width, height), texture.Surface.GetRectangle());
                    height += textureHeight;
                }
                width += textureWidth;
            }




            double zoomX = (double)Program.screenWidth / (double)width;
            double zoomY = (double)Program.screenHeight / (double)height;

            surface = surface.CreateScaledSurface(zoomX, zoomY, true);
            backgroundWidth = Program.screenWidth;
            backgroundHeight = Program.screenHeight;
        }
        #endregion
    }
}
