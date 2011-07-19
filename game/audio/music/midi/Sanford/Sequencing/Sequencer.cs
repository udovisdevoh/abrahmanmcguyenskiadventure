using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// Midi Sequencer
    /// </summary>
    public class Sequencer : IComponent
    {
        /// <summary>
        /// Sequence
        /// </summary>
        private Sequence sequence = null;

        /// <summary>
        /// Enumerators
        /// </summary>
        private List<IEnumerator<int>> enumerators = new List<IEnumerator<int>>();

        /// <summary>
        /// Dispatcher
        /// </summary>
        private MessageDispatcher dispatcher = new MessageDispatcher();

        /// <summary>
        /// Chaser
        /// </summary>
        private ChannelChaser chaser = new ChannelChaser();

        /// <summary>
        /// Stopper
        /// </summary>
        private ChannelStopper stopper = new ChannelStopper();

        /// <summary>
        /// Midi internal clock
        /// </summary>
        private MidiInternalClock clock = new MidiInternalClock();

        /// <summary>
        /// Tracks playing count
        /// </summary>
        private int tracksPlayingCount;

        /// <summary>
        /// Lock object
        /// </summary>
        private readonly object lockObject = new object();

        /// <summary>
        /// Is playing
        /// </summary>
        private bool playing = false;

        /// <summary>
        /// Is Disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Site
        /// </summary>
        private ISite site = null;

        #region Events
        /// <summary>
        /// Playing completed
        /// </summary>
        public event EventHandler PlayingCompleted;

        /// <summary>
        /// Channed message played
        /// </summary>
        public event EventHandler<ChannelMessageEventArgs> ChannelMessagePlayed
        {
            add
            {
                dispatcher.ChannelMessageDispatched += value;
            }
            remove
            {
                dispatcher.ChannelMessageDispatched -= value;
            }
        }

        /// <summary>
        /// Sys ex message played
        /// </summary>
        public event EventHandler<SysExMessageEventArgs> SysExMessagePlayed
        {
            add
            {
                dispatcher.SysExMessageDispatched += value;
            }
            remove
            {
                dispatcher.SysExMessageDispatched -= value;
            }
        }

        /// <summary>
        /// Meta message played
        /// </summary>
        public event EventHandler<MetaMessageEventArgs> MetaMessagePlayed
        {
            add
            {
                dispatcher.MetaMessageDispatched += value;
            }
            remove
            {
                dispatcher.MetaMessageDispatched -= value;
            }
        }

        /// <summary>
        /// When chased
        /// </summary>
        public event EventHandler<ChasedEventArgs> Chased
        {
            add
            {
                chaser.Chased += value;
            }
            remove
            {
                chaser.Chased -= value;
            }
        }

        /// <summary>
        /// When stopped
        /// </summary>
        public event EventHandler<StoppedEventArgs> Stopped
        {
            add
            {
                stopper.Stopped += value;
            }
            remove
            {
                stopper.Stopped -= value;
            }
        }
        #endregion

        /// <summary>
        /// Sequencer
        /// </summary>
        public Sequencer()
        {
            dispatcher.MetaMessageDispatched += delegate(object sender, MetaMessageEventArgs e)
            {
                if(e.Message.MetaType == MetaType.EndOfTrack)
                {
                    tracksPlayingCount--;

                    if(tracksPlayingCount == 0)
                    {
                        Stop();

                        OnPlayingCompleted(EventArgs.Empty);
                    }
                }
                else
                {
                    clock.Process(e.Message);
                }
            };

            dispatcher.ChannelMessageDispatched += delegate(object sender, ChannelMessageEventArgs e)
            {
                stopper.Process(e.Message);
            };

            clock.Tick += delegate(object sender, EventArgs e)
            {
                lock(lockObject)
                {
                    if(!playing)
                    {
                        return;
                    }

                    foreach(IEnumerator<int> enumerator in enumerators)
                    {
                        enumerator.MoveNext();
                    }
                }
            };
        }

        /// <summary>
        /// Dispose
        /// </summary>
        ~Sequencer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                lock(lockObject)
                {
                    Stop();

                    clock.Dispose();

                    disposed = true;

                    GC.SuppressFinalize(this);
                }
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion           

            lock(lockObject)
            {
                Stop();

                Position = 0;

                Continue();
            }
        }

        /// <summary>
        /// Continue
        /// </summary>
        public void Continue()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            #region Guard

            if(Sequence == null)
            {
                return;
            }

            #endregion

            lock(lockObject)
            {
                Stop();

                enumerators.Clear();

                foreach(Track t in Sequence)
                {
                    enumerators.Add(t.TickIterator(Position, chaser, dispatcher).GetEnumerator());
                }

                tracksPlayingCount = Sequence.Count;

                playing = true;
                clock.Ppqn = sequence.Division;
                clock.Continue();
            }
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            #region Require

            if(disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }

            #endregion

            lock(lockObject)
            {
                #region Guard

                if(!playing)
                {
                    return;
                }

                #endregion

                playing = false;
                clock.Stop();
                stopper.AllSoundOff();
            }
        }

        /// <summary>
        /// When playing is complete
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnPlayingCompleted(EventArgs e)
        {
            EventHandler handler = PlayingCompleted;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// When is disposed
        /// </summary>
        /// <param name="e">event arguments</param>
        protected virtual void OnDisposed(EventArgs e)
        {
            EventHandler handler = Disposed;

            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Position
        /// </summary>
        public int Position
        {
            get
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }

                #endregion

                return clock.Ticks;
            }
            set
            {
                #region Require

                if(disposed)
                {
                    throw new ObjectDisposedException(this.GetType().Name);
                }
                else if(value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                #endregion

                bool wasPlaying;

                lock(lockObject)
                {
                    wasPlaying = playing;

                    Stop();

                    clock.SetTicks(value);
                }

                lock(lockObject)
                {
                    if(wasPlaying)
                    {
                        Continue();
                    }
                }
            }
        }

        /// <summary>
        /// Sequence
        /// </summary>
        public Sequence Sequence
        {
            get
            {
                return sequence;
            }
            set
            {
                #region Require

                if(value == null)
                {
                    throw new ArgumentNullException();
                }
                else if(value.SequenceType == SequenceType.Smpte)
                {
                    throw new NotSupportedException();
                }

                #endregion

                lock(lockObject)
                {
                    Stop();
                    sequence = value;
                }
            }
        }

        #region IComponent Members
        /// <summary>
        /// When disposed
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Site
        /// </summary>
        public ISite Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            #region Guard

            if(disposed)
            {
                return;
            }

            #endregion

            Dispose(true);
        }
        #endregion
    }
}
