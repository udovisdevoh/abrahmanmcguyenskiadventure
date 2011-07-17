using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.Midi;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Music player
    /// </summary>
    internal class SongPlayer
    {
        #region Fields and parts
        private OutputDevice outputDevice;
        #endregion

        #region Constructor
        public SongPlayer()
        {
            outputDevice = outputDevice = new OutputDevice(0);
        }
        #endregion

        #region Play music
        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="renderedSong">music</param>
        /// <param name="timeDelta">time delta</param>
        internal void Play(RenderedSong renderedSong, double timeDelta)
        {
            
        }
        #endregion
    }
}