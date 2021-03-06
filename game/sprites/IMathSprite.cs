﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite for which collision detection is done in a custom way through math formulas
    /// </summary>
    interface IMathSprite
    {
        /// <summary>
        /// Whether Math sprite is in collision with other sprite
        /// </summary>
        /// <param name="otherSprite">other sprite</param>
        /// <returns>Whether Math sprite is in collision with other sprite</returns>
        bool IsDetectCollision(AbstractSprite otherSprite);
    }
}
