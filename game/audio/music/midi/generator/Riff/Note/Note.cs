using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Midi note
    /// </summary>
    public class Note : IEquatable<Note>
    {
        #region Fields
        /// <summary>
        /// Pitch
        /// </summary>
        private int pitch;

        /// <summary>
        /// Length
        /// </summary>
        private double length;

        /// <summary>
        /// Riff position
        /// </summary>
        private double riffPosition;

        /// <summary>
        /// Velocity (0 to 127)
        /// </summary>
        private int velocity;
        #endregion

        #region Constructors
        /// <summary>
        /// Create note
        /// </summary>
        /// <param name="riffPosition">riff position</param>
        /// <param name="length">length</param>
        /// <param name="pitch">pitch</param>
        /// <param name="velocity">velocity</param>
        public Note(double riffPosition, double length, int pitch, int velocity)
        {
            this.riffPosition = riffPosition;
            this.length = length;
            this.pitch = pitch;
            this.velocity = velocity;
        }
        #endregion

        #region IEquatable<Note> Members
        /// <summary>
        /// Whether note equals other note
        /// </summary>
        /// <param name="other">other note</param>
        /// <returns>Whether note equals other note</returns>
        public bool Equals(Note other)
        {
            return pitch == other.pitch && length == other.length && riffPosition == other.riffPosition;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Pitch (0 to 127)
        /// </summary>
        public int Pitch
        {
            get { return pitch; }
            set { pitch = value; }
        }

        /// <summary>
        /// Length
        /// </summary>
        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// Riff Position
        /// </summary>
        public double RiffPosition
        {
            get { return riffPosition; }
            set { riffPosition = value; }
        }

        /// <summary>
        /// Velocity (from 0 to 127)
        /// </summary>
        public int Velocity
        {
            get{return velocity;}
            set{velocity = value;}
        }
        #endregion
    }
}
