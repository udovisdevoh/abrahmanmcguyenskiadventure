using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using AbrahmanAdventure.waves;

namespace AbrahmanAdventure.level
{
    internal class LevelViewer
    {
        private WaveViewer waveViewer;

        private Surface mainSurface;

        public LevelViewer(Surface mainSurface)
        {
            waveViewer = new WaveViewer(mainSurface);
            this.mainSurface = mainSurface;
        }

        internal void Update(Level level)
        {
            mainSurface.Fill(Color.Black);
            foreach (IWave wave in level)
            {
                waveViewer.Update(wave);
            }
            mainSurface.Update();
        }
    }
}
