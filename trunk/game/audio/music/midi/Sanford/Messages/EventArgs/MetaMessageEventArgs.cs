using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Meta Message event args
    /// </summary>
    public class MetaMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Meta message
        /// </summary>
        private MetaMessage message;

        /// <summary>
        /// Meta message event arguments
        /// </summary>
        /// <param name="message">message</param>
        public MetaMessageEventArgs(MetaMessage message)
        {
            this.message = message;
        }

        /// <summary>
        /// Message
        /// </summary>
        public MetaMessage Message
        {
            get
            {
                return message;
            }
        }
    }
}
