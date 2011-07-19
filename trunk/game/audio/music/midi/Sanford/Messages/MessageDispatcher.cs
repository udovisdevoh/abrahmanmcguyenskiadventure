#region License

/* Copyright (c) 2006 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

using System;
using System.Collections.Generic;

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// Dispatches IMidiMessages to their corresponding sink.
    /// </summary>
    public class MessageDispatcher
    {
        #region MessageDispatcher Members
        #region Events
        /// <summary>
        /// Channel message dispatched
        /// </summary>
        public event EventHandler<ChannelMessageEventArgs> ChannelMessageDispatched;

        /// <summary>
        /// System ex message dispatched
        /// </summary>
        public event EventHandler<SysExMessageEventArgs> SysExMessageDispatched;

        /// <summary>
        /// System common message dispatched
        /// </summary>
        public event EventHandler<SysCommonMessageEventArgs> SysCommonMessageDispatched;

        /// <summary>
        /// System real time message dispatched
        /// </summary>
        public event EventHandler<SysRealtimeMessageEventArgs> SysRealtimeMessageDispatched;

        /// <summary>
        /// Meta message dispatched
        /// </summary>
        public event EventHandler<MetaMessageEventArgs> MetaMessageDispatched;
        #endregion

        /// <summary>
        /// Dispatches IMidiMessages to their corresponding sink.
        /// </summary>
        /// <param name="message">
        /// The IMidiMessage to dispatch.
        /// </param>
        public void Dispatch(IMidiMessage message)
        {
            #region Require

            if(message == null)
            {
                throw new ArgumentNullException("message");
            }

            #endregion

            switch(message.MessageType)
            {
                case MessageType.Channel:
                    OnChannelMessageDispatched(new ChannelMessageEventArgs((ChannelMessage)message));
                    break;

                case MessageType.SystemExclusive:
                    OnSysExMessageDispatched(new SysExMessageEventArgs((SysExMessage)message));
                    break;

                case MessageType.Meta:
                    OnMetaMessageDispatched(new MetaMessageEventArgs((MetaMessage)message));
                    break;

                case MessageType.SystemCommon:
                    OnSysCommonMessageDispatched(new SysCommonMessageEventArgs((SysCommonMessage)message));
                    break;

                case MessageType.SystemRealtime:
                    switch(((SysRealtimeMessage)message).SysRealtimeType)
                    {
                        case SysRealtimeType.ActiveSense:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.ActiveSense);
                            break;

                        case SysRealtimeType.Clock:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.Clock);
                            break;

                        case SysRealtimeType.Continue:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.Continue);
                            break;

                        case SysRealtimeType.Reset:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.Reset);
                            break;

                        case SysRealtimeType.Start:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.Start);
                            break;

                        case SysRealtimeType.Stop:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.Stop);
                            break;

                        case SysRealtimeType.Tick:
                            OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs.Tick);
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// When channel message dispatched
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnChannelMessageDispatched(ChannelMessageEventArgs e)
        {
            EventHandler<ChannelMessageEventArgs> handler = ChannelMessageDispatched;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// When system ex message dispatched
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnSysExMessageDispatched(SysExMessageEventArgs e)
        {
            EventHandler<SysExMessageEventArgs> handler = SysExMessageDispatched;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// When system common message is dispatched
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnSysCommonMessageDispatched(SysCommonMessageEventArgs e)
        {
            EventHandler<SysCommonMessageEventArgs> handler = SysCommonMessageDispatched;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// When system realtime message dispatched
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnSysRealtimeMessageDispatched(SysRealtimeMessageEventArgs e)
        {
            EventHandler<SysRealtimeMessageEventArgs> handler = SysRealtimeMessageDispatched;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// When meta message is dispatched
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnMetaMessageDispatched(MetaMessageEventArgs e)
        {
            EventHandler<MetaMessageEventArgs> handler = MetaMessageDispatched;

            if(handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
    }
}
