using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// To view planet
    /// </summary>
    internal static class PlanetViewer
    {
        #region Fields and parts
        /// <summary>
        /// Menu's font
        /// </summary>
        private static Font __largeFont;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Draw a planet and write its name
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="skyColorHsl">sky color (HSL)</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="mainSurface">surface to draw on</param>
        internal static void ShowPlanet(string name, ColorHsl skyColorHsl, ColorTheme colorTheme, Surface mainSurface)
        {
            Surface planetNameSurface = LargeFont.Render(name, System.Drawing.Color.White);
            mainSurface.Blit(planetNameSurface, new System.Drawing.Point(Program.screenWidth / 2 - planetNameSurface.Width / 2, Program.screenHeight / 12 * 11));
            mainSurface.Update();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Menu's font
        /// </summary>
        private static Font LargeFont
        {
            get
            {
                if (__largeFont == null)
                {
                    __largeFont = new Font("./assets/rendered/MenuFont.ttf", 24 * Program.screenWidth / 640);
                }
                return __largeFont;
            }
        }
        #endregion
    }
}
