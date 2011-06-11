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
        #endregion

        #region Constructor
        public SpriteViewer(Surface mainSurface)
        {
            this.mainSurface = mainSurface;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// View sprites at specified offset
        /// </summary>
        /// <param name="viewOffsetX">X offset</param>
        /// <param name="viewOffsetY">Y offset</param>
        /// <param name="isOddFrame">whether frame is odd</param>
        /// <param name="playerSprite">player's sprite</param>
        /// <param name="visibleSpriteList">list of currently visible sprites</param>
        internal void Update(double viewOffsetX, double viewOffsetY, AbstractSprite playerSprite, HashSet<AbstractSprite> visibleSpriteList, bool isOddFrame)
        {
            foreach (AbstractSprite sprite in visibleSpriteList)
                if (sprite != playerSprite)
                    ShowSprite(sprite, viewOffsetX, viewOffsetY, isOddFrame);

            ShowSprite(playerSprite, viewOffsetX, viewOffsetY, isOddFrame);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Show sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="viewOffsetX">view offset X</param>
        /// <param name="viewOffsetY">view offset Y</param>
        /// <param name="isOddFrame">whether frame is odd</param>
        private void ShowSprite(AbstractSprite sprite, double viewOffsetX, double viewOffsetY, bool isOddFrame)
        {
            if (isOddFrame && (sprite.HitCycle.IsFired || sprite is PlayerSprite && ((PlayerSprite)sprite).FromVortexCycle.IsFired))
                return;

            double specialOffsetX, specialOffsetY;

            Surface spriteSurface = sprite.GetCurrentSurface(out specialOffsetX, out specialOffsetY);

            int xBlitPosition = (int)Math.Round(((sprite.XPosition - ((double)spriteSurface.GetWidth() / (double)Program.tileSize) / 2.0 - viewOffsetX + specialOffsetX) * Program.tileSize));
            int yBlitPosition = (int)((sprite.YPosition - viewOffsetY + specialOffsetY) * (double)Program.tileSize) - spriteSurface.GetHeight();

            mainSurface.Blit(spriteSurface, new Point(xBlitPosition, yBlitPosition),spriteSurface.GetRectangle());
        }
        #endregion
    }
}
