using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Manage sprite position in level
    /// </summary>
    internal static class SpriteDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch sprite on level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchSprites(Level level, SpritePopulation spritePopulation, int skillLevel, WaterInfo waterInfo, Random random)
        {
            MonsterDispatcher.DispatchMonsters(level, spritePopulation, skillLevel, random);
            VortexDispatcher.DispatchVortexes(level, spritePopulation, skillLevel, random);
            TrampolineDispatcher.DispatchTrampolines(level, spritePopulation, random);
            MusicNoteDispatcher.DispatchMusicNotes(level, spritePopulation, random);
            AddedBlockMemory addedBlockMemory = BlockDispatcher.DispatchBlocks(level, spritePopulation, random);
            PipeDispatcher.DispatchPipes(level, spritePopulation, skillLevel, waterInfo, random);
        }

        /// <summary>
        /// Get random ground
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="random">random number generator</param>
        /// <param name="xPosition">x position to be visible</param>
        /// <returns>random ground</returns>
        internal static Ground GetRandomVisibleGround(Level level, Random random, double xPosition)
        {
            return GetRandomVisibleGround(level, random, xPosition, false);
        }

        /// <summary>
        /// Get random ground
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="random">random number generator</param>
        /// <param name="xPosition">x position to be visible</param>
        /// <param name="isConsiderCeilingAsGround">whether we consider ceiling as a ground like other grounds (default: false)</param>
        /// <returns>random ground</returns>
        internal static Ground GetRandomVisibleGround(Level level, Random random, double xPosition, bool isConsiderCeilingAsGround)
        {
            if (isConsiderCeilingAsGround && level.Ceiling != null && random.Next(0, level.Count + 1) == 1)
                return level.Ceiling;

            Ground ground;

            int tryCount = 0;
            do
            {
                ground = level[random.Next(level.Count)];
                tryCount++;
            } while ((!IGroundHelper.IsGroundVisible(ground,level,xPosition) || ground[xPosition] >= Program.holeHeight) && tryCount < 20);

            if (tryCount >= 20)
                ground = IGroundHelper.GetHighestGround(level, xPosition);

            return ground;
        }

        /// <summary>
        /// PreCache some sprite surfaces
        /// </summary>
        internal static void PreCacheSpriteSurfaces()
        {
            Random spriteCachingRandom = new Random();
            MushroomSprite mushroom = new MushroomSprite(0, 0, spriteCachingRandom);
            PeyoteSprite peyote = new PeyoteSprite(0, 0, spriteCachingRandom);
            RastaHatSprite rastaHat = new RastaHatSprite(0, 0, spriteCachingRandom);
            MusicNoteSprite musicNote = new MusicNoteSprite(0, 0, spriteCachingRandom);
            WhiskySprite whisky = new WhiskySprite(0, 0, spriteCachingRandom);
            ExplosionSprite explosion = new ExplosionSprite(0, 0, spriteCachingRandom);
            HelmetSprite helmet = new HelmetSprite(0, 0, spriteCachingRandom, true);
            BibleSprite bible = new BibleSprite(0, 0, spriteCachingRandom);
            CrystalBallSprite crystalBall = new CrystalBallSprite(0, 0, spriteCachingRandom);
            PillSprite pill = new PillSprite(0, 0, spriteCachingRandom);
            BeaverSprite beaverSprite = new BeaverSprite(0, 0, spriteCachingRandom);
            CornSprite cornSprite = new CornSprite(0, 0, spriteCachingRandom);
        }
        #endregion
    }
}
