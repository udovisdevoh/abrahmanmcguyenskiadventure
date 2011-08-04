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
using AbrahmanAdventure.audio.midi.generator;

namespace AbrahmanAdventure
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
        #region Constants and static variables
        public const bool isBindViewOffsetToPlayer = true;

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

        public const bool isLimitMonsterSkillBySkillLevel = true;

        public const bool isTellPlanetName = false;

        public const bool isAllowCeiling = true;

        public const bool isAlwaysCeiling = false;

        public const int waveResolution = 1;

        public const int zoneHeightScreenCount = 4;

        public const int bitDepth = 32;

        public const int maxCachedColumnCount = 100;

        public const int maxCachedSquareCount = 1000;

        public const int spatialHashingBucketWidth = 2;

        public const int maxPlayerFireBallPerScreen = 2;

        public const int squareZoneTileWidth = 1;

        public const int squareZoneTileHeight = 10;

        public const int vineMinHeight = 7;

        public const int vineMaxHeight = 36;
       
        public const double holeHeight = 100.0;

        public const double powerUpGrowthTime = 6.0;

        public const double rastaFallingSpeed = 0.07;

        public const double waterMaxFallingSpeed = 0.07;

        public const double waterWalkingSpeedMultiplier = 0.66;

        public const double waterJumpingAccelerationMultiplier = 1.75;

        public const double waterJumpingSpeedMultiplier = 0.25;

        public const double beaverHoleDiameter = 1.0;

        public const double beaverHoleDepth = 0.25;

        public const double pipeTeleportSpeed = 0.006;

        public const double minSizeForExtraVortex = 100.0;

        public const double monsterDensityAdjustment = 0.5;

        public const double maximumAllowedJumpingStep = 3.5;

        public const double maxCloudHeightFromGround = 7;

        public static double zoneWidthScreenCount = 0.025;

        public static double collisionDetectionResolution = 0.0625;

        public static double absoluteMaxCeilingHeight = 2.0;

        public static bool isFullScreen = PersistentConfig.IsFullScreen;

        public static int screenWidth;

        public static int screenHeight;

        public static int tileColumnCount; //20 for 4/3 screen

        public static int tileSize;

        public static int totalZoneWidth;

        public static int totalZoneHeight;

        public static int terrainColumnBufferRightCount;

        public static int terrainColumnBufferLeftCount;

        public static int totalHeightTileCount;
        
        public static int tileRowCount;

        public static double maxViewOffsetY;

        public static double zoneColumnWidthTileCount;
        #endregion

        #region Fields and parts
        private Surface mainSurface;

        private UserInput userInput;

        private ILevelViewer levelViewer;

        private SpriteViewer spriteViewer;

        private JoystickManager joystickManager;

        private BeaverManager beaverManager;

        private PipeManager pipeManager;

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

        private bool isPlayTutorialSounds = false;
        #endregion

        #region Constructor
        public Program()
        {
            InitSurfaceViewPortRatioSettingsEtc();//Will affect mainSurface
            physics = new Physics();
            monsterAi = new MonsterAi();
            joystickManager = new JoystickManager();
            beaverManager = new BeaverManager();
            pipeManager = new PipeManager();
            userInput = new UserInput();
            gameMetaState = new GameMetaState();
            int soundVolume = PersistentConfig.SoundVolume;
            SongPlayer.IRiff = SongGenerator.BuildSong(123, 0, SongType.Menu);
            SongPlayer.PlayAsync();

            spriteBehaviorRandom = new Random();
            #warning Put back random seed
            seedNextGameState = new Random().Next();        
            
            if (isFullScreen)
                Cursor.Hide();

            levelViewer = new LevelViewerSquareBased(mainSurface);
            spriteViewer = new SpriteViewer(mainSurface);

            #region Some pre-caching
            SoundManager.PreCache();
            SpriteDispatcher.PreCacheSpriteSurfaces();
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
                    PersistentConfig.JumpButton = userInput.jumpButton;
                    GameMenu.IsWaitingForJumpButtonRemap = false;
                    GameMenu.Dirthen();
                    return;
                }
                else if (GameMenu.IsWaitingForAttackButtonRemap)
                {
                    userInput.attackButton = args.Button;
                    PersistentConfig.AttackButton = userInput.attackButton;
                    GameMenu.IsWaitingForAttackButtonRemap = false;
                    GameMenu.Dirthen();
                    return;
                }
                else if (GameMenu.IsWaitingForLeaveBeaverButtonRemap)
                {
                    userInput.leaveBeaverButton = args.Button;
                    PersistentConfig.LeaveBeaverButton = userInput.leaveBeaverButton;
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
                GameMenu.ShowMenu(mainSurface, userInput, this);
            }
            else //Main game loop starts here
            {
                if (gameState == null || gameState.IsExpired)
                {
                    #region We regenerate game state because it is nonexistant or expired (changing environment)
                    SongPlayer.StopSync();
                    mainSurface.Fill(System.Drawing.Color.Black);
                    mainSurface.Update();
                    GC.Collect();
                    SurfaceSizeCache.Clear();
                    if (gameState != null)
                    {
                        SoundManager.PlayVortexInSound();
                        gameMetaState.PreviousSeed = gameState.Seed;
                        gameMetaState.GetInfoFromPlayer(gameState.PlayerSprite);
                    }
                    gameState = new GameState(seedNextGameState, gameMetaState.GetSkillLevel(seedNextGameState), mainSurface);
                    gameMetaState.ApplyPlayerInfoToSprite(gameState.PlayerSprite);
                    List<int> listWarpBackSeed;
                    if (gameMetaState.TryGetWarpBackTargetSeed(gameState.Seed, out listWarpBackSeed))
                        gameState.AddWarpBackVortexList(listWarpBackSeed);
                    if (gameMetaState.PreviousSeed != -1)
                    {
                        gameState.MovePlayerToVortexGoingToSeed(gameMetaState.PreviousSeed);
                        SoundManager.PlayVortexOutSound();
                        gameState.PlayerSprite.IsTryToWalkUp = false;
                        gameState.PlayerSprite.IsNeedToAttackAgain = true;
                        gameState.PlayerSprite.IsNeedToJumpAgain = true;
                    }
                    gameState.PlayerSprite.FromVortexCycle.Fire();
                    gameState.PlayerSprite.YPosition = IGroundHelper.GetHighestGround(gameState.Level, gameState.PlayerSprite.XPosition)[gameState.PlayerSprite.XPosition];
                    gameState.PlayerSprite.XPosition = gameState.PlayerSprite.XPosition;//reset previous X
                    gameState.PlayerSprite.YPosition = gameState.PlayerSprite.YPosition;//reset previous Y
                    levelViewer.ClearCache();
                    SongGenerator.GetInvincibilitySong(gameState.Seed);//We pre-render invincibility song if it's not pre-rendered (it will be the same for this episode)
                    GC.Collect();
                    SongPlayer.IRiff = gameState.Song;
                    SongPlayer.PlayAsync();
                    #endregion
                }

                SpritePopulation spritePopulation = gameState.SpritePopulation;
                PlayerSprite playerSprite = gameState.PlayerSprite;
                Level level = gameState.Level;
                HashSet<AbstractSprite> toUpdateSpriteList;
                HashSet<AbstractSprite> visibleSpriteList = spritePopulation.GetVisibleSpriteList(viewOffsetX, viewOffsetY, out toUpdateSpriteList);

                //We process the time multiplicator
                double timeDelta = Math.Max(0, ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 32.0);
                if (isLimitFrameSkip)
                    timeDelta = Math.Min(1.0, timeDelta);

                previousDateTime = DateTime.Now;

                isOddFrame = !isOddFrame;

                if (playerSprite.DestinationPipe == null)
                {
                    #region We manage jumping input logic
                    playerSprite.IsTryingToJump = false;
                    if (userInput.isPressJump || userInput.isPressLeaveBeaver)
                    {
                        //We manage jumping from one ground to a lower ground
                        if (userInput.isPressDown && !userInput.isPressLeft && !userInput.isPressRight && playerSprite.IGround != null && !playerSprite.IsNeedToJumpAgain && playerSprite.CurrentWalkingSpeed == 0)
                        {
                            playerSprite.YPosition += playerSprite.MaximumWalkingHeight;

                            if (playerSprite.IsInWater)
                                playerSprite.IsNeedToJumpAgain = true;

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

                    #region We manage attack (and harvesting) input logic
                    playerSprite.IsTryThrowingBall = false;
                    if (userInput.isPressAttack)
                    {
                        if (!playerSprite.IsNeedToAttackAgain && playerSprite.AttackingCycle.IsReadyToFire)
                        {
                            if (userInput.isPressDown && playerSprite.IGround is IHarvestable && playerSprite.CarriedSprite == null)
                            {
                                playerSprite.CarriedSprite = (AbstractSprite)playerSprite.IGround;
                                playerSprite.CarriedSprite.IsImpassable = false;
                                //playerSprite.CarriedSprite.IsAnnihilateOnExitScreen = true;
                                playerSprite.CarriedSprite.AttackStrengthCollision = ((IHarvestable)playerSprite.CarriedSprite).ProjectileAttackStrengthCollision;

                                if (playerSprite.CarriedSprite is MonsterSprite)
                                {
                                    ((MonsterSprite)playerSprite.CarriedSprite).IsCanDoDamageToPlayerWhenTouched = false;
                                    ((MonsterSprite)playerSprite.CarriedSprite).IsDieOnTouchGround = true;
                                    ((MonsterSprite)playerSprite.CarriedSprite).IsFullSpeedAfterBounceNoAi = true;
                                    ((MonsterSprite)playerSprite.CarriedSprite).IsNoAiChangeDirectionWhenStucked = false;
                                    //((MonsterSprite)playerSprite.CarriedSprite).IsNoAiAlwaysBounce = true;
                                }
                                SoundManager.PlayHarvestSound();
                                playerSprite.IGround = null;
                            }
                            else
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
                            gameState.IsPlayerReady = true;
                            previousDateTime = DateTime.Now;
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
                            gameState.IsPlayerReady = true;
                            previousDateTime = DateTime.Now;
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
                            gameState.IsPlayerReady = true;
                            previousDateTime = DateTime.Now;
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

                    if (gameState.IsPlayerReady)
                    {
                        physics.Update(playerSprite, playerSprite, level, this, timeDelta, visibleSpriteList, spritePopulation, gameMetaState, gameState, levelViewer, spriteBehaviorRandom);
                        foreach (AbstractSprite sprite in toUpdateSpriteList)
                            if (sprite != playerSprite)
                            {
                                physics.Update(sprite, playerSprite, level, this, timeDelta, visibleSpriteList, spritePopulation, gameMetaState, gameState, levelViewer, spriteBehaviorRandom);
                                if (sprite is MonsterSprite && sprite.IsAlive)
                                    monsterAi.Update((MonsterSprite)sprite, playerSprite, level, timeDelta, visibleSpriteList, spriteBehaviorRandom);
                            }
                    }
                }
                else
                {
                    pipeManager.ContinuePipeTeleportation(playerSprite);
                }

                #region We position the camera
                viewOffsetX = playerSprite.XPosition - (double)Program.tileColumnCount / 2.0;
                viewOffsetY = playerSprite.YPosition - (double)Program.tileRowCount / 2.0 - playerSprite.Height / 2.0;
                viewOffsetY = Math.Min(viewOffsetY, maxViewOffsetY);
                #endregion

                #region We update the viewers
                levelViewer.Update(level, gameState.ColorTheme, gameState.Background, gameState.WaterInfo, viewOffsetX, viewOffsetY);
                spriteViewer.Update(viewOffsetX, viewOffsetY, SpriteDistanceSorter.SortByZIndex(visibleSpriteList), isOddFrame);
                HudViewer.Update(mainSurface, playerSprite.Health, gameState.IsPlayerReady);
                if (isPlayTutorialSounds && gameState.IsPlayerReady && playerSprite.DestinationPipe == null)
                    foreach (AbstractSprite sprite in visibleSpriteList)
                        if (sprite != playerSprite)
                            if (SpriteDistanceSorter.GetExactDistanceTile(playerSprite, sprite) <= 7.0)
                                TutorialTalker.TryTalkAbout(sprite);
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

        /// <summary>
        /// We call this when resolution changes or when we switch from windowed to fullscreen
        /// </summary>
        internal void InitSurfaceViewPortRatioSettingsEtc()
        {
            screenWidth = PersistentConfig.ScreenWidth;
            screenHeight = PersistentConfig.ScreenHeight;
            tileColumnCount = (int)Math.Round(20.0 / (640.0 / 480.0) * ((double)screenWidth / (double)screenHeight)); //20 for 4/3 screen
            tileSize = screenWidth / tileColumnCount;
            totalZoneWidth = (int)(Program.zoneWidthScreenCount * Program.screenWidth);
            totalZoneHeight = Program.zoneHeightScreenCount * Program.screenHeight;
            terrainColumnBufferRightCount = (int)(1.1 / zoneWidthScreenCount);
            terrainColumnBufferLeftCount = (int)(0.1 / zoneWidthScreenCount);
            totalHeightTileCount = totalZoneHeight / tileSize;
            tileRowCount = screenHeight / tileSize;
            maxViewOffsetY = totalHeightTileCount / 2.0 - (double)tileRowCount;
            zoneColumnWidthTileCount = (double)totalZoneWidth / (double)tileSize;
            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, Program.bitDepth, false, false, isFullScreen, isHardwareSurface);
            Video.WindowCaption = "Abrahman McGuyenski Adventure";
            HudViewer.InitCachedSurfaces();
            GameMenu.ClearCache();
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
            set { gameMetaState = value; }
        }

        /// <summary>
        /// Whether we show menu
        /// </summary>
        public bool IsShowMenu
        {
            get { return isShowMenu; }
            set { isShowMenu = value; }
        }

        /// <summary>
        /// Whether we play tutorial sounds (TTS)
        /// </summary>
        public bool IsPlayTutorialSounds
        {
            get { return isPlayTutorialSounds; }
            set { isPlayTutorialSounds = value; }
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
            set { gameState = value; }
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

        [STAThread]
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
            Environment.Exit(0);
        }
        #endregion
    }
}