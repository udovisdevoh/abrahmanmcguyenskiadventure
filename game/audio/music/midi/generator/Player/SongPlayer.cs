using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AbrahmanAdventure.audio.midi;
using AbrahmanAdventure.audio.midi.generator;
using AbrahmanAdventure.hud;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Facade to play midi content
    /// </summary>
    public static class SongPlayer
    {
        #region Fields and parts
        /// <summary>
        /// Song player
        /// </summary>
        private static RiffPackPlayer riffPackPlayer;
        
        /// <summary>
        /// Midi output device
        /// </summary>
        private static OutputDevice outputDevice = new OutputDevice(0);

        /// <summary>
        /// IRiff to play
        /// </summary>
        private static IRiff iRiff = null;

        /// <summary>
        /// Playing thread
        /// </summary>
        private static Thread playingThread = null;

        /// <summary>
        /// Midi volume
        /// </summary>
        private static int volume;
        #endregion

        #region Event
        /// <summary>
        /// When playing note
        /// </summary>
        public static event EventHandler OnNoteOn;

        /// <summary>
        /// When stopping note
        /// </summary>
        public static event EventHandler OnNoteOff;

        /// <summary>
        /// When black note time has passed
        /// </summary>
        public static event EventHandler OnBlackNoteTimeElapsed;
        #endregion

        #region Constructor
        /// <summary>
        /// Build midi player
        /// </summary>
        static SongPlayer()
        {
            volume = PersistantConfig.MusicVolume;
            riffPackPlayer = new RiffPackPlayer();
            riffPackPlayer.OnNoteOn += NoteOnHandler;
            riffPackPlayer.OnNoteOff += NoteOffHandler;
            riffPackPlayer.OnBlackNoteTimeElapsed += BlackNoteTimeElapsedHandler;
        }
        #endregion

        #region Public Methods
        internal static void PlayAsync()
        {
            if (IsPlaying || (playingThread != null && playingThread.IsAlive))
                return;
            
            ClearEventHandlers();
            playingThread = new Thread(PlaySync);
            playingThread.IsBackground = true;
            //playingThread.Priority = ThreadPriority.BelowNormal;
            playingThread.Start();
        }

        internal static void StopSync()
        {
            if (!IsPlaying)
                return;

            riffPackPlayer.Stop();
            while (riffPackPlayer.IsPlaying)
            {
                Thread.Sleep(10);
            }

            while (playingThread != null && playingThread.IsAlive)
            {
                Thread.Sleep(10);
                playingThread.Abort();
                playingThread = null;
            }
        }

        /// <summary>
        /// Remove event listeners
        /// </summary>
        public static void ClearEventHandlers()
        {
            OnNoteOn = null;
            OnNoteOff = null;
            OnBlackNoteTimeElapsed = null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Play a riff or a riff pack
        /// </summary>
        /// <param name="stateInfo">when method is started by threadPool</param>
        private static void PlaySync(object stateInfo)
        {
            if (iRiff == null)
                throw new MidiPlayerException("Must set IRiff before playing");


            if (iRiff is Riff)
            {
                RiffPack riffPack = new RiffPack(iRiff);
                riffPackPlayer.Play(riffPack, outputDevice);
            }
            else if (iRiff is RiffPack)
            {
                RiffPack riffPack = (RiffPack)iRiff;
                riffPackPlayer.Play(riffPack, outputDevice);
            }
            else
            {
                throw new Exception("Unrecognized IRiff implementation");
            }
        }
        #endregion

        #region Event Handlers
        private static void NoteOnHandler(object source, EventArgs e)
        {
            if (OnNoteOn != null) OnNoteOn(source, e);
        }

        private static void NoteOffHandler(object source, EventArgs e)
        {
            if (OnNoteOff != null) OnNoteOff(source, e);
        }

        private static void BlackNoteTimeElapsedHandler(object source, EventArgs e)
        {
            if (OnBlackNoteTimeElapsed != null) OnBlackNoteTimeElapsed(source, e);
        }
        #endregion

        #region Properties
        /// <summary>
        /// IRiff to play
        /// </summary>
        public static IRiff IRiff
        {
            get { return iRiff; }
            set { iRiff = value; }
        }

        /// <summary>
        /// Whether the player is playing
        /// </summary>
        public static bool IsPlaying
        {
            get { return riffPackPlayer.IsPlaying; }
        }

        /// <summary>
        /// Midi volume
        /// </summary>
        public static int Volume
        {
            get { return volume; }
            set
            {
                if (value >= 0 && value <= 10)
                    volume = value;
            }
        }
        #endregion
    }
}