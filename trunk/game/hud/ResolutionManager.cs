using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// Manages resolution
    /// </summary>
    internal static class ResolutionManager
    {
        #region Internal Methods
        internal static void ChangeResolution(int incrementation, Program program)
        {
            Size[] listModes = Video.ListModes();

            int index = 0;
            foreach (Size size in listModes)
            {
                if (size.Width == Program.screenWidth && size.Height == Program.screenHeight)
                    break;
                index++;
            }

            int newIndex = index + incrementation;

            while (newIndex < 0)
                newIndex += listModes.Count();

            while (newIndex >= listModes.Count())
                newIndex -= listModes.Count();

            Size newMode = listModes[newIndex];

            Program.screenWidth = newMode.Width;
            Program.screenHeight = newMode.Height;
            PersistentConfig.ScreenWidth = Program.screenWidth;
            PersistentConfig.ScreenHeight = Program.screenHeight;
            program.LevelViewer.ClearCache();
            program.GameState.IsPlayerReady = false;
            program.GameState.Level.RenderGroundTextures();
            program.GameState.Background.RenderSurface();
            program.InitSurfaceViewPortRatioSettingsEtc();
            SpriteDispatcher.PreCacheSpriteSurfaces();
        }
        #endregion
    }
}
