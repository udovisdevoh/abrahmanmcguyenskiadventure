using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.Midi;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// To schedule noteoff message
    /// </summary>
    internal class NoteOffScheduler
    {
        #region Fields and parts
        private Queue<MessageInfo> internalQueue;
        #endregion

        #region Constructor
        public NoteOffScheduler()
        {
            internalQueue = new Queue<MessageInfo>();
        }
        #endregion

        #region Internal Methods
        internal void Reset()
        {
            internalQueue.Clear();
        }

        internal void TurnOffScheduledNotes(double timePointer, double timePointerPrevious, OutputDevice outputDevice)
        {
            int elementsCountToDelete = 0;
            foreach (MessageInfo messageInfo in internalQueue)
            {
                if (messageInfo.TimePosition > timePointerPrevious)
                {
                    if (messageInfo.TimePosition > timePointer)
                        break;

                    outputDevice.Send(messageInfo.ChannelMessage);
                    elementsCountToDelete++;
                }
            }

            for (int i = 0; i < elementsCountToDelete; i++)
                internalQueue.Dequeue();
        }

        internal void Add(MessageInfo messageInfo)
        {
            internalQueue.Enqueue(messageInfo);
        }
        #endregion
    }
}
