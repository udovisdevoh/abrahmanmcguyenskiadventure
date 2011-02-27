using System;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Windows.Forms;
using AbrahmanAdventure.level;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
        #region Constants and static variables
        public const int screenWidth = 640;

        public const int screenHeight = 480;

        public const int targetFps = 60;
        
        public const bool isFullScreen = false;

        public const bool isHardwareSurface = true;

        public const bool isUseBottomTexture = true;

        public const bool isUseTopTextureThicknessScaling = true;

        public const int tileColumnCount = 20;

        public const int waveResolution = 1;

        public const double zoneWidthScreenCount = 0.025;

        public const int zoneHeightScreenCount = 4;

        public const int bitDepth = 32;

        public const int maxCachedColumnCount = 100;

        public const int spatialHashingBucketWidth = 2;

        public static int tileSize = screenWidth / tileColumnCount;

        public static int totalZoneWidth = (int)(Program.zoneWidthScreenCount * Program.screenWidth);

        public static int totalZoneHeight = Program.zoneHeightScreenCount * Program.screenHeight;

        public static int terrainColumnBufferRightCount = (int)(1.1 / zoneWidthScreenCount);

        public static int terrainColumnBufferLeftCount = (int)(0.1 / zoneWidthScreenCount);
        #endregion

        #region Fields and parts
        private Surface mainSurface;

        private UserInput userInput;

        private Level level;

        private LevelViewer levelViewer;

        private SpriteViewer spriteViewer;

        private DateTime previousDateTime = DateTime.Now;

        private Random random;

        private SpritePopulation spritePopulation;

        private PlayerSprite playerSprite;

        private double viewOffsetX = -(Program.tileColumnCount / 2);

        private double viewOffsetY = 0.0;
        #endregion

        #region Constructor
        public Program()
        {
            random = new Random();

            LevelBuilder levelBuilder = new LevelBuilder();
            level = levelBuilder.Build(random);

            userInput = new UserInput();

            spritePopulation = new SpritePopulation();
            playerSprite = new PlayerSprite(0, 5);
            spritePopulation.Add(playerSprite);

            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, Program.bitDepth, false, false, isFullScreen, isHardwareSurface);

            levelViewer = new LevelViewer(mainSurface);
            spriteViewer = new SpriteViewer(spritePopulation, mainSurface);
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
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 32.0;
            previousDateTime = DateTime.Now;


            if (userInput.isPressLeft && !userInput.isPressRight)
                viewOffsetX -= timeDelta;
            else if (userInput.isPressRight && !userInput.isPressLeft)
                viewOffsetX += timeDelta;

            if (userInput.isPressUp && !userInput.isPressDown)
            {
                viewOffsetY -= timeDelta;
                viewOffsetY = Math.Min(viewOffsetY, totalZoneHeight - screenHeight);
            }
            else if (userInput.isPressDown && !userInput.isPressUp)
            {
                viewOffsetY += timeDelta;
                viewOffsetY = Math.Max(viewOffsetY, -totalZoneHeight + screenHeight);
            }

            levelViewer.Update(level, viewOffsetX, viewOffsetY);
            spriteViewer.Update(viewOffsetX, viewOffsetY);
            mainSurface.Update();
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