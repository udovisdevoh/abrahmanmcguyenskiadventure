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
            for (int x = 0; x < Program.screenWidth; x+= Program.waveResolution)
            {
                double waveInput = (double)(x) / Program.tileSize + (Program.viewOffsetX * Program.tileSize);
                double waveOutput = wave[waveInput];
                waveOutput *= (double)(Program.tileSize / 2);

                Rectangle rectangle = new Rectangle(x, Program.screenHeight / 2 + (int)waveOutput, 1, Program.screenHeight);
                mainSurface.Fill(rectangle, Color.Blue);
            }
        }
    }
}
