using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.textGenerator;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Represents the level and the sprite inside
    /// </summary>
    internal class GameState
    {
        #region Fields and parts
        /// <summary>
        /// Level (grounds)
        /// </summary>
        private Level level;

        /// <summary>
        /// All the sprites in the level
        /// </summary>
        private SpritePopulation spritePopulation;

        /// <summary>
        /// Player's sprite
        /// </summary>
        private PlayerSprite playerSprite;

        /// <summary>
        /// Color theme
        /// </summary>
        private ColorTheme colorTheme;

        /// <summary>
        /// Name of the environment
        /// </summary>
        private string name;
        #endregion

        #region Constructor
        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="random"></param>
        public GameState(Random random)
        {
            name = TextGenerator.GenerateName(random);
            colorTheme = new ColorTheme(random);
            level = new Level(random, colorTheme);
            spritePopulation = new SpritePopulation();
            playerSprite = new PlayerSprite(0, Program.totalHeightTileCount / -2, random);
            spritePopulation.Add(playerSprite);

            #warning Eventually remove test sprites
            AddHardCodedTestSprite(random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add some hardcoded test sprites
        /// </summary>
        /// <param name="random">random number generator</param>
        private void AddHardCodedTestSprite(Random random)
        {
            spritePopulation.Add(new HamburgerSprite(20, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new BlobSprite(40, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new RiotControlSprite(60, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new HamburgerSprite(65, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new SnakeSprite(80, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new JewSprite(120, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new RaptorSprite(160, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new JewSprite(-10, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new Trampoline(10, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new PriestSprite(-30, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new MuslimSprite(-40, Program.totalHeightTileCount / -2, random));

            spritePopulation.Add(new BrickSprite(-10, -10, random, true));
            spritePopulation.Add(new BrickSprite(-11, -10, random, true));
            spritePopulation.Add(new BrickSprite(-11, -11, random, true));

            spritePopulation.Add(new BrickSprite(5, -10, random, true));
            spritePopulation.Add(new BrickSprite(5, -11, random, true));
            spritePopulation.Add(new BrickSprite(5, -12, random));
            spritePopulation.Add(new BrickSprite(6, -12, random));
            spritePopulation.Add(new BrickSprite(5, -13, random));
            spritePopulation.Add(new BrickSprite(6, -10, random));
            spritePopulation.Add(new BrickSprite(7, -10, random));
            spritePopulation.Add(new BrickSprite(8, -10, random));

            spritePopulation.Add(new AnarchyBlockSprite(13, -20, random));
            spritePopulation.Add(new AnarchyBlockSprite(14, -20, random, true));
            spritePopulation.Add(new AnarchyBlockSprite(15, -15, random));
            spritePopulation.Add(new AnarchyBlockSprite(16, -15, random, true));
            spritePopulation.Add(new AnarchyBlockSprite(17, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(18, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(19, -5, random));
            spritePopulation.Add(new AnarchyBlockSprite(20, -5, random));

            spritePopulation.Add(new MusicNoteSprite(25, Program.totalHeightTileCount / -2, random));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Current level
        /// </summary>
        public Level Level
        {
            get { return level; }
        }

        /// <summary>
        /// All the sprites in the level
        /// </summary>
        public SpritePopulation SpritePopulation
        {
            get { return spritePopulation; }
        }

        /// <summary>
        /// Player's sprite
        /// </summary>
        public PlayerSprite PlayerSprite
        {
            get { return playerSprite; }
        }

        /// <summary>
        /// Name of the environment
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Color theme
        /// </summary>
        public ColorTheme ColorTheme
        {
            get { return colorTheme; }
        }
        #endregion
    }
}
