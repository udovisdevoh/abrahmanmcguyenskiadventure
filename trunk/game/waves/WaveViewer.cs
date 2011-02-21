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

        internal void Update(IWave wave)
        {
            mainSurface.Fill(Color.Black);
            Rectangle rectangle;
            double relativeTileSize = Program.tileSize * Program.zoomRatio;
            for (int x = 0; x < Program.screenWidth; x+= Program.waveResolution)
            {
                double waveInput = (double)(x) / relativeTileSize + (Program.viewOffsetX * relativeTileSize);
                double waveOutput = wave[waveInput];
                waveOutput *= relativeTileSize / 2.0;
                waveOutput += Program.viewOffsetY * relativeTileSize * 28;

                rectangle = new Rectangle(x, Program.screenHeight / 2 + (int)waveOutput, 1, (int)relativeTileSize * 4);
                mainSurface.Fill(rectangle, Color.Blue);

                if ((int)(x % relativeTileSize) == 0)
                {
                    rectangle = new Rectangle(x, 0, 1, Program.screenHeight);
                    mainSurface.Fill(rectangle, Color.Gray);
                }
            }
            mainSurface.Update();
        }
    }
}
