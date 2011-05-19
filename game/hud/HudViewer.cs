using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using System.Drawing;

namespace AbrahmanAdventure.hud
{
    class HudViewer
    {
        #region Fields and parts
        /// <summary>
        /// Surface to draw on
        /// </summary>
        private Surface surface;

        private int maxBarWidth;

        private int xYOffset;

        private int barThickness;
        #endregion

        #region Constructor
        /// <summary>
        /// Create hud viewer
        /// </summary>
        /// <param name="surface">surface to draw on</param>
        public HudViewer(Surface surface)
        {
            this.surface = surface;
            maxBarWidth = (int)(75 * Program.screenWidth / 640);
            xYOffset = (int)(16 * Program.screenWidth / 640);
            barThickness = (int)(8 * Program.screenWidth / 640);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Show Hud
        /// </summary>
        /// <param name="playerHealth">player's health (1.0 = default max)</param>
        internal void Update(double playerHealth)
        {
            #warning Fix this method, it is buggy for now
            playerHealth = 0.8;
            int yellowBarWidth = (int)((playerHealth * (double)(75)) * Program.screenWidth / 640);

            Rectangle yellowRectangle = new Rectangle(xYOffset, xYOffset, yellowBarWidth, barThickness);
            Rectangle redRectangle = new Rectangle(yellowBarWidth, xYOffset, maxBarWidth - yellowBarWidth, barThickness);
            
            surface.Fill(yellowRectangle, Color.Yellow);
            surface.Fill(redRectangle, Color.Red);
        }
        #endregion
    }
}
