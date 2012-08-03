using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// A linkage that can have child components
    /// </summary>
    interface ILinkageNode
    {
        /// <summary>
        /// Add a child component to the linkage
        /// </summary>
        /// <param name="childComponent">child component</param>
        void AddChild(AbstractLinkage childComponent);

        /// <summary>
        /// List of child components
        /// </summary>
        List<AbstractLinkage> ChildList { get; }
    }
}
