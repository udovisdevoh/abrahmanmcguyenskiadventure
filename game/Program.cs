using System;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Windows.Forms;
using AbrahmanAdventure.waves;

namespace AbrahmanAdventure
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
        #region Constants
        public const int screenWidth = 640;

        public const int screenHeight = 480;

        public const int targetFps = 60;
        
        public const bool isFullScreen = false;
        #endregion

        #region Fields and parts
        private IWave wave;

        private WaveViewer waveViewer;

        private double offsetX;

        private double offsetY;

        private DateTime previousDateTime = DateTime.Now;
        #endregion

        #region Constructor
        public Program()
        {
            waveViewer = new WaveViewer();
        }
        #endregion

        #region Public Methods and event handlers        
        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
        	#warning Implement OnKeyboardDown
        }

        public void OnKeyboardUp(object sender, KeyboardEventArgs args)
        {
            #warning Implement OnKeyboardUp
        }
        
        public void Update(object sender, TickEventArgs args)
        {
            //We process the time multiplicator
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 16.0;
            previousDateTime = DateTime.Now;
            
            waveViewer.Update(wave);
        }

        public void Start()
        {
            Events.TargetFps = targetFps;
            Events.Tick += Update;
            Events.KeyboardDown += OnKeyboardDown;
            Events.KeyboardUp += OnKeyboardUp;
            Events.Run();
        }
		#endregion
		
        #region Static
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }
        #endregion
	}
}