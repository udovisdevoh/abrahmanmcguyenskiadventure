using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Loads metaRiffPacks
    /// </summary>
    public class MetaRiffPackLoader : IEnumerable<string>
    {
        #region Fields
        private Dictionary<string, Type> metaRiffPackClassNameList = new Dictionary<string, Type>();

        private Dictionary<string, MetaRiffPack> lazyInitialization = new Dictionary<string, MetaRiffPack>();
        #endregion

        #region Constructor
        /// <summary>
        /// Build metaRiffPack loader
        /// </summary>
        public MetaRiffPackLoader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
                if (type.IsSubclassOf(typeof(MetaRiffPack)) && type != typeof(CompositeMetaRiffPack))
                    metaRiffPackClassNameList.Add(type.Name,type);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load metariff pack from name
        /// </summary>
        /// <param name="metaRiffPackName">name</param>
        /// <returns>metariff pack</returns>
        public MetaRiffPack Load(string metaRiffPackName)
        {
            MetaRiffPack metaRiffPack;
            if (!lazyInitialization.TryGetValue(metaRiffPackName, out metaRiffPack))
                metaRiffPack = (MetaRiffPack)Activator.CreateInstance(metaRiffPackClassNameList[metaRiffPackName]);
            return metaRiffPack;
        }
        #endregion

        #region IEnumerable<string> Members
        /// <summary>
        /// Riff pack name list
        /// </summary>
        /// <returns>Riff pack name list</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return metaRiffPackClassNameList.Keys.GetEnumerator();
        }

        /// <summary>
        /// Riff pack name list
        /// </summary>
        /// <returns>Riff pack name list</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return metaRiffPackClassNameList.Keys.GetEnumerator();
        }
        #endregion
    }
}