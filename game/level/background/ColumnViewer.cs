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
            ViewLayer(0, mainSurface, columnSet, viewOffsetX, viewOffsetY);
        }
        #endregion

        #region Private Methods
        private void ViewLayer(int layerId, Surface mainSurface, ColumnSet columnSet, double viewOffsetX, double viewOffsetY)
        {
            int spaceBetweenColumns;

            spaceBetweenColumns = Program.screenWidth / columnSet.ColumnCount;

            Surface columnSurface = columnSet.Surface;
            int columnWidth = columnSurface.GetWidth();
            int columnHeight = columnSurface.GetHeight();

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

                mainSurface.Blit(columnSurface, new Point(viewOffsetXInt, viewOffsetYInt));

                bool isOverlapX = viewOffsetXInt + columnWidth > Program.screenWidth;
                bool isOverlapY = viewOffsetYInt > 0;

                if (isOverlapX)
                    mainSurface.Blit(columnSurface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt));

                if (isOverlapY)
                {
                    mainSurface.Blit(columnSurface, new Point(viewOffsetXInt, viewOffsetYInt - columnHeight));
                    if (isOverlapX)
                        mainSurface.Blit(columnSurface, new Point(viewOffsetXInt - Program.screenWidth, viewOffsetYInt - columnHeight));
                }
            }
        }
        #endregion
    }
}
