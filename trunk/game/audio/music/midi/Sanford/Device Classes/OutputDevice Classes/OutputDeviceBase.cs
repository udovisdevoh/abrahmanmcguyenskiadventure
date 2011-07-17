#region License

/* Copyright (c) 2006 Leslie Sanford
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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Sanford.Threading;
using Sanford.Multimedia;

namespace AbrahmanAdventure.audio.Midi
{
    /// <summary>
    /// Output device base
    /// </summary>
    public abstract class OutputDeviceBase : MidiDevice
    {
        /// <summary>
        /// midi out reset
        /// </summary>
        /// <param name="handle">handle</param>
        /// <returns>midi out reset</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutReset(int handle);

        /// <summary>
        /// midi out short message
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="message">message</param>
        /// <returns>midi out short message</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutShortMsg(int handle, int message);

        /// <summary>
        /// midi out prepare header
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="headerPtr">header pointer</param>
        /// <param name="sizeOfMidiHeader">size of midi header</param>
        /// <returns>midi out prepare header</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutPrepareHeader(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        /// <summary>
        /// midi out unprepare header
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="headerPtr">header pointer</param>
        /// <param name="sizeOfMidiHeader">size of midi header</param>
        /// <returns>midi out unprepare header</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutUnprepareHeader(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        /// <summary>
        /// midi out long message
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="headerPtr">header pointer</param>
        /// <param name="sizeOfMidiHeader">size of midi header</param>
        /// <returns>midi out long message</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutLongMsg(int handle,
            IntPtr headerPtr, int sizeOfMidiHeader);

        /// <summary>
        /// Get midi out get device caps
        /// </summary>
        /// <param name="deviceID">device id</param>
        /// <param name="caps">caps</param>
        /// <param name="sizeOfMidiOutCaps">size of midi out caps</param>
        /// <returns>midi out get device caps</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutGetDevCaps(int deviceID,
            ref MidiOutCaps caps, int sizeOfMidiOutCaps);

        /// <summary>
        /// get midi out get nummber devices
        /// </summary>
        /// <returns>midi device numbers</returns>
        [DllImport("winmm.dll")]
        protected static extern int midiOutGetNumDevs();

        /// <summary>
        /// MOM_OPEN
        /// </summary>
        protected const int MOM_OPEN = 0x3C7;

        /// <summary>
        /// MOM_DONE
        /// </summary>
        protected const int MOM_CLOSE = 0x3C8;

        /// <summary>
        /// MOM_DONE
        /// </summary>
        protected const int MOM_DONE = 0x3C9;

        /// <summary>
        /// Generic delegate
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="args">arguments</param>
        protected delegate void GenericDelegate<T>(T args);

        /// <summary>
        /// Represents the method that handles messages from Windows.
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="msg">message</param>
        /// <param name="instance">instance</param>
        /// <param name="param1">parameter 1</param>
        /// <param name="param2">parameter 2</param>
        protected delegate void MidiOutProc(int handle, int msg, int instance, int param1, int param2);

        /// <summary>
        /// For releasing buffers.
        /// </summary>
        protected DelegateQueue delegateQueue = new DelegateQueue();
        
        /// <summary>
        /// Lock object
        /// </summary>
        protected readonly object lockObject = new object();

        /// <summary>
        /// The number of buffers still in the queue.
        /// </summary>
        protected int bufferCount = 0;

        /// <summary>
        /// Builds MidiHeader structures for sending system exclusive messages.
        /// </summary>
        private MidiHeaderBuilder headerBuilder = new MidiHeaderBuilder();

        /// <summary>
        /// Device handle
        /// </summary>
        protected int hndle = 0;        

        /// <summary>
        /// Get output device
        /// </summary>
        /// <param name="deviceID">device id</param>
        public OutputDeviceBase(int deviceID) : base(deviceID)
        {
        }

        /// <summary>
        /// Dispose
        /// </summary>
        ~OutputDeviceBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">is disposing</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                delegateQueue.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message</param>
        public virtual void Send(ChannelMessage message)
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            Send(message.Message);
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message</param>
        public virtual void Send(SysExMessage message)
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            lock(lockObject)
            {
                headerBuilder.InitializeBuffer(message);
                headerBuilder.Build();

                // Prepare system exclusive buffer.
                int result = midiOutPrepareHeader(Handle, headerBuilder.Result, SizeOfMidiHeader);

                // If the system exclusive buffer was prepared successfully.
                if(result == MidiDeviceException.MMSYSERR_NOERROR)
                {
                    bufferCount++;

                    // Send system exclusive message.
                    result = midiOutLongMsg(Handle, headerBuilder.Result, SizeOfMidiHeader);

                    // If the system exclusive message could not be sent.
                    if(result != MidiDeviceException.MMSYSERR_NOERROR)
                    {
                        midiOutUnprepareHeader(Handle, headerBuilder.Result, SizeOfMidiHeader);
                        bufferCount--;
                        headerBuilder.Destroy();

                        // Throw an exception.
                        throw new OutputDeviceException(result);
                    }
                }
                // Else the system exclusive buffer could not be prepared.
                else
                {
                    // Destroy system exclusive buffer.
                    headerBuilder.Destroy();

                    // Throw an exception.
                    throw new OutputDeviceException(result);
                }
            }
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message</param>
        public virtual void Send(SysCommonMessage message)
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            Send(message.Message);
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message</param>
        public virtual void Send(SysRealtimeMessage message)
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            Send(message.Message);
        }

        /// <summary>
        /// Reset
        /// </summary>
        public override void Reset()
        {
            #region Require

            if(IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            lock(lockObject)
            {
                // Reset the OutputDevice.
                int result = midiOutReset(Handle); 

                if(result == MidiDeviceException.MMSYSERR_NOERROR)
                {
                    while(bufferCount > 0)
                    {
                        Monitor.Wait(lockObject);
                    }
                }
                else
                {
                    // Throw an exception.
                    throw new OutputDeviceException(result);
                }                
            }
        }        

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message</param>
        protected void Send(int message)
        {
            lock(lockObject)
            {
                int result = midiOutShortMsg(Handle, message);

                if(result != MidiDeviceException.MMSYSERR_NOERROR)
                {
                    throw new OutputDeviceException(result);
                }
            }
        }

        /// <summary>
        /// Get device capabilities
        /// </summary>
        /// <param name="deviceID">device id</param>
        /// <returns>device capabilities</returns>
        public static MidiOutCaps GetDeviceCapabilities(int deviceID)
        {
            MidiOutCaps caps = new MidiOutCaps();

            // Get the device's capabilities.
            int result = midiOutGetDevCaps(deviceID, ref caps, Marshal.SizeOf(caps));

            // If the capabilities could not be retrieved.
            if(result != MidiDeviceException.MMSYSERR_NOERROR)
            {
                // Throw an exception.
                throw new OutputDeviceException(result);
            }

            return caps;
        }

        /// <summary>
        /// Handle windows messages
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="msg">message</param>
        /// <param name="instance">instance</param>
        /// <param name="param1">param 1</param>
        /// <param name="param2">param 2</param>
        protected virtual void HandleMessage(int handle, int msg, int instance, int param1, int param2)
        {
            if(msg == MOM_OPEN)
            {
            }
            else if(msg == MOM_CLOSE)
            {
            }
            else if(msg == MOM_DONE)
            {
                delegateQueue.Post(ReleaseBuffer, new IntPtr(param1));
            }
        }

        /// <summary>
        /// Release buffer
        /// </summary>
        /// <param name="state">state</param>
        private void ReleaseBuffer(object state)
        {
            lock(lockObject)
            {
                IntPtr headerPtr = (IntPtr)state;

                // Unprepare the buffer.
                int result = midiOutUnprepareHeader(Handle, headerPtr, SizeOfMidiHeader);

                if(result != MidiDeviceException.MMSYSERR_NOERROR)
                {
                    Exception ex = new OutputDeviceException(result);

                    OnError(new ErrorEventArgs(ex));
                }

                // Release the buffer resources.
                headerBuilder.Destroy(headerPtr);

                bufferCount--;

                Monitor.Pulse(lockObject);

                Debug.Assert(bufferCount >= 0);                
            }
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

            lock(lockObject)
            {
                Close();          
            }
        }
        
        /// <summary>
        /// Handle
        /// </summary>
        public override int Handle
        {
            get
            {
                return hndle;
            }
        }

        /// <summary>
        /// Device count
        /// </summary>
        public static int DeviceCount
        {
            get
            {
                return midiOutGetNumDevs();
            }
        }        
    }
}
