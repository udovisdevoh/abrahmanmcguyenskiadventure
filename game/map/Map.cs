using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio.midi.generator;
using AbrahmanAdventure.textGenerator;

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
        /// Width (in tiles)
        /// </summary>
        private double width;

        /// <summary>
        /// Height (in tiles)
        /// </summary>
        private double height;

        /// <summary>
        /// Width in pixels
        /// </summary>
        private int widthInPixels;

        /// <summary>
        /// Height in pixels
        /// </summary>
        private int heightInPixels;

        /// <summary>
        /// Bit matrix for collision
        /// </summary>
        private BitMatrix collisionBitMatrix;

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

        #region Constructor
        /// <summary>
        /// Create map
        /// </summary>
        /// <param name="random">random number generator</param>
        public Map(Random random)
        {
            name = WordGenerator.GenerateName(random);

            width = 20.0;
            height = 15.0;

            int collisionBitMatrixWidth = (int)(width * 32);
            int collisionBitMatrixHeight = (int)(height * 32);
            collisionBitMatrix = new BitMatrix(collisionBitMatrixWidth, collisionBitMatrixHeight, true);

            widthInPixels = (int)(width * Program.tileSize);
            heightInPixels = (int)(height * Program.tileSize);
            renderedSurface = new Surface(widthInPixels, heightInPixels, Program.bitDepth);

            abrahmanOnMap = new AbrahmanOnMap(random.NextDouble() * width, random.NextDouble() * height, AbrahmanOnMapSpriteType.Tiny);
            listMapSprite.Add(abrahmanOnMap);

            //AddLevelSprites(random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add level sprites
        /// </summary>
        /// <param name="random">random number generator</param>
        private void AddLevelSprites(Random random, int skillLevelOfFirstLevel)
        {
            int levelCount = random.Next(4, 11);
            for (int i = 0; i < levelCount; i++)
            {
                LevelSprite levelSprite = new LevelSprite(random.NextDouble() * width, random.NextDouble() * height, i, skillLevelOfFirstLevel, random);
                listMapSprite.Add(levelSprite);
            }
        }
        #endregion
    }
}
