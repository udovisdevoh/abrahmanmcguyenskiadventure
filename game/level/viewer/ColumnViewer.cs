using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;

namespace AbrahmanAdventure.level
{
    public class ColumnViewer
    {
        #region Public Methods
        /// <summary>
        /// View column
        /// </summary>
        /// <param name="mainSurface">surface to draw on</param>
        /// <param name="columnSet">column</param>
        internal void ViewColumnSet(Surface mainSurface, ColumnSet columnSet, double viewOffsetX, double viewOffsetY)
        {
            /*for (int layerId = columnSet.LayerCount - 1; layerId >= 0; layerId--)
            {
                ViewLayer(layerId, mainSurface, columnSet, viewOffsetX, viewOffsetY);
            }*/

            /*ViewLayer(3, mainSurface, columnSet, viewOffsetX, viewOffsetY);
            ViewLayer(2, mainSurface, columnSet, viewOffsetX, viewOffsetY);
            ViewLayer(1, mainSurface, columnSet, viewOffsetX, viewOffsetY);*/

            if (columnSet.IsHorizontal)
                ViewBeamLayer(0, mainSurface, columnSet, viewOffsetX, viewOffsetY);
            else
                ViewColumnLayer(0, mainSurface, columnSet, viewOffsetX, viewOffsetY);
        }
        #endregion

        #region Private Methods
        private void ViewBeamLayer(int layerId, Surface mainSurface, ColumnSet beamSet, double viewOffsetX, double viewOffsetY)
        {
            int spaceBetweenBeams = Program.screenHeight / beamSet.ColumnCount;

            int beamLength = beamSet.Surface.GetWidth();
            int beamThickness = beamSet.Surface.GetHeight();

            double movementCoeficient = 0.333 * Math.Sqrt((double)(layerId + 1));

            for (int beamId = 0; beamId < beamSet.ColumnCount; beamId++)
            {
                int viewOffsetXInt = (int)(-viewOffsetX * Program.tileSize * movementCoeficient);
                int viewOffsetYInt = (int)(-viewOffsetY * Program.tileSize * movementCoeficient);

                viewOffsetYInt += (spaceBetweenBeams * beamId);


                while (viewOffsetYInt > Program.screenHeight)
                    viewOffsetYInt -= Program.screenHeight;
                while (viewOffsetYInt < 0)
                    viewOffsetYInt += Program.screenHeight;


                while (viewOffsetXInt > beamLength)
                    viewOffsetXInt -= beamLength;
                while (viewOffsetXInt < 0)
                    viewOffsetXInt += beamLength;

                viewOffsetXInt -= Program.screenWidth;

                mainSurface.Blit(beamSet.Surface, new Point(viewOffsetXInt, viewOffsetYInt));

                bool isOverlapY = viewOffsetYInt + beamThickness > Program.screenHeight;
                bool isOverlapX = viewOffsetXInt > 0;

                if (isOverlapY)
                    mainSurface.Blit(beamSet.Surface, new Point(viewOffsetXInt, viewOffsetYInt - Program.screenHeight));

                if (isOverlapX)
                {
                    mainSurface.Blit(beamSet.Surface, new Point(viewOffsetXInt - beamLength, viewOffsetYInt));
                    if (isOverlapY)
                        mainSurface.Blit(beamSet.Surface, new Point(viewOffsetXInt - beamLength, viewOffsetYInt - Program.screenHeight));
                }
            }
        }

        private void ViewColumnLayer(int layerId, Surface mainSurface, ColumnSet columnSet, double viewOffsetX, double viewOffsetY)
        {
            int spaceBetweenColumns = Program.screenWidth / columnSet.ColumnCount;

            int columnWidth = columnSet.Surface.GetWidth();
            int columnHeight = columnSet.Surface.GetHeight();

            double movementCoeficient = 0.333 * Math.Sqrt((double)(layerId + 1));

            for (int columnId = 0; columnId < columnSet.ColumnCount; columnId++)
            {
                int viewOffsetXInt = (int)(-viewOffsetX * Program.tileSize * movementCoeficient);
                int viewOffsetYInt = (int)(-viewOffsetY * Program.tileSize * movementCoeficient);


                viewOffsetXInt += (spaceBetweenColumns * columnId);


                while (viewOffsetXInt > Program.screenWidth)
                    viewOffsetXInt -= Program.screenWidth;
                while (viewOffsetXInt < 0)
                    viewOffsetXInt += Program.screenWidth;

                while (viewOffsetYInt > columnHeight)
                    viewOffsetYInt -= columnHeight;
                while (viewOffsetYInt < 0)
                    viewOffsetYInt += columnHeight;

                viewOffsetYInt -= Program.screenHeight;

                mainSurface.Blit(columnSet.Surface, new Point(viewOffsetXInt, viewOffsetYInt));

                bool isOverlapX = viewOffsetXInt + columnWidth > Program.screenWidth;
                bool isOverlapY = viewOffsetYInt > 0;

                if (isOverlapX)
                    mainSurface.Blit(columnSet.Surface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt));

                if (isOverlapY)
                {
                    mainSurface.Blit(columnSet.Surface, new Point(viewOffsetXInt, viewOffsetYInt - columnHeight));
                    if (isOverlapX)
                        mainSurface.Blit(columnSet.Surface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt - columnHeight));
                }
            }
        }
        #endregion
    }
}
