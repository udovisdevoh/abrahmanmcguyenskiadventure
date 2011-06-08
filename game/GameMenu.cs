using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure
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
        /// "New game" label
        /// </summary>
        private static Surface __labelNewGame;

        /// <summary>
        /// Whether we need to refresh the menu
        /// </summary>
        private static bool isNeedRefresh = true;

        /*/// <summary>
        /// To cache rendered stuff from fonts
        /// </summary>
        private static Dictionary<string, Surface> __textCache = new Dictionary<string, Surface>();*/
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

            isNeedRefresh = false;
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
            /*Surface surface;
            if (!__textCache.TryGetValue(text, out surface))
                surface = MenuFont.Render(text, System.Drawing.Color.White);
            return surface;*/
            return MenuFont.Render(text, System.Drawing.Color.White);
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
