using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    class TimeFrameException : Exception
    {
        public TimeFrameException(string message) : base(message) { }
    }
}
