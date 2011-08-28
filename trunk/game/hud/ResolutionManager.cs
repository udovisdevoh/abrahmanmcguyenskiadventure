using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

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

            ResetAfterResolutionChange(program);
        }

        internal static void ResetAfterResolutionChange(Program program)
        {
            program.LevelViewer.ClearCache();
            program.InitSurfaceViewPortRatioSettingsEtc();
            if (program.GameState != null)
                program.GameState.IsExpired = true;
            SurfaceSizeCache.Clear();
            ClearAllCachedSpriteSurfaces();
            SpriteGuide.ClearSpriteList();
            //RenderAllSpriteSurfaces();
            SpriteDispatcher.PreCacheSpriteSurfaces();
            VineSprite.ClearCompositeSurfaces();
            LianaSprite.ClearCachedSurfaces();
            GC.Collect();
        }
        #endregion

        #region Private Methods
        private static void ClearAllCachedSpriteSurfaces()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(SideScrollerSprite)) && !type.IsAbstract && !type.IsInterface)
                {
                    FieldInfo[] fieldList = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static);

                    foreach (FieldInfo fieldInfo in fieldList)
                    {
                        if (fieldInfo.FieldType == typeof(Surface))
                        {
                            fieldInfo.SetValue(null, null);
                        }
                    }
                }
            }
        }

        /*private static void RenderAllSpriteSurfaces()
        {
            Random random = new Random();
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(AbstractSprite)) && !type.IsAbstract && !type.IsInterface)
                {
                    object[] constructorArgumentList;
                    Type[] constructorArgumentTypeList;

                    if (type == typeof(RiotControlSprite) || type == typeof(VortexSprite) || type == typeof(HelmetSprite))
                    {
                        constructorArgumentList = new object[4];
                        constructorArgumentList[0] = 0.0;
                        constructorArgumentList[1] = 0.0;
                        constructorArgumentList[2] = random;
                        constructorArgumentList[3] = true;

                        constructorArgumentTypeList = new Type[4];
                        constructorArgumentTypeList[0] = typeof(double);
                        constructorArgumentTypeList[1] = typeof(double);
                        constructorArgumentTypeList[2] = typeof(Random);
                        constructorArgumentTypeList[3] = typeof(bool);
                    }
                    else
                    {
                        constructorArgumentList = new object[3];
                        constructorArgumentList[0] = 0.0;
                        constructorArgumentList[1] = 0.0;
                        constructorArgumentList[2] = random;

                        constructorArgumentTypeList = new Type[3];
                        constructorArgumentTypeList[0] = typeof(double);
                        constructorArgumentTypeList[1] = typeof(double);
                        constructorArgumentTypeList[2] = typeof(Random);
                    }


                    ConstructorInfo constructorInfoGenericMonster = type.GetConstructor(constructorArgumentTypeList);

                    AbstractSprite sprite = (AbstractSprite)constructorInfoGenericMonster.Invoke(constructorArgumentList);
                }
            }
        }*/
        #endregion
    }
}
