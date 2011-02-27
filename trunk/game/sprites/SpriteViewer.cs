using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite viewer
    /// </summary>
    internal class SpriteViewer
    {
        #region Fields and parts
        private SpritePopulation spritePopulation;

        private HashSet<AbstractSprite> spriteListToView;
        #endregion

        #region Constructor
        public SpriteViewer(SpritePopulation spritePopulation)
        {
            this.spritePopulation = spritePopulation;
            spriteListToView = new HashSet<AbstractSprite>();
        }
        #endregion

        #region Public Methods
        internal void Update()
        {
            /*int leftMostViewableBucketId = GetLeftMostViewableBucketId(Program.viewOffsetX);
            int rightMostViewableBucketId = GetRightMostViewableBucketId(Program.viewOffsetX);

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
                viewSprite(sprite, Program.viewOffsetX, Program.viewOffsetY);*/
        }
        #endregion
    }
}
