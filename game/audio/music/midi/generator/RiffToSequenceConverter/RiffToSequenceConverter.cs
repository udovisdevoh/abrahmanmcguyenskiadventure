using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Convert riffs and riff packs to midi sequences
    /// </summary>
    internal class RiffToSequenceConverter
    {
        #region Internal Methods
        /// <summary>
        /// Convert a riff or riff pack to midi sequence
        /// </summary>
        /// <param name="riff">riff or riff pack</param>
        /// <returns>midi sequence</returns>
        internal Sequence Convert(IRiff riff)
        {
            if (riff is Riff)
                return ConvertRiffPack(new RiffPack(riff));
            else if (riff is RiffPack)
                return ConvertRiffPack((RiffPack)riff);
            else
                throw new Exception("Unrecoginzed riff type");
        }
        #endregion

        #region Private Region
        /// <summary>
        /// Convert riff pack to midi sequence
        /// </summary>
        /// <param name="riffPack">riff pack</param>
        /// <returns>midi sequence</returns>
        private Sequence ConvertRiffPack(RiffPack riffPack)
        {
            Sequence sequence = new Sequence();

            Track track = new Track();

            int channel = 0;

            double doubleToIntTimeMultiplicator = 32.0;

            foreach (Riff riff in riffPack)
            {
                foreach (Note note in riff)
                {
                    IMidiMessage noteOn = new ChannelMessage(ChannelCommand.NoteOn, channel, note.Pitch, note.Velocity);
                    IMidiMessage noteOff = new ChannelMessage(ChannelCommand.NoteOff, channel, note.Pitch, 0);

                    int noteStartPosition = (int)Math.Round(note.RiffPosition * doubleToIntTimeMultiplicator);
                    int noteEndPosition = (int)Math.Round((note.RiffPosition + note.Length) * doubleToIntTimeMultiplicator);

                    track.Insert(noteStartPosition, noteOn);
                    track.Insert(noteEndPosition, noteOff);
                }
            }

            sequence.Add(track);

            return sequence;
        }
        #endregion
    }
}
