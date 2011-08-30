using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    abstract class AbstractSprite
    {
        #region Protected Methods
        /// <summary>
        /// Build sprite's surface from file name
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>sprite's surface</returns>
        protected Surface BuildSpriteSurface(string fileName)
        {
            Surface spriteSurface = new Surface(fileName);

            if (Program.screenHeight != 480)
            {
                double zoom = (double)Program.screenHeight / 480.0;
                spriteSurface = spriteSurface.CreateScaledSurface(zoom);
            }

            return spriteSurface;
        }
        #endregion
    }
}
