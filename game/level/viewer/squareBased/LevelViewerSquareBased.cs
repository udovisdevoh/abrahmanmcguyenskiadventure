﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Level viewer for which the cached info is stored in square-like segments
    /// </summary>
    internal class LevelViewerSquareBased : ILevelViewer
    {
        #region Fields and parts
        /// <summary>
        /// Main surface
        /// </summary>
        private Surface mainSurface;

        /// <summary>
        /// Level viewer cache (segments are square shaped)
        /// </summary>
        private LevelViewerCacheSquareBased levelViewerCache;
        #endregion

        #region Constructors
        /// <summary>
        /// Create level viewer
        /// </summary>
        /// <param name="mainSurface">surface to draw on</param>
        public LevelViewerSquareBased(Surface mainSurface)
        {
            levelViewerCache = new LevelViewerCacheSquareBased();
            this.mainSurface = mainSurface;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clear the cache
        /// </summary>
        public void ClearCache()
        {
            levelViewerCache.Clear();
        }

        /// <summary>
        /// View the level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="sky">sky</param>
        /// <param name="viewOffsetX">view offset x</param>
        /// <param name="viewOffsetY">view offset y</param>
        public void Update(Level level, ColorTheme colorTheme, Sky sky, double viewOffsetX, double viewOffsetY)
        {
            mainSurface.Blit(sky.Surface, new Point(0, Sky.skyHeight / -4), sky.Surface.GetRectangle());

            int minZoneX = GetMinZoneX(viewOffsetX);
            int maxZoneX = GetMaxZoneX(viewOffsetX);
            int minZoneY = GetMinZoneY(viewOffsetY);
            int maxZoneY = GetMaxZoneY(viewOffsetY);

            for (int zoneX = minZoneX; zoneX <= maxZoneX; zoneX++)
            {
                for (int zoneY = minZoneY; zoneY <= maxZoneY; zoneY++)
                {
                    Surface currentZone;
                    if (!levelViewerCache.TryGetValue(zoneX, zoneY, out currentZone))
                    {
                        currentZone = BuildZoneSurface(level, colorTheme, zoneX, zoneY);
                        levelViewerCache.Add(zoneX, zoneY, currentZone);
                    }

                    Point zonePosition = GetZonePositionOnScreen(zoneX, zoneY, viewOffsetX, viewOffsetY);
                    mainSurface.Blit(currentZone, zonePosition, GetZoneRectangle());
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Position of a zone on screen
        /// </summary>
        /// <param name="zoneX">zone's X index</param>
        /// <param name="zoneY">zone's Y index</param>
        /// <param name="viewOffsetX">view offset X</param>
        /// <param name="viewOffsetY">view offset Y</param>
        /// <returns>Position of a zone on screen</returns>
        private Point GetZonePositionOnScreen(int zoneX, int zoneY, double viewOffsetX, double viewOffsetY)
        {
            return new Point((int)((zoneX - viewOffsetX) * Program.tileSize), (int)((zoneY - viewOffsetY) * Program.tileSize));
        }

        private Surface BuildZoneSurface(Level level, ColorTheme colorTheme, int zoneX, int zoneY)
        {
            Surface zoneSurface = new Surface(Program.tileSize, Program.tileSize, Program.bitDepth);
            zoneSurface.Transparent = true;

            /*int themeColorId = level.Count - 1;
            foreach (Ground ground in level)
            {
                Color waveColor = colorTheme.GetColor(themeColorId);
                themeColorId--;

                for (int x = 0; x < Program.tileSize; x ++)
                {
                    double waveInputX = (double)(x + zoneX) / Program.tileSize;
                    double waveOutputY = ground[waveInputX];

                    if (waveOutputY < (double)zoneY || waveOutputY > (double)zoneY + 1.0)
                        continue;
                }
            }

            return null;*/

            Random random = new Random();

            zoneSurface.Fill(Color.FromArgb(255, random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));

            return zoneSurface;
        }

        /// <summary>
        /// Left-most visible zone index
        /// </summary>
        /// <param name="viewOffsetX">X view offset</param>
        /// <returns>Left-most visible zone index</returns>
        private int GetMinZoneX(double viewOffsetX)
        {
            return (int)viewOffsetX - 1;
        }

        /// <summary>
        /// Last zone X index
        /// </summary>
        /// <param name="viewOffsetX">X view offset</param>
        /// <returns>Last zone X index</returns>
        private int GetMaxZoneX(double viewOffsetX)
        {
            return (int)viewOffsetX + Program.tileColumnCount;
        }

        /// <summary>
        /// First zone Y index
        /// </summary>
        /// <param name="viewOffsetY">Y view offset</param>
        /// <returns>First zone Y index</returns>
        private int GetMinZoneY(double viewOffsetY)
        {
            return (int)viewOffsetY - 1;
        }

        /// <summary>
        /// Last zone Y index
        /// </summary>
        /// <param name="viewOffsetY">Y view offset</param>
        /// <returns>Last zone Y index</returns>
        private int GetMaxZoneY(double viewOffsetY)
        {
            return (int)viewOffsetY + Program.tileRowCount;
        }

        /// <summary>
        /// Rectangle for a zone (all zones)
        /// </summary>
        /// <returns>Rectangle for a zone (all zones)</returns>
        private Rectangle GetZoneRectangle()
        {
            return new Rectangle(0, 0, Program.tileSize, Program.tileSize);
        }
        #endregion
    }
}
