using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Set of joined metaRiffs
    /// </summary>
    public abstract class MetaRiffPack : IEnumerable<MetaRiff>
    {
        #region Fields
        /// <summary>
        /// Internal list of metaRiffs
        /// </summary>
        private IEnumerable<MetaRiff> metaRiffList = null;

        /// <summary>
        /// Facultative scale to override other scales
        /// </summary>
        private Scale overridenScale1 = null;

        /// <summary>
        /// Facultative scale to override other scales
        /// </summary>
        private Scale overridenScale2 = null;

        /// <summary>
        /// Whether we override key
        /// </summary>
        private bool isOverrideKey;

        /// <summary>
        /// Facultative key to override (only works if IsOverrideKey)
        /// </summary>
        private int forcedModulationOffset;

        private int scaleCycleLength = 0;
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a riffPackBuilder (object to create riffPacks deterministically)
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="desiredLength">desired bar count</param>
        /// <returns>riffPackBuilder (object to create riffPacks deterministically)</returns>
        public RiffPackBuilder BuildRiffPackBuilder(Random random, int desiredLength)
        {
            IEnumerable<MetaRiff> metaRiffList = BuildMetaRiffList();

            RiffPackBuilder riffPackBuilder = new RiffPackBuilder();
            RiffBuilder firstRiffBuilder = null;

            foreach (MetaRiff currentMetaRiff in metaRiffList)
            {
                RiffBuilder riffBuilder = currentMetaRiff.Build(random);

                if (firstRiffBuilder == null)
                    firstRiffBuilder = riffBuilder;

                riffBuilder.DesiredRiffLength = desiredLength;

                if (!riffBuilder.IsDrum)
                {
                    riffBuilder.IsOverrideKey = isOverrideKey;
                    if (isOverrideKey)
                        riffBuilder.ForcedModulationOffset = forcedModulationOffset;

                    if (riffBuilder.IsOverrideKey)
                        riffBuilder.MidPitch = GetMatchedMidPitch(riffBuilder.MidPitch, forcedModulationOffset);
                    else
                        riffBuilder.MidPitch = GetMatchedMidPitch(riffBuilder.MidPitch, firstRiffBuilder.MidPitch);

                    riffBuilder.Scale1 = firstRiffBuilder.Scale1;

                    if (overridenScale1 != null)
                        riffBuilder.Scale1 = overridenScale1;

                    if (overridenScale2 != null)
                        riffBuilder.Scale2 = overridenScale2;

                    riffBuilder.ScaleCycleLength = scaleCycleLength;
                }

                riffPackBuilder.Add(riffBuilder);
            }

            return riffPackBuilder;
        }

        /// <summary>
        /// Returns description tag list from joined metaRiffs
        /// </summary>
        /// <returns>description tag list from joined metaRiffs</returns>
        public IEnumerable<string> GetDescriptionTagList()
        {
            HashSet<string> descriptionTagList = new HashSet<string>();

            foreach (MetaRiff metaRiff in BuildMetaRiffList())
                descriptionTagList.UnionWith(metaRiff.GetDescriptionTagList());

            return descriptionTagList;
        }

        /// <summary>
        /// Whether metaRiffPack contains specified metaRiff type
        /// </summary>
        /// <param name="metaRiffType">metaRiff type</param>
        /// <returns>Whether metaRiffPack contains specified metaRiff type</returns>
        public bool ContainsMetaRiffType(Type metaRiffType)
        {
            foreach (MetaRiff currentMetaRiff in BuildMetaRiffList())
                if (currentMetaRiff.GetType() == metaRiffType)
                    return true;
            return false;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Return a pitch position that matches the other (same music key, but may not be same octave)
        /// </summary>
        /// <param name="toMatch">pitch to be matched</param>
        /// <param name="toMatchWith">pitch to match with</param>
        /// <returns>matched pitch</returns>
        private int GetMatchedMidPitch(int toMatch, int toMatchWith)
        {
            while (toMatchWith - toMatch >= 12)
                toMatchWith -= 12;

            while (toMatch - toMatchWith >= 12)
                toMatchWith += 12;

            return toMatchWith;
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Join two metaRiff packs
        /// </summary>
        /// <param name="metaRiffPack1">metaRiff packs 1</param>
        /// <param name="metaRiffPack2">metaRiff packs 2</param>
        /// <returns></returns>
        public static MetaRiffPack operator +(MetaRiffPack metaRiffPack1, MetaRiffPack metaRiffPack2)
        {
            return new CompositeMetaRiffPack(metaRiffPack1, metaRiffPack2);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build metaRiff list if not built yet
        /// </summary>
        /// <returns>metaRiff list</returns>
        private IEnumerable<MetaRiff> GetMetaRiffListLazy()
        {
            if (metaRiffList == null)
                metaRiffList = BuildMetaRiffList();
            return metaRiffList;
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Build matched metaRiff list (metaRiffs must be musically compatible among metaRiffPack)
        /// </summary>
        /// <returns>metaRiff list</returns>
        protected abstract IEnumerable<MetaRiff> BuildMetaRiffList();
        #endregion

        #region Properties
        /// <summary>
        /// How many metaRiffs in this
        /// </summary>
        public int Count
        {
            get { return GetMetaRiffListLazy().Count(); }
        }

        /// <summary>
        /// Facultative scale to override other scales
        /// </summary>
        public Scale OverridenScale1
        {
            get { return overridenScale1; }
            set { overridenScale1 = value; }
        }

        /// <summary>
        /// Facultative scale to override other scales
        /// </summary>
        public Scale OverridenScale2
        {
            get { return overridenScale2; }
            set { overridenScale2 = value; }
        }

        /// <summary>
        /// Facultative key to override (only works if IsOverrideKey)
        /// </summary>
        public int ForcedModulationOffset
        {
            get { return forcedModulationOffset; }
            set { forcedModulationOffset = value; }
        }

        /// <summary>
        /// Whether we override key
        /// </summary>
        public bool IsOverrideKey
        {
            get { return isOverrideKey; }
            set { isOverrideKey = value; }
        }

        public int ScaleCycleLength
        {
            get { return scaleCycleLength; }
            set { scaleCycleLength = value; }
        }
        #endregion

        #region IEnumerable<MetaRiff> Members
        /// <summary>
        /// Cycle through list of metaRiffs
        /// </summary>
        /// <returns></returns>
        public IEnumerator<MetaRiff> GetEnumerator()
        {
            return GetMetaRiffListLazy().GetEnumerator();
        }

        /// <summary>
        /// Cycle through list of metaRiffs
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetMetaRiffListLazy().GetEnumerator();
        }
        #endregion
    }
}
