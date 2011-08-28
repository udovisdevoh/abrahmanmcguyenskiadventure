using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// To convert sprites
    /// </summary>
    internal class SpriteConversionManager
    {
        #region Internal Methods
        /// <summary>
        /// Convert sprite to another one
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="oldSprite">sprite to convert (old sprite)</param>
        /// <param name="newSprite">new sprite (to replace old sprite)</param>
        /// <param name="spritePopulation">sprite population</param>
        internal void PerformSpriteConversion(SideScrollerSprite playerSprite, SideScrollerSprite oldSprite, SideScrollerSprite newSprite, SpritePopulation spritePopulation)
        {
            oldSprite.IsAlive = false;
            oldSprite.YPosition = Program.totalHeightTileCount + 1.0;//The sprite will have already fell down
            spritePopulation.Add(newSprite);

            if (newSprite is MonsterSprite && oldSprite is MonsterSprite)
            {
                if (((MonsterSprite)oldSprite).IsInstantKickConvertedSprite)
                {
                    ((MonsterSprite)newSprite).IsNoAiDefaultDirectionWalkingRight = playerSprite.XPosition < oldSprite.XPosition; //default direction for helmet
                    newSprite.CurrentWalkingSpeed = newSprite.WalkingAcceleration;
                }
                else
                {
                    ((MonsterSprite)newSprite).IsWalkEnabled = false;
                    if (((MonsterSprite)newSprite).IsEnableSpontaneousConversion)
                        ((MonsterSprite)newSprite).SpontaneousTransformationCycle.Fire(); //We schedule an eventual transformation from helmet to monster
                }
            }
        }
        #endregion
    }
}
