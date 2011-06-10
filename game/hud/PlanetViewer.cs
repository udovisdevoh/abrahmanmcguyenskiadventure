using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
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
            Random random = new Random(); //This random number generator is independent of the main seed

            Surface planetNameSurface = LargeFont.Render(name, System.Drawing.Color.White);
            mainSurface.Blit(planetNameSurface, new System.Drawing.Point(Program.screenWidth / 2 - planetNameSurface.Width / 2, Program.screenHeight / 12 * 11));

            Circle circle = new Circle(Program.screenWidth / 2, Program.screenHeight / 2, Program.screenHeight / 3);
            System.Drawing.Color waterColor = skyColorHsl.GetColor();

            mainSurface.Draw(circle, waterColor, true, true);

            int totalPointCount = (Program.screenWidth * Program.screenHeight) / 8;
            int pointX = random.Next(Program.screenWidth);
            int pointY = random.Next(Program.screenHeight);
            for (int pointCounter = 0; pointCounter < totalPointCount; pointCounter++)
            {
                int pointRadius = random.Next(1, Program.screenWidth / 200);

                pointX += random.Next(-1, 2) * pointRadius;
                pointY += random.Next(-1, 2) * pointRadius;

                if (pointX < Program.screenWidth / 2 - Program.screenHeight / 3)
                    pointX = Program.screenWidth / 2 - Program.screenHeight / 3;
                else if (pointX > Program.screenWidth / 2 + Program.screenHeight / 3)
                    pointX = Program.screenWidth / 2 + Program.screenHeight / 3;

                if (pointY < Program.screenHeight / 2 - Program.screenHeight / 3)
                    pointY = Program.screenHeight / 2 - Program.screenHeight / 3;
                else if (pointY > Program.screenHeight / 2 + Program.screenHeight / 3)
                    pointY = Program.screenHeight / 2 + Program.screenHeight / 3;

                Circle currentPoint = new Circle((short)pointX, (short)pointY, (short)pointRadius);

                if (Math.Sqrt(Math.Pow(Math.Abs(pointX - Program.screenWidth / 2), 2.0) + Math.Pow(Math.Abs(pointY - Program.screenHeight / 2), 2.0)) <= Program.screenHeight / 3)
                {
                    mainSurface.Draw(currentPoint, colorTheme.GetColor(random.Next(0, colorTheme.Count)),true,true);
                }
            }

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
