using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.level;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.textGenerator;
using AbrahmanAdventure.hud;
using AbrahmanAdventure.physics;
using AbrahmanAdventure.audio;

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
        /// Sky's color (HSL)
        /// </summary>
        private ColorHsl skyColorHsl;

        /// <summary>
        /// Random number generator using local seed
        /// </summary>
        private Random random;

        /// <summary>
        /// Level's sky
        /// </summary>
        private Sky sky;

        /// <summary>
        /// Level's music moode
        /// </summary>
        private MusicMood musicMood;

        /// <summary>
        /// Name of the environment
        /// </summary>
        private string name;

        /// <summary>
        /// Seed for random number generator
        /// </summary>
        private int seed;

        /// <summary>
        /// Whether game state must be recreated because we change the world
        /// </summary>
        private bool isExpired = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="seed">seed for random number generator</param>
        public GameState(int seed) : this(seed, null, null)
        {
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="seed">seed for random number generator</param>
        /// <param name="surfaceToDrawLoadingProgress">optional surface to draw loading progress on</param>
        public GameState(int seed, Surface surfaceToDrawLoadingProgress) : this(seed, null, surfaceToDrawLoadingProgress)
        {
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="seed">seed for random number generator</param>
        /// <param name="playerSprite">player sprite (if null, it will create a new one)</param>
        /// <param name="surfaceToDrawLoadingProgress">optional surface to draw loading progress on</param>
        public GameState(int seed, PlayerSprite playerSprite, Surface surfaceToDrawLoadingProgress)
        {
            this.seed = seed;
            random = new Random(seed);
            name = WordGenerator.GenerateName(random);
            colorTheme = new ColorTheme(random);
            skyColorHsl = new ColorHsl(random);
            musicMood = new MusicMood(random);

            Surface planetSurface = PlanetViewer.ShowPlanet(name, skyColorHsl, colorTheme, random);

            if (surfaceToDrawLoadingProgress != null)
            {
                surfaceToDrawLoadingProgress.Blit(planetSurface,new System.Drawing.Point(0,0), planetSurface.GetRectangle());
                GameMenu.ShowLoading(surfaceToDrawLoadingProgress);
                surfaceToDrawLoadingProgress.Update();
            }

            sky = new Sky(random, skyColorHsl);
            level = new Level(random, colorTheme, seed);
            spritePopulation = new SpritePopulation();

            if (playerSprite != null)
                this.playerSprite = playerSprite;
            else
                this.playerSprite = new PlayerSprite(0, Program.totalHeightTileCount / -2, random);

            spritePopulation.Add(this.playerSprite);

            #warning Eventually remove test sprites
            AddHardCodedTestSprite();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add some hardcoded test sprites
        /// </summary>
        /// <param name="random">random number generator</param>
        private void AddHardCodedTestSprite()
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
            spritePopulation.Add(new MormonSprite(-55, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new GypsySprite(-75, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new DoctorSprite(-85, Program.totalHeightTileCount / -2, random));

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
            spritePopulation.Add(new AnarchyBlockSprite(17, -15, random, true));
            
            
            spritePopulation.Add(new AnarchyBlockSprite(17, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(18, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(19, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(20, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(21, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(22, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(23, -10, random));
            spritePopulation.Add(new AnarchyBlockSprite(24, -10, random));

            spritePopulation.Add(new AnarchyBlockSprite(19, -5, random));
            spritePopulation.Add(new AnarchyBlockSprite(20, -5, random));

            spritePopulation.Add(new VortexSprite(-60, Program.totalHeightTileCount / -2, random, true));
            spritePopulation.Add(new VortexSprite(-75, Program.totalHeightTileCount / -2, random, true));

            spritePopulation.Add(new MusicNoteSprite(25, Program.totalHeightTileCount / -2, random));
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Create "warp back" vortex sprites, ususally we create only one, but there could be more than one
        /// </summary>
        /// <param name="listWarpBackSeed"></param>
        internal void AddWarpBackVortexList(List<int> listWarpBackSeed)
        {
            double xOffset = 0;//-1.5;
            foreach (int seed in listWarpBackSeed)
            {
                bool isFoundIndenticalVortex = false;
                foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
                    if (sprite is VortexSprite && ((VortexSprite)sprite).DestinationSeed == seed)
                        isFoundIndenticalVortex = true;
                if (!isFoundIndenticalVortex)
                {
                    VortexSprite warpBack = new VortexSprite(xOffset, Program.totalHeightTileCount / -2, random, seed, false);
                    spritePopulation.Add(warpBack);
                    xOffset -= 5;
                }
            }
        }

        /// <summary>
        /// Move player so it is in front of vortex that goes to the provided seed
        /// </summary>
        /// <param name="seed">seed</param>
        internal void MovePlayerToVortexGoingToSeed(int seed)
        {
            foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
                if (sprite is VortexSprite && ((VortexSprite)sprite).DestinationSeed == seed)
                {
                    playerSprite.XPosition = sprite.XPosition;
                    playerSprite.YPosition = sprite.YPosition;
                    playerSprite.IGround = sprite.IGround;
                    return;
                }
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


        /// <summary>
        /// Sky
        /// </summary>
        public Sky Sky
        {
            get { return sky; }
        }

        /// <summary>
        /// Whether game state needs to be recreated (changing world)
        /// </summary>
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        /// <summary>
        /// Seed that was used to generate game state
        /// </summary>
        public int Seed
        {
            get { return seed; }
        }
        #endregion
    }
}
