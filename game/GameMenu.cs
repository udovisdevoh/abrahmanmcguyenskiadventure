using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.menu
{
    enum SubMenu { Main, Display, Controller, Audio, HowTo }

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
        /// Current X position in menu
        /// </summary>
        private static short currentMenuPositionX = 0;

        /// <summary>
        /// Current Y position in menu
        /// </summary>
        private static short currentMenuPositionY = 0;

        /// <summary>
        /// "New game" label
        /// </summary>
        private static Surface __labelNewGame;

        /// <summary>
        /// Whether we need to refresh the menu
        /// </summary>
        private static bool isNeedRefresh = true;
        #endregion

        #region Internal methods
        /// <summary>
        /// Show menu
        /// </summary>
        /// <param name="mainSurface">main surface to show menu on</param>
        internal static void ShowMenu(Surface mainSurface)
        {
            if (!isNeedRefresh)
                return;

            int mainMenuCursorLeft = (int)(Program.screenWidth * 0.295);
            int mainMenuMarginLeft = (int)(Program.screenWidth * 0.32);
            int mainMenuMarginTop = (int)(Program.screenHeight * 0.28);
            int lineSpace = Program.screenHeight / 22;

            if (currentSubMenu == SubMenu.Main)
            {
                mainSurface.Blit(TitleScreen);
                mainSurface.Blit(GetFontText("New game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                mainSurface.Blit(GetFontText("Load game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Save game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                mainSurface.Blit(GetFontText("Display"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));
                mainSurface.Blit(GetFontText("Controller"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 4));
                mainSurface.Blit(GetFontText("Audio"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 5));
                mainSurface.Blit(GetFontText("How to play"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 6));
                mainSurface.Blit(GetFontText("Exit"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 7));
            }

            mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(mainMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY));

            isNeedRefresh = false;
        }

        /// <summary>
        /// Menu will need refresh
        /// </summary>
        internal static void Dirthen()
        {
            isNeedRefresh = true;
        }

        /// <summary>
        /// Menu selection logic
        /// </summary>
        internal static void Select(Program program)
        {
            SoundManager.PlayPunchSound();
            Dirthen();
            if (currentSubMenu == SubMenu.Main)
            {
                switch (currentMenuPositionY)
                {
                    case 0:
                        program.IsShowMenu = false;
                        program.GameState = null;
                        program.LevelViewer.ClearCache();
                        break;
                    case 7:
                        Events.QuitApplication();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Moving left
        /// </summary>
        internal static void MoveLeft()
        {
            SoundManager.PlayHitSound();
            Dirthen();
            currentMenuPositionX--;
        }

        /// <summary>
        /// Moving right
        /// </summary>
        internal static void MoveRight()
        {
            SoundManager.PlayHitSound();
            Dirthen();
            currentMenuPositionX++;
        }

        /// <summary>
        /// Moving up
        /// </summary>
        internal static void MoveUp()
        {
            SoundManager.PlayHitSound();
            Dirthen();
            currentMenuPositionY--;
            if (currentMenuPositionY < 0)
                currentMenuPositionY = 7;
        }

        /// <summary>
        /// Moving down
        /// </summary>
        internal static void MoveDown()
        {
            SoundManager.PlayHitSound();
            Dirthen();
            currentMenuPositionY++;
            if (currentMenuPositionY > 7)
                currentMenuPositionY = 0;
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
        #endregion

        #region Properties
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
        /// "New game" label
        /// </summary>
        private static Surface LabelNewGame
        {
            get
            {
                if (__labelNewGame == null)
                {
                    __labelNewGame = MenuFont.Render("New game", System.Drawing.Color.White);
                }
                return __labelNewGame;
            }
        }
        #endregion

    }
}
