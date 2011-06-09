using System;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using System.Collections.Generic;
using System.Windows.Forms;
using AbrahmanAdventure.level;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.physics;
using AbrahmanAdventure.ai;
using AbrahmanAdventure.hud;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
        #region Constants and static variables
        public const bool isBindViewOffsetToPlayer = true;

        public const bool isFullScreen = false;

        public const bool isShowMenuOnStart = true;

        public const bool isHardwareSurface = true;

        public const bool isUseBottomTexture = true;

        public const bool isAlwaysUseBottomTexture = false;

        public const bool isUseBumpMapLightness = false;

        public const bool isUseTopTextureThicknessScaling = true;

        public const bool isBroadRangeUpdateSprite = true;

        public const bool isEnableCrouchedWalk = false;

        public const int screenWidth = 640;

        public const int screenHeight = 480;

        public const int tileColumnCount = 20;

        public const int waveResolution = 1;

        public const int zoneHeightScreenCount = 4;

        public const int bitDepth = 32;

        public const int maxCachedColumnCount = 100;

        public const int spatialHashingBucketWidth = 2;

        public const int maxPlayerFireBallPerScreen = 2;
        
        public const double zoneWidthScreenCount = 0.025;
        
        public const double collisionDetectionResolution = 0.0625;
        
        public const double holeHeight = 100.0;

        public const double powerUpGrowthTime = 6.0;

        public const double rastaFallingSpeed = 0.07;

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

        private LevelViewer levelViewer;

        private SpriteViewer spriteViewer;

        private HudViewer hudViewer;

        private JoystickManager joystickManager;

        private MonsterAi monsterAi;

        private DateTime previousDateTime = DateTime.Now;

        private Random random;

        private GameState gameState = null;

        private Physics physics;

        private double viewOffsetX = -(Program.tileColumnCount / 2);

        private double viewOffsetY = 0.0;

        private bool isOddFrame = true;

        private bool isShowMenu = isShowMenuOnStart;
        #endregion

        #region Constructor
        public Program()
        {
            physics = new Physics();
            monsterAi = new MonsterAi();
            joystickManager = new JoystickManager();
            userInput = new UserInput();

            random = new Random();


            if (isFullScreen)
                Cursor.Hide();

            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, Program.bitDepth, false, false, isFullScreen, isHardwareSurface);

            levelViewer = new LevelViewer(mainSurface);
            spriteViewer = new SpriteViewer(mainSurface);
            hudViewer = new HudViewer(mainSurface);

            #region Some pre-caching
            SoundManager.PreCache();
            MushroomSprite mushroom = new MushroomSprite(0, 0, random);
            PeyoteSprite peyote = new PeyoteSprite(0, 0, random);
            RastaHatSprite rastaHat = new RastaHatSprite(0, 0, random);
            MusicNoteSprite musicNote = new MusicNoteSprite(0, 0, random);
            WhiskySprite whisky = new WhiskySprite(0, 0, random);
            ExplosionSprite explosion = new ExplosionSprite(0, 0, random);
            HelmetSprite helmet = new HelmetSprite(0, 0, random, true);
            //levelViewer.PreCache(level);
            #endregion
        }
        #endregion

        #region Event handlers
        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            joystickManager.DefaultJoystickForRealAxes = null;
            if (args.Key == Key.Escape)
            {
                if (!isShowMenu || (isShowMenu && gameState != null))
                {
                    GameMenu.Dirthen();
                    isShowMenu = !isShowMenu;
                    previousDateTime = DateTime.Now;//To reset time delta
                }
            }
            else if (args.Key == Key.LeftArrow)
                userInput.isPressLeft = true;
            else if (args.Key == Key.RightArrow)
                userInput.isPressRight = true;
            else if (args.Key == Key.UpArrow)
                userInput.isPressUp = true;
            else if (args.Key == Key.DownArrow)
                userInput.isPressDown = true;
            else if (args.Key == Key.Space || args.Key == Key.Return || args.Key == Key.KeypadEnter)
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
            else if (args.Key == Key.Space || args.Key == Key.Return)
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
            #warning When falling, must not be able to fall and stay on a ground for which the slope is to high to climp (we then pretend it's in the background)
        	#warning Textures: make sure multiplied (x * y) doesn't multiply negative number with positive numbers
        	#warning for transparent grounds and maybe for all grounds, must use texture's thickness for collision detection (should not block if player is under texture) (consider crouching height too)
            #warning ?Must prevent sprite to suicide by jumping torward the lowest ground under the ground, over the secondary texture or color
        	#warning Create decorations (columns, trees)
        	#warning Create paralax effect (rendered map, decorations)
            #warning Must allow user to setup input (keyboard / joystick) config
            #warning Must prevent sprite from faling on the tip of a sharp surface and get stucked on it, or half on a clif and stucked on it            

            if (joystickManager.DefaultJoystickForRealAxes != null)
                joystickManager.SetInputStateFromAxes(userInput);

            if (isShowMenu)
            {
                GameMenu.ParseUserInput(userInput, this);
                GameMenu.ShowMenu(mainSurface);
            }
            else //Main game loop starts here
            {
                if (gameState == null)
                {
                    GC.Collect();
                    GameMenu.ShowLoadingScreen(mainSurface);
                    gameState = new GameState(random, mainSurface);
                    levelViewer.ClearCache();
                    GC.Collect();
                }

                SpritePopulation spritePopulation = gameState.SpritePopulation;
                PlayerSprite playerSprite = gameState.PlayerSprite;
                Level level = gameState.Level;
                HashSet<AbstractSprite> toUpdateSpriteList;
                HashSet<AbstractSprite> visibleSpriteList = spritePopulation.GetVisibleSpriteList(viewOffsetX, viewOffsetY, out toUpdateSpriteList);

                //We process the time multiplicator
                double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 32.0;
                previousDateTime = DateTime.Now;

                isOddFrame = !isOddFrame;

                #region We manage jumping input logic
                playerSprite.IsTryingToJump = false;
                if (userInput.isPressJump)
                {
                    //We manage jumping from one ground to a lower ground
                    if (userInput.isPressDown && !userInput.isPressLeft && !userInput.isPressRight && playerSprite.IGround != null && !playerSprite.IsNeedToJumpAgain && playerSprite.CurrentWalkingSpeed == 0)
                    {
                        playerSprite.YPosition += playerSprite.MaximumWalkingHeight;
                        IGround highestVisibleGroundBelowSprite = IGroundHelper.GetHighestVisibleIGroundBelowSprite(playerSprite, level, visibleSpriteList);

                        if (playerSprite.IGround is Ground && highestVisibleGroundBelowSprite != null && highestVisibleGroundBelowSprite != playerSprite.IGround && highestVisibleGroundBelowSprite[playerSprite.XPosition] < (double)Program.totalHeightTileCount /*&& !IGroundHelper.IsSpriteIGroundHeightStackedOn(playerSprite.IGround, highestVisibleGroundBelowSprite)*/)
                            playerSprite.IGround = null;
                        else //Oops, we jumped from the lowest ground or we jumped from over a hole. Let's undo the falling
                            playerSprite.YPosition = playerSprite.IGround[playerSprite.XPosition];

                        if (playerSprite.IGround == null) //play sound if jump down was a success
                            SoundManager.PlayJumpDownSound();
                    }
                    else
                    {
                        playerSprite.IsTryingToJump = true;
                    }
                }
                else
                    playerSprite.IsNeedToJumpAgain = false;
                #endregion

                playerSprite.IsCrouch = userInput.isPressDown && !userInput.isPressUp && !playerSprite.IsTiny;
                
                playerSprite.IsTryToWalkUp = userInput.isPressUp && !userInput.isPressDown;

                #region We manage attack input logic
                //Attacking logic
                playerSprite.IsTryThrowingBall = false;
                if (userInput.isPressAttack)
                {
                    if (!playerSprite.IsNeedToAttackAgain && playerSprite.AttackingCycle.IsReadyToFire)
                    {
                        if (playerSprite.IsDoped)
                        {
                            playerSprite.IsTryThrowingBall = true;
                        }
                        else
                        {
                            SoundManager.PlayAttemptSound();
                            playerSprite.AttackingCycle.Fire();
                        }
                        playerSprite.IsNeedToAttackAgain = true;
                    }
                }
                else
                {
                    playerSprite.IsNeedToAttackAgain = false;
                }
                if (playerSprite.AttackingCycle.IsFired)
                    playerSprite.AttackingCycle.Increment(timeDelta);
                if (playerSprite.AttackingCycle.IsFinished && playerSprite.IGround != null)
                    playerSprite.AttackingCycle.Reset();
                #endregion

                #region We manage walking input logic
                if (playerSprite.IsAlive)
                {
                    playerSprite.IsRunning = userInput.isPressAttack;
                    if (userInput.isPressLeft && !userInput.isPressRight && (!userInput.isPressDown || Program.isEnableCrouchedWalk))
                    {
                        #region Walking left
                        if (playerSprite.IsTryingToWalkRight)
                            playerSprite.CurrentWalkingSpeed = 0;

                        playerSprite.IsTryingToWalk = true;
                        playerSprite.IsTryingToWalkRight = false;
                        playerSprite.IsTryingToSlide = false;
                        #endregion
                    }
                    else if (!userInput.isPressLeft && userInput.isPressRight && (!userInput.isPressDown || Program.isEnableCrouchedWalk))
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
                        if (playerSprite.IGround != null && !playerSprite.AttackingCycle.IsFired)
                        {
                            double rightSlope = Physics.GetSlopeRatio(playerSprite, playerSprite.IGround, Program.collisionDetectionResolution, true);
                            if (rightSlope > 0.125 && (!playerSprite.IsTryingToSlide || playerSprite.IsTryingToWalkRight))
                            {
                                playerSprite.IsTryingToWalk = true;
                                playerSprite.IsTryingToWalkRight = true;
                                playerSprite.IsTryingToSlide = true;
                            }
                            else
                            {
                                double leftSlope = Physics.GetSlopeRatio(playerSprite, playerSprite.IGround, -Program.collisionDetectionResolution, false);
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
                }
                #endregion

                physics.Update(playerSprite, playerSprite, level, timeDelta, visibleSpriteList, spritePopulation, random);

                foreach (AbstractSprite sprite in toUpdateSpriteList)
                    if (sprite != playerSprite)
                    {
                        physics.Update(sprite, playerSprite, level, timeDelta, visibleSpriteList, spritePopulation, random);
                        if (sprite is MonsterSprite && sprite.IsAlive)
                            monsterAi.Update((MonsterSprite)sprite, playerSprite, level, timeDelta, visibleSpriteList, random);
                    }

                #region We position the camera
                viewOffsetX = playerSprite.XPosition - (double)Program.tileColumnCount / 2.0;
                viewOffsetY = playerSprite.YPosition - (double)Program.tileRowCount / 2.0 - playerSprite.Height / 2.0;
                viewOffsetY = Math.Min(viewOffsetY, maxViewOffsetY);
                #endregion


                #region We update the viewers
                levelViewer.Update(level, gameState.ColorTheme, gameState.Sky, viewOffsetX, viewOffsetY);
                spriteViewer.Update(viewOffsetX, viewOffsetY, playerSprite, visibleSpriteList, isOddFrame);
                hudViewer.Update(playerSprite.Health);
                #endregion

                //levelViewer.PreCacheNextZoneIfLevelViewerCacheNotFull(level, playerSprite.IsTryingToWalkRight);
            }

            mainSurface.Update();
        }
		#endregion

        #region Properties
        /// <summary>
        /// Game's state
        /// </summary>
        public GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        /// <summary>
        /// Level viewer
        /// </summary>
        public LevelViewer LevelViewer
        {
            get { return levelViewer; }
        }

        /// <summary>
        /// Whether we show menu
        /// </summary>
        public bool IsShowMenu
        {
            get { return isShowMenu; }
            set { isShowMenu = value; }
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