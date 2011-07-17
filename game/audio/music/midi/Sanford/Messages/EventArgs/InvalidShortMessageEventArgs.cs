using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Invalid short message arguments
    /// </summary>
    public class InvalidShortMessageEventArgs : EventArgs
    {
        private int message;

        /// <summary>
        /// Build Invalid short message arguments
        /// </summary>
        /// <param name="message">message</param>
        public InvalidShortMessageEventArgs(int message)
        {
            this.message = message;
        }

        /// <summary>
        /// Message
        /// </summary>
        public int Message
        {
            get
            {
                return message;
            }
        }
    }
}
