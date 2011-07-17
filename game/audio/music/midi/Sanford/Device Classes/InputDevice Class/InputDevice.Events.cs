using System;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Input device
    /// </summary>
    public partial class InputDevice
    {
        /// <summary>
        /// Channel message received
        /// </summary>
        public event EventHandler<ChannelMessageEventArgs> ChannelMessageReceived;

        /// <summary>
        /// System ex message received
        /// </summary>
        public event EventHandler<SysExMessageEventArgs> SysExMessageReceived;

        /// <summary>
        /// System common message received
        /// </summary>
        public event EventHandler<SysCommonMessageEventArgs> SysCommonMessageReceived;

        /// <summary>
        /// System realtime message received
        /// </summary>
        public event EventHandler<SysRealtimeMessageEventArgs> SysRealtimeMessageReceived;

        /// <summary>
        /// Invalid short message received
        /// </summary>
        public event EventHandler<InvalidShortMessageEventArgs> InvalidShortMessageReceived;

        /// <summary>
        /// Invalid system ex message received
        /// </summary>
        public event EventHandler<InvalidSysExMessageEventArgs> InvalidSysExMessageReceived;

        /// <summary>
        /// When channel message is received
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnChannelMessageReceived(ChannelMessageEventArgs e)
        {
            EventHandler<ChannelMessageEventArgs> handler = ChannelMessageReceived;

            if(handler != null)
            {
                context.Post(delegate(object dummy)
                {
                    handler(this, e);
                }, null);
            }
        }

        /// <summary>
        /// When system ex message received
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnSysExMessageReceived(SysExMessageEventArgs e)
        {
            EventHandler<SysExMessageEventArgs> handler = SysExMessageReceived;

            if(handler != null)
            {
                context.Post(delegate(object dummy)
                {
                    handler(this, e);
                }, null);
            }
        }

        /// <summary>
        /// When system common message received
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnSysCommonMessageReceived(SysCommonMessageEventArgs e)
        {
            EventHandler<SysCommonMessageEventArgs> handler = SysCommonMessageReceived;

            if(handler != null)
            {
                context.Post(delegate(object dummy)
                {
                    handler(this, e);
                }, null);
            }
        }

        /// <summary>
        /// On system realtime message received
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnSysRealtimeMessageReceived(SysRealtimeMessageEventArgs e)
        {
            EventHandler<SysRealtimeMessageEventArgs> handler = SysRealtimeMessageReceived;

            if(handler != null)
            {
                context.Post(delegate(object dummy)
                {
                    handler(this, e);
                }, null);
            }
        }

        /// <summary>
        /// On invalid short message received
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnInvalidShortMessageReceived(InvalidShortMessageEventArgs e)
        {
            EventHandler<InvalidShortMessageEventArgs> handler = InvalidShortMessageReceived;

            if(handler != null)
            {
                context.Post(delegate(object dummy)
                {
                    handler(this, e);
                }, null);
            }
        }

        /// <summary>
        /// On invalid system ex message received
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnInvalidSysExMessageReceived(InvalidSysExMessageEventArgs e)
        {
            EventHandler<InvalidSysExMessageEventArgs> handler = InvalidSysExMessageReceived;

            if(handler != null)
            {
                context.Post(delegate(object dummy)
                {
                    handler(this, e);
                }, null);
            }
        }
    }
}
