using System;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Windows.Forms;
using AbrahmanAdventure.waves;
using AbrahmanAdventure.level;

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

        public const int zoneWidthScreenCount = 1;

        public const int zoneHeightScreenCount = 6;

        public const int bitDepth = 16;
        #endregion

        #region Fields and parts
        public static int tileSize = screenWidth / tileColumnCount;

        public static double viewOffsetX = 0.0;

        public static double viewOffsetY = 0.0;

        public static int totalZoneWidth = Program.zoneWidthScreenCount * Program.screenWidth;

        public static int totalZoneHeight = Program.zoneHeightScreenCount * Program.screenHeight;

        private UserInput userInput;

        private Level level;

        private LevelViewer levelViewer;

        private DateTime previousDateTime = DateTime.Now;

        private Random random;
        #endregion

        #region Constructor
        public Program()
        {
            random = new Random();

            LevelBuilder levelBuilder = new LevelBuilder();
            level = levelBuilder.Build(random);

            userInput = new UserInput();

            Surface mainSurface = Video.SetVideoMode(screenWidth, screenHeight, Program.bitDepth, false, false, isFullScreen,true);

            levelViewer = new LevelViewer(mainSurface);
        }
        #endregion

        #region Public Methods and event handlers        
        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            if (args.Key == Key.Escape)
                Events.QuitApplication();
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


            if (userInput.isPressLeft && !userInput.isPressRight)
                viewOffsetX += timeDelta * 16.0;
            else if (userInput.isPressRight && !userInput.isPressLeft)
                viewOffsetX -= timeDelta * 16.0;

            if (userInput.isPressUp && !userInput.isPressDown)
                viewOffsetY += timeDelta * 16.0;
            else if (userInput.isPressDown && !userInput.isPressUp)
                viewOffsetY -= timeDelta * 16.0;

            levelViewer.Update(level);
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