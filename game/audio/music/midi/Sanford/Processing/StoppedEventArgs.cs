using System;
using System.Collections;
using System.Text;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Stopped event arguments
    /// </summary>
    public class StoppedEventArgs : EventArgs
    {
        /// <summary>
        /// Messages
        /// </summary>
        private ICollection messages;

        /// <summary>
        /// Stopped event arguments
        /// </summary>
        /// <param name="messages">messages</param>
        public StoppedEventArgs(ICollection messages)
        {
            this.messages = messages;
        }

        /// <summary>
        /// Messages
        /// </summary>
        public ICollection Messages
        {
            get
            {
                return messages;
            }
        }
    }
}
