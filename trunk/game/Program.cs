﻿using System;
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

        public const bool isLimitFrameSkip = false;

        public const bool isAlwaysUseBottomTexture = false;

        public const bool isUseBumpMapLightness = false;

        public const bool isUseTopTextureThicknessScaling = true;

        public const bool isBroadRangeUpdateSprite = true;

        public const bool isEnableCrouchedWalk = false;

        public const bool isUseTextureCache = false;

        public const bool isUseWaveValueCache = true;

        public const int screenWidth = 640;

        public const int screenHeight = 480;

        public const int waveResolution = 1;

        public const int zoneHeightScreenCount = 4;

        public const int bitDepth = 32;

        public const int maxCachedColumnCount = 100;

        public const int maxCachedSquareCount = 1000;

        public const int spatialHashingBucketWidth = 2;

        public const int maxPlayerFireBallPerScreen = 2;

        public const int squareZoneTileWidth = 1;

        public const int squareZoneTileHeight = 10;

        public const double zoneWidthScreenCount = 0.025;
        
        public const double collisionDetectionResolution = 0.0625;
        
        public const double holeHeight = 100.0;

        public const double powerUpGrowthTime = 6.0;

        public const double rastaFallingSpeed = 0.07;

        public const double beaverHoleDiameter = 1.0;

        public const double beaverHoleDepth = 0.25;

        public static int tileColumnCount = (int)Math.Round(20.0 / (640.0 / 480.0) * ((double)screenWidth / (double)screenHeight)); //20 for 4/3 screen

        public static int tileSize = screenWidth / tileColumnCount;

        public static int totalZoneWidth = (int)(Program.zoneWidthScreenCount * Program.screenWidth);

        public static int totalZoneHeight = Program.zoneHeightScreenCount * Program.screenHeight;

        public static int terrainColumnBufferRightCount = (int)(1.1 / zoneWidthScreenCount);

        public static int terrainColumnBufferLeftCount = (int)(0.1 / zoneWidthScreenCount);

        public static int totalHeightTileCount = totalZoneHeight / tileSize;
        
        public static int tileRowCount = screenHeight / tileSize;

        public static double maxViewOffsetY = totalHeightTileCount / 2.0 - (double)tileRowCount;

        public static double zoneColumnWidthTileCount = (double)totalZoneWidth / (double)tileSize;
        #endregion

        #region Fields and parts
        private Surface mainSurface;

        private UserInput userInput;

        private ILevelViewer levelViewer;

        private SpriteViewer spriteViewer;

        private HudViewer hudViewer;

        private JoystickManager joystickManager;

        private BeaverManager beaverManager;

        private MonsterAi monsterAi;

        private DateTime previousDateTime = DateTime.Now;

        private Random spriteBehaviorRandom;

        private GameMetaState gameMetaState;

        private GameState gameState = null;

        private Physics physics;

        private double viewOffsetX = -(Program.tileColumnCount / 2);

        private double viewOffsetY = 0.0;

        private int seedNextGameState;

        private bool isOddFrame = true;

        private bool isShowMenu = isShowMenuOnStart;
        #endregion

        #region Constructor
        public Program()
        {
            physics = new Physics();
            monsterAi = new MonsterAi();
            joystickManager = new JoystickManager();
            beaverManager = new BeaverManager();
            userInput = new UserInput();
            gameMetaState = new GameMetaState();

            spriteBehaviorRandom = new Random();
            #warning Put back random seed
            seedNextGameState = new Random().Next();        
            
            if (isFullScreen)
                Cursor.Hide();

            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, Program.bitDepth, false, false, isFullScreen, isHardwareSurface);

            levelViewer = new LevelViewerSquareBased(mainSurface);
            spriteViewer = new SpriteViewer(mainSurface);
            hudViewer = new HudViewer(mainSurface);

            #region Some pre-caching
            SoundManager.PreCache();
            Random spriteCachingRandom = new Random();
            MushroomSprite mushroom = new MushroomSprite(0, 0, spriteCachingRandom);
            PeyoteSprite peyote = new PeyoteSprite(0, 0, spriteCachingRandom);
            RastaHatSprite rastaHat = new RastaHatSprite(0, 0, spriteCachingRandom);
            MusicNoteSprite musicNote = new MusicNoteSprite(0, 0, spriteCachingRandom);
            WhiskySprite whisky = new WhiskySprite(0, 0, spriteCachingRandom);
            ExplosionSprite explosion = new ExplosionSprite(0, 0, spriteCachingRandom);
            HelmetSprite helmet = new HelmetSprite(0, 0, spriteCachingRandom, true);
            BibleSprite bible = new BibleSprite(0, 0, spriteCachingRandom);
            CrystalBallSprite crystalBall = new CrystalBallSprite(0, 0, spriteCachingRandom);
            PillSprite pill = new PillSprite(0, 0, spriteCachingRandom);
            BeaverSprite beaverSprite = new BeaverSprite(0, 0, spriteCachingRandom);
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
                if (!isShowMenu || (isShowMenu && gameState != null) || (isShowMenu && GameMenu.CurrentSubMenu != SubMenu.Main))
                {
                    GameMenu.Dirthen();
                    if (isShowMenu && GameMenu.CurrentSubMenu != SubMenu.Main)
                    {
                        GameMenu.Escape();
                    }
                    else
                    {
                        isShowMenu = !isShowMenu;
                        previousDateTime = DateTime.Now;//To reset time delta
                    }                 
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
            else if (args.Key == Key.LeftControl || args.Key == Key.RightControl)
                userInput.isPressAttack = true;
            else if (args.Key == Key.LeftAlt || args.Key == Key.RightAlt)
                userInput.isPressLeaveBeaver = true;
            else if (args.Key == Key.PageUp)
                userInput.isPressPageUp = true;
            else if (args.Key == Key.PageDown)
                userInput.isPressPageDown = true;
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
            else if (args.Key == Key.Space || args.Key == Key.Return || args.Key == Key.KeypadEnter)
                userInput.isPressJump = false;
            else if (args.Key == Key.LeftControl || args.Key == Key.RightControl)
                userInput.isPressAttack = false;
            else if (args.Key == Key.LeftAlt || args.Key == Key.RightAlt)
                userInput.isPressLeaveBeaver = false;
            else if (args.Key == Key.PageUp)
                userInput.isPressPageUp = false;
            else if (args.Key == Key.PageDown)
                userInput.isPressPageDown = false;
        }

        public void OnJoystickButtonDown(object sender, JoystickButtonEventArgs args)
        {
            if (isShowMenu && GameMenu.CurrentSubMenu == SubMenu.Controller)
            {
                if (GameMenu.IsWaitingForJumpButtonRemap)
                {
                    userInput.jumpButton = args.Button;
                    GameMenu.IsWaitingForJumpButtonRemap = false;
                    GameMenu.Dirthen();
                    return;
                }
                else if (GameMenu.IsWaitingForAttackButtonRemap)
                {
                    userInput.attackButton = args.Button;
                    GameMenu.IsWaitingForAttackButtonRemap = false;
                    GameMenu.Dirthen();
                    return;
                }
                else if (GameMenu.IsWaitingForLeaveBeaverButtonRemap)
                {
                    userInput.leaveBeaverButton = args.Button;
                    GameMenu.IsWaitingForLeaveBeaverButtonRemap = false;
                    GameMenu.Dirthen();
                    return;
                }
            }

            if (args.Button == userInput.attackButton)
                userInput.isPressAttack = true;
            else if (args.Button == userInput.jumpButton)
                userInput.isPressJump = true;
            else if (args.Button == userInput.leaveBeaverButton)
                userInput.isPressLeaveBeaver = true;
        }

        public void OnJoystickButtonUp(object sender, JoystickButtonEventArgs args)
        {
            if (args.Button == userInput.attackButton)
                userInput.isPressAttack = false;
            else if (args.Button == userInput.jumpButton)
                userInput.isPressJump = false;
            else if (args.Button == userInput.leaveBeaverButton)
                userInput.isPressLeaveBeaver = false;
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
                GameMenu.ShowMenu(mainSurface, userInput);
            }
            else //Main game loop starts here
            {
                if (gameState == null || gameState.IsExpired)
                {
                    #region We regenerate game state because it is nonexistant or expired (changing environment)
                    GC.Collect();
                    SurfaceSizeCache.Clear();
                    if (gameState != null)
                    {
                        SoundManager.PlayVortexInSound();
                        gameMetaState.PreviousSeed = gameState.Seed;
                        gameMetaState.GetInfoFromPlayer(gameState.PlayerSprite);
                    }
                    gameState = new GameState(seedNextGameState, mainSurface);
                    gameMetaState.ApplyPlayerInfoToSprite(gameState.PlayerSprite);
                    List<int> listWarpBackSeed;
                    if (gameMetaState.TryGetWarpBackTargetSeed(gameState.Seed, out listWarpBackSeed))
                        gameState.AddWarpBackVortexList(listWarpBackSeed);
                    if (gameMetaState.PreviousSeed != -1)
                    {
                        gameState.MovePlayerToVortexGoingToSeed(gameMetaState.PreviousSeed);
                        SoundManager.PlayVortexOutSound();
                        gameState.PlayerSprite.FromVortexCycle.Fire();
                        gameState.PlayerSprite.IsTryToWalkUp = false;
                        gameState.PlayerSprite.IsNeedToAttackAgain = true;
                        gameState.PlayerSprite.IsNeedToJumpAgain = true;
                    }
                    levelViewer.ClearCache();
                    GC.Collect();
                    #endregion
                }

                SpritePopulation spritePopulation = gameState.SpritePopulation;
                PlayerSprite playerSprite = gameState.PlayerSprite;
                Level level = gameState.Level;
                HashSet<AbstractSprite> toUpdateSpriteList;
                HashSet<AbstractSprite> visibleSpriteList = spritePopulation.GetVisibleSpriteList(viewOffsetX, viewOffsetY, out toUpdateSpriteList);

                //We process the time multiplicator
                double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 32.0;
                if (isLimitFrameSkip)
                    timeDelta = Math.Min(1.0, timeDelta);

                previousDateTime = DateTime.Now;

                isOddFrame = !isOddFrame;

                #region We manage jumping input logic
                playerSprite.IsTryingToJump = false;
                if (userInput.isPressJump || userInput.isPressLeaveBeaver)
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

                playerSprite.IsCrouch = userInput.isPressDown && !userInput.isPressUp && (!playerSprite.IsTiny || playerSprite.IsBeaver);

                playerSprite.IsTryToWalkUp = userInput.isPressUp && !userInput.isPressDown && !userInput.isPressLeft && !userInput.isPressRight;

                #region We manage the "beaver digs ground" logic
                playerSprite.IsTryDigGround = playerSprite.IsCrouch && userInput.isPressAttack && playerSprite.IsBeaver && !playerSprite.IsNeedToAttackAgain;
                #endregion

                #region We manage attack input logic
                //Attacking logic
                playerSprite.IsTryThrowingBall = false;
                if (userInput.isPressAttack)
                {
                    if (!playerSprite.IsNeedToAttackAgain && playerSprite.AttackingCycle.IsReadyToFire)
                    {
                        if (playerSprite.IsDoped && !playerSprite.IsBeaver)
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
                        if (playerSprite.IGround != null && !playerSprite.AttackingCycle.IsFired && !playerSprite.IsBeaver)
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

                #region We manage the "leave beaver" input logic
                if (userInput.isPressLeaveBeaver && playerSprite.IsBeaver)
                    beaverManager.LeaveBeaver(playerSprite, spritePopulation);
                #endregion


                physics.Update(playerSprite, playerSprite, level, this, timeDelta, visibleSpriteList, spritePopulation, gameMetaState, gameState, levelViewer, spriteBehaviorRandom);

                foreach (AbstractSprite sprite in toUpdateSpriteList)
                    if (sprite != playerSprite)
                    {
                        physics.Update(sprite, playerSprite, level, this, timeDelta, visibleSpriteList, spritePopulation, gameMetaState, gameState, levelViewer, spriteBehaviorRandom);
                        if (sprite is MonsterSprite && sprite.IsAlive)
                            monsterAi.Update((MonsterSprite)sprite, playerSprite, level, timeDelta, visibleSpriteList, spriteBehaviorRandom);
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

        /// <summary>
        /// Chage game state
        /// </summary>
        /// <param name="nextGameState">seed for next game state</param>
        internal void ChangeGameState(int seedNextGameState)
        {
            if (this.gameState != null)
                gameState.IsExpired = true;
            this.seedNextGameState = seedNextGameState;
        }
		#endregion

        #region Properties
        /// <summary>
        /// Level viewer
        /// </summary>
        public ILevelViewer LevelViewer
        {
            get { return levelViewer; }
        }

        /// <summary>
        /// Game's meta state
        /// </summary>
        public GameMetaState GameMetaState
        {
            get { return gameMetaState; }
        }

        /// <summary>
        /// Whether we show menu
        /// </summary>
        public bool IsShowMenu
        {
            get { return isShowMenu; }
            set { isShowMenu = value; }
        }

        public UserInput UserInput
        {
            get { return userInput; }
        }

        /// <summary>
        /// Current game state
        /// </summary>
        public GameState GameState
        {
            get { return gameState; }
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