using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    enum LevelIcon { Pyramid, StarPort, Temple2012, TempleOfReligions }

    /// <summary>
    /// Level sprite
    /// </summary>
    internal class LevelSprite : MapSprite
    {
        #region Fields and parts
        private int levelId;

        private int levelSeed;

        private int skillLevel;

        private LevelIcon levelIcon;

        private static Surface pyramidSurface, starPortSurface, temple2012Surface, templeOfReligionsSurface;
        #endregion

        #region Constructor
        public LevelSprite(double xPosition, double yPosition, int levelId, int skillLevel, Random random)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            this.levelId = levelId;
            this.skillLevel = skillLevel;
            levelSeed = random.Next();
            levelIcon = (LevelIcon)random.Next(0, 4);

            if (pyramidSurface == null)
            {
                pyramidSurface = BuildSpriteSurface("./assets/rendered/map/Pyramid.png");
                starPortSurface = BuildSpriteSurface("./assets/rendered/map/StarPort.png");
                temple2012Surface = BuildSpriteSurface("./assets/rendered/map/Temple.png");
                templeOfReligionsSurface = BuildSpriteSurface("./assets/rendered/map/TempleOfReligions.png");
            }
        }
        #endregion

        #region Overrides
        internal override Surface GetSurface()
        {
            switch (levelIcon)
            {
                case LevelIcon.Pyramid:
                    return pyramidSurface;
                case LevelIcon.StarPort:
                    return starPortSurface;
                case LevelIcon.Temple2012:
                    return temple2012Surface;
                default:
                    return templeOfReligionsSurface;
            }
        }
        #endregion
    }
}
