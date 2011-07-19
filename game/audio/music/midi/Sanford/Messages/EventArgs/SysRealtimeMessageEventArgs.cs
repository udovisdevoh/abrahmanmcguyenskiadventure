using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// Sys realtime message event arguments
    /// </summary>
    public class SysRealtimeMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Start
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs Start = new SysRealtimeMessageEventArgs(SysRealtimeMessage.StartMessage);

        /// <summary>
        /// Continue
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs Continue = new SysRealtimeMessageEventArgs(SysRealtimeMessage.ContinueMessage);

        /// <summary>
        /// Stop
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs Stop = new SysRealtimeMessageEventArgs(SysRealtimeMessage.StopMessage);

        /// <summary>
        /// Clock
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs Clock = new SysRealtimeMessageEventArgs(SysRealtimeMessage.ClockMessage);

        /// <summary>
        /// Tick
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs Tick = new SysRealtimeMessageEventArgs(SysRealtimeMessage.TickMessage);

        /// <summary>
        /// Active sense
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs ActiveSense = new SysRealtimeMessageEventArgs(SysRealtimeMessage.ActiveSenseMessage);

        /// <summary>
        /// Reset
        /// </summary>
        public static readonly SysRealtimeMessageEventArgs Reset = new SysRealtimeMessageEventArgs(SysRealtimeMessage.ResetMessage);

        /// <summary>
        /// Sys realtime message
        /// </summary>
        private SysRealtimeMessage message;

        /// <summary>
        /// Sys realtime message event args
        /// </summary>
        /// <param name="message"></param>
        private SysRealtimeMessageEventArgs(SysRealtimeMessage message)
        {
            this.message = message;
        }

        /// <summary>
        /// Sys realtime message
        /// </summary>
        public SysRealtimeMessage Message
        {
            get
            {
                return message;
            }
        }
    }
}
