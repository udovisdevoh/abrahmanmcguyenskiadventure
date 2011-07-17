using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.Midi;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Rendered song
    /// </summary>
    internal class RenderedSong
    {
        #region Field and parts
        /// <summary>
        /// Current position in list of messages
        /// </summary>
        private int listMessagePointer;

        /// <summary>
        /// List of midi messages
        /// </summary>
        private List<ChannelMessage> listMessages;
        #endregion

        #region Constructor
        /// <summary>
        /// Create rendered song from song
        /// </summary>
        /// <param name="song">song</param>
        public RenderedSong(Song song)
        {
            listMessages = new List<ChannelMessage>();
        }
        #endregion
    }
}
