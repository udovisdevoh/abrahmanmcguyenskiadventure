using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class MidiPlayerException : Exception
    {
        public MidiPlayerException(string message) : base(message) { }
    }
}
