using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio.midi.generator;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.map
{
    /// <summary>
    /// Map (in meta-world)
    /// </summary>
    internal class Map
    {
        #region Fields and parts
        /// <summary>
        /// Name of the planet
        /// </summary>
        private string name;

        /// <summary>
        /// List of map sprites
        /// </summary>
        private List<MapSprite> listMapSprite;

        /// <summary>
        /// Abrahman on map
        /// </summary>
        private AbrahmanOnMap abrahmanOnMap;

        /// <summary>
        /// Rendered surface of the map
        /// </summary>
        private Surface renderedSurface;

        /// <summary>
        /// Song played on the map
        /// </summary>
        private IRiff song;
        #endregion
    }
}
