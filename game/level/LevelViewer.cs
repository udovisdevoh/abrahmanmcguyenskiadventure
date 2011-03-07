using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.level
{
    internal class LevelViewer
    {
        #region Fields and parts
        private Surface mainSurface;

        private LevelViewerCache levelViewerCache = new LevelViewerCache();
        
        private static Color transparentColor = ColorTheme.ColorFromHSV(0,0,0);
        #endregion

        public LevelViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }

        internal void Update(Level level, double viewOffsetX, double viewOffsetY)
        {
            viewOffsetX *= Program.tileSize * -1;
            viewOffsetY *= Program.tileSize;

            int zoneColumnIndex = -((int)(viewOffsetX) / Program.totalZoneWidth);
            double offsetXPerZone = viewOffsetX % (double)Program.totalZoneWidth;

            mainSurface.Blit(level.Sky.Surface,new Point(0,Sky.skyHeight / -4));
            
            for (int currentZoneOffset = -Program.terrainColumnBufferLeftCount; currentZoneOffset < Program.terrainColumnBufferRightCount; currentZoneOffset++)
            {
                Surface currentSurface;
                if (!levelViewerCache.TryGetValue(zoneColumnIndex + currentZoneOffset, out currentSurface))
                {
                	int absoluteXOffset = (int)(Math.Round((double)zoneColumnIndex * (double)Program.totalZoneWidth));
                	currentSurface = BuildZoneSurface(level, zoneColumnIndex + currentZoneOffset, absoluteXOffset);
                    levelViewerCache.Add(zoneColumnIndex + currentZoneOffset, currentSurface);
                }

                mainSurface.Blit(currentSurface, new Point((int)offsetXPerZone + Program.totalZoneWidth * currentZoneOffset, - (int)viewOffsetY - Program.totalZoneHeight / 2));
            }

            levelViewerCache.Trim(Program.maxCachedColumnCount);
        }

        private Surface BuildZoneSurface(Level level, int zoneColumnIndex, int absoluteXOffset)
        {
            Rectangle rectangle;
            Surface zoneSurface = new Surface(Program.totalZoneWidth, Program.totalZoneHeight, Program.bitDepth);
            zoneSurface.Transparent = true;

            int startX = zoneColumnIndex * Program.totalZoneWidth;

            int themeColorId = level.Count - 1;
            foreach (Ground ground in level)
            {
                IWave terrainWave = ground.TerrainWave;
                Color waveColor = level.ColorTheme.GetColor(themeColorId);
                
                themeColorId--;

                for (int x = 0; x < Program.totalZoneWidth; x += Program.waveResolution)
                {
                    double waveInput = (double)(x + startX) / Program.tileSize;
                    double waveOutput = terrainWave[waveInput];

                    //waveOutput *= Program.tileSize / 2.0;
                    //int relativeFloorHeight = (int)(waveOutput *= Program.tileSize) + Program.totalZoneHeight;

                    //waveOutput = Program.totalHeightTileCount / -2;
                    //int relativeFloorHeight = (int)((waveOutput + (double)Program.totalHeightTileCount / 2.0) * Program.tileSize);

                    int relativeFloorHeight = (int)((waveOutput + (double)Program.totalHeightTileCount / 2.0) * (double)Program.tileSize);

                    int textureInputX = absoluteXOffset + x;
                    textureInputX %= ground.TopTexture.Surface.Width;
                    while (textureInputX > ground.TopTexture.Surface.Width)
                        textureInputX -= ground.TopTexture.Surface.Width;
                    while (textureInputX < 0)
                        textureInputX += ground.TopTexture.Surface.Width;

                    if (ground.IsTransparent && GroundHelper.IsHigherThanOtherGrounds(ground, level, waveInput))
                    {
                        rectangle = new Rectangle(x, relativeFloorHeight + ground.TopTexture.Surface.Height, Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                        zoneSurface.Fill(rectangle, transparentColor);
                    }
                    else
                    {
                        if (Program.isUseBottomTexture && ground.IsUseBottomTexture)
                        {
                            int bottomSurfaceAligment = Math.Min(Program.tileSize, ground.TopTexture.Surface.Height);
                            int bottomSurfacePositionY = (relativeFloorHeight + ground.TopTexture.Surface.Height) / bottomSurfaceAligment * bottomSurfaceAligment;

                            zoneSurface.Blit(ground.BottomTexture.Surface, new Point(x, bottomSurfacePositionY), new Rectangle(textureInputX, 0, 1, ground.BottomTexture.Surface.Height));
                        }
                        else
                        {
                            rectangle = new Rectangle(x, relativeFloorHeight + ground.TopTexture.Surface.Height, Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                            zoneSurface.Fill(rectangle, waveColor);
                        }
                    }

                    if (Program.isUseTopTextureThicknessScaling && ground.IsUseTopTextureThicknessScaling)
                    {
                        double scaling = ground.TopTexture.HorizontalThicknessWave[textureInputX] + 2.0;
                        Surface scaledSurface = ground.TopTexture.GetCachedScaledSurface(scaling);
                        if (scaledSurface == null)
                        {
                            scaledSurface = ground.TopTexture.Surface.CreateScaledSurface(1.0, scaling);
                            ground.TopTexture.SetCachedScaledSurface(scaledSurface, scaling);
                        }
                        zoneSurface.Blit(scaledSurface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, scaledSurface.Height));
                    }
                    else
                    {
                        zoneSurface.Blit(ground.TopTexture.Surface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, ground.TopTexture.Surface.Height));
                    }
                }
            }

            return zoneSurface;
        }
    }
}
