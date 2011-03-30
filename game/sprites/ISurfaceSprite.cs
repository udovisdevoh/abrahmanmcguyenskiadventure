using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// A sprite for which we can extract texture
    /// </summary>
    interface ISurfaceSprite
    {
        /// <summary>
        /// Sprite's surface
        /// </summary>
        /// <returns>Sprite's surface</returns>
        Surface GetCurrentSurface();
    }
}
