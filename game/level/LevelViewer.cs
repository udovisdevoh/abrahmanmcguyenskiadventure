﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
//using SdlDotNet.Graphics.Primitives;

namespace AbrahmanAdventure.level
{
    internal class LevelViewer
    {
        #region Fields and parts
        private Surface mainSurface;

        private LevelViewerCache levelViewerCache = new LevelViewerCache();
        #endregion

        public LevelViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }

        internal void Update(Level level)
        {
            int zoneColumnIndex = -((int)(Program.viewOffsetX) / Program.totalZoneWidth);
            double offsetXPerZone = Program.viewOffsetX % (double)Program.totalZoneWidth;

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

                mainSurface.Blit(currentSurface, new Point((int)offsetXPerZone + Program.totalZoneWidth * currentZoneOffset, (int)Program.viewOffsetY));
            }

            levelViewerCache.Trim(Program.maxCachedColumnCount);
            mainSurface.Update();
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
                Color waveColor = level.colorTheme.GetColor(themeColorId);
                themeColorId--;

                for (int x = 0; x < Program.totalZoneWidth; x += Program.waveResolution)
                {
                    double waveInput = (double)(x + startX) / Program.tileSize;
                    double waveOutput = terrainWave[waveInput];

                    waveOutput *= Program.tileSize / 2.0;

                    int relativeFloorHeight = Program.totalZoneHeight / 2 + (int)waveOutput;

                    rectangle = new Rectangle(x, relativeFloorHeight, Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                    zoneSurface.Fill(rectangle, waveColor);

                    #warning There seem to be a problem with offset and texture sampling x coordinates and transparency
                    int textureInputX = absoluteXOffset + x;
                    textureInputX %= ground.Texture.Surface.Width;
                    while (textureInputX > ground.Texture.Surface.Width)
                        textureInputX -= ground.Texture.Surface.Width;
                    while (textureInputX < 0)
                        textureInputX += ground.Texture.Surface.Width;
                    
                    zoneSurface.Blit(ground.Texture.Surface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, ground.Texture.Surface.Height));
                }
            }

            return zoneSurface;
        }
    }
}
