using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite that does damage when touched
    /// </summary>
    interface IEvilSprite
    {
        /// <summary>
        /// Damage
        /// </summary>
        double AttackStrengthCollision
        {
            get;
        }
    }
}
