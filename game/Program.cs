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
       
        public const bool isBindViewOffsetToPlayer = true;
        
        public const bool isFullScreen = false;

        public const bool isHardwareSurface = true;

        public const bool isUseBottomTexture = true;

        public const bool isAlwaysUseBottomTexture = false;

        public const bool isUseTopTextureThicknessScaling = true;

        public const int tileColumnCount = 20;

        public const int waveResolution = 1;

        public const int zoneHeightScreenCount = 4;

        public const int bitDepth = 32;

        public const int maxCachedColumnCount = 100;

        public const int spatialHashingBucketWidth = 2;
        
        public const double zoneWidthScreenCount = 0.025;
        
        public const double collisionDetectionResolution = 0.0625;
        
        public const double holeHeight = 100.0;

        public static int tileSize = screenWidth / tileColumnCount;

        public static int totalZoneWidth = (int)(Program.zoneWidthScreenCount * Program.screenWidth);

        public static int totalZoneHeight = Program.zoneHeightScreenCount * Program.screenHeight;

        public static int terrainColumnBufferRightCount = (int)(1.1 / zoneWidthScreenCount);

        public static int terrainColumnBufferLeftCount = (int)(0.1 / zoneWidthScreenCount);

        public static int totalHeightTileCount = totalZoneHeight / tileSize;
        
        public static int tileRowCount = screenHeight / tileSize;

        public static double maxViewOffsetY = totalHeightTileCount / 2.0 - (double)tileRowCount;
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

            level = new Level(random);

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

        #region Event handlers
        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            joystickManager.DefaultJoystickForRealAxes = null;
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
                userInput.isPressAttack = true;
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
                userInput.isPressAttack = false;
        }

        public void OnJoystickButtonDown(object sender, JoystickButtonEventArgs args)
        {
            if (args.Button == 3)
                userInput.isPressAttack = true;
            else if (args.Button == 2)
                userInput.isPressJump = true;
        }

        public void OnJoystickButtonUp(object sender, JoystickButtonEventArgs args)
        {
            if (args.Button == 3)
                userInput.isPressAttack = false;
            else if (args.Button == 2)
                userInput.isPressJump = false;
        }

        public void OnJoystickHatMotion(object sender, JoystickHatEventArgs args)
        {
            joystickManager.DefaultJoystickForRealAxes = null;
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

            joystickManager.DefaultJoystickForRealAxes = null;
        }

        public void OnJoystickAxisMotion(object sender, JoystickAxisEventArgs args)
        {
            joystickManager.DefaultJoystickForRealAxes = joystickManager[args.Device];
        }
        #endregion

        #region Public Methods
        public void Update(object sender, TickEventArgs args)
        {
        	#warning Textures: make sure multiplied (x * y) doesn't multiply negative number with positive numbers
        	#warning for transparent grounds and maybe for all grounds, must use texture's thickness for collision detection (should not block if player is under texture) (consider crouching height too)
            #warning ?Must prevent sprite to suicide by jumping torward the lowest ground under the ground, over the secondary texture or color
        	#warning Create decorations (columns, trees)
        	#warning Create paralax effect (rendered map, decorations)
            #warning Must allow user to setup input (keyboard / joystick) config
            #warning Must prevent sprite from faling on the tip of a sharp surface and get stucked on it, or half on a clif and stucked on it            

            //We process the time multiplicator
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 32.0;
            previousDateTime = DateTime.Now;

            if (joystickManager.DefaultJoystickForRealAxes != null)
                joystickManager.SetInputStateFromAxes(userInput);

            #region We manage the death logic
            if (!playerSprite.IsAlive)
            {
                //levelViewer.ClearCache();
                //level = new Level(random);
                playerSprite.XPosition = 0;
                playerSprite.YPosition = Program.totalHeightTileCount / -2;
                playerSprite.IsAlive = true;
            }
            #endregion

            #region We manage jumping input logic
            playerSprite.IsTryingToJump = false;
            if (userInput.isPressJump)
            {
                //We manage jumping from one ground to a lower ground
                if (userInput.isPressDown && !userInput.isPressLeft && !userInput.isPressRight && playerSprite.Ground != null && !playerSprite.IsNeedToJumpAgain && playerSprite.CurrentWalkingSpeed == 0)
                {
                    playerSprite.YPosition += playerSprite.MaximumWalkingHeight;
                    Ground highestVisibleGroundBelowSprite = GroundHelper.GetHighestVisibleGroundBelowSprite(playerSprite, level);
                    if (highestVisibleGroundBelowSprite != null && highestVisibleGroundBelowSprite != playerSprite.Ground && highestVisibleGroundBelowSprite[playerSprite.XPosition] < (double)Program.totalHeightTileCount)
                        playerSprite.Ground = null;
                    else //Oops, we jumped from the lowest ground or we jumped from over a hole. Let's undo the falling
                        playerSprite.YPosition = playerSprite.Ground[playerSprite.XPosition];
                }
                else
                {
                    playerSprite.IsTryingToJump = true;
                }
            }
            else
                playerSprite.IsNeedToJumpAgain = false;
            #endregion

            playerSprite.IsCrouch = userInput.isPressDown && !userInput.isPressUp;
            
            playerSprite.IsTryToWalkUp = userInput.isPressUp && !userInput.isPressDown;

            #region We manage walking input logic
            playerSprite.IsRunning = userInput.isPressAttack;
            if (userInput.isPressLeft && !userInput.isPressRight && !userInput.isPressDown)
            {
                #region Walking left
                if (playerSprite.IsTryingToWalkRight)
                    playerSprite.CurrentWalkingSpeed = 0;

                playerSprite.IsTryingToWalk = true;
                playerSprite.IsTryingToWalkRight = false;
                playerSprite.IsTryingToSlide = false;
                #endregion
            }
            else if (!userInput.isPressLeft && userInput.isPressRight && !userInput.isPressDown)
            {
                #region Walking right
                if (!playerSprite.IsTryingToWalkRight)
                    playerSprite.CurrentWalkingSpeed = 0;

                playerSprite.IsTryingToWalk = true;
                playerSprite.IsTryingToWalkRight = true;
                playerSprite.IsTryingToSlide = false;
                #endregion
            }
            else if (userInput.isPressDown && userInput.isPressAttack)
            {
                #region Sliding
                playerSprite.IsTryingToWalk = false;
                if (playerSprite.Ground != null)
                {
                    double rightSlope = Physics.GetSlopeRatio(playerSprite, playerSprite.Ground, Program.collisionDetectionResolution,true);
                    if (rightSlope > 0.125 && (!playerSprite.IsTryingToSlide || playerSprite.IsTryingToWalkRight))
                    {
                        playerSprite.IsTryingToWalk = true;
                        playerSprite.IsTryingToWalkRight = true;
                        playerSprite.IsTryingToSlide = true;
                    }
                    else
                    {
                        double leftSlope = Physics.GetSlopeRatio(playerSprite, playerSprite.Ground, -Program.collisionDetectionResolution,false);
                        if (leftSlope > 0.125 && (!playerSprite.IsTryingToSlide || !playerSprite.IsTryingToWalkRight))
                        {
                            playerSprite.IsTryingToWalk = true;
                            playerSprite.IsTryingToWalkRight = false;
                            playerSprite.IsTryingToSlide = true;
                        }
                    }
                }
                #endregion
            }
            else
            {
                playerSprite.IsTryingToWalk = false;
                playerSprite.IsTryingToSlide = false;
                playerSprite.CurrentWalkingSpeed -= playerSprite.WalkingAcceleration;
                playerSprite.CurrentWalkingSpeed = Math.Max(0, playerSprite.CurrentWalkingSpeed);
            }
            #endregion

            #region We manage attack input logic
            //Attacking logic
            if (userInput.isPressAttack)
            {
                if (!playerSprite.IsNeedToAttackAgain && playerSprite.AttackingCycle.IsReadyToFire)
                {
                    playerSprite.AttackingCycle.Fire();
                    playerSprite.IsNeedToAttackAgain = true;
                }
            }
            else
            {
                playerSprite.IsNeedToAttackAgain = false;
            }
            if (playerSprite.AttackingCycle.IsFired)
                playerSprite.AttackingCycle.Increment(timeDelta);
            if (playerSprite.AttackingCycle.IsFinished && playerSprite.Ground != null)
                playerSprite.AttackingCycle.Reset();
            #endregion

            physics.Update(playerSprite, level, timeDelta);

            #region We position the camera
            viewOffsetX = playerSprite.XPosition - (double)Program.tileColumnCount / 2.0;
            viewOffsetY = playerSprite.YPosition - (double)Program.tileRowCount / 2.0 - playerSprite.Height / 2.0;
            viewOffsetY = Math.Min(viewOffsetY, maxViewOffsetY);
            #endregion


            #region We update the viewers
            levelViewer.Update(level, viewOffsetX, viewOffsetY);
            spriteViewer.Update(viewOffsetX, viewOffsetY);
            mainSurface.Update();
            #endregion
        }
		#endregion
		
        #region Main
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

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }
        #endregion
	}
}