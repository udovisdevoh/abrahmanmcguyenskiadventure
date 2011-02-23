using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
/*using SdlDotNet.Graphics.Primitives;*/

namespace AbrahmanAdventure.level
{
    internal class LevelViewer
    {
        #region Fields and parts
        private Surface mainSurface;

        private Dictionary<int, Surface> zoneSurfaceCache = new Dictionary<int, Surface>();
        #endregion

        public LevelViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }

        internal void Update(Level level)
        {
            int zoneColumnIndex = -((int)(Program.viewOffsetX) / Program.totalZoneWidth);
            double offsetXPerZone = Program.viewOffsetX % (double)Program.totalZoneWidth;

            for (int currentZoneOffset = -4; currentZoneOffset < 44; currentZoneOffset++)
            {
                Surface currentSurface;
                if (!zoneSurfaceCache.TryGetValue(zoneColumnIndex + currentZoneOffset, out currentSurface))
                {
                    currentSurface = BuildZoneSurface(level, zoneColumnIndex + currentZoneOffset);
                    zoneSurfaceCache.Add(zoneColumnIndex + currentZoneOffset, currentSurface);
                }

                mainSurface.Blit(currentSurface, new Point((int)offsetXPerZone + Program.totalZoneWidth * currentZoneOffset, (int)Program.viewOffsetY));
            }
            
            mainSurface.Update();
        }

        private Surface BuildZoneSurface(Level level, int zoneColumnIndex)
        {
            Rectangle rectangle;
            Surface zoneSurface = new Surface(Program.totalZoneWidth, Program.totalZoneHeight, Program.bitDepth);

            int startX = zoneColumnIndex * Program.totalZoneWidth;

            int themeColorId = level.Count - 1;
            bool isFirstWave = true;
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

                    if (isFirstWave)
                    {
                        rectangle = new Rectangle(x, 0, Program.waveResolution, relativeFloorHeight);
                        zoneSurface.Fill(rectangle, Color.Black);
                    }

                    rectangle = new Rectangle(x, relativeFloorHeight, Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                    zoneSurface.Fill(rectangle, waveColor);
                }
                isFirstWave = false;
            }

            return zoneSurface;
        }
    }
}
