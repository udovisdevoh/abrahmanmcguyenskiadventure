using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages dead sprite (make fall / annihilate / respawn)
    /// </summary>
    internal class DeathManager
    {
        /// <summary>
        /// Make fall, annhilate or respawn dead sprite if sprite is dead
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="timeDelta"></param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="gameMetaState">game meta state</param>
        /// <param name="gameState">game state</param>
        /// <param name="levelViewer">level viewer</param>
        /// <param name="visibleSpriteList">visible sprite list</param>
        internal void Update(AbstractSprite sprite, double timeDelta, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList, GameMetaState gameMetaState, GameState gameState, ILevelViewer levelViewer)
        {
            if (sprite.YPosition > Program.totalHeightTileCount / 2 + 3)
                sprite.IsAlive = false;

            if (sprite.IsAnnihilateOnExitScreen && !visibleSpriteList.Contains(sprite))
            {
                sprite.IsAlive = false;
                sprite.YPosition = Program.totalHeightTileCount + 1.0;
            }

            if (sprite.IsAlive)
                return;

            if (sprite.YPosition > Program.totalHeightTileCount / 2 + 3)
            {
                if (sprite is PlayerSprite)
                {
                    sprite.XPosition = 0;
                    //sprite.YPosition = Program.totalHeightTileCount / -2;
                    sprite.YPosition = IGroundHelper.GetHighestGround(gameState.Level, sprite.XPosition)[sprite.XPosition];

                    if (SongPlayer.IRiff == SongGenerator.GetInvincibilitySong(gameState.Seed)) //If player died (in hole) while invincible
                    {
                        SongPlayer.StopSync();
                        SongPlayer.IRiff = gameState.Song;
                        SongPlayer.PlayAsync();
                    }

                    sprite.IsAlive = true;
                    sprite.Health = ((PlayerSprite)sprite).DefaultHealth;
                    ((PlayerSprite)sprite).IsDoped = false;
                    ((PlayerSprite)sprite).IsRasta = false;
                    ((PlayerSprite)sprite).IsBeaver = false;
                    ((PlayerSprite)sprite).InvincibilityCycle.StopAndReset();
                    ((PlayerSprite)sprite).HitCycle.StopAndReset();
                    ((PlayerSprite)sprite).PunchedCycle.StopAndReset();
                    ((PlayerSprite)sprite).IsTiny = true;
                    ((PlayerSprite)sprite).FromVortexCycle.Fire();
                    sprite.CarriedSprite = null;

                    if (gameMetaState.PreviousSeed != -1)
                        gameState.MovePlayerToVortexGoingToSeed(gameMetaState.PreviousSeed);

                    gameState.Level.ClearBeaverDestruction();
                    levelViewer.ClearCache();

                    gameState.IsPlayerReady = false;

                    #region We remove powerups after player dies
                    foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
                    {
                        if (otherSprite is AnarchyBlockSprite)
                            ((AnarchyBlockSprite)otherSprite).IsFinalized = false;
                        else if (otherSprite is PeyoteSprite || otherSprite is RastaHatSprite || otherSprite is WhiskySprite || otherSprite is MushroomSprite || otherSprite is BeaverSprite || otherSprite is VineSprite)
                        {
                            otherSprite.IsAlive = false;
                            otherSprite.YPosition = Program.totalHeightTileCount + 1.0;
                        }
                    }
                    #endregion

                    GC.Collect();
                }
                else
                {
                    spritePopulation.Remove(sprite);
                }
            }
            else
            {
                sprite.IGround = null;
                sprite.YPosition += 0.25;//we make it fall even faster so it doesn't get stucked by falling on grounds
            }
        }
    }
}
