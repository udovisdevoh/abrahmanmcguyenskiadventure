using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To view water
    /// </summary>
    internal class WaterViewer
    {
        internal void ViewWater(Surface mainSurface, WaterInfo waterInfo, double viewOffsetY)
        {
            short waterHeight = (short)Math.Round(((double)waterInfo.Height - viewOffsetY) * (double)Program.tileSize);

            if (waterHeight <= Program.screenHeight)
            {
                mainSurface.Draw(new Box(0, waterHeight, (short)Program.screenWidth, (short)(Program.screenHeight)), waterInfo.Color, false, true);
                mainSurface.Draw(new Line(0, waterHeight, (short)Program.screenWidth, waterHeight), waterInfo.EdgeColor, false, true);
            }
        }
    }
}
