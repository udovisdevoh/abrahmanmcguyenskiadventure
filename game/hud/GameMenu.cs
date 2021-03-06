﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using AbrahmanAdventure.audio;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.hud
{
    enum SubMenu { Main, Display, Controller, Audio, HowTo, EpisodeList, GameMode }

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
        /// Whether we are expecting to press a new joystick button to remap throw rope button
        /// </summary>
        private static bool isWaitingForThrowRopeButtonRemap = false;

        /// <summary>
        /// Max menu item per menu
        /// </summary>
        private static int[] listMaxMenuItemCount = {9,1,3,2,0,12,3};
        #endregion

        #region Internal methods
        /// <summary>
        /// Show menu
        /// </summary>
        /// <param name="mainSurface">main surface to show menu on</param>
        /// <param name="userInput">user input</param>
        internal static void ShowMenu(Surface mainSurface, UserInput userInput, Program program)
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
                mainSurface.Blit(GetFontText("Tutorial"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Load game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                mainSurface.Blit(GetFontText("Save game"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));
                mainSurface.Blit(GetFontText("Display"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 4));
                mainSurface.Blit(GetFontText("Gamepad"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 5));
                mainSurface.Blit(GetFontText("Audio volume"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 6));
                mainSurface.Blit(GetFontText("How to play"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 7));
                mainSurface.Blit(GetFontText("Reset config"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 8));
                mainSurface.Blit(GetFontText("Exit"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 9));

                mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(mainMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY));
            }
            else if (currentSubMenu == SubMenu.GameMode)
            {
                mainSurface.Fill(System.Drawing.Color.Black);
                mainSurface.Blit(GetFontText("Game mode"), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 2));
                mainSurface.Blit(GetFontText("Action Platform"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                mainSurface.Blit(GetFontText("Extreme Action"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Adventure RPG"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                mainSurface.Blit(GetFontText("Racing"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));

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

                if (isWaitingForThrowRopeButtonRemap)
                    mainSurface.Blit(GetFontText("Throw rope button: press throw rope"), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));
                else
                    mainSurface.Blit(GetFontText("Throw rope button: button " + userInput.throwRopeButton), new System.Drawing.Point(mainMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));

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
                mainSurface.Fill(System.Drawing.Color.White);

                mainSurface.Blit(GetFontText("How to play", System.Drawing.Color.Black), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 4));

                if (currentMenuPositionX < 0)
                    currentMenuPositionX = SpriteGuide.SpriteList.Count;
                else if (currentMenuPositionX > SpriteGuide.SpriteList.Count)
                    currentMenuPositionX = 0;

                if (currentMenuPositionX == 0)
                {
                    mainSurface.Blit(GetFontText("Arrows: (move)", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                    mainSurface.Blit(GetFontText("Z or Space: Jump", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                    mainSurface.Blit(GetFontText("A or Ctrl: Attack / run / grab / dig", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                    mainSurface.Blit(GetFontText("X or Alt: Jump out of beaver, throw ninja grappler up", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));
                    mainSurface.Blit(GetFontText("S or Shift: Throw ninja grappler up and keep beaver", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 4));

                    mainSurface.Blit(GetFontText("Enter: Select menu item", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 6));
                    mainSurface.Blit(GetFontText("Esc: Go back", System.Drawing.Color.Brown), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 7));
                }
                else
                {
                    AbstractSprite spriteFromGuide = SpriteGuide.SpriteList[currentMenuPositionX - 1];
                    System.Drawing.Color color = (spriteFromGuide is MonsterSprite && (spriteFromGuide is IExplodable || (((MonsterSprite)spriteFromGuide).IsCanDoDamageToPlayerWhenTouched && ((MonsterSprite)spriteFromGuide).AttackStrengthCollision > 0))) ? System.Drawing.Color.DarkRed : System.Drawing.Color.Green;
                    double xOffsetSprite, yOffsetSprite;
                    Surface spriteSurface = spriteFromGuide.GetCurrentSurface(out xOffsetSprite, out yOffsetSprite);
                    mainSurface.Blit(spriteSurface, new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 2), spriteSurface.GetRectangle());

                    string[] lineList = spriteFromGuide.TutorialComment.Split('\n');
                    int lineOffset = 0;
                    foreach (string line in lineList)
                    {
                        mainSurface.Blit(GetFontText(line, color), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * (8 + lineOffset)));
                        lineOffset++;
                    }
                }

                mainSurface.Blit(GetFontText("< Page " + (currentMenuPositionX + 1) + " of " + (SpriteGuide.Count + 1) + ">", System.Drawing.Color.Blue), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 14));
            }
            else if (currentSubMenu == SubMenu.Display)
            {
                mainSurface.Fill(System.Drawing.Color.Black);

                //VideoInfo.HasColorFills

                mainSurface.Blit(GetFontText("Display (" + Video.VideoDriver + " " + VideoInfo.BitsPerPixel + " bits)"), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 5));

                mainSurface.Blit(GetFontText("Video memory: " + VideoInfo.VideoMemory / 1024), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 3));
                mainSurface.Blit(GetFontText("Hardware: " + (Program.isHardwareSurface ? "yes" : "no")), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 2));
                mainSurface.Blit(GetFontText("Hardware surface: " + (VideoInfo.HasHardwareSurfaces ? "yes" : "no")), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 1));
                mainSurface.Blit(GetFontText("Hardware blits: " + (VideoInfo.HasHardwareBlits ? "yes" : "no")), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 0));
                mainSurface.Blit(GetFontText("Hardware alpha blits: " + (VideoInfo.HasHardwareAlphaBlits ? "yes" : "no")), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Software alpha blits: " + (VideoInfo.HasSoftwareAlphaBlits ? "yes" : "no")), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));
                mainSurface.Blit(GetFontText("Color fills: " + (VideoInfo.HasColorFills ? "yes" : "no")), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 3));


                if (program.GameState != null)
                {
                    mainSurface.Blit(GetFontText("The level will be restarted"), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 4));
                    mainSurface.Blit(GetFontText("if you change the resolution"), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 5));
                }

                string isFullScreenText = (Program.isFullScreen) ? "on" : "off";
                string resolutionText = Program.screenWidth + " x " + Program.screenHeight;

                if (Program.screenWidth == 1920 && Program.screenHeight == 1080)
                    resolutionText = "1080p full HD";
                else if (Program.screenWidth == 1280 && Program.screenHeight == 720)
                    resolutionText = "720p HD";

                mainSurface.Blit(GetFontText("Fullscreen: " + isFullScreenText, System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 6));
                mainSurface.Blit(GetFontText("Resolution: " + resolutionText, System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 7));

                mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(episodeMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY + (lineSpace * 6)));
            }
            else if (currentSubMenu == SubMenu.Audio)
            {
                mainSurface.Fill(System.Drawing.Color.Black);

                string soundVolumeBar = "";
                for (int i = 0; i < SoundManager.Volume; i++)
                    soundVolumeBar += "+";

                string musicVolumeBar = "";
                for (int i = 0; i < SongPlayer.Volume; i++)
                    musicVolumeBar += "+";

                string voiceVolumeBar = "";
                for (int i = 0; i < TutorialTalker.Volume; i++)
                    voiceVolumeBar += "+";

                mainSurface.Blit(GetFontText("Sound / music volume"), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop - lineSpace * 2));
                mainSurface.Blit(GetFontText("Sounds: " + soundVolumeBar, System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 0));
                mainSurface.Blit(GetFontText("Music: " + musicVolumeBar, System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 1));
                mainSurface.Blit(GetFontText("Voice: " + voiceVolumeBar, System.Drawing.Color.Yellow), new System.Drawing.Point(episodeMenuMarginLeft, mainMenuMarginTop + lineSpace * 2));

                mainSurface.Blit(GetFontText(">", System.Drawing.Color.Red), new System.Drawing.Point(episodeMenuCursorLeft, mainMenuMarginTop + lineSpace * currentMenuPositionY));
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
                    MoveLeft(program);
                    keyCycle.Fire();
                }
                else if (userInput.isPressRight)
                {
                    MoveRight(program);
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
            isWaitingForThrowRopeButtonRemap = false;

            if (currentSubMenu == SubMenu.EpisodeList)
                currentSubMenu = SubMenu.GameMode;
            else
                currentSubMenu = SubMenu.Main;

            currentMenuPositionY = 0;
            keyCycle.StopAndReset();
            Dirthen();
        }

        /// <summary>
        /// Write font text
        /// </summary>
        /// <param name="text">text to write</param>
        /// <returns>font text</returns>
        internal static Surface GetFontText(string text)
        {
            return GetFontText(text, System.Drawing.Color.White);
        }

        /// <summary>
        /// Write font text
        /// </summary>
        /// <param name="text">text to write</param>
        /// <param name="color">default: white</param>
        /// <returns>font text</returns>
        internal static Surface GetFontText(string text, System.Drawing.Color color)
        {
            return MenuFont.Render(text, color);
        }

        internal static void ClearCache()
        {
            __titleScreen = null;
            __menuFont = null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Moving left
        /// </summary>
        private static void MoveLeft(Program program)
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
            else if (currentSubMenu == SubMenu.Audio)
            {
                if (currentMenuPositionY == 0)
                {
                    SoundManager.Volume--;
                    SoundManager.Volume = Math.Max(0, SoundManager.Volume);
                    PersistentConfig.SoundVolume = SoundManager.Volume;
                }
                else if (currentMenuPositionY == 1)
                {
                    SongPlayer.Volume--;
                    PersistentConfig.MusicVolume = SongPlayer.Volume;
                }
                else
                {
                    if (TutorialTalker.Volume > 0)
                    {
                        TutorialTalker.Volume--;
                        TutorialTalker.Talk("ah");
                        PersistentConfig.VoiceVolume = TutorialTalker.Volume;
                    }
                }
            }
            else if (currentSubMenu == SubMenu.Display)
            {
                if (currentMenuPositionY == 0)
                {
                    Program.isFullScreen = !Program.isFullScreen;
                    PersistentConfig.IsFullScreen = Program.isFullScreen;
                    program.InitSurfaceViewPortRatioSettingsEtc();
                    if (Program.isFullScreen)
                        Cursor.Hide();
                    else
                        Cursor.Show();
                }
                else
                {
                    ResolutionManager.ChangeResolution(-1,program);
                }
            }
        }

        /// <summary>
        /// Moving right
        /// </summary>
        private static void MoveRight(Program program)
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
            else if (currentSubMenu == SubMenu.Audio)
            {
                if (currentMenuPositionY == 0)
                {
                    SoundManager.Volume++;
                    SoundManager.Volume = Math.Min(16, SoundManager.Volume);
                    PersistentConfig.SoundVolume = SoundManager.Volume;
                }
                else if (currentMenuPositionY == 1)
                {
                    SongPlayer.Volume++;
                    PersistentConfig.MusicVolume = SongPlayer.Volume;
                }
                else
                {
                    if (TutorialTalker.Volume < 10)
                    {
                        TutorialTalker.Volume++;
                        TutorialTalker.Talk("ah");
                        PersistentConfig.VoiceVolume = TutorialTalker.Volume;
                    }
                }
            }
            else if (currentSubMenu == SubMenu.Display)
            {
                if (currentMenuPositionY == 0)
                {
                    Program.isFullScreen = !Program.isFullScreen;
                    PersistentConfig.IsFullScreen = Program.isFullScreen;
                    program.InitSurfaceViewPortRatioSettingsEtc();
                    if (Program.isFullScreen)
                        Cursor.Hide();
                    else
                        Cursor.Show();
                }
                else
                {
                    ResolutionManager.ChangeResolution(1,program);
                }
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
                        SongGenerator.ResetInvincibilitySong();
                        currentSubMenu = SubMenu.GameMode;
                        break;
                    case 1: //Tutorial
                        program.IsPlayTutorialSounds = true;
                        SongGenerator.ResetInvincibilitySong();
                        if (program.GameState != null)
                            program.GameState.PlayerSprite.ResetHealthAndPowerUps();
                        program.GameMetaState.SkillLevelForUnknownLevels = 0;
                        program.GameMetaState.ClearWarpBack();
                        program.GameMetaState.ClearMapSkillLevel();
                        currentMenuPositionY = 0;
                        program.IsShowMenu = false;
                        break;
                    case 2: //Load game
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
                            SongGenerator.ResetInvincibilitySong();
                            currentMenuPositionY = 0;
                            program.GameMetaState = loadedGame;
                            program.ChangeGameState(program.GameMetaState.PreviousSeed);
                            program.GameState = null;
                            program.IsPlayTutorialSounds = false;
                        }
                        program.IsShowMenu = false;
                        break;
                    case 3: //Save game
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
                        program.IsShowMenu = false;
                        if (program.GameState != null)
                            program.GameState.IsPlayerReady = false;
                        break;
                    case 4:
                        currentMenuPositionY = 0;
                        currentSubMenu = SubMenu.Display;
                        break;
                    case 5:
                        currentMenuPositionY = 0;
                        currentSubMenu = SubMenu.Controller;
                        break;
                    case 6:
                        currentMenuPositionY = 0;
                        currentSubMenu = SubMenu.Audio;
                        break;
                    case 7:
                        currentMenuPositionY = 0;
                        currentMenuPositionX = 0;
                        currentSubMenu = SubMenu.HowTo;
                        break;
                    case 8:
                        PersistentConfig.Clear(program);
                        ResolutionManager.ResetAfterResolutionChange(program);
                        break;
                    case 9: //exit
                        SongPlayer.StopSync();
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
                isWaitingForThrowRopeButtonRemap = false;
                if (currentMenuPositionY == 0)
                    isWaitingForJumpButtonRemap = true;
                else if (currentMenuPositionY == 1)
                    isWaitingForAttackButtonRemap = true;
                else if (currentMenuPositionY == 2)
                    isWaitingForLeaveBeaverButtonRemap = true;
                else if (currentMenuPositionY == 3)
                    isWaitingForThrowRopeButtonRemap = true;
            }
            else if (currentSubMenu == SubMenu.EpisodeList)
            {
                if (program.GameState != null)
                    program.GameState.PlayerSprite.ResetHealthAndPowerUps();
                currentSubMenu = SubMenu.Main;
                program.ChangeGameState(currentMenuPositionY + episodeOffset);
                program.GameMetaState.SkillLevelForUnknownLevels = skillLevel;
                program.GameMetaState.ClearWarpBack();
                program.GameMetaState.ClearMapSkillLevel();
                currentMenuPositionY = 0;
                program.IsPlayTutorialSounds = false;
                program.IsShowMenu = false;
            }
            else if (currentSubMenu == SubMenu.GameMode)
            {
                if (currentMenuPositionY == 0)
                {
                    program.GameMetaState.IsAdventureRpg = false;
                    program.GameMetaState.IsExtremeAction = false;
                    program.GameMetaState.IsRacing = false;
                }
                else if (currentMenuPositionY == 1)
                {
                    program.GameMetaState.IsAdventureRpg = false;
                    program.GameMetaState.IsExtremeAction = true;
                    program.GameMetaState.IsRacing = false;
                }
                else if (currentMenuPositionY == 3)
                {
                    program.GameMetaState.IsAdventureRpg = false;
                    program.GameMetaState.IsExtremeAction = false;
                    program.GameMetaState.IsRacing = true;
                }
                else
                {
                    program.GameMetaState.IsAdventureRpg = true;
                    program.GameMetaState.IsExtremeAction = false;
                    program.GameMetaState.IsRacing = false;
                }

                currentSubMenu = SubMenu.EpisodeList;
                currentMenuPositionY = 0;
                currentMenuPositionX = 0;
            }
            else if (currentSubMenu == SubMenu.Display)
            {
                if (currentMenuPositionY == 0)
                {
                    Program.isFullScreen = !Program.isFullScreen;
                    PersistentConfig.IsFullScreen = Program.isFullScreen;
                    program.InitSurfaceViewPortRatioSettingsEtc();
                    if (Program.isFullScreen)
                        Cursor.Hide();
                    else
                        Cursor.Show();
                }
                else
                {
                    ResolutionManager.ChangeResolution(1, program);
                }
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
                    if (Program.screenHeight > 720)
                        __titleScreen = new Surface("./assets/rendered/1080/TitleScreen.png");
                    else if (Program.screenHeight > 480)
                        __titleScreen = new Surface("./assets/rendered/720/TitleScreen.png");
                    else
                        __titleScreen = new Surface("./assets/rendered/480/TitleScreen.png");

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

        /// <summary>
        /// Whether we are expecting to press new joystick button to remap throw rope button
        /// </summary>
        public static bool IsWaitingForThrowRopeButtonRemap
        {
            get { return isWaitingForThrowRopeButtonRemap; }
            set { isWaitingForThrowRopeButtonRemap = value; }
        }
        #endregion
    }
}
