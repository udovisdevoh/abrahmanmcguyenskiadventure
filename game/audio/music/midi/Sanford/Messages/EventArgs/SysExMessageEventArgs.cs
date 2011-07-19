using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// System Ex message event arguments
    /// </summary>
    public class SysExMessageEventArgs : EventArgs
    {
        private SysExMessage message;

        /// <summary>
        /// Build System Ex message event arguments
        /// </summary>
        /// <param name="message">message</param>
        public SysExMessageEventArgs(SysExMessage message)
        {
            this.message = message;
        }

        /// <summary>
        /// Message
        /// </summary>
        public SysExMessage Message
        {
            get
            {
                return message;
            }
        }
    }
}
