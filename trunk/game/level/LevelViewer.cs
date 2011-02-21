using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
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
            
            int themeColorId = level.Count -1;
            foreach (IWave wave in level)
            {
                Color color = level.colorTheme.GetColor(themeColorId);
                waveViewer.Update(wave, color);
                themeColorId--;
            }
            mainSurface.Update();
        }
    }
}
