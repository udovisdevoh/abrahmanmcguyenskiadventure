using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    internal class WaterManager
    {
        internal void Update(SideScrollerSprite spriteToUpdate, WaterInfo waterInfo)
        {
            if (waterInfo == null)
            {
                spriteToUpdate.IsInWater = false;
                return;
            }

            bool wasInWater = spriteToUpdate.IsInWater;

            spriteToUpdate.IsInWater = spriteToUpdate.YPosition >= waterInfo.HeightInDouble;

            if (spriteToUpdate is PlayerSprite)
            {
                if (spriteToUpdate.IsInWater && !wasInWater)
                    SoundManager.PlayDiveInSound();
                else if (!spriteToUpdate.IsInWater && wasInWater)
                    SoundManager.PlayDiveOutSound();
            }
        }
    }
}
