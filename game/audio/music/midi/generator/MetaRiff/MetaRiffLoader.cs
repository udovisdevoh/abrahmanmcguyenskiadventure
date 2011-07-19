using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Loads meta riff from their name
    /// </summary>
    public class MetaRiffLoader : IEnumerable<string>
    {
        #region Fields
        /// <summary>
        /// metaRiff class name list
        /// </summary>
        private Dictionary<string, Type> metaRiffClassNameList = new Dictionary<string, Type>();
        #endregion

        #region Constructor
        /// <summary>
        /// Build metariff loader
        /// </summary>
        public MetaRiffLoader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
                if (type.IsSubclassOf(typeof(MetaRiff)))
                    metaRiffClassNameList.Add(type.Name,type);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load metaRiff
        /// </summary>
        /// <param name="metaRiffName">metaRiff's name</param>
        /// <returns>metaRiff</returns>
        public MetaRiff Load(string metaRiffName)
        {
            return (MetaRiff)Activator.CreateInstance(metaRiffClassNameList[metaRiffName]);
        }
        #endregion

        #region IEnumerable<string> Members
        /// <summary>
        /// MetaRiff name enumerator
        /// </summary>
        /// <returns>MetaRiff name enumerator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return metaRiffClassNameList.Keys.GetEnumerator();
        }

        /// <summary>
        /// MetaRiff name enumerator
        /// </summary>
        /// <returns>MetaRiff name enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return metaRiffClassNameList.Keys.GetEnumerator();
        }
        #endregion
    }
}
