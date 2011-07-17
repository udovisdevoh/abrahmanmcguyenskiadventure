#region License

/* Copyright (c) 2005 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Sanford.Multimedia;

namespace AbrahmanAdventure.audio.Midi
{
	/// <summary>
	/// The base class for all MIDI devices.
	/// </summary>
	public abstract class MidiDevice : Device
	{
        #region MidiDevice Members

        #region Win32 Midi Device Functions

        [DllImport("winmm.dll")]
        private static extern int midiConnect(int handleA, int handleB, int reserved);         

        [DllImport("winmm.dll")]
        private static extern int midiDisconnect(int handleA, int handleB, int reserved);             

        #endregion

        /// <summary>
        /// Size of midi header structure
        /// </summary>
        protected static readonly int SizeOfMidiHeader;

        static MidiDevice()
        {
            SizeOfMidiHeader = Marshal.SizeOf(typeof(MidiHeader));
        }

        /// <summary>
        /// Midi device
        /// </summary>
        /// <param name="deviceID">device id</param>
        public MidiDevice(int deviceID) : base(deviceID)
        {            
        }
        
        /// <summary>
        /// Connects a MIDI InputDevice to a MIDI thru or OutputDevice, or 
        /// connects a MIDI thru device to a MIDI OutputDevice. 
        /// </summary>
        /// <param name="handleA">
        /// Handle to a MIDI InputDevice or a MIDI thru device (for thru 
        /// devices, this handle must belong to a MIDI OutputDevice).
        /// </param>
        /// <param name="handleB">
        /// Handle to the MIDI OutputDevice or thru device.
        /// </param>
        /// <exception cref="DeviceException">
        /// If an error occurred while connecting the two devices.
        /// </exception>
        public static void Connect(int handleA, int handleB)
        {
            int result = midiConnect(handleA, handleB, 0);

            if(result != MidiDeviceException.MMSYSERR_NOERROR)
            {
                throw new MidiDeviceException(result);
            }
        }

        /// <summary>
        /// Disconnects a MIDI InputDevice from a MIDI thru or OutputDevice, or 
        /// disconnects a MIDI thru device from a MIDI OutputDevice. 
        /// </summary>
        /// <param name="handleA">
        /// Handle to a MIDI InputDevice or a MIDI thru device.
        /// </param>
        /// <param name="handleB">
        /// Handle to the MIDI OutputDevice to be disconnected. 
        /// </param>
        /// <exception cref="DeviceException">
        /// If an error occurred while disconnecting the two devices.
        /// </exception>
        public static void Disconnect(int handleA, int handleB)
        {
            int result = midiDisconnect(handleA, handleB, 0);

            if(result != MidiDeviceException.MMSYSERR_NOERROR)
            {
                throw new MidiDeviceException(result);
            }
        }        

        #endregion        
    }
}
