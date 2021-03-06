﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Level viewer for which the cached info is stored in square-like segments
    /// </summary>
    internal class LevelViewerSquareBased
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

        private ColumnViewer columnViewer = new ColumnViewer();

        private HillViewer hillViewer = new HillViewer();

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
        /// <param name="background">background</param>
        /// <param name="viewOffsetX">view offset x</param>
        /// <param name="viewOffsetY">view offset y</param>
        /// <param name="waterInfo">waterInfo</param>
        /// <param name="columnSet">column (1st parallax)</param>
        public void Update(Level level, ColorTheme colorTheme, AbstractBackground background, ColumnSet columnSet, HillSet hillSet, WaterInfo waterInfo, double viewOffsetX, double viewOffsetY)
        {
            viewBackground(mainSurface, background, viewOffsetX, viewOffsetY);

            if (hillSet != null)
                hillViewer.ViewHillSet(mainSurface, hillSet, viewOffsetX, viewOffsetY);

            if (columnSet != null)
                columnViewer.ViewColumnSet(mainSurface, columnSet, viewOffsetX, viewOffsetY);

            int minTileX = GetMinZoneX(viewOffsetX);
            int maxTileX = GetMaxZoneX(viewOffsetX);
            int minTileY = GetMinZoneY(viewOffsetY);
            int maxTileY = GetMaxZoneY(viewOffsetY);

            /*if (Program.screenHeight > 480)
                maxTileX++;*/

            for (int zoneX = minTileX; zoneX <= maxTileX; zoneX += Program.squareZoneTileWidth)
            {
                for (int zoneY = minTileY; zoneY <= maxTileY; zoneY += Program.squareZoneTileHeight)
                {
                    Surface currentZone;
                    if (!levelViewerCache.TryGetValue(zoneX, zoneY, out currentZone))
                    {
                        currentZone = BuildZoneSurface(level, waterInfo, colorTheme, zoneX, zoneY);
                        levelViewerCache.Add(zoneX, zoneY, currentZone);
                    }

                    Point zonePosition = GetZonePositionOnScreen(zoneX, zoneY, viewOffsetX, viewOffsetY);
                    mainSurface.Blit(currentZone, zonePosition, GetZoneRectangle());
                }
            }

            levelViewerCache.Trim(Program.maxCachedSquareCount);
        }

        /// <summary>
        /// Clear level viewer's cache within provided bounds
        /// </summary>
        /// <param name="leftBound">left bound</param>
        /// <param name="rightBound">right bound</param>
        /// <param name="topBound">top bound</param>
        /// <param name="bottomBound">bottom bound</param>
        public void ClearCacheAtRange(double leftBound, double rightBound, double topBound, double bottomBound)
        {
            int leftBoundInt = (int)(leftBound / (double)Program.squareZoneTileWidth) * Program.squareZoneTileWidth;
            int rightBoundInt = (int)Math.Ceiling(rightBound / (double)Program.squareZoneTileWidth) * Program.squareZoneTileWidth;

            int topBoundInt = (int)(topBound / (double)Program.squareZoneTileHeight) * Program.squareZoneTileHeight;
            int bottomBoundInt = (int)(bottomBound / (double)Program.squareZoneTileHeight) * Program.squareZoneTileHeight;

            levelViewerCache.ClearCacheAtRange(leftBoundInt, rightBoundInt, topBoundInt, bottomBoundInt);
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

        /// <summary>
        /// Build zone surface
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="waterInfo">waterInfo</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="zoneX">zone X</param>
        /// <param name="zoneY">zone Y</param>
        /// <returns>Zone surface</returns>
        private Surface BuildZoneSurface(Level level, WaterInfo waterInfo, ColorTheme colorTheme, int zoneX, int zoneY)
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
                DrawGround(zoneSurface, ground, waveColor, zoneX, zoneY, zoneWidth, zoneHeight, level, waterInfo);
            }

            if (level.Ceiling != null)
                DrawCeiling(zoneSurface, level.Ceiling, colorTheme.GetColor(level.Count - 1), zoneX, zoneY, zoneWidth, zoneHeight, level, waterInfo);

            if (level.Path != null)
                DrawPath(zoneSurface, level.Path, Color.White, zoneX, zoneY, zoneWidth, zoneHeight, level, waterInfo);

            #region Water
            if (!Program.isEnableTransparentWater && waterInfo != null && waterInfo.Height <= zoneY + Program.squareZoneTileHeight)
            {
                short waterHeight = 0;
                bool isDrawLine = false;

                if (waterInfo.Height >= zoneY)
                {
                    waterHeight = (short)((waterInfo.Height - zoneY) * Program.tileSize);
                    isDrawLine = true;
                }

                zoneSurface.Draw(new Box(0, waterHeight, (short)zoneWidth, (short)zoneHeight), waterInfo.Color, false, true);

                if (isDrawLine)
                    zoneSurface.Draw(new Line(0,waterHeight,(short)zoneWidth,waterHeight),waterInfo.EdgeColor,false,true);
            }
            #endregion

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

        /// <summary>
        /// Draw ground on zone's surface
        /// </summary>
        /// <param name="zoneSurface">zone's surface</param>
        /// <param name="ground">ground</param>
        /// <param name="waveColor">wave's color</param>
        /// <param name="zoneX">zone's X coordinates</param>
        /// <param name="zoneY">zone's Y coordinates</param>
        /// <param name="zoneWidth">zone's width</param>
        /// <param name="zoneHeight">zone's height</param>
        /// <param name="level">level</param>
        /// <param name="waterInfo">info about water</param>
        private void DrawGround(Surface zoneSurface, Ground ground, Color waveColor, int zoneX, int zoneY, int zoneWidth, int zoneHeight, Level level, WaterInfo waterInfo)
        {
            for (int x = 0; x < zoneWidth; x++)
            {
                double waveInputX = (double)(zoneX) + (double)x / (double)Program.tileSize;
                double waveOutputY = ground[waveInputX];

                int textureInputX = Math.Abs((zoneX * zoneWidth + x) % ground.TopTexture.Surface.GetWidth());

                if (waveOutputY > (double)(zoneY + Program.squareZoneTileHeight))
                    continue;
                else if (!IGroundHelper.IsHigherThanOtherGroundsInFront(ground, level, waveInputX, zoneY))
                    continue;

                int groundYOnTile;

                groundYOnTile = (int)(waveOutputY * Program.tileSize) - zoneY * Program.tileSize;

                if (x >= 0 && x < zoneWidth) //if it's within zone's range
                {
                    #region Bottom Texture
                    if (IGroundHelper.IsTransparentAt(waveOutputY, waterInfo, ground, level, waveInputX))
                    {
                        zoneSurface.Fill(new Rectangle(x, Math.Max(0, groundYOnTile), 1, zoneHeight), transparentColor);
                    }
                    else
                    {
                        if (Program.isUseBottomTexture && ground.IsUseBottomTexture)
                        {
                            textureInputX = Math.Abs((zoneX * zoneWidth + x) % ground.BottomTexture.Surface.GetWidth());

                            int bottomSurfaceAligment = Math.Min(Program.tileSize, ground.TopTexture.Surface.GetHeight());
                            int bottomSurfacePositionY = (groundYOnTile + ground.TopTexture.Surface.GetHeight()) / bottomSurfaceAligment * bottomSurfaceAligment;

                            int bottomSurfaceHeight = ground.BottomTexture.Surface.GetHeight();

                        DrawBottomTextureAgain:
                            if (bottomSurfacePositionY >= 0 || bottomSurfacePositionY + bottomSurfaceHeight <= zoneHeight || (bottomSurfacePositionY < 0 && bottomSurfaceHeight > zoneHeight))
                                zoneSurface.Blit(ground.BottomTexture.Surface, new Point(x, bottomSurfacePositionY), new Rectangle(textureInputX, 0, 1, bottomSurfaceHeight));

                            if (bottomSurfacePositionY + bottomSurfaceHeight < zoneHeight)
                            {
                                bottomSurfacePositionY += bottomSurfaceHeight;
                                goto DrawBottomTextureAgain;
                            }
                        }
                        else
                        {
                            zoneSurface.Fill(new Rectangle(x, Math.Max(0, groundYOnTile), 1, zoneHeight), waveColor);
                        }
                    }
                    #endregion

                    #region Top texture
                    if (Program.isUseTopTextureThicknessScaling && ground.IsUseTopTextureThicknessScaling)
                    {
                        double scaling = (Program.isUseWaveValueCache) ? ground.TopTexture.HorizontalThicknessWave.GetCachedValue(textureInputX) + 2.0 : ground.TopTexture.HorizontalThicknessWave[textureInputX] + 2.0;
                        Surface scaledSurface = ground.TopTexture.GetCachedScaledSurface(scaling);
                        if (scaledSurface == null)
                        {
                            scaledSurface = ground.TopTexture.Surface.CreateScaledSurface(1.0, scaling);
                            ground.TopTexture.SetCachedScaledSurface(scaledSurface, scaling);
                        }
                        if (groundYOnTile >= 0 || groundYOnTile + scaledSurface.GetHeight() <= zoneHeight)
                            zoneSurface.Blit(scaledSurface, new Point(x, groundYOnTile), new Rectangle(textureInputX, 0, 1, scaledSurface.GetHeight()));
                    }
                    else
                    {
                        if (groundYOnTile >= 0 || groundYOnTile + ground.TopTexture.Surface.GetHeight() <= zoneHeight)
                            zoneSurface.Blit(ground.TopTexture.Surface, new Point(x, groundYOnTile), new Rectangle(textureInputX, 0, 1, ground.TopTexture.Surface.GetHeight()));
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Draw ground on zone's surface
        /// </summary>
        /// <param name="zoneSurface">zone's surface</param>
        /// <param name="ground">ground</param>
        /// <param name="waveColor">wave's color</param>
        /// <param name="zoneX">zone's X coordinates</param>
        /// <param name="zoneY">zone's Y coordinates</param>
        /// <param name="zoneWidth">zone's width</param>
        /// <param name="zoneHeight">zone's height</param>
        /// <param name="level">level</param>
        /// <param name="waterInfo">info about water</param>
        private void DrawPath(Surface zoneSurface, Ground ground, Color waveColor, int zoneX, int zoneY, int zoneWidth, int zoneHeight, Level level, WaterInfo waterInfo)
        {
            for (int x = 0; x < zoneWidth; x++)
            {
                double waveInputX = (double)(zoneX) + (double)x / (double)Program.tileSize;
                double waveOutputY = ground[waveInputX];


                if (waveOutputY > (double)(zoneY + Program.squareZoneTileHeight))
                    continue;

                int groundYOnTile = (int)(waveOutputY * Program.tileSize) - zoneY * Program.tileSize;

                if (groundYOnTile < 0)
                    continue;

                zoneSurface.Draw(new Point(x,groundYOnTile), waveColor);
            }
        }

        /// <summary>
        /// Draw ceiling on zone's surface
        /// </summary>
        /// <param name="zoneSurface">zone's surface</param>
        /// <param name="ground">ground</param>
        /// <param name="waveColor">wave's color</param>
        /// <param name="zoneX">zone's X coordinates</param>
        /// <param name="zoneY">zone's Y coordinates</param>
        /// <param name="zoneWidth">zone's width</param>
        /// <param name="zoneHeight">zone's height</param>
        /// <param name="level">level</param>
        /// <param name="waterInfo">info about water</param>
        private void DrawCeiling(Surface zoneSurface, Ground ground, Color waveColor, int zoneX, int zoneY, int zoneWidth, int zoneHeight, Level level, WaterInfo waterInfo)
        {
            for (int x = 0; x < zoneWidth; x++)
            {
                double waveInputX = (double)(zoneX) + (double)x / (double)Program.tileSize;
                double waveOutputY = ground[waveInputX];

                int textureInputX = Math.Abs((zoneX * zoneWidth + x) % ground.TopTexture.Surface.GetWidth());

                if (waveOutputY < (double)zoneY)
                    continue;

                int ceilingYOnTile;

                ceilingYOnTile = (int)(waveOutputY * Program.tileSize) - zoneY * Program.tileSize;

                #region Bottom Texture
                /*if (Program.isUseBottomTexture && ground.IsUseBottomTexture)
                {
                    textureInputX = Math.Abs((zoneX * zoneWidth + x) % ground.BottomTexture.Surface.GetWidth());
                    int bottomSurfaceAligment = Math.Min(Program.tileSize, ground.TopTexture.Surface.GetHeight());

                    int bottomSurfacePositionY = (ceilingYOnTile) / bottomSurfaceAligment * bottomSurfaceAligment - ground.BottomTexture.Surface.GetHeight();

                    int bottomSurfaceHeight = ground.BottomTexture.Surface.GetHeight();

                DrawBottomTextureAgain:
                    if (bottomSurfacePositionY >= 0 || bottomSurfacePositionY + bottomSurfaceHeight <= zoneHeight || (bottomSurfacePositionY < 0 && bottomSurfaceHeight > zoneHeight))
                        zoneSurface.Blit(ground.BottomTexture.Surface, new Point(x, bottomSurfacePositionY), new Rectangle(textureInputX, 0, 1, bottomSurfaceHeight));

                    if (bottomSurfacePositionY > 0)
                    {
                        bottomSurfacePositionY -= bottomSurfaceHeight;
                        goto DrawBottomTextureAgain;
                    }
                }
                else
                {*/
                    zoneSurface.Fill(new Rectangle(x, 0, 1, Math.Min(zoneHeight, ceilingYOnTile)), waveColor);
                //}
                #endregion

                #region Top texture
                if (Program.isUseTopTextureThicknessScaling && ground.IsUseTopTextureThicknessScaling)
                {
                    double scaling = (Program.isUseWaveValueCache) ? ground.TopTexture.HorizontalThicknessWave.GetCachedValue(textureInputX) + 2.0 : ground.TopTexture.HorizontalThicknessWave[textureInputX] + 2.0;
                    Surface scaledSurface = ground.TopTexture.GetCachedScaledSurface(scaling);
                    if (scaledSurface == null)
                    {
                        scaledSurface = ground.TopTexture.Surface.CreateScaledSurface(1.0, scaling);
                        ground.TopTexture.SetCachedScaledSurface(scaledSurface, scaling);
                    }
                    if (ceilingYOnTile >= 0 || ceilingYOnTile + scaledSurface.GetHeight() <= zoneHeight)
                        zoneSurface.Blit(scaledSurface, new Point(x, ceilingYOnTile - scaledSurface.GetHeight()), new Rectangle(textureInputX, 0, 1, scaledSurface.GetHeight()));
                }
                else
                {
                    if (ceilingYOnTile >= 0 || ceilingYOnTile + ground.TopTexture.Surface.GetHeight() <= zoneHeight)
                        zoneSurface.Blit(ground.TopTexture.Surface, new Point(x, ceilingYOnTile - ground.TopTexture.Surface.GetHeight()), new Rectangle(textureInputX, 0, 1, ground.TopTexture.Surface.GetHeight()));
                }
                #endregion
            }
        }

        /// <summary>
        /// View background
        /// </summary>
        /// <param name="mainSurface">surface to draw on</param>
        /// <param name="background">background</param>
        /// <param name="viewOffsetX">x offset</param>
        /// <param name="viewOffsetY">y offset</param>
        private void viewBackground(Surface mainSurface, AbstractBackground background, double viewOffsetX, double viewOffsetY)
        {
            if (background is Sky)
                mainSurface.Blit(background.Surface, new Point(0, background.BackgroundHeight / -4), background.Surface.GetRectangle());
            else
            {
                int viewOffsetXInt = (int)(-viewOffsetX * Program.tileSize * 0.15);
                int viewOffsetYInt = (int)(-viewOffsetY * Program.tileSize * 0.15);

                while (viewOffsetXInt > Program.screenWidth)
                    viewOffsetXInt -= Program.screenWidth;
                while (viewOffsetXInt < 0)
                    viewOffsetXInt += Program.screenWidth;

                while (viewOffsetYInt > Program.screenHeight)
                    viewOffsetYInt -= Program.screenHeight;
                while (viewOffsetYInt < 0)
                    viewOffsetYInt += Program.screenHeight;

                /*mainSurface.Blit(background.Surface, new Point(viewOffsetXInt, viewOffsetYInt), background.Surface.GetRectangle());
                mainSurface.Blit(background.Surface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt), background.Surface.GetRectangle());

                mainSurface.Blit(background.Surface, new Point(viewOffsetXInt, viewOffsetYInt - Program.screenHeight), background.Surface.GetRectangle());*/
                mainSurface.Blit(background.Surface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt - Program.screenHeight), background.Surface.GetRectangle());
            }
        }
        #endregion
    }
}
