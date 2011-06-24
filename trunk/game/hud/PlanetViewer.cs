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
        /// <param name="skillLevel">skill level</param>
        /// <param name="skyColorHsl">sky color (HSL)</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="random">random number generator</param>
        internal static Surface ShowPlanet(string name, int skillLevel, ColorHsl skyColorHsl, ColorTheme colorTheme, Random random)
        {
            Surface planetSurface = new Surface(640, 480, 32, true);

            bool isShowNebulae = random.Next(0, 2) == 1;

            if (isShowNebulae)
                DrawNebulae(planetSurface, random);
            DrawStars(planetSurface, random);
            
            if (!isShowNebulae)
                DrawSun(planetSurface, random);

            Surface planetNameSurface = LargeFont640Res.Render(name + " - (skill level: " + (skillLevel + 1) + ")", System.Drawing.Color.White);
            planetSurface.Blit(planetNameSurface, new System.Drawing.Point(640 / 2 - planetNameSurface.GetWidth() / 2, 480 / 12 * 11));

            System.Drawing.Color waterColor = skyColorHsl.GetColor();

            planetSurface.Draw(new Circle(640 / 2, 480 / 2, 480 / 3), waterColor, true, true);

            Surface shadeSphere = new Surface("./assets/rendered/Sphere.png");
            double scaling = ((double)480 / 1.5) / (double)shadeSphere.Width * 1.01;
            shadeSphere = shadeSphere.CreateScaledSurface(scaling);

            for (int i = 0; i < 4; i++)
                if (random.Next(0, 7) != 0)
                    DrawContinent(planetSurface, colorTheme, random);

            if (random.Next(0, 7) != 0)
                DrawClouds(planetSurface, random);

            planetSurface.Blit(shadeSphere, new System.Drawing.Point(640 / 2 - shadeSphere.Width / 2, 480 / 2 - shadeSphere.Height / 2));

            if (planetSurface.GetWidth() != Program.screenWidth || planetSurface.GetHeight() != Program.screenHeight)
            {
                double zoomX = (double)Program.screenWidth / (double)planetSurface.GetWidth();
                double zoomY = (double)Program.screenHeight / (double)planetSurface.GetHeight();
                planetSurface = planetSurface.CreateScaledSurface(zoomX, zoomY, true);
            }

            return planetSurface;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Draw nebulae
        /// </summary>
        /// <param name="surface">surface to draw on</param>
        /// <param name="random">random number generator</param>
        private static void DrawNebulae(Surface surface, Random random)
        {
            short radius = (short)random.Next(50, 1200);

            System.Drawing.Color coronaColor = new ColorHsl(random.Next(0, 256), random.Next(128, 256), random.Next(0, 32)).GetColor();

            Surface nebulaeSurface = new Surface("./assets/rendered/Nebulae.png");

            double zoom = (double)radius / ((double)nebulaeSurface.Width / 2.0);

            nebulaeSurface = nebulaeSurface.CreateScaledSurface(zoom);

            short positionX = (short)random.Next(-160, 800);
            short positionY = (short)random.Next(-120, 600);

            surface.Draw(new Circle(positionX, positionY, (short)(radius - 1)), coronaColor, true, true);

            surface.Blit(nebulaeSurface, new System.Drawing.Point(positionX - radius, positionY - radius), nebulaeSurface.GetRectangle());
        }

        /// <summary>
        /// Draw planet's sun
        /// </summary>
        /// <param name="surface">surface</param>
        /// <param name="random">random number generator</param>
        private static void DrawSun(Surface surface, Random random)
        {
            short coronaRadius = (short)random.Next(30, 150);
            short coreRadius = (short)(coronaRadius / 2);

            System.Drawing.Color coronaColor = new ColorHsl(random.Next(0, 256), 255, 255).GetColor();


            Surface coronaSurface = new Surface("./assets/rendered/Corona.png");

            double zoom = (double)coronaRadius / ((double)coronaSurface.Width / 2.0);

            coronaSurface = coronaSurface.CreateScaledSurface(zoom);


            short positionX, positionY;
            do
            {
                positionX = (short)random.Next(-160, 800);
                positionY = (short)random.Next(-120, 600);
            } while (positionX > 140 && positionX < 500 && positionY > 140 && positionY < 340);

            surface.Draw(new Circle(positionX, positionY, (short)(coronaRadius - 1)), coronaColor, true, true);

            surface.Blit(coronaSurface, new System.Drawing.Point(positionX - coronaRadius, positionY - coronaRadius), coronaSurface.GetRectangle());

            surface.Draw(new Circle(positionX, positionY, coreRadius), System.Drawing.Color.White, true, true);
        }

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
        private static void DrawContinent(Surface mainSurface, ColorTheme colorTheme, Random random)
        {
            int totalPointCount = 38400;
            int pointX = random.Next(640);
            int pointY = random.Next(480);

            int changeColorRate = random.Next(1, 10);
            if (changeColorRate > 7)
                changeColorRate *= 100;

            int counter = 0;

            int maxRadiusSize = random.Next(1, 7);

            totalPointCount = totalPointCount * 3 / maxRadiusSize;
            totalPointCount *= random.Next(1, 4);
            totalPointCount /= random.Next(1, 4);
            totalPointCount /= 4;

            System.Drawing.Color continentColor = colorTheme.GetColor(random.Next(0, colorTheme.Count));
            for (int pointCounter = 0; pointCounter < totalPointCount; pointCounter++)
            {
                int pointRadius = random.Next(1, maxRadiusSize);

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
                    continentColor = System.Drawing.Color.FromArgb(32, continentColor.R, continentColor.G, continentColor.B);
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

            System.Drawing.Color transparentWhite = System.Drawing.Color.FromArgb(16, random.Next(230, 256), random.Next(230, 256), random.Next(230, 256));

            int maxRadiusSize = random.Next(1, 6);
            totalPointCount = totalPointCount * 3 / maxRadiusSize;

            totalPointCount *= random.Next(1, 4);
            totalPointCount /= random.Next(1, 4);

            int minHorizontalMovement = random.Next(1, 7);
            int minVerticalMovement = random.Next(1, 7);
            int maxHorizontalMovement = random.Next(1, 7);
            int maxVerticalMovement = random.Next(1, 7);

            for (int pointCounter = 0; pointCounter < totalPointCount; pointCounter++)
            {
                pointX += random.Next(-minHorizontalMovement, maxHorizontalMovement + 1);
                pointY += random.Next(-minVerticalMovement, maxVerticalMovement + 1);

                if (pointX < 640 / 2 - 480 / 3)
                    pointX = 640 / 2 + 480 / 3;
                else if (pointX > 640 / 2 + 480 / 3)
                    pointX = 640 / 2 - 480 / 3;

                if (pointY < 480 / 2 - 480 / 3)
                    pointY = 480 / 2 + 480 / 3;
                else if (pointY > 480 / 2 + 480 / 3)
                    pointY = 480 / 2 - 480 / 3;

                int pointRadius = random.Next(1, maxRadiusSize);
                Circle currentPoint = new Circle((short)pointX, (short)pointY, (short)pointRadius);

                if (Math.Sqrt(Math.Pow(Math.Abs(pointX - 640 / 2), 2.0) + Math.Pow(Math.Abs(pointY - 480 / 2), 2.0)) <= 480 / 3)
                {
                    mainSurface.Draw(currentPoint, transparentWhite, true, true);
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
