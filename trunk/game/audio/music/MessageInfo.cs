using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.Midi;

namespace AbrahmanAdventure.audio
{
    internal class MessageInfo
    {
        #region Fields and parts
        private ChannelMessage channelMessage;

        private double timePosition;
        #endregion

        #region Constructor
        public MessageInfo(double timePosition, ChannelMessage channelMessage)
        {
            this.timePosition = timePosition;
            this.channelMessage = channelMessage;
        }
        #endregion

        #region Properties
        public double TimePosition
        {
            get { return timePosition; }
        }

        public ChannelMessage ChannelMessage
        {
            get { return channelMessage; }
        }
        #endregion
    }
}
