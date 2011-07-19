using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// Channel message event arguments
    /// </summary>
    public class ChannelMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Channel message
        /// </summary>
        private ChannelMessage message;

        /// <summary>
        /// Channel message event arguments
        /// </summary>
        /// <param name="message">message</param>
        public ChannelMessageEventArgs(ChannelMessage message)
        {
            this.message = message;
        }

        /// <summary>
        /// Message
        /// </summary>
        public ChannelMessage Message
        {
            get
            {
                return message;
            }
        }
    }
}
