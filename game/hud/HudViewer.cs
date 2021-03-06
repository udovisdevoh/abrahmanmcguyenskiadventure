﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using SdlDotNet.Core;
using System.Drawing;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// To view hud (energy, how many lives, current level, time left, score)
    /// </summary>
    internal static class HudViewer
    {
        #region Fields and parts
        /// <summary>
        /// Paused text
        /// </summary>
        private static Surface pausedText;

        /// <summary>
        /// Maximum width of energy bar (in pixels)
        /// </summary>
        private static int maxEnergyBarWidth;

        /// <summary>
        /// Both X and Y (they are the same) offset (in pixels) for energy bar position
        /// </summary>
        private static int xYOffsetEnergyBar;

        /// <summary>
        /// Thickness of energy bar
        /// </summary>
        private static int energyBarThickness;

        private static int previousGenericSkillValue = 0;

        private static int previousGenericSkillValue2;

        private static Surface genericSkillInfoSurface = null;

        private static Cycle cycleDisplayGenericSkill = new Cycle(50, false, false, false);
        #endregion

        #region Constructor
        /// <summary>
        /// Create hud viewer
        /// </summary>
        /// <param name="surface">surface to draw on</param>
        static HudViewer()
        {
            InitCachedSurfaces();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Show Hud
        /// </summary>
        /// <param name="dataSize">player's health (1.0 = default max)</param>
        /// <param name="isHealth">true: red bar, false: blue bar</param>
        internal static void UpdateHealthBar(Surface surface, double dataSize, bool isPlayerReady)
        {
            int yellowBarWidth = (int)((dataSize * (double)(75)) * Program.screenWidth / 640);

            Rectangle yellowRectangle = new Rectangle(xYOffsetEnergyBar, xYOffsetEnergyBar, yellowBarWidth, energyBarThickness);
            Rectangle redRectangle = new Rectangle(yellowBarWidth + xYOffsetEnergyBar, xYOffsetEnergyBar, maxEnergyBarWidth - yellowBarWidth, energyBarThickness);

            surface.Fill(yellowRectangle, Color.Yellow);
            surface.Fill(redRectangle, Color.Red);


            if (!isPlayerReady)
            {
                surface.Blit(pausedText, new System.Drawing.Point(Program.screenWidth / 2 - pausedText.GetWidth() / 2, Program.screenHeight * 3 / 4));
            }
        }

        internal static void InitCachedSurfaces()
        {
            maxEnergyBarWidth = (int)(75 * Program.screenWidth / 640);
            xYOffsetEnergyBar = (int)(16 * Program.screenWidth / 640);
            energyBarThickness = (int)(8 * Program.screenWidth / 640);
            pausedText = GameMenu.GetFontText("move left or right to resume", Color.White);
            genericSkillInfoSurface = null;
        }

        
        internal static void UpdateKarmaCounter(Surface mainSurface, int karmaValue, double timeDelta)
        {
            if (karmaValue != previousGenericSkillValue || genericSkillInfoSurface == null)
            {
                genericSkillInfoSurface = GameMenu.GetFontText("Karma: " + karmaValue + " / " + Program.musicNoteCountForBodhi);
                previousGenericSkillValue = karmaValue;
                cycleDisplayGenericSkill.Fire();
            }

            if (cycleDisplayGenericSkill.IsFired)
            {
                cycleDisplayGenericSkill.Increment(timeDelta);
                mainSurface.Blit(genericSkillInfoSurface, new Point(xYOffsetEnergyBar, xYOffsetEnergyBar));
            }
        }

        internal static void UpdateMusicNoteCounter(Surface mainSurface, int noteCount, double timeDelta)
        {
            if (noteCount != previousGenericSkillValue || genericSkillInfoSurface == null)
            {
                genericSkillInfoSurface = GameMenu.GetFontText(noteCount.ToString());
                previousGenericSkillValue = noteCount;
                cycleDisplayGenericSkill.Fire();
            }

            if (cycleDisplayGenericSkill.IsFired)
            {
                /*cycleDisplayGenericSkill.Increment(timeDelta);*/
                mainSurface.Blit(genericSkillInfoSurface, new Point(xYOffsetEnergyBar, xYOffsetEnergyBar));
            }
        }

        internal static void UpdateExpCounter(Surface mainSurface, int experience, int experienceForNextLevel, int playerLevel, double timeDelta)
        {
            if (experience != previousGenericSkillValue || playerLevel != previousGenericSkillValue2 || genericSkillInfoSurface == null)
            {
                Surface levelLine = GameMenu.GetFontText("Level: " + (playerLevel + 1));
                Surface expLine = GameMenu.GetFontText("Exp: " + experience + " / " + experienceForNextLevel);

                genericSkillInfoSurface = new Surface(Math.Max(levelLine.Width, expLine.Width), levelLine.Height + expLine.Height);
                genericSkillInfoSurface.Transparent = true;

                genericSkillInfoSurface.Blit(levelLine);
                genericSkillInfoSurface.Blit(expLine, new Point(0, levelLine.Height));

                previousGenericSkillValue = experience;
                previousGenericSkillValue2 = playerLevel;
                cycleDisplayGenericSkill.Fire();
            }

            mainSurface.Blit(genericSkillInfoSurface, new Point(xYOffsetEnergyBar, xYOffsetEnergyBar * 2));
        }
        #endregion
    }
}
