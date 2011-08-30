using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
        #endregion
    }
}
