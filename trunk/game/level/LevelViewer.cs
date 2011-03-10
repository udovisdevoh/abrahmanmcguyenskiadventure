using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To view levels
    /// </summary>
    internal class LevelViewer
    {
        #region Instance fields and parts
        /// <summary>
        /// Main surface
        /// </summary>
        private Surface mainSurface;

        /// <summary>
        /// Level viewer's cache
        /// </summary>
        private LevelViewerCache levelViewerCache = new LevelViewerCache();
        #endregion

        #region Static fields and parts
        /// <summary>
        /// Color of transparency
        /// </summary>
        private static Color transparentColor = ColorTheme.ColorFromHSV(0,0,0);
        #endregion

        #region Constructors
        /// <summary>
        /// Build level viewer
        /// </summary>
        /// <param name="mainSurface">main surface</param>
        public LevelViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update level viewer
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="viewOffsetX">view offset x</param>
        /// <param name="viewOffsetY">view offset y</param>
        internal void Update(Level level, double viewOffsetX, double viewOffsetY)
        {
            viewOffsetX *= Program.tileSize * -1;
            viewOffsetY *= Program.tileSize;

            int zoneColumnIndex = -((int)(viewOffsetX) / Program.totalZoneWidth);
            double offsetXPerZone = viewOffsetX % (double)Program.totalZoneWidth;

            mainSurface.Blit(level.Sky.Surface,new Point(0,Sky.skyHeight / -4));
            
            for (int currentZoneOffset = -Program.terrainColumnBufferLeftCount; currentZoneOffset < Program.terrainColumnBufferRightCount; currentZoneOffset++)
            {
                Surface currentSurface;
                if (!levelViewerCache.TryGetValue(zoneColumnIndex + currentZoneOffset, out currentSurface))
                {
                	int absoluteXOffset = (int)(Math.Round((double)zoneColumnIndex * (double)Program.totalZoneWidth));
                	currentSurface = BuildZoneSurface(level, zoneColumnIndex + currentZoneOffset, absoluteXOffset);
                    levelViewerCache.Add(zoneColumnIndex + currentZoneOffset, currentSurface);
                }

                mainSurface.Blit(currentSurface, new Point((int)offsetXPerZone + Program.totalZoneWidth * currentZoneOffset, - (int)viewOffsetY - Program.totalZoneHeight / 2));
            }

            levelViewerCache.Trim(Program.maxCachedColumnCount);
        }

        /// <summary>
        /// Clear level viewer cache
        /// </summary>
        internal void ClearCache()
        {
            levelViewerCache.Clear();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build zone surface (internally)
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="zoneColumnIndex">zone column index</param>
        /// <param name="absoluteXOffset">absolute x index</param>
        /// <returns>zone surface</returns>
        private Surface BuildZoneSurface(Level level, int zoneColumnIndex, int absoluteXOffset)
        {
            Rectangle rectangle;
            Surface zoneSurface = new Surface(Program.totalZoneWidth, Program.totalZoneHeight, Program.bitDepth);
            zoneSurface.Transparent = true;

            int startX = zoneColumnIndex * Program.totalZoneWidth;

            int themeColorId = level.Count - 1;
            foreach (Ground ground in level)
            {
                Color waveColor = level.ColorTheme.GetColor(themeColorId);
                
                themeColorId--;

                for (int x = 0; x < Program.totalZoneWidth; x += Program.waveResolution)
                {
                    double waveInput = (double)(x + startX) / Program.tileSize;


                    int relativeFloorHeight = GetRelativeFloorHeight(ground, x, startX);

                    int textureInputX = absoluteXOffset + x;
                    textureInputX %= ground.TopTexture.Surface.Width;
                    while (textureInputX > ground.TopTexture.Surface.Width)
                        textureInputX -= ground.TopTexture.Surface.Width;
                    while (textureInputX < 0)
                        textureInputX += ground.TopTexture.Surface.Width;

                    if (ground.IsTransparent && GroundHelper.IsHigherThanOtherGrounds(ground, level, waveInput))
                    {
                        rectangle = new Rectangle(x, relativeFloorHeight + ground.TopTexture.Surface.Height, Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                        zoneSurface.Fill(rectangle, transparentColor);
                    }
                    else
                    {
                        if (Program.isUseBottomTexture && ground.IsUseBottomTexture)
                        {
                            int bottomSurfaceAligment = Math.Min(Program.tileSize, ground.TopTexture.Surface.Height);
                            int bottomSurfacePositionY = (relativeFloorHeight + ground.TopTexture.Surface.Height) / bottomSurfaceAligment * bottomSurfaceAligment;

                            int desiredBottomSurfaceLowerBound = Program.totalZoneHeight;
                            Ground nextCloserGround = ground.NextCloser;
                            if (nextCloserGround != null)
                                desiredBottomSurfaceLowerBound = GetRelativeFloorHeight(nextCloserGround, x, startX);

                            do
                            {
                                zoneSurface.Blit(ground.BottomTexture.Surface, new Point(x, bottomSurfacePositionY), new Rectangle(textureInputX, 0, 1, ground.BottomTexture.Surface.Height));
                                bottomSurfacePositionY += ground.BottomTexture.Surface.Height;
                            } while (bottomSurfacePositionY - ground.BottomTexture.Surface.Height < Program.totalZoneHeight);
                        }
                        else
                        {
                            rectangle = new Rectangle(x, relativeFloorHeight + ground.TopTexture.Surface.Height, Program.waveResolution, Program.totalZoneHeight - relativeFloorHeight);
                            zoneSurface.Fill(rectangle, waveColor);
                        }
                    }

                    if (Program.isUseTopTextureThicknessScaling && ground.IsUseTopTextureThicknessScaling)
                    {
                        double scaling = ground.TopTexture.HorizontalThicknessWave[textureInputX] + 2.0;
                        Surface scaledSurface = ground.TopTexture.GetCachedScaledSurface(scaling);
                        if (scaledSurface == null)
                        {
                            scaledSurface = ground.TopTexture.Surface.CreateScaledSurface(1.0, scaling);
                            ground.TopTexture.SetCachedScaledSurface(scaledSurface, scaling);
                        }
                        zoneSurface.Blit(scaledSurface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, scaledSurface.Height));
                    }
                    else
                    {
                        zoneSurface.Blit(ground.TopTexture.Surface, new Point(x, relativeFloorHeight), new Rectangle(textureInputX, 0, 1, ground.TopTexture.Surface.Height));
                    }
                }
            }

            return zoneSurface;
        }

        /// <summary>
        /// Get relative floor height
        /// </summary>
        /// <param name="ground">ground</param>
        /// <param name="x">x position</param>
        /// <param name="startX">starting x</param>
        /// <returns>relative floor height</returns>
        private int GetRelativeFloorHeight(Ground ground, int x, int startX)
        {
            double waveInput = (double)(x + startX) / Program.tileSize;
            double waveOutput = ground[waveInput];
            return (int)((waveOutput + (double)Program.totalHeightTileCount / 2.0) * (double)Program.tileSize);
        }
        #endregion
    }
}
