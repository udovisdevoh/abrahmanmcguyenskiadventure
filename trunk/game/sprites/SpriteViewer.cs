using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using System.Drawing;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite viewer
    /// </summary>
    internal class SpriteViewer
    {
        #region Fields and parts
        private Surface mainSurface;

        private SpritePopulation spritePopulation;
        #endregion

        #region Constructor
        public SpriteViewer(SpritePopulation spritePopulation, Surface mainSurface)
        {
            this.spritePopulation = spritePopulation;
            this.mainSurface = mainSurface;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// View sprites at specified offset
        /// </summary>
        /// <param name="viewOffsetX">X offset</param>
        /// <param name="viewOffsetY">Y offset</param>
        internal void Update(double viewOffsetX, double viewOffsetY, HashSet<AbstractSprite> visibleSpriteList)
        {
            foreach (AbstractSprite sprite in visibleSpriteList)
            {
                Surface spriteSurface = sprite.GetCurrentSurface();

                int xBlitPosition = (int)Math.Round(((sprite.XPosition - ((double)spriteSurface.Width / (double)Program.tileSize) / 2.0 - viewOffsetX) * Program.tileSize));
                int yBlitPosition = (int)((sprite.YPosition - viewOffsetY) * (double)Program.tileSize) - spriteSurface.Height;

                mainSurface.Blit(spriteSurface, new Point(xBlitPosition, yBlitPosition));
            }
        }
        #endregion
    }
}
