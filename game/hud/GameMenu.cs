using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.audio;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.hud
{
    enum SubMenu { Main, Display, Controller, Audio, HowTo, EpisodeList }

    /// <summary>
    /// Game's main menu
    /// </summary>
    internal static class GameMenu
    {
        #region Fields and parts
        /// <summary>
        /// Title screen's background image
        /// </summary>
        private static Surface __titleScreen;

        /// <summary>
        /// Menu's font
        /// </summary>
        private static Font __menuFont;

        /// <summary>
        /// Current sub-menu
        /// </summary>
        private static SubMenu currentSubMenu = SubMenu.Main;

        /// <summary>
        /// Key cycle
        /// </summary>
        private static Cycle keyCycle = new Cycle(10, false);

        /// <summary>
        /// Current X position in menu
        /// </summary>
        private static int currentMenuPositionX = 0;

        /// <summary>
        /// Current Y position in menu
        /// </summary>
        private static int currentMenuPositionY = 0;

        /// <summary>
        /// Offset of the episode menu
        /// </summary>
        private static int episodeOffset = 0;

        /// <summary>
        /// Current skill level
        /// </summary>
        private static int skillLevel = 0;

        /// <summary>
        /// Whether we need to refresh the menu
        /// </summary>
        private static bool isNeedRefresh = true;

        /// <summary>
        /// Whether we are expecting to press a new joystick button to remap jump button
        /// </summary>
        private static bool isWaitingForJumpButtonRemap = false;

        /// <summary>
        /// Whether we are expecting to press a new joystick button to remap attack button
        /// </summary>
        private static bool isWaitingForAttackButtonRemap = false;

        /// <summary>
        /// Whether we are expecting to press a new joystick button to remap leave beaver button
        /// </summary>
        private static bool isWaitingForLeaveBeaverButtonRemap = false;

        /// <summary>
        /// Max menu item per menu
        /// </summary>
        private static int[] listMaxMenuItemCount = {7,0,2,0,0,12};
        #endregion

        #region Internal methods
        /// <summary>
        /// Show menu
        /// </summary>
        /// <param name="mainSurface">main surface to show menu on</param>
        /// <param name="userInput">user input</param>
        internal static void ShowMenu(Surface mainSurface, UserInput userInput)
        {
            if (!isNeedRefresh)
                return;

            int mainMenuCursorLeft = (int)(Program.screenWidth * 0.295);
            int episodeMenuCursorLeft = (int)(Program.screenWidth * 0.024);
            int mainMenuMarginLeft = (int)(Program.screenWidth * 0.32);
            int episodeMenuMarginLeft = (int)(Program.screenWidth * 0.05);
            int mainMenuMarginTop = (int)(Program.screenHeight * 0.28);
            int lineSpace = Program.screenHeight / 22;

            if (currentSubMenu == SubMenu.Main)
            {
                mainSurface.Blit(TitleScreen);
                mainSurface.Blit(GetFontText("New game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                mainSurface.Blit(GetFontText("Load game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Save game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                mainSurface.Blit(GetFontText("Display"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));
                mainSurface.Blit(GetFontText("Gamepad"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 4));
                mainSurface.Blit(GetFontText("Audio"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 5));
                mainSurface.Blit(GetFontText("How to play"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 6));
                mainSurface.Blit(GetFontText("Exit"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 7));

                mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(mainMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY));
            }
            else if (currentSubMenu == SubMenu.Controller)
            {
                mainSurface.Fill(System.Drawing.Color.Black);
                if (isWaitingForJumpButtonRemap)
                    mainSurface.Blit(GetFontText("Jump button: press jump"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                else
                    mainSurface.Blit(GetFontText("Jump button: button " + userInput.jumpButton), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));

                if (isWaitingForAttackButtonRemap)
                    mainSurface.Blit(GetFontText("Attack button: press attack"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                else
                    mainSurface.Blit(GetFontText("Attack button: button " + userInput.attackButton), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));

                if (isWaitingForLeaveBeaverButtonRemap)
                    mainSurface.Blit(GetFontText("Leave beaver button: press leave beaver"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                else
                    mainSurface.Blit(GetFontText("Leave beaver button: button " + userInput.leaveBeaverButton), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));

                mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(mainMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY));
            }
            else if (currentSubMenu == SubMenu.EpisodeList)
            {
                mainSurface.Fill(System.Drawing.Color.Black);

                string skillLevelBar = "+";
                for (int i = 0; i < skillLevel; i++)
                    skillLevelBar += "+";

                mainSurface.Blit(GetFontText("Choose episode and skill level (<->): " + skillLevelBar), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 2));

                for (int episodeIndex = 0; episodeIndex <= listMaxMenuItemCount[(int)SubMenu.EpisodeList]; episodeIndex++)
                {
                    mainSurface.Blit(GetFontText((episodeIndex + episodeOffset + 1) + ": " + EpisodeNameManager.GetEpisodeName(episodeIndex + episodeOffset), EpisodeNameManager.GetEpisodeColor(episodeIndex + episodeOffset)), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * episodeIndex));
                }

                mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(episodeMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY));
            }
            else if (currentSubMenu == SubMenu.HowTo)
            {
                mainSurface.Fill(System.Drawing.Color.Black);

                mainSurface.Blit(GetFontText("How to play"), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 2));

                mainSurface.Blit(GetFontText("Arrows: (move)", System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                mainSurface.Blit(GetFontText("Space: Jump", System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Ctrl: Attack / run / grab / dig", System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                mainSurface.Blit(GetFontText("Alt: Jump out of beaver", System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));                
               
                mainSurface.Blit(GetFontText("Enter: Select menu item", System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 5));
                mainSurface.Blit(GetFontText("Esc: Go back", System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 6));
            }

            isNeedRefresh = false;
        }

        /// <summary>
        /// Show loading screen
        /// </summary>
        /// <param name="mainSurface">Show loading screen</param>
        internal static void ShowLoading(Surface mainSurface)
        {
            /*if (isBlack)
                mainSurface.Fill(System.Drawing.Color.Black);*/
            Surface loadingLabel = GetFontText("Loading...");
            mainSurface.Blit(loadingLabel, new System.Drawing.Point(Program.screenWidth / 2 - loadingLabel.Width / 2, Program.screenHeight / 48));
            mainSurface.Update();
        }

        /// <summary>
        /// Menu will need refresh
        /// </summary>
        internal static void Dirthen()
        {
            isNeedRefresh = true;
        }

        /// <summary>
        /// Parse user input
        /// </summary>
        /// <param name="userInput">user input</param>
        /// <param name="program">program</param>
        internal static void ParseUserInput(UserInput userInput, Program program)
        {
            keyCycle.Increment(1.0f);
            if (keyCycle.IsFinished)
            {
                if (userInput.isPressJump)
                {
                    Select(program);
                    keyCycle.Fire();
                }
                else if (userInput.isPressAttack)
                {
                    Escape();
                    keyCycle.Fire();
                }
                else if (userInput.isPressLeft)
                {
                    MoveLeft();
                    keyCycle.Fire();
                }
                else if (userInput.isPressRight)
                {
                    MoveRight();
                    keyCycle.Fire();
                }
                else if (userInput.isPressUp)
                {
                    SoundManager.PlayHitSound();
                    MoveUp();
                    keyCycle.Fire();
                }
                else if (userInput.isPressDown)
                {
                    SoundManager.PlayHitSound();
                    MoveDown();
                    keyCycle.Fire();
                }
                else if (userInput.isPressPageDown)
                {
                    SoundManager.PlayHitSound();
                    for (int i = 0; i < listMaxMenuItemCount[(short)currentSubMenu]; i++)
                        MoveDown();
                    keyCycle.Fire();
                }
                else if (userInput.isPressPageUp)
                {
                    SoundManager.PlayHitSound();
                    for (int i = 0; i < listMaxMenuItemCount[(short)currentSubMenu]; i++)
                        MoveUp();
                    keyCycle.Fire();
                }
            }
        }

        /// <summary>
        /// Escape menu
        /// </summary>
        internal static void Escape()
        {
            isWaitingForJumpButtonRemap = false;
            isWaitingForAttackButtonRemap = false;
            isWaitingForLeaveBeaverButtonRemap = false;
            currentSubMenu = SubMenu.Main;
            currentMenuPositionY = 0;
            keyCycle.StopAndReset();
            Dirthen();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Write font text
        /// </summary>
        /// <param name="text">text to write</param>
        /// <returns>font text</returns>
        private static Surface GetFontText(string text)
        {
            return GetFontText(text, System.Drawing.Color.White);
        }

        /// <summary>
        /// Write font text
        /// </summary>
        /// <param name="text">text to write</param>
        /// <param name="color">default: white</param>
        /// <returns>font text</returns>
        private static Surface GetFontText(string text, System.Drawing.Color color)
        {
            return MenuFont.Render(text, color);
        }

        /// <summary>
        /// Moving left
        /// </summary>
        private static void MoveLeft()
        {
            isWaitingForJumpButtonRemap = false;
            isWaitingForAttackButtonRemap = false;
            SoundManager.PlayHitSound();
            Dirthen();
            currentMenuPositionX--;

            if (currentSubMenu == SubMenu.EpisodeList)
            {
                skillLevel--;
                skillLevel = Math.Max(0, skillLevel);
            }
        }

        /// <summary>
        /// Moving right
        /// </summary>
        private static void MoveRight()
        {
            isWaitingForJumpButtonRemap = false;
            isWaitingForAttackButtonRemap = false;
            SoundManager.PlayHitSound();
            Dirthen();
            currentMenuPositionX++;

            if (currentSubMenu == SubMenu.EpisodeList)
            {
                skillLevel++;
                skillLevel = Math.Min(9, skillLevel);
            }
        }

        /// <summary>
        /// Moving up
        /// </summary>
        private static void MoveUp()
        {
            isWaitingForJumpButtonRemap = false;
            isWaitingForAttackButtonRemap = false;
            Dirthen();
            currentMenuPositionY--;
            if (currentMenuPositionY < 0)
            {
                if (currentSubMenu == SubMenu.EpisodeList)
                {
                    currentMenuPositionY = 0;
                    if (episodeOffset > 0)
                        episodeOffset--;
                }
                else
                {
                    currentMenuPositionY = listMaxMenuItemCount[(int)currentSubMenu];
                }
            }
        }

        /// <summary>
        /// Moving down
        /// </summary>
        private static void MoveDown()
        {
            isWaitingForJumpButtonRemap = false;
            isWaitingForAttackButtonRemap = false;
            Dirthen();
            currentMenuPositionY++;
            if (currentMenuPositionY > listMaxMenuItemCount[(int)currentSubMenu])
            {
                if (currentSubMenu == SubMenu.EpisodeList)
                {
                    episodeOffset++;
                    currentMenuPositionY = listMaxMenuItemCount[(int)currentSubMenu];
                }
                else
                {
                    currentMenuPositionY = 0;
                }
            }
        }

        /// <summary>
        /// Menu selection logic
        /// </summary>
        private static void Select(Program program)
        {
            SoundManager.PlayPunchSound();
            keyCycle.StopAndReset();
            Dirthen();
            if (currentSubMenu == SubMenu.Main)
            {
                switch (currentMenuPositionY)
                {
                    case 0: //new game
                        currentMenuPositionY = 0;
                        currentSubMenu = SubMenu.EpisodeList;
                        break;
                    case 1: //Load game
                        currentMenuPositionY = 0;
                        string directory = Directory.GetCurrentDirectory();
                        if (Program.isFullScreen)
                            Cursor.Show();
                        GameMetaState loadedGame = SaverLoader.LoadGame();
                        if (Program.isFullScreen)
                            Cursor.Hide();
                        Directory.SetCurrentDirectory(directory);
                        if (loadedGame != null)
                        {
                            currentMenuPositionY = 0;
                            program.GameMetaState = loadedGame;
                            program.ChangeGameState(program.GameMetaState.PreviousSeed);
                            program.GameState = null;
                            program.IsShowMenu = false;
                        }
                        break;
                    case 2: //Save game
                        if (program.GameState != null)
                        {
                            program.GameMetaState.PreviousSeed = program.GameState.Seed;
                            program.GameMetaState.GetInfoFromPlayer(program.GameState.PlayerSprite);
                        }
                        string directory2 = Directory.GetCurrentDirectory();
                        if (Program.isFullScreen)
                            Cursor.Show();
                        SaverLoader.SaveGame(program.GameMetaState);
                        if (Program.isFullScreen)
                            Cursor.Hide();
                        Directory.SetCurrentDirectory(directory2);
                        break;
                    case 4:
                        currentMenuPositionY = 0;
                        currentSubMenu = SubMenu.Controller;
                        break;
                    case 6:
                        currentMenuPositionY = 0;
                        currentSubMenu = SubMenu.HowTo;
                        break;
                    case 7: //exit
                        Events.QuitApplication();
                        break;
                    default:
                        break;
                }
            }
            else if (currentSubMenu == SubMenu.Controller)
            {
                isWaitingForJumpButtonRemap = false;
                isWaitingForAttackButtonRemap = false;
                isWaitingForLeaveBeaverButtonRemap = false;
                if (currentMenuPositionY == 0)
                    isWaitingForJumpButtonRemap = true;
                else if (currentMenuPositionY == 1)
                    isWaitingForAttackButtonRemap = true;
                else if (currentMenuPositionY == 2)
                    isWaitingForLeaveBeaverButtonRemap = true;
            }
            else if (currentSubMenu == SubMenu.EpisodeList)
            {
                if (program.GameState != null)
                    program.GameState.PlayerSprite.ResetHealthAndPowerUps();
                currentSubMenu = SubMenu.Main;
                program.ChangeGameState(currentMenuPositionY + episodeOffset);
                currentMenuPositionY = 0;
                program.IsShowMenu = false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Current sub menu (could be main menu too)
        /// </summary>
        public static SubMenu CurrentSubMenu
        {
            get { return currentSubMenu; }
        }

        /// <summary>
        /// Title screen
        /// </summary>
        private static Surface TitleScreen
        {
            get
            {
                if (__titleScreen == null)
                {
                    __titleScreen = new Surface("./assets/rendered/TitleScreen.png");

                    if (__titleScreen.Width != Program.screenWidth)
                    {
                        double zoomX = (double)Program.screenWidth / (double)__titleScreen.Width;
                        double zoomY = (double)Program.screenHeight / (double)__titleScreen.Height;
                        __titleScreen = __titleScreen.CreateScaledSurface(zoomX, zoomY, true);
                    }

                }
                return __titleScreen;
            }
        }

        /// <summary>
        /// Menu's font
        /// </summary>
        private static Font MenuFont
        {
            get
            {
                if (__menuFont == null)
                {
                    __menuFont = new Font("./assets/rendered/MenuFont.ttf", 16 * Program.screenWidth / 640);
                }
                return __menuFont;
            }
        }

        /// <summary>
        /// Whether we are expecting to press a new joystick button to remap jump button
        /// </summary>
        public static bool IsWaitingForJumpButtonRemap
        {
            get { return isWaitingForJumpButtonRemap; }
            set { isWaitingForJumpButtonRemap = value; }
        }

        /// <summary>
        /// Whether we are expecting to press a new joystick button to remap attack button
        /// </summary>
        public static bool IsWaitingForAttackButtonRemap
        {
            get { return isWaitingForAttackButtonRemap; }
            set { isWaitingForAttackButtonRemap = value; }
        }

        /// <summary>
        /// Whether we are expecting to press new joystick button to remap leave beaver button
        /// </summary>
        public static bool IsWaitingForLeaveBeaverButtonRemap
        {
            get { return isWaitingForLeaveBeaverButtonRemap; }
            set { isWaitingForLeaveBeaverButtonRemap = value; }
        }
        #endregion
    }
}
