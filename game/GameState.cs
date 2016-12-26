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
using AbrahmanAdventure.audio.midi.generator;

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
        /// Background's color (HSL)
        /// </summary>
        private ColorHsl backgroundColorHsl;

        /// <summary>
        /// Represents the game mode ("action platform", "adventure rpg", "extreme action"
        /// </summary>
        private AbstractGameMode gameMode;

        /// <summary>
        /// Random number generator using local seed
        /// </summary>
        private Random random;

        /// <summary>
        /// Level's background
        /// </summary>
        private AbstractBackground background;

        /// <summary>
        /// Columns (1st parallax)
        /// </summary>
        private ColumnSet columnSet = null;

        /// <summary>
        /// Parallax mountains
        /// </summary>
        private HillSet hillSet = null;

        /// <summary>
        /// Water info (or null if no water)
        /// </summary>
        private WaterInfo waterInfo = null;

        /// <summary>
        /// Song
        /// </summary>
        private IRiff song;

        /// <summary>
        /// Name of the environment
        /// </summary>
        private string name;

        /// <summary>
        /// Seed for random number generator
        /// </summary>
        private int seed;

        /// <summary>
        /// Current skill level
        /// </summary>
        private int skillLevel;

        /// <summary>
        /// Whether player is ready to play (has moved or jumped)
        /// </summary>
        private bool isPlayerReady = false;

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
        /// <param name="skillLevel">skill level</param>
        public GameState(int seed, int skillLevel, bool isExtremeAction, bool isAdventureRpg, bool isRacing)
            : this(seed, skillLevel, null, null, isExtremeAction, isAdventureRpg, isRacing)
        {
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="seed">seed for random number generator</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="surfaceToDrawLoadingProgress">optional surface to draw loading progress on</param>
        public GameState(int seed, int skillLevel, Surface surfaceToDrawLoadingProgress, bool isExtremeAction, bool isAdventureRpg, bool isRacing)
            : this(seed, skillLevel, null, surfaceToDrawLoadingProgress, isExtremeAction, isAdventureRpg, isRacing)
        {
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="seed">seed for random number generator</param>
        /// <param name="playerSprite">player sprite (if null, it will create a new one)</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="surfaceToDrawLoadingProgress">optional surface to draw loading progress on</param>
        public GameState(int seed, int skillLevel, PlayerSprite playerSprite, Surface surfaceToDrawLoadingProgress, bool isExtremeAction, bool isAdventureRpg, bool isRacing)
        {
            this.seed = seed;
            this.skillLevel = skillLevel;
            random = new Random(seed);
            name = WordGenerator.GenerateName(random);
            colorTheme = new ColorTheme(random);
            backgroundColorHsl = new ColorHsl(random);

            if (isAdventureRpg)
                gameMode = new AdventureRpgGameMode(surfaceToDrawLoadingProgress);
            else if (isExtremeAction)
                gameMode = new ExtremeActionGameMode(surfaceToDrawLoadingProgress);
            else if (isRacing)
                gameMode = new RacingGameMode(surfaceToDrawLoadingProgress);
            else
                gameMode = new PlatformerGameMode(surfaceToDrawLoadingProgress);

            if (Program.isTellPlanetName)
            {
                TutorialTalker.Talk("Planet " + name);
            }

            Surface planetSurface = PlanetViewer.ShowPlanet(name, skillLevel, backgroundColorHsl, colorTheme, random);

            if (surfaceToDrawLoadingProgress != null)
            {
                surfaceToDrawLoadingProgress.Blit(planetSurface,new System.Drawing.Point(0,0), planetSurface.GetRectangle());
                GameMenu.ShowLoading(surfaceToDrawLoadingProgress);
                surfaceToDrawLoadingProgress.Update();
            }

            if (skillLevel != 0 && random.Next(0, 7) == 1)
                waterInfo = new WaterInfo(backgroundColorHsl, random);

            int seedForColumn = random.Next();
            if (random.Next(0,2) == 1 && Program.isEnableParallaxExtraLayers)
                columnSet = new ColumnSet(seedForColumn, colorTheme);

            seedForColumn = random.Next();
            if (random.Next(0, 2) == 1 && Program.isEnableParallaxExtraLayers)
                hillSet = new HillSet(seedForColumn, colorTheme);

            level = new Level(random, colorTheme, seed, skillLevel, waterInfo != null, gameMode);

            #region We set the background
            if ((level.Ceiling == null && random.Next(0, 4) == 1) || (level.Ceiling != null && random.Next(0, 4) != 1))
                background = new Wall(random, colorTheme.GetRandomColor(random));
            else
                background = new Sky(random, backgroundColorHsl);
            #endregion

            spritePopulation = new SpritePopulation();

            if (playerSprite != null)
                this.playerSprite = playerSprite;
            else
                this.playerSprite = new PlayerSprite(0, Program.totalHeightTileCount / -2, random);

            spritePopulation.Add(this.playerSprite);

            this.playerSprite.YPosition = IGroundHelper.GetHighestGround(this.level, this.playerSprite.XPosition)[this.playerSprite.XPosition];

            SpriteDispatcher.DispatchSprites(level, spritePopulation, skillLevel, waterInfo, gameMode, random);

            song = SongGenerator.BuildSong(seed, skillLevel, SongType.Level, gameMode);

            //AddHardCodedTestSprite();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add some hardcoded test sprites
        /// </summary>
        /// <param name="random">random number generator</param>
        private void AddHardCodedTestSprite()
        {
            /*PipeSprite sourcePipe1 = AddHardCodedPipeAndDrillTestSprite(-4, false, true, null);
            PipeSprite sourcePipe2 = AddHardCodedPipeAndDrillTestSprite(-8, true, true, null);
            AddHardCodedPipeAndDrillTestSprite(4, false, false, sourcePipe1);
            AddHardCodedPipeAndDrillTestSprite(8, true, false, sourcePipe2);*/

            //spritePopulation.Add(new HorseSprite(20, Program.totalHeightTileCount / -2, random));
            //spritePopulation.Add(new PuppetSprite(20, Program.totalHeightTileCount / -2, random));
            
            /*spritePopulation.Add(new HamburgerSprite(20, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new BlobSprite(40, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new RiotControlSprite(60, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new HamburgerSprite(65, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new SnakeSprite(80, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new JewSprite(120, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new RaptorSprite(160, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new RaptorJesusSprite(220, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new RonaldSprite(280, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new MouseSprite(330, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new JewSprite(-10, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new TrampolineSprite(10, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new PriestSprite(-30, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new MuslimSprite(-40, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new MormonSprite(-55, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new GypsySprite(-75, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new DoctorSprite(-85, Program.totalHeightTileCount / -2, random));
            spritePopulation.Add(new FarmerSprite(-100, Program.totalHeightTileCount / -2, random));

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

            spritePopulation.Add(new MusicNoteSprite(25, Program.totalHeightTileCount / -2, random));*/
        }

        /// <summary>
        /// Add hardcoded pipe and drill (test sprites)
        /// </summary>
        /// <param name="xPosition">x position of pipe</param>
        /// <param name="isBlack">whether drill is black</param>
        /// <param name="isUpSide">whether pipe is upside</param>
        /// <param name="linkedPipe">pipe linked to it (none if null)</param>
        /// <returns>Pipe added</returns>
        private PipeSprite AddHardCodedPipeAndDrillTestSprite(double xPosition, bool isBlack, bool isUpSide, PipeSprite linkedPipe)
        {
            double pipeHeight;

            if (isUpSide)
                pipeHeight = Program.totalHeightTileCount / -6.5;
            else
                pipeHeight = Program.totalHeightTileCount / -5.0;

            PipeSprite pipeSprite = new PipeSprite(xPosition, pipeHeight, isUpSide, random);
            DrillSprite drillSprite = new DrillSprite(xPosition, Program.totalHeightTileCount / -2, isBlack, isUpSide, random);

            spritePopulation.Add(pipeSprite);
            spritePopulation.Add(drillSprite);

            if (isUpSide)
                drillSprite.YPosition = pipeSprite.TopBound;
            else
                drillSprite.TopBound = pipeSprite.YPosition;

            if (linkedPipe != null)
                pipeSprite.LinkedPipe = linkedPipe;

            pipeSprite.LinkedDrill = drillSprite;

            return pipeSprite;
            
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
                    warpBack.YPosition = IGroundHelper.GetHighestGround(level, warpBack.XPosition)[warpBack.XPosition];
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
        /// Game mode
        /// </summary>
        public AbstractGameMode GameMode
        {
            get { return gameMode; }
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
        /// Background
        /// </summary>
        public AbstractBackground Background
        {
            get { return background; }
        }

        /// <summary>
        /// 1st parallax column background
        /// </summary>
        public ColumnSet ColumnSet
        {
            get { return columnSet; }
        }

        /// <summary>
        /// Hills
        /// </summary>
        public HillSet HillSet
        {
            get { return hillSet; }
        }

        /// <summary>
        /// Info about water
        /// </summary>
        public WaterInfo WaterInfo
        {
            get { return waterInfo; }
        }

        /// <summary>
        /// Song
        /// </summary>
        public IRiff Song
        {
            get { return song; }
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
        /// Whether player is ready to play (has moved or jumped)
        /// </summary>
        public bool IsPlayerReady
        {
            get { return isPlayerReady; }
            set { isPlayerReady = value; }
        }

        /// <summary>
        /// Seed that was used to generate game state
        /// </summary>
        public int Seed
        {
            get { return seed; }
        }

        /// <summary>
        /// Current skill level
        /// </summary>
        public int SkillLevel
        {
            get { return skillLevel; }
        }
        #endregion
    }
}
