using System;
using System.Collections;
using System.Text;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Invalid sys ex message event args
    /// </summary>
    public class InvalidSysExMessageEventArgs : EventArgs
    {
        private byte[] messageData;

        /// <summary>
        /// Invalid sys ex message event args
        /// </summary>
        /// <param name="messageData"></param>
        public InvalidSysExMessageEventArgs(byte[] messageData)
        {
            this.messageData = messageData;
        }

        /// <summary>
        /// Message data
        /// </summary>
        public ICollection MessageData
        {
            get
            {
                return messageData;
            }
        }
    }
}
