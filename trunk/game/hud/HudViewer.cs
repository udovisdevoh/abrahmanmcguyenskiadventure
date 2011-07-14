using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using SdlDotNet.Core;
using System.Drawing;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// To view hud (energy, how many lives, current level, time left, score)
    /// </summary>
    internal class HudViewer
    {
        #region Fields and parts
        /// <summary>
        /// Surface to draw on
        /// </summary>
        private Surface surface;

        /// <summary>
        /// Paused text
        /// </summary>
        private Surface pausedText;

        /// <summary>
        /// Maximum width of energy bar (in pixels)
        /// </summary>
        private int maxEnergyBarWidth;

        /// <summary>
        /// Both X and Y (they are the same) offset (in pixels) for energy bar position
        /// </summary>
        private int xYOffsetEnergyBar;

        /// <summary>
        /// Thickness of energy bar
        /// </summary>
        private int energyBarThickness;
        #endregion

        #region Constructor
        /// <summary>
        /// Create hud viewer
        /// </summary>
        /// <param name="surface">surface to draw on</param>
        public HudViewer(Surface surface)
        {
            this.surface = surface;
            maxEnergyBarWidth = (int)(75 * Program.screenWidth / 640);
            xYOffsetEnergyBar = (int)(16 * Program.screenWidth / 640);
            energyBarThickness = (int)(8 * Program.screenWidth / 640);
            pausedText = GameMenu.GetFontText("move left or right to resume", Color.White);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Show Hud
        /// </summary>
        /// <param name="playerHealth">player's health (1.0 = default max)</param>
        internal void Update(double playerHealth, bool isPlayerReady)
        {
            int yellowBarWidth = (int)((playerHealth * (double)(75)) * Program.screenWidth / 640);

            Rectangle yellowRectangle = new Rectangle(xYOffsetEnergyBar, xYOffsetEnergyBar, yellowBarWidth, energyBarThickness);
            Rectangle redRectangle = new Rectangle(yellowBarWidth + xYOffsetEnergyBar, xYOffsetEnergyBar, maxEnergyBarWidth - yellowBarWidth, energyBarThickness);
            
            surface.Fill(yellowRectangle, Color.Yellow);
            surface.Fill(redRectangle, Color.Red);

            if (!isPlayerReady)
            {
                surface.Blit(pausedText, new System.Drawing.Point(Program.screenWidth / 2 - pausedText.GetWidth() / 2, Program.screenHeight * 3 / 4));
            }
        }
        #endregion
    }
}
