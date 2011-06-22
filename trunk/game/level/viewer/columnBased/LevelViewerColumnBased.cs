﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To view levels
    /// </summary>
    internal class LevelViewerColumnBased : ILevelViewer
    {
        #region Instance fields and parts
        /// <summary>
        /// Main surface
        /// </summary>
        private Surface mainSurface;

        /// <summary>
        /// Level viewer's cache
        /// </summary>
        private LevelViewerCacheColumnBased levelViewerCache = new LevelViewerCacheColumnBased();
        #endregion

        #region Static fields and parts
        /// <summary>
        /// Color of transparency
        /// </summary>
        private static Color transparentColor = ColorTheme.ColorFromHSV(0,0,0);
        #endregion

        #region Constructors
        /// <summary>
        /// Build level viewer
        /// </summary>
        /// <param name="mainSurface">main surface</param>
        public LevelViewerColumnBased(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update level viewer
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="viewOffsetX">view offset x</param>
        /// <param name="viewOffsetY">view offset y</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="sky">sky</param>
        public void Update(Level level, ColorTheme colorTheme, Sky sky, float viewOffsetX, float viewOffsetY)
        {
            viewOffsetX *= Program.tileSize * -1;
            viewOffsetY *= Program.tileSize;

            int zoneColumnIndex = -((int)(viewOffsetX) / Program.totalZoneWidth);
            float offsetXPerZone = viewOffsetX % (float)Program.totalZoneWidth;

            mainSurface.Blit(sky.Surface, new Point(0,Sky.skyHeight / -4), sky.Surface.GetRectangle());
            
            for (int currentZoneOffset = -Program.terrainColumnBufferLeftCount; currentZoneOffset < Program.terrainColumnBufferRightCount; currentZoneOffset++)
            {
                Surface currentSurface;
                if (!levelViewerCache.TryGetValue(zoneColumnIndex + currentZoneOffset, out currentSurface))
                {
                	int absoluteXOffset = (int)(Math.Round((float)zoneColumnIndex * (float)Program.totalZoneWidth));
                    currentSurface = BuildZoneSurface(level, colorTheme, zoneColumnIndex + currentZoneOffset, absoluteXOffset);
                    levelViewerCache.Add(zoneColumnIndex + currentZoneOffset, currentSurface);
                }

                mainSurface.Blit(currentSurface, new Point((int)offsetXPerZone + Program.totalZoneWidth * currentZoneOffset, - (int)viewOffsetY - Program.totalZoneHeight / 2), currentSurface.GetRectangle());
            }

            levelViewerCache.Trim(Program.maxCachedColumnCount);
        }

        /// <summary>
        /// If level viewer
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="isPlayerWalkingRight">whether player is walking right</param>
        /// <param name="colorTheme">color theme</param>
        public void PreCacheNextZoneIfLevelViewerCacheNotFull(Level level, ColorTheme colorTheme, bool isPlayerWalkingRight)
        {
            if (!levelViewerCache.IsFull)
            {
                int nextZoneIndex = levelViewerCache.GetNextUnrenderedZoneIndex(isPlayerWalkingRight);
                int absoluteXOffset = (int)(Math.Round((float)nextZoneIndex * (float)Program.totalZoneWidth));
                Surface nextZoneSurface = BuildZoneSurface(level, colorTheme, nextZoneIndex, absoluteXOffset);
                levelViewerCache.Add(nextZoneIndex, nextZoneSurface);
            }
        }

        /// <summary>
        /// Clear level viewer cache
        /// </summary>
        public void ClearCache()
        {
            levelViewerCache.Clear();
        }

        /// <summary>
        /// Precache level viewer until cache is full
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="colorTheme">color theme</param>
        public void PreCache(Level level, ColorTheme colorTheme)
        {
            while (!levelViewerCache.IsFull)
            {
                int nextZoneIndex;
                int absoluteXOffset;
                Surface nextZoneSurface;

                nextZoneIndex = levelViewerCache.GetNextUnrenderedZoneIndex(true);
                absoluteXOffset = (int)(Math.Round((float)nextZoneIndex * (float)Program.totalZoneWidth));
                nextZoneSurface = BuildZoneSurface(level, colorTheme, nextZoneIndex, absoluteXOffset);
                levelViewerCache.Add(nextZoneIndex, nextZoneSurface);
            }
        }

        /// <summary>
        /// Clear level viewer's cache within provided bounds
        /// </summary>
        /// <param name="leftBound">left bound</param>
        /// <param name="rightBound">right bound</param>
        /// <param name="topBound">top bound</param>
        /// <param name="bottomBound">bottom bound</param>
        public void ClearCacheAtRange(float leftBound, float rightBound, float topBound, float bottomBound)
        {
            int leftBoundInt = (int)(leftBound / Program.zoneColumnWidthTileCount);
            int rightBoundInt = (int)Math.Ceiling(rightBound / Program.zoneColumnWidthTileCount);
            levelViewerCache.ClearCacheAtRange(leftBoundInt, rightBoundInt);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build zone surface (internally)
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="zoneColumnIndex">zone column index</param>
        /// <param name="absoluteXOffset">absolute x index</param>
        /// <param name="colorTheme">color theme</param>
        /// <returns>zone surface</returns>
        private Surface BuildZoneSurface(Level level, ColorTheme colorTheme, int zoneColumnIndex, int absoluteXOffset)
        {
            Rectangle rectangle;
            Surface zoneSurface = new Surface(Program.totalZoneWidth, Program.totalZoneHeight, Program.bitDepth);
            zoneSurface.Transparent = true;

            int startX = zoneColumnIndex * Program.totalZoneWidth;

            int themeColorId = level.Count - 1;
            foreach (Ground ground in level)
            {
                Color waveColor = colorTheme.GetColor(themeColorId);
                
                themeColorId--;

                for (int x = 0; x < Program.totalZoneWidth; x += Program.waveResolution)
                {
                    float waveInput = (float)(x + startX) / Program.tileSize;


                    int relativeFloorHeight = GetRelativeFloorHeight(ground, x, startX);

                    int textureInputX = absoluteXOffset + x;
                    textureInputX %= ground.TopTexture.Surface.GetWidth();
                    while (textureInputX > ground.TopTexture.Surface.GetWidth())
                        textureInputX -= ground.TopTexture.Surface.GetWidth();
                    while (textureInputX < 0)
                        textureInputX += ground.TopTexture.Surface.GetWidth();

                    if (IGroundHelper.IsTransparentAt(ground, level, waveInput))
                    {
                        rectangle = new Rectangle(x, relativeFloorHeight + ground.TopTexture.Surface.GetHeight(), Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                        zoneSurface.Fill(rectangle, transparentColor);
                    }
                    else
                    {
                        if (Program.isUseBottomTexture && ground.IsUseBottomTexture)
                        {
                            int bottomSurfaceAligment = Math.Min(Program.tileSize, ground.TopTexture.Surface.GetHeight());
                            int bottomSurfacePositionY = (relativeFloorHeight + ground.TopTexture.Surface.GetHeight()) / bottomSurfaceAligment * bottomSurfaceAligment;

                            int desiredBottomSurfaceLowerBound = Program.totalZoneHeight;
                            Ground nextCloserGround = ground.NextCloser;
                            if (nextCloserGround != null)
                                desiredBottomSurfaceLowerBound = GetRelativeFloorHeight(nextCloserGround, x, startX);
                            desiredBottomSurfaceLowerBound = Math.Min(desiredBottomSurfaceLowerBound, Program.totalZoneHeight);


                            do
                            {
                                zoneSurface.Blit(ground.BottomTexture.Surface, new Point(x, bottomSurfacePositionY), new Rectangle(textureInputX, 0, 1, ground.BottomTexture.Surface.GetHeight()));
                                bottomSurfacePositionY += ground.BottomTexture.Surface.GetHeight();
                            } while (bottomSurfacePositionY - ground.BottomTexture.Surface.GetHeight() < Program.totalZoneHeight);
                        }
                        else
                        {
                            rectangle = new Rectangle(x, relativeFloorHeight + ground.TopTexture.Surface.GetHeight(), Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                            zoneSurface.Fill(rectangle, waveColor);
                        }
                    }

                    if (Program.isUseTopTextureThicknessScaling && ground.IsUseTopTextureThicknessScaling)
                    {
                        float scaling = (Program.isUseWaveValueCache) ? ground.TopTexture.HorizontalThicknessWave.GetCachedValue(textureInputX) + 2.0f: ground.TopTexture.HorizontalThicknessWave[textureInputX] + 2.0f;
                        Surface scaledSurface = ground.TopTexture.GetCachedScaledSurface(scaling);
                        if (scaledSurface == null)
                        {
                            scaledSurface = ground.TopTexture.Surface.CreateScaledSurface(1.0, scaling);
                            ground.TopTexture.SetCachedScaledSurface(scaledSurface, scaling);
                        }
                        zoneSurface.Blit(scaledSurface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, scaledSurface.GetHeight()));
                    }
                    else
                    {
                        zoneSurface.Blit(ground.TopTexture.Surface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, ground.TopTexture.Surface.GetHeight()));
                    }
                }
            }

            return zoneSurface;
        }

        /// <summary>
        /// Get relative floor height
        /// </summary>
        /// <param name="ground">ground</param>
        /// <param name="x">x position</param>
        /// <param name="startX">starting x</param>
        /// <returns>relative floor height</returns>
        private int GetRelativeFloorHeight(Ground ground, int x, int startX)
        {
            float waveInput = (float)(x + startX) / Program.tileSize;
            float waveOutput = ground[waveInput];
            return (int)((waveOutput + (float)Program.totalHeightTileCount / 2.0) * (float)Program.tileSize);
        }
        #endregion
    }
}
