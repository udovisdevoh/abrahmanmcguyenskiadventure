using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a mechanical component. It could be linkage on a wheel, a pendulum, a seesaw, an aerial lift's attachment or a liana
    /// Platforms or other linkages are attached to it. (platforms and flail balls are linkages)
    /// Linkages can be attached to "rope-grounds", elevator's vertical line, or can be held in the void, or can be affected by gravity and use the platforms as legs
    /// </summary>
    internal abstract class AbstractLinkage : AbstractSprite
    {

    }
}
