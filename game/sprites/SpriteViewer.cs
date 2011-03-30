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

        private HashSet<AbstractSprite> spriteListToView;
        #endregion

        #region Constructor
        public SpriteViewer(SpritePopulation spritePopulation, Surface mainSurface)
        {
            this.spritePopulation = spritePopulation;
            spriteListToView = new HashSet<AbstractSprite>();
            this.mainSurface = mainSurface;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// View sprite at specified offset
        /// </summary>
        /// <param name="viewOffsetX">X offset</param>
        /// <param name="viewOffsetY">Y offset</param>
        internal void Update(double viewOffsetX, double viewOffsetY)
        {
            int leftMostViewableBucketId = ((int)Math.Floor(viewOffsetX)) / Program.spatialHashingBucketWidth;
            int rightMostViewableBucketId = ((int)Math.Ceiling(viewOffsetX + Program.tileColumnCount)) / Program.spatialHashingBucketWidth;

            spriteListToView.Clear();

            for (int bucketId = leftMostViewableBucketId; bucketId <= rightMostViewableBucketId; bucketId++)
            {
                Bucket bucket = spritePopulation[bucketId];
                foreach (AbstractSprite sprite in bucket)
                {
                    spriteListToView.Add(sprite);
                }
            }

            foreach (AbstractSprite sprite in spriteListToView)
            {
                if (sprite is ISurfaceSprite)
                {
                    Surface spriteSurface = ((ISurfaceSprite)sprite).GetCurrentSurface();

                    int xBlitPosition = (int)Math.Round(((sprite.XPosition - ((double)spriteSurface.Width / (double)Program.tileSize) / 2.0 - viewOffsetX) * Program.tileSize));
                    int yBlitPosition = (int)((sprite.YPosition - viewOffsetY) * (double)Program.tileSize) - spriteSurface.Height;

                    mainSurface.Blit(spriteSurface, new Point(xBlitPosition, yBlitPosition));
                }
                else if (sprite is IMeshSprite)
                {
                    BlitMeshSprite(mainSurface, (IMeshSprite)sprite);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// View mesh sprite
        /// </summary>
        /// <param name="mainSurface">surface to blit on</param>
        /// <param name="iMeshSprite">sprite to show</param>
        private void BlitMeshSprite(Surface mainSurface, IMeshSprite iMeshSprite)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
