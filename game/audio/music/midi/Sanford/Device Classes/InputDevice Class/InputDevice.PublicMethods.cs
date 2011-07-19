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
using System.Diagnostics;
using System.Threading;
using Sanford.Multimedia;

namespace AbrahmanAdventure.audio.midi
{
    public partial class InputDevice
    {
        /// <summary>
        /// Close
        /// </summary>
        public override void Close()
        {
            #region Guard

            if(IsDisposed)
            {
                return;
            }

            #endregion

            Dispose(true);
        }

        /// <summary>
        /// Start recording
        /// </summary>
        public void StartRecording()
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException("InputDevice");
            }

            #endregion

            #region Guard

            if(recording)
            {
                return;
            }

            #endregion

            lock(lockObject)
            {
                int result = AddSysExBuffer();

                if(result == DeviceException.MMSYSERR_NOERROR)
                {
                    result = AddSysExBuffer();
                }

                if(result == DeviceException.MMSYSERR_NOERROR)
                {
                    result = AddSysExBuffer();
                }

                if(result == DeviceException.MMSYSERR_NOERROR)
                {
                    result = AddSysExBuffer();
                }

                if(result == DeviceException.MMSYSERR_NOERROR)
                {
                    result = midiInStart(Handle);
                }

                if(result == MidiDeviceException.MMSYSERR_NOERROR)
                {
                    recording = true;
                }
                else
                {
                    throw new InputDeviceException(result);
                }
            }
        }

        /// <summary>
        /// Stop recording
        /// </summary>
        public void StopRecording()
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException("InputDevice");
            }

            #endregion

            #region Guard

            if(!recording)
            {
                return;
            }

            #endregion

            lock(lockObject)
            {
                int result = midiInStop(Handle);

                if(result == MidiDeviceException.MMSYSERR_NOERROR)
                {
                    recording = false;
                }
                else
                {
                    throw new InputDeviceException(result);
                }
            }
        }

        /// <summary>
        /// Reset
        /// </summary>
        public override void Reset()
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException("InputDevice");
            }

            #endregion

            lock(lockObject)
            {
                resetting = true;

                int result = midiInReset(Handle);                

                if(result == MidiDeviceException.MMSYSERR_NOERROR)
                {
                    recording = false;

                    while(bufferCount > 0)
                    {
                        Monitor.Wait(lockObject);
                    }

                    resetting = false;
                }
                else
                {
                    resetting = false;

                    throw new InputDeviceException(result);
                }
            }
        }
        
        /// <summary>
        /// Get device capabilities
        /// </summary>
        /// <param name="deviceID">device id</param>
        /// <returns>Midi in caps</returns>
        public static MidiInCaps GetDeviceCapabilities(int deviceID)
        {
            int result;
            MidiInCaps caps = new MidiInCaps();

            result = midiInGetDevCaps(deviceID, ref caps, SizeOfMidiHeader);

            if(result != MidiDeviceException.MMSYSERR_NOERROR)
            {
                throw new InputDeviceException(result);
            }

            return caps;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        {
            #region Guard

            if(IsDisposed)
            {
                return;
            }

            #endregion

            Dispose(true);
        }
    }
}
