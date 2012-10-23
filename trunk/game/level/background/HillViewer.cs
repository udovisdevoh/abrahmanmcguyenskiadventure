using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;

namespace AbrahmanAdventure.level
{
    class HillViewer
    {
        /// <summary>
        /// View hill
        /// </summary>
        /// <param name="mainSurface">surface to draw on</param>
        /// <param name="columnSet">column</param>
        internal void ViewHillSet(Surface mainSurface, HillSet hillSet, double viewOffsetX, double viewOffsetY)
        {
            double movementCoeficient = 0.222;
            int viewOffsetXInt = (int)(-viewOffsetX * Program.tileSize * movementCoeficient);
            int viewOffsetYInt = (int)(-viewOffsetY * Program.tileSize * movementCoeficient);

            while (viewOffsetXInt > Program.screenWidth)
                viewOffsetXInt -= Program.screenWidth;
            while (viewOffsetXInt < 0)
                viewOffsetXInt += Program.screenWidth;

            if (viewOffsetYInt < Program.screenHeight)
            {
                mainSurface.Blit(hillSet.Surface, new Point(viewOffsetXInt, viewOffsetYInt));

                if (viewOffsetXInt != 0)
                    mainSurface.Blit(hillSet.Surface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt));
            }
        }
    }
}
