using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Riff or RiffPack
    /// </summary>
    public interface IRiff : IEquatable<IRiff>
    {
        /// <summary>
        /// Tempo
        /// </summary>
        int Tempo
        {
            get;
            set;
        }
    }
}
