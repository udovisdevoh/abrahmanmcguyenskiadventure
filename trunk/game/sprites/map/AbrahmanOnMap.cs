﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    enum AbrahmanOnMapSpriteType { Tiny, Big, Doped, Rasta, Ninja }

    /// <summary>
    /// Abrahman McGuyenski viewed on map
    /// </summary>
    internal class AbrahmanOnMap : MapSprite
    {
        #region Fields and parts
        private AbrahmanOnMapSpriteType abrahmanOnMapSpriteType;
        #endregion

        #region Constructor
        public AbrahmanOnMap(double xPosition, double yPosition, AbrahmanOnMapSpriteType abrahmanOnMapSpriteType)
        {
            this.abrahmanOnMapSpriteType = abrahmanOnMapSpriteType;
            XPosition = xPosition;
            YPosition = yPosition;
        }
        #endregion
    }
}