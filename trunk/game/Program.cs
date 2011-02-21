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

        public const int tileColumnCount = 20;

        public const int waveResolution = 1;
        #endregion

        #region Fields and parts
        public static int tileSize = screenWidth / tileColumnCount;

        public static double viewOffsetX = 0.0;

        public static double viewOffsetY = 0.0;

        public static double zoomRatio = 1.0;

        private UserInput userInput;

        private WavePack wave;

        private WaveViewer waveViewer;

        private DateTime previousDateTime = DateTime.Now;
        #endregion

        #region Constructor
        public Program()
        {
            userInput = new UserInput();
            wave = new WavePack(new Wave(4, 8, 0, WaveFunctions.Sine));
            wave.Add(new Wave(32, 126.1234, 0, WaveFunctions.Sine));
            wave.Add(new Wave(2, 11.012, 0, WaveFunctions.Sine));
            wave.Add(new Wave(3, 30.012, 0, WaveFunctions.Square));
            wave.Add(new Wave(2.5, 31.112, 0, WaveFunctions.Square));

            Surface mainSurface = Video.SetVideoMode(screenWidth, screenHeight, false, false, isFullScreen, true);

            waveViewer = new WaveViewer(mainSurface);
        }
        #endregion

        #region Public Methods and event handlers        
        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            if (args.Key == Key.Escape)
                Events.QuitApplication();
            else if (args.Key == Key.Plus || args.Key == Key.Equals)
                zoomRatio *= 1.25;
            else if (args.Key == Key.Minus)
                zoomRatio *= 0.8;
            else if (args.Key == Key.LeftArrow)
                userInput.isPressLeft = true;
            else if (args.Key == Key.RightArrow)
                userInput.isPressRight = true;
            else if (args.Key == Key.UpArrow)
                userInput.isPressUp = true;
            else if (args.Key == Key.DownArrow)
                userInput.isPressDown = true;
        }

        public void OnKeyboardUp(object sender, KeyboardEventArgs args)
        {
            if (args.Key == Key.LeftArrow)
                userInput.isPressLeft = false;
            else if (args.Key == Key.RightArrow)
                userInput.isPressRight = false;
            else if (args.Key == Key.UpArrow)
                userInput.isPressUp = false;
            else if (args.Key == Key.DownArrow)
                userInput.isPressDown = false;
        }

        public void OnJoystickButtonDown(object sender, JoystickButtonEventArgs args)
        {
            #warning Implement OnJoystickButtonDown
        }

        public void OnJoystickButtonUp(object sender, JoystickButtonEventArgs args)
        {
            #warning Implement OnJoystickButtonUp
        }

        public void OnJoystickHatMotion(object sender, JoystickHatEventArgs args)
        {
            #warning Implement OnJoystickHatMotion
        }

        public void OnJoystickAxisMotion(object sender, JoystickAxisEventArgs args)
        {
            #warning Implement OnJoystickAxisMotion
        }

        public void Update(object sender, TickEventArgs args)
        {
            //We process the time multiplicator
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 16.0;
            previousDateTime = DateTime.Now;

            if (userInput.isPressLeft)
                viewOffsetX -= (timeDelta / 100) / zoomRatio;
            if (userInput.isPressRight)
                viewOffsetX += (timeDelta / 100) / zoomRatio;
            if (userInput.isPressUp)
                viewOffsetY += (timeDelta / 100) / zoomRatio;
            if (userInput.isPressDown)
                viewOffsetY -= (timeDelta / 100) / zoomRatio;

            waveViewer.Update(wave);
        }

        public void Start()
        {
            Events.TargetFps = targetFps;
            Events.Tick += Update;
            Events.KeyboardDown += OnKeyboardDown;
            Events.KeyboardUp += OnKeyboardUp;
            Events.JoystickButtonDown += OnJoystickButtonDown;
            Events.JoystickButtonUp += OnJoystickButtonUp;
            Events.JoystickAxisMotion += OnJoystickAxisMotion;
            Events.JoystickHatMotion += OnJoystickHatMotion;
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