using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

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
        internal void Update(AbstractSprite sprite, float timeDelta, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList, GameMetaState gameMetaState, GameState gameState, ILevelViewer levelViewer)
        {
            if (sprite.YPosition > Program.totalHeightTileCount)
                sprite.IsAlive = false;

            if (sprite.IsAnnihilateOnExitScreen && !visibleSpriteList.Contains(sprite))
            {
                sprite.IsAlive = false;
                sprite.YPosition = Program.totalHeightTileCount + 1.0f;
            }

            if (sprite.IsAlive)
                return;

            if (sprite.YPosition > Program.totalHeightTileCount)
            {
                if (sprite is PlayerSprite)
                {
                    sprite.XPosition = 0;
                    sprite.YPosition = Program.totalHeightTileCount / -2;
                    sprite.IsAlive = true;
                    sprite.Health = ((PlayerSprite)sprite).DefaultHealth;
                    ((PlayerSprite)sprite).IsDoped = false;
                    ((PlayerSprite)sprite).IsRasta = false;
                    ((PlayerSprite)sprite).IsBeaver = false;
                    ((PlayerSprite)sprite).InvincibilityCycle.StopAndReset();
                    ((PlayerSprite)sprite).IsTiny = true;
                    sprite.CarriedSprite = null;

                    if (gameMetaState.PreviousSeed != -1)
                        gameState.MovePlayerToVortexGoingToSeed(gameMetaState.PreviousSeed);

                    gameState.Level.ClearBeaverDestruction();
                    levelViewer.ClearCache();

                    #region We remove powerups after player dies
                    foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
                    {
                        if (otherSprite is AnarchyBlockSprite)
                            ((AnarchyBlockSprite)otherSprite).IsFinalized = false;
                        else if (otherSprite is PeyoteSprite || otherSprite is RastaHatSprite || otherSprite is WhiskySprite || otherSprite is MushroomSprite || otherSprite is BeaverSprite)
                        {
                            otherSprite.IsAlive = false;
                            otherSprite.YPosition = Program.totalHeightTileCount + 1.0f;
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
                sprite.YPosition += 0.25f;//we make it fall even faster so it doesn't get stucked by falling on grounds
            }
        }
    }
}
