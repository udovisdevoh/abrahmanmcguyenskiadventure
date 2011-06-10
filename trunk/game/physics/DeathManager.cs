using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

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
        /// <param name="spritePopulation"></param>
        internal void Update(AbstractSprite sprite, double timeDelta, SpritePopulation spritePopulation, HashSet<AbstractSprite> visibleSpriteList)
        {
            if (sprite.YPosition > Program.totalHeightTileCount)
                sprite.IsAlive = false;

            if (sprite.IsAnnihilateOnExitScreen && !visibleSpriteList.Contains(sprite))
            {
                sprite.IsAlive = false;
                sprite.YPosition = Program.totalHeightTileCount + 1.0;
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
                    ((PlayerSprite)sprite).InvincibilityCycle.StopAndReset();
                    ((PlayerSprite)sprite).IsTiny = true;

                    foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
                    {
                        if (otherSprite is AnarchyBlockSprite)
                            ((AnarchyBlockSprite)otherSprite).IsFinalized = false;
                        else if (otherSprite is PeyoteSprite || otherSprite is RastaHatSprite || otherSprite is WhiskySprite || otherSprite is MushroomSprite)
                        {
                            otherSprite.IsAlive = false;
                            otherSprite.YPosition = Program.totalHeightTileCount + 1.0;
                        }
                    }
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
