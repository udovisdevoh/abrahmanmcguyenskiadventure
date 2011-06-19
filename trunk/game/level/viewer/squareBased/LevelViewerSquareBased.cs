using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using AbrahmanAdventure.physics;

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

        private Random random = new Random();

        private Color transparentColor = ColorTheme.ColorFromHSV(0, 0, 0);
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

            int minTileX = GetMinZoneX(viewOffsetX);
            int maxTileX = GetMaxZoneX(viewOffsetX);
            int minTileY = GetMinZoneY(viewOffsetY);
            int maxTileY = GetMaxZoneY(viewOffsetY);

            for (int zoneX = minTileX; zoneX <= maxTileX; zoneX += Program.squareZoneTileWidth)
            {
                for (int zoneY = minTileY; zoneY <= maxTileY; zoneY += Program.squareZoneTileHeight)
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

            levelViewerCache.Trim(Program.maxCachedSquareCount);
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
            int zoneWidth = Program.tileSize * Program.squareZoneTileWidth;
            int zoneHeight = Program.tileSize * Program.squareZoneTileHeight;

            Surface zoneSurface = new Surface(zoneWidth, zoneHeight, Program.bitDepth);
            zoneSurface.Transparent = true;

            int themeColorId = level.Count - 1;
            foreach (Ground ground in level)
            {
                Color waveColor = colorTheme.GetColor(themeColorId);
                themeColorId--;

                for (int x = 0; x < zoneWidth; x++)
                {
                    double waveInputX = (double)(zoneX) + (double)x / (double)Program.tileSize;
                    double waveOutputY = ground[waveInputX];

                    int textureInputX = zoneX + x;

                    textureInputX = Math.Abs(textureInputX) % ground.TopTexture.Surface.GetWidth();

                    /*while (textureInputX > ground.TopTexture.Surface.GetWidth())
                        textureInputX -= ground.TopTexture.Surface.GetWidth();
                    while (textureInputX < 0)
                        textureInputX += ground.TopTexture.Surface.GetWidth();*/

                    if (waveOutputY > (double)zoneY + Program.squareZoneTileHeight)
                        continue;
                    else if (!IGroundHelper.IsHigherThanOtherGroundsInFront(ground, level, waveInputX))
                        continue;

                    int groundYOnTile;

                    groundYOnTile = (int)(waveOutputY * Program.tileSize) - zoneY * Program.tileSize;

                    if (IGroundHelper.IsTransparentAt(ground, level, waveInputX))
                    {
                        zoneSurface.Fill(new Rectangle(x, Math.Max(0, groundYOnTile), 1, zoneHeight), transparentColor);
                    }
                    else
                    {
                        if (Program.isUseBottomTexture && ground.IsUseBottomTexture)
                        {
                            int bottomSurfaceAligment = Math.Min(Program.tileSize, ground.TopTexture.Surface.GetHeight());
                            int bottomSurfacePositionY = (groundYOnTile + ground.TopTexture.Surface.GetHeight()) / bottomSurfaceAligment * bottomSurfaceAligment;

                            zoneSurface.Blit(ground.BottomTexture.Surface, new Point(x, bottomSurfacePositionY), new Rectangle(textureInputX, 0, 1, ground.BottomTexture.Surface.GetHeight()));
                        }
                        else
                        {
                            zoneSurface.Fill(new Rectangle(x, Math.Max(0, groundYOnTile), 1, zoneHeight), waveColor);
                        }
                    }

                    if (groundYOnTile >= 0 || groundYOnTile + ground.TopTexture.Surface.GetHeight() <= zoneHeight)
                        zoneSurface.Blit(ground.TopTexture.Surface, new Point(x, groundYOnTile), new Rectangle(textureInputX, 0, 1, ground.TopTexture.Surface.GetHeight()));
                }
            }

            

            //zoneSurface.Fill(Color.FromArgb(255, random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));

            return zoneSurface;
        }

        /// <summary>
        /// Left-most visible zone index
        /// </summary>
        /// <param name="viewOffsetX">X view offset</param>
        /// <returns>Left-most visible zone index</returns>
        private int GetMinZoneX(double viewOffsetX)
        {
            return (int)(viewOffsetX / (double)Program.squareZoneTileWidth) * Program.squareZoneTileWidth - Program.squareZoneTileWidth;
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
            return (int)(viewOffsetY / (double)Program.squareZoneTileHeight) * Program.squareZoneTileHeight - Program.squareZoneTileHeight;
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
            return new Rectangle(0, 0, Program.tileSize * Program.squareZoneTileWidth, Program.tileSize * Program.squareZoneTileHeight);
        }
        #endregion
    }
}
