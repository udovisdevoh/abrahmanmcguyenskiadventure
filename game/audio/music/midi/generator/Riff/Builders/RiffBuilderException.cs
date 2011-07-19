using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Exception thrown when building riff fails
    /// </summary>
    class RiffBuilderException : Exception
    {
        /// <summary>
        /// Exception thrown when building riff fails
        /// </summary>
        /// <param name="message">exception message</param>
        public RiffBuilderException(string message) : base(message)
        {
        }
    }
}
