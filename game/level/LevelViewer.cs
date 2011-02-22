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

        public LevelViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }

        internal void Update(Level level)
        {
            Rectangle rectangle;
            int previousX = 0;
            int previousRelativeFloorHeight = 0;


            int themeColorId = level.Count - 1;
            bool isFirstWave = true;
            foreach (IWave wave in level)
            {
                Color waveColor = level.colorTheme.GetColor(themeColorId);
                themeColorId--;

                for (int x = 0; x < Program.screenWidth; x += Program.waveResolution)
                {
                    double waveInput = (double)(x) / Program.tileSize + (Program.viewOffsetX * Program.tileSize);
                    double waveOutput = wave[waveInput];

                    waveOutput *= Program.tileSize / 2.0;
                    waveOutput += Program.viewOffsetY * Program.tileSize * 28;

                    int relativeFloorHeight = Program.screenHeight / 2 + (int)waveOutput;

                    if (isFirstWave)
                    {
                        rectangle = new Rectangle(x, 0, Program.waveResolution, relativeFloorHeight);
                        mainSurface.Fill(rectangle, Color.Black);
                    }

                    rectangle = new Rectangle(x, relativeFloorHeight, Program.waveResolution, Program.screenHeight - relativeFloorHeight);
                    mainSurface.Fill(rectangle, waveColor);

                    previousX = x;
                    previousRelativeFloorHeight = relativeFloorHeight;
                }
                isFirstWave = false;
            }

            mainSurface.Update();
        }
    }
}
