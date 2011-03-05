using System;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Windows.Forms;
using AbrahmanAdventure.level;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.physics;

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

        //public const int targetFps = 60;
        
        public const bool isBindViewOffsetToPlayer = true;
        
        public const bool isFullScreen = false;

        public const bool isHardwareSurface = true;

        public const bool isUseBottomTexture = true;

        public const bool isUseTopTextureThicknessScaling = true;

        public const int tileColumnCount = 20;

        public const int waveResolution = 1;

        public const double zoneWidthScreenCount = 0.025;
        
        public const double collisionDetectionResolution = 0.0625;

        public const int zoneHeightScreenCount = 4;

        public const int bitDepth = 32;

        public const int maxCachedColumnCount = 100;

        public const int spatialHashingBucketWidth = 2;

        public static int tileSize = screenWidth / tileColumnCount;

        public static int totalZoneWidth = (int)(Program.zoneWidthScreenCount * Program.screenWidth);

        public static int totalZoneHeight = Program.zoneHeightScreenCount * Program.screenHeight;

        public static int terrainColumnBufferRightCount = (int)(1.1 / zoneWidthScreenCount);

        public static int terrainColumnBufferLeftCount = (int)(0.1 / zoneWidthScreenCount);

        public static int totalHeightTileCount = totalZoneHeight / tileSize;
        
        public static int tileRowCount = screenHeight / tileSize;
        #endregion

        #region Fields and parts
        private Surface mainSurface;

        private UserInput userInput;

        private Level level;

        private LevelViewer levelViewer;

        private SpriteViewer spriteViewer;

        private JoystickManager joystickManager;

        private DateTime previousDateTime = DateTime.Now;

        private Random random;

        private SpritePopulation spritePopulation;

        private PlayerSprite playerSprite;

        private Physics physics;

        private double viewOffsetX = -(Program.tileColumnCount / 2);

        private double viewOffsetY = 0.0;
        #endregion

        #region Constructor
        public Program()
        {
            physics = new Physics();
            random = new Random();
            joystickManager = new JoystickManager();

            LevelBuilder levelBuilder = new LevelBuilder();
            level = levelBuilder.Build(random);

            userInput = new UserInput();

            spritePopulation = new SpritePopulation();
            playerSprite = new PlayerSprite(0, Program.totalHeightTileCount / -2);
            spritePopulation.Add(playerSprite);

            if (isFullScreen)
                Cursor.Hide();

            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, Program.bitDepth, false, false, isFullScreen, isHardwareSurface);

            levelViewer = new LevelViewer(mainSurface);
            spriteViewer = new SpriteViewer(spritePopulation, mainSurface);
        }
        #endregion

        #region Public Methods and event handlers
        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            joystickManager.IsUseAxes = false;
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
            else if (args.Key == Key.Space)
                userInput.isPressJump = true;
            else if (args.Key == Key.LeftControl)
                userInput.isPressRun = true;
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
            else if (args.Key == Key.Space)
                userInput.isPressJump = false;
            else if (args.Key == Key.LeftControl)
                userInput.isPressRun = false;
        }

        public void OnJoystickButtonDown(object sender, JoystickButtonEventArgs args)
        {
            if (args.Button == 3)
                userInput.isPressRun = true;
            else if (args.Button == 2)
                userInput.isPressJump = true;
        }

        public void OnJoystickButtonUp(object sender, JoystickButtonEventArgs args)
        {
            if (args.Button == 3)
                userInput.isPressRun = false;
            else if (args.Button == 2)
                userInput.isPressJump = false;
        }

        public void OnJoystickHatMotion(object sender, JoystickHatEventArgs args)
        {
            joystickManager.IsUseAxes = false;
            if (args.HatValue == 0)
            {
                userInput.isPressDown = false;
                userInput.isPressUp = false;
                userInput.isPressLeft = false;
                userInput.isPressRight = false;
            }
            else if (args.HatValue == 8)
            {
                userInput.isPressDown = false;
                userInput.isPressUp = false;
                userInput.isPressLeft = true;
                userInput.isPressRight = false;
            }
            else if (args.HatValue == 2)
            {
                userInput.isPressDown = false;
                userInput.isPressUp = false;
                userInput.isPressLeft = false;
                userInput.isPressRight = true;
            }
            else if (args.HatValue == 1)
            {
                userInput.isPressDown = false;
                userInput.isPressUp = true;
                userInput.isPressLeft = false;
                userInput.isPressRight = false;
            }
            else if (args.HatValue == 4)
            {
                userInput.isPressDown = true;
                userInput.isPressUp = false;
                userInput.isPressLeft = false;
                userInput.isPressRight = false;
            }
            else if (args.HatValue == 9)
            {
                userInput.isPressDown = false;
                userInput.isPressUp = true;
                userInput.isPressLeft = true;
                userInput.isPressRight = false;
            }
            else if (args.HatValue == 3)
            {
                userInput.isPressDown = false;
                userInput.isPressUp = true;
                userInput.isPressLeft = false;
                userInput.isPressRight = true;
            }
            else if (args.HatValue == 12)
            {
                userInput.isPressDown = true;
                userInput.isPressUp = false;
                userInput.isPressLeft = false;
                userInput.isPressRight = true;
            }
            else if (args.HatValue == 6)
            {
                userInput.isPressDown = true;
                userInput.isPressUp = false;
                userInput.isPressLeft = true;
                userInput.isPressRight = false;
            }

            joystickManager.IsUseAxes = false;

            #warning Implement OnJoystickHatMotion
        }

        public void OnJoystickAxisMotion(object sender, JoystickAxisEventArgs args)
        {
            joystickManager.IsUseAxes = true;
            joystickManager.DefaultJoystick = joystickManager[args.Device];
        }

        public void Update(object sender, TickEventArgs args)
        {
            //We process the time multiplicator
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 32.0;
            previousDateTime = DateTime.Now;
            playerSprite.IsTryingToJump = false;

            if (joystickManager.IsUseAxes)
                joystickManager.SetInputStateFromAxes(userInput);

            
            if (isBindViewOffsetToPlayer)
            {
                playerSprite.IsRunning = userInput.isPressRun;

                if (userInput.isPressJump)
                {
                    //We manage jumping from one ground to a lower ground
                    if (userInput.isPressDown && !userInput.isPressLeft && !userInput.isPressRight && playerSprite.Ground != null && !playerSprite.IsNeedToJumpAgain && playerSprite.CurrentWalkingSpeed == 0)
                    {
                        playerSprite.YPosition += playerSprite.MaximumWalkingHeight;
                        Ground highestVisibleGroundBelowSprite = physics.GetHighestVisibleGroundBelowSprite(playerSprite, level);
                        if (highestVisibleGroundBelowSprite != null && highestVisibleGroundBelowSprite != playerSprite.Ground)
                        {
                            playerSprite.Ground = null;
                        }
                        else
                        {
                            //Oops, we jumped from the lowest ground. Let's undo the falling
                            playerSprite.YPosition = playerSprite.Ground.TerrainWave[playerSprite.XPosition];
                        }
                    }
                    else
                    {
                        playerSprite.IsTryingToJump = true;
                        physics.StartOrContinueJump(playerSprite, timeDelta);
                    }
                }
                else
                    playerSprite.IsNeedToJumpAgain = false;

                
                playerSprite.IsCrouch = userInput.isPressDown;


                if (userInput.isPressLeft && !userInput.isPressRight && !playerSprite.IsCrouch)
                {
                    if (playerSprite.IsTryingToWalkRight)
                        playerSprite.CurrentWalkingSpeed = 0;

                    playerSprite.WalkingCycle.Increment(timeDelta);
                    playerSprite.IsTryingToWalk = true;
                    playerSprite.IsTryingToWalkRight = false;
                }
                else if (!userInput.isPressLeft && userInput.isPressRight && !playerSprite.IsCrouch)
                {
                    if (!playerSprite.IsTryingToWalkRight)
                        playerSprite.CurrentWalkingSpeed = 0;

                    playerSprite.WalkingCycle.Increment(timeDelta * playerSprite.CurrentWalkingSpeed);
                    playerSprite.IsTryingToWalk = true;
                    playerSprite.IsTryingToWalkRight = true;
                }
                else
                {
                    playerSprite.WalkingCycle.Reset();
                    playerSprite.IsTryingToWalk = false;
                    playerSprite.CurrentWalkingSpeed -= playerSprite.WalkingAcceleration;
                    playerSprite.CurrentWalkingSpeed = Math.Max(0, playerSprite.CurrentWalkingSpeed);
                }


                if (playerSprite.IsTryingToWalk || playerSprite.CurrentWalkingSpeed > 0)
                    physics.TryMakeWalk(playerSprite, playerSprite.IsTryingToWalk, playerSprite.IsTryingToWalkRight, timeDelta, level);
		        
                viewOffsetY = playerSprite.YPosition - (double)Program.tileRowCount / 2.0 - playerSprite.Height / 2.0;
            	viewOffsetX = playerSprite.XPosition - (double)Program.tileColumnCount / 2.0;
            }
			else
			{                   
		        if (userInput.isPressLeft && !userInput.isPressRight)
		            viewOffsetX -= timeDelta;
		        else if (userInput.isPressRight && !userInput.isPressLeft)
		            viewOffsetX += timeDelta;
            
	            if (userInput.isPressUp && !userInput.isPressDown)
	            {
	                viewOffsetY -= timeDelta;                
	                viewOffsetY = Math.Max(viewOffsetY, totalHeightTileCount / -2);
	            }
	            else if (userInput.isPressDown && !userInput.isPressUp)
	            {
	                viewOffsetY += timeDelta;
	                viewOffsetY = Math.Min(viewOffsetY, totalHeightTileCount / 2 - tileRowCount);
	            }
			}


            physics.Update(playerSprite, level, timeDelta);

            levelViewer.Update(level, viewOffsetX, viewOffsetY);
            spriteViewer.Update(viewOffsetX, viewOffsetY);
            mainSurface.Update();
        }

        public void Start()
        {
            Events.Tick += Update;
            Events.KeyboardDown += OnKeyboardDown;
            Events.KeyboardUp += OnKeyboardUp;
            Events.JoystickButtonDown += OnJoystickButtonDown;
            Events.JoystickButtonUp += OnJoystickButtonUp;
            Events.JoystickHatMotion += OnJoystickHatMotion;
            Events.JoystickAxisMotion += OnJoystickAxisMotion;
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