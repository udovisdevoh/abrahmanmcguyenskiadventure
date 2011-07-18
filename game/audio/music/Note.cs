using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    internal struct Note
    {
        #region Fields and parts
        public int pitch;

        public int velocity;

        public double position;

        public double length;
        #endregion

        #region Constructor
        public Note(int pitch, int velocity, double position, double length)
        {
            this.pitch = pitch;
            this.velocity = velocity;
            this.position = position;
            this.length = length;
        }
        #endregion
    }
}
