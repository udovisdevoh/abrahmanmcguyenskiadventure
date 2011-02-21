using System;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Windows.Forms;

namespace AbrahmanAdventure.waves
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
        #region Constants
        private const int screenWidth = 640;

        private const int screenHeight = 480;
        
        private const int targetFps = 60;
        
        private const bool isFullScreen = false;
        #endregion
        
		#region Public Methods and event handlers
        public void Start()
        {
        	Events.TargetFps = targetFps;
            Events.Tick += Update;
            Events.KeyboardDown += OnKeyboardDown;
            Events.KeyboardUp += OnKeyboardUp;
            Events.Run();	
        }
        
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
            
            gameViewer.Update(gameModel);
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