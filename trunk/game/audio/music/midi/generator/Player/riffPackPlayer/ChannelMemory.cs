using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.audio.midi;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Remembers association for instrument and midi channels
    /// </summary>
    class ChannelMemory
    {
        #region Fields
        private Dictionary<int, int> trackToChannel = new Dictionary<int, int>();
        #endregion

        #region Public Methods
        public void Clear()
        {
            trackToChannel.Clear();
        }

        public int GetChannel(int midiInstrument, bool isDrum)
        {
            if (isDrum)
                return 9;

            int channel;

            if (!trackToChannel.TryGetValue(midiInstrument, out channel))
            {
                if (trackToChannel.Count == 0)
                    channel = 0;
                else
                    channel = trackToChannel.Last().Value + 1;

                if (channel == 9)
                    channel++;

                trackToChannel.Add(midiInstrument, channel);
            }

            return channel;
        }

        public void InitInstrumentsAndChannels(RiffPack riffPack, OutputDevice outputDevice)
        {
            int channel;

            HashSet<int> listDone = new HashSet<int>();

            foreach (Riff riff in riffPack)
            {
                channel = GetChannel(riff.MidiInstrument, riff.IsDrum);
                if (!listDone.Contains(channel))
                {
                    outputDevice.Send(new ChannelMessage(ChannelCommand.ProgramChange, channel, riff.MidiInstrument, 0));
                    listDone.Add(channel);
                }
            }
        }
        #endregion
    }
}
