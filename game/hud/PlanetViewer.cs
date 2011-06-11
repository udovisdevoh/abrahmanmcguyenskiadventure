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
        private static Font __largeFont640Res;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Draw a planet and write its name
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="skyColorHsl">sky color (HSL)</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="random">random number generator</param>
        internal static Surface ShowPlanet(string name, ColorHsl skyColorHsl, ColorTheme colorTheme, Random random)
        {
            Surface planetSurface = new Surface(640, 480, 32, true);

            DrawStars(planetSurface, random);

            Surface planetNameSurface = LargeFont640Res.Render(name, System.Drawing.Color.White);
            planetSurface.Blit(planetNameSurface, new System.Drawing.Point(640 / 2 - planetNameSurface.Width / 2, 480 / 12 * 11));

            System.Drawing.Color waterColor = skyColorHsl.GetColor();

            planetSurface.Draw(new Circle(640 / 2, 480 / 2, 480 / 3), waterColor, true, true);

            Surface shadeSphere = new Surface("./assets/rendered/Sphere.png");
            double scaling = ((double)480 / 1.5) / (double)shadeSphere.Width * 1.01;
            shadeSphere = shadeSphere.CreateScaledSurface(scaling);

            DrawContinents(planetSurface, colorTheme, random);
            DrawClouds(planetSurface, random);

            planetSurface.Blit(shadeSphere, new System.Drawing.Point(640 / 2 - shadeSphere.Width / 2, 480 / 2 - shadeSphere.Height / 2));

            if (planetSurface.Width != Program.screenWidth || planetSurface.Height != Program.screenHeight)
            {
                double zoomX = (double)Program.screenWidth / (double)planetSurface.Width;
                double zoomY = (double)Program.screenHeight / (double)planetSurface.Height;
                planetSurface = planetSurface.CreateScaledSurface(zoomX, zoomY, true);
            }

            return planetSurface;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Draw stars
        /// </summary>
        /// <param name="mainSurface">main surface</param>
        /// <param name="random">random number generator</param>
        private static void DrawStars(Surface surface, Random random)
        {
            int totalStarCount = 640 * 480 / random.Next(100, 500);

            System.Drawing.Point point;
            System.Drawing.Color color;
            for (int i = 0; i < totalStarCount; i++)
            {
                point = new System.Drawing.Point(random.Next(0, 640), random.Next(0, 480));
                int starLightness = random.Next(0, 256);
                color = System.Drawing.Color.FromArgb(starLightness, starLightness, starLightness);
                surface.Draw(point, color);
            }
        }

        /// <summary>
        /// Draw continents
        /// </summary>
        /// <param name="mainSurface">main surface</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="random">random number generator</param>
        private static void DrawContinents(Surface mainSurface, ColorTheme colorTheme, Random random)
        {
            int totalPointCount = 38400;
            int pointX = random.Next(640);
            int pointY = random.Next(480);

            int changeColorRate = random.Next(1, 10);
            if (changeColorRate > 7)
                changeColorRate *= 100;

            int counter = 0;
            System.Drawing.Color continentColor = colorTheme.GetColor(random.Next(0, colorTheme.Count));
            for (int pointCounter = 0; pointCounter < totalPointCount; pointCounter++)
            {
                int pointRadius = random.Next(1, 640 / 200);

                pointX += random.Next(-1, 2) * pointRadius;
                pointY += random.Next(-1, 2) * pointRadius;

                if (pointX < 640 / 2 - 480 / 3)
                    pointX = 640 / 2 + 480 / 3;
                else if (pointX > 640 / 2 + 480 / 3)
                    pointX = 640 / 2 - 480 / 3;

                if (pointY < 480 / 2 - 480 / 3)
                    pointY = 480 / 2 + 480 / 3;
                else if (pointY > 480 / 2 + 480 / 3)
                    pointY = 480 / 2 - 480 / 3;

                if (counter > changeColorRate)
                {
                    continentColor = colorTheme.GetColor(random.Next(0, colorTheme.Count));
                    counter = 0;
                }
                counter++;

                Circle currentPoint = new Circle((short)pointX, (short)pointY, (short)pointRadius);

                if (Math.Sqrt(Math.Pow(Math.Abs(pointX - 640 / 2), 2.0) + Math.Pow(Math.Abs(pointY - 480 / 2), 2.0)) <= 480 / 3)
                {
                    mainSurface.Draw(currentPoint, continentColor, true, true);
                }
            }
        }

        /// <summary>
        /// Draw clouds
        /// </summary>
        /// <param name="mainSurface">main surface</param>
        /// <param name="random">random number generator</param>
        private static void DrawClouds(Surface mainSurface, Random random)
        {
            int totalPointCount = (640 * 480) / 16;
            int pointX = random.Next(640);
            int pointY = random.Next(480);

            System.Drawing.Color transparentWhite = System.Drawing.Color.FromArgb(128, 255, 255, 255);

            for (int pointCounter = 0; pointCounter < totalPointCount; pointCounter++)
            {
                pointX += random.Next(-2, 3);
                pointY += random.Next(-2, 3);

                if (pointX < 640 / 2 - 480 / 3)
                    pointX = 640 / 2 + 480 / 3;
                else if (pointX > 640 / 2 + 480 / 3)
                    pointX = 640 / 2 - 480 / 3;

                if (pointY < 480 / 2 - 480 / 3)
                    pointY = 480 / 2 + 480 / 3;
                else if (pointY > 480 / 2 + 480 / 3)
                    pointY = 480 / 2 - 480 / 3;


                System.Drawing.Point currentPoint = new System.Drawing.Point((short)pointX, (short)pointY);

                if (Math.Sqrt(Math.Pow(Math.Abs(pointX - 640 / 2), 2.0) + Math.Pow(Math.Abs(pointY - 480 / 2), 2.0)) <= 480 / 3)
                {
                    mainSurface.Draw(currentPoint, transparentWhite, true);
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Menu's font
        /// </summary>
        private static Font LargeFont640Res
        {
            get
            {
                if (__largeFont640Res == null)
                {
                    __largeFont640Res = new Font("./assets/rendered/MenuFont.ttf", 24);
                }
                return __largeFont640Res;
            }
        }
        #endregion
    }
}
