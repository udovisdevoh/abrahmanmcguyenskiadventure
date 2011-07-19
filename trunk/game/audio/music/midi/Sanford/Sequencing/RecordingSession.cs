using System;
using System.Collections.Generic;
using System.Text;

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// Recording session
    /// </summary>
    public class RecordingSession
    {
        /// <summary>
        /// Clock
        /// </summary>
        private IClock clock;

        /// <summary>
        /// Buffer
        /// </summary>
        private List<TimestampedMessage> buffer = new List<TimestampedMessage>();

        /// <summary>
        /// Resu;t
        /// </summary>
        private Track result = new Track();

        /// <summary>
        /// Create rectording session
        /// </summary>
        /// <param name="clock">clock</param>
        public RecordingSession(IClock clock)
        {
            this.clock = clock;
        }

        /// <summary>
        /// Build 
        /// </summary>
        public void Build()
        {
            result = new Track();

            buffer.Sort(new TimestampComparer());

            foreach(TimestampedMessage tm in buffer)
            {
                result.Insert(tm.ticks, tm.message);
            }
        }

        /// <summary>
        /// Clear buffer
        /// </summary>
        public void Clear()
        {
            buffer.Clear();
        }

        /// <summary>
        /// Result
        /// </summary>
        public Track Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// Record
        /// </summary>
        /// <param name="message">channel message</param>
        public void Record(ChannelMessage message)
        {
            if(clock.IsRunning)
            {
                buffer.Add(new TimestampedMessage(clock.Ticks, message));
            }
        }

        /// <summary>
        /// Record
        /// </summary>
        /// <param name="message">Sys Ex Message</param>
        public void Record(SysExMessage message)
        {
            if(clock.IsRunning)
            {
                buffer.Add(new TimestampedMessage(clock.Ticks, message));
            }
        }

        /// <summary>
        /// TimeStampedMEssage
        /// </summary>
        private struct TimestampedMessage
        {
            public int ticks;

            public IMidiMessage message;

            public TimestampedMessage(int ticks, IMidiMessage message)
            {
                this.ticks = ticks;
                this.message = message;
            }
        }

        /// <summary>
        /// TimeStamp comparer
        /// </summary>
        private class TimestampComparer : IComparer<TimestampedMessage>
        {
            #region IComparer<TimestampedMessage> Members
            public int Compare(TimestampedMessage x, TimestampedMessage y)
            {
                if(x.ticks > y.ticks)
                {
                    return 1;
                }
                else if(x.ticks < y.ticks)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            #endregion
        }
    }
}
