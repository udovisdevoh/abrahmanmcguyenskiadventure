using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure.physics
{
    internal class DeathManager
    {
        internal void Update(AbstractSprite sprite, double timeDelta, SpritePopulation spritePopulation)
        {
            if (sprite.YPosition > Program.totalHeightTileCount)
                sprite.IsAlive = false;

            if (!sprite.IsAlive)
            {
                //levelViewer.ClearCache();
                //level = new Level(random);

                if (sprite.YPosition > Program.totalHeightTileCount)
                {
                    if (sprite is PlayerSprite)
                    {
                        sprite.XPosition = 0;
                        sprite.YPosition = Program.totalHeightTileCount / -2;
                        sprite.IsAlive = true;
                    }
                    else
                    {
                        if (!sprite.IsAlive)
                            spritePopulation.Remove(sprite);
                    }
                }
                else
                {
                    sprite.Ground = null;
                    sprite.YPosition += 0.25;//*sprite.MaximumWalkingHeight;
                }
            }
        }
    }
}
