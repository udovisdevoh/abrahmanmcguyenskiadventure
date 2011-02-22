using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
/*using SdlDotNet.Graphics.Primitives;*/
using AbrahmanAdventure.waves;

namespace AbrahmanAdventure.level
{
    internal class LevelViewer
    {
        private Surface mainSurface;

        private Surface levelSurface;
        
        private Surface scaledSurface;

        public LevelViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }

        internal void Update(Level level)
        {
            if (levelSurface == null)
                levelSurface = BuildLevelSurface(level);

            if (scaledSurface == null)
            {
            	if (Program.zoomRatio == 1.0)
            		scaledSurface = levelSurface;
            	else
            		scaledSurface = levelSurface.CreateScaledSurface(Program.zoomRatio, false);
            }
            
            mainSurface.Blit(scaledSurface, new Point((int)Program.viewOffsetX, (int)Program.viewOffsetY));
            
            mainSurface.Update();
        }
        
        public void ClearScaledSurface()
        {
        	scaledSurface = null;
        }

        private Surface BuildLevelSurface(Level level)
        {
            Rectangle rectangle;
            
            int totalLevelWith = Program.levelWidth * Program.screenWidth;
            int totalLevelHeight = Program.levelHeight * Program.screenHeight;
            Surface levelSurface = new Surface(totalLevelWith, totalLevelHeight, Program.bitDepth);

            int themeColorId = level.Count - 1;
            bool isFirstWave = true;
            foreach (IWave wave in level)
            {
                Color waveColor = level.colorTheme.GetColor(themeColorId);
                themeColorId--;

                for (int x = 0; x < totalLevelWith; x += Program.waveResolution)
                {
                    double waveInput = (double)(x) / Program.tileSize + (Program.viewOffsetX * Program.tileSize);
                    double waveOutput = wave[waveInput];

                    waveOutput *= Program.tileSize / 2.0;
                    waveOutput += Program.viewOffsetY * Program.tileSize * 28;

                    int relativeFloorHeight = totalLevelHeight / 2 + (int)waveOutput;

                    if (isFirstWave)
                    {
                        rectangle = new Rectangle(x, 0, Program.waveResolution, relativeFloorHeight);
                        levelSurface.Fill(rectangle, Color.Black);
                    }

                    rectangle = new Rectangle(x, relativeFloorHeight, Program.waveResolution, totalLevelHeight - relativeFloorHeight);
                    levelSurface.Fill(rectangle, waveColor);
                }
                isFirstWave = false;
            }

            return levelSurface;
        }
    }
}
