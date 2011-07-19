using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Composite implementation of metaRiffPack. Used to join existing metaRiffsPacks
    /// </summary>
    class CompositeMetaRiffPack : MetaRiffPack
    {
        #region Constants
        public const int MaxCountPerType = 2;
        #endregion

        #region Fields
        /// <summary>
        /// Internal list of metaRiffs
        /// </summary>
        private List<MetaRiff> metaRiffList;
        #endregion

        #region Constructors
        /// <summary>
        /// Join two metaRiff packs
        /// </summary>
        /// <param name="metaRiffPack1">metaRiff pack 1</param>
        /// <param name="metaRiffPack2">metaRiff pack 2</param>
        public CompositeMetaRiffPack(MetaRiffPack metaRiffPack1, MetaRiffPack metaRiffPack2)
        {
            metaRiffList = new List<MetaRiff>(metaRiffPack1);
            metaRiffList.AddRange(metaRiffPack2);
            CleanMetaRiffs(metaRiffList, MaxCountPerType);
        }

        /// <summary>
        /// Create metaRiffPack from existing metaRiff
        /// </summary>
        /// <param name="metaRiff">existing metaRiff</param>
        public CompositeMetaRiffPack(MetaRiff metaRiff)
        {
            metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(metaRiff);
            CleanMetaRiffs(metaRiffList, MaxCountPerType);
        }

        /// <summary>
        /// Create metaRiffPack from existing metaRiff
        /// </summary>
        public CompositeMetaRiffPack()
        {
            metaRiffList = new List<MetaRiff>();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Internal list of metaRiffs
        /// </summary>
        /// <returns>Internal list of metaRiffs</returns>
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            return metaRiffList;
        }
        #endregion

        #region Private Methods
        private void CleanMetaRiffs(List<MetaRiff> metaRiffList, int maxCountPerType)
        {
            List<MetaRiff> newMetaRiffList = new List<MetaRiff>(metaRiffList);

            foreach (MetaRiff metaRiff in metaRiffList)
                if (CountForCurrentType(metaRiff, newMetaRiffList) > maxCountPerType)
                    newMetaRiffList.Remove(metaRiff);

            this.metaRiffList = newMetaRiffList;
        }

        private int CountForCurrentType(MetaRiff metaRiff, List<MetaRiff> metaRiffList)
        {
            int count = 0;
            foreach (MetaRiff toCount in metaRiffList)
            {
                if (metaRiff.GetType() == toCount.GetType())
                {
                    count++;
                }
            }
            return count;
        }
        #endregion
    }
}
