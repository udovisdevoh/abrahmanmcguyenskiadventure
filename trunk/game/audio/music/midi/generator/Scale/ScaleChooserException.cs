using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Thrown when scale selection fails
    /// </summary>
    class ScaleChooserException :Exception
    {
        /// <summary>
        /// Thrown when scale selection fails
        /// </summary>
        /// <param name="message">exception message</param>
        public ScaleChooserException(string message) : base(message) { }
    }
}
