using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.hud
{
    /// <summary>
    /// User guide for sprites
    /// </summary>
    internal static class SpriteGuide
    {
        #region Fields and parts
        /// <summary>
        /// List of sprites
        /// </summary>
        private static List<SideScrollerSprite> __spriteList;

        private static Random random = new Random();
        #endregion

        #region Private Methods
        private static List<SideScrollerSprite> BuildSpriteList()
        {
            List<MonsterSprite> monsterSpriteList = new List<MonsterSprite>();
            List<SideScrollerSprite> staticSpriteList = new List<SideScrollerSprite>();
            List<SideScrollerSprite> spriteList = new List<SideScrollerSprite>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(SideScrollerSprite)) && !type.IsAbstract && !type.IsInterface)
                {
                    object[] constructorArgumentList;
                    Type[] constructorArgumentTypeList;

                    if (type == typeof(RiotControlSprite) || type == typeof(VortexSprite) || type == typeof(HelmetSprite))
                    {
                        constructorArgumentList = new object[4];
                        constructorArgumentList[0] = 0.0;
                        constructorArgumentList[1] = 0.0;
                        constructorArgumentList[2] = random;
                        constructorArgumentList[3] = false;

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
                    ConstructorInfo constructorInfo = type.GetConstructor(constructorArgumentTypeList);

                    SideScrollerSprite sprite = (SideScrollerSprite)constructorInfo.Invoke(constructorArgumentList);

                    if (sprite.TutorialComment != null)
                    {
                        if (sprite is MonsterSprite)
                        {
                            monsterSpriteList.Add((MonsterSprite)sprite);
                        }
                        else
                        {
                            staticSpriteList.Add(sprite);
                        }
                    }
                }
            }

            monsterSpriteList = new List<MonsterSprite>(from sprite in monsterSpriteList orderby sprite.SkillDispatchRatio select sprite);

            foreach (SideScrollerSprite sprite in staticSpriteList)
                spriteList.Add(sprite);

            foreach (MonsterSprite sprite in monsterSpriteList)
                spriteList.Add(sprite);

            return spriteList;
        }
        #endregion

        #region Internal Methods
        internal static void ClearSpriteList()
        {
            __spriteList = null;
        }
        #endregion

        #region Properties
        public static List<SideScrollerSprite> SpriteList
        {
            get
            {
                if (__spriteList == null)
                    __spriteList = BuildSpriteList();
                return __spriteList;
            }
        }

        public static int Count
        {
            get { return SpriteList.Count; }
        }
        #endregion
    }
}
