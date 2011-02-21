using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Drawing;
using AbrahmanAdventure.waves;

namespace AbrahmanAdventure
{
    class WaveViewer
    {
        private Surface mainSurface;

        public WaveViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }

        internal void Update(IWave wave, Color waveColor)
        {
            Rectangle rectangle;
            double relativeTileSize = Program.tileSize * Program.zoomRatio;
            for (int x = 0; x < Program.screenWidth; x+= Program.waveResolution)
            {
                double waveInput = (double)(x) / relativeTileSize + (Program.viewOffsetX * relativeTileSize);
                double waveOutput = wave[waveInput];
                waveOutput *= relativeTileSize / 2.0;
                waveOutput += Program.viewOffsetY * relativeTileSize * 28;

                int relativeFloorHeight = Program.screenHeight / 2 + (int)waveOutput;
                rectangle = new Rectangle(x, relativeFloorHeight, Program.waveResolution, Program.screenHeight - relativeFloorHeight);
                mainSurface.Fill(rectangle, waveColor);

                if (Program.zoomRatio > 0.1)
                    if ((int)(x % relativeTileSize) == 0)
                    {
                        rectangle = new Rectangle(x, 0, 1, Program.screenHeight);
                        mainSurface.Fill(rectangle, Color.Gray);
                    }
            }
        }
    }
}
