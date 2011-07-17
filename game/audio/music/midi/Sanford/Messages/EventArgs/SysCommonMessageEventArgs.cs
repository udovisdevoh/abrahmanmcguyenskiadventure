using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Build System comment message event arguments
    /// </summary>
    public class SysCommonMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Message
        /// </summary>
        private SysCommonMessage message;

        /// <summary>
        /// System comment message event arguments
        /// </summary>
        /// <param name="message">message</param>
        public SysCommonMessageEventArgs(SysCommonMessage message)
        {
            this.message = message;
        }

        /// <summary>
        /// System common message
        /// </summary>
        public SysCommonMessage Message
        {
            get
            {
                return message;
            }
        }
    }
}
