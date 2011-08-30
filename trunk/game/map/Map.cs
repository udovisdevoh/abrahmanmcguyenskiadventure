using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;
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
        public Map(Random random, int skillLevelOfFirstLevel, PlayerSprite playerSprite)
        {
            name = WordGenerator.GenerateName(random);

            width = 20.0;
            height = 15.0;

            song = SongGenerator.BuildSong(random.Next(), skillLevelOfFirstLevel, SongType.Map);

            int collisionBitMatrixWidth = (int)(width * 32);
            int collisionBitMatrixHeight = (int)(height * 32);
            collisionBitMatrix = new BitMatrix(collisionBitMatrixWidth, collisionBitMatrixHeight, true);

            widthInPixels = (int)(width * Program.tileSize);
            heightInPixels = (int)(height * Program.tileSize);

            abrahmanOnMap = new AbrahmanOnMap(random.NextDouble() * (width - 2.0) + 1.0, random.NextDouble() * (height - 2.0) + 1.0, playerSprite);
            listMapSprite.Add(abrahmanOnMap);

            listMapSprite = new List<MapSprite>();
            AddLevelSprites(random, skillLevelOfFirstLevel);

            renderedSurface = new Surface(widthInPixels, heightInPixels, Program.bitDepth);
            foreach (MapSprite mapSprite in listMapSprite)
            {
                Point positionOnScreen = new Point((int)(mapSprite.XPosition * Program.tileSize), (int)(mapSprite.YPosition * Program.tileSize));
                renderedSurface.Blit(mapSprite.GetSurface(), positionOnScreen, renderedSurface.GetRectangle());
            }
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
            
            int skillLevel = skillLevelOfFirstLevel;

            double xPosition = -1;
            double yPosition = -1;

            for (int i = 0; i < levelCount; i++)
            {
                bool isIncrementSkill = random.Next(0, skillLevel + 1) == 0;

                if (isIncrementSkill)
                    skillLevel++;

                if (xPosition == -1 || yPosition == -1)
                {
                    xPosition = random.NextDouble() * (width - 2.0) + 1.0;
                    yPosition = random.NextDouble() * (height - 2.0) + 1.0;
                }
                else
                {
                    double xLevelDistance, yLevelDistance;
                    int tryCount = 10000;
                    do
                    {
                        xLevelDistance = random.NextDouble() * 2.0 + 1.0;
                        yLevelDistance = random.NextDouble() * 2.0 + 1.0;

                        if (random.Next(0, 2) == 1)
                            xLevelDistance = -xLevelDistance;

                        if (random.Next(0, 2) == 1)
                            yLevelDistance = -yLevelDistance;

                        tryCount--;
                    } while (tryCount > 0 && (isCloseFromAnotherLevel(xPosition + xLevelDistance, yPosition + yLevelDistance, listMapSprite, 1.5) || xPosition + xLevelDistance <= 1.0 || yPosition + yLevelDistance <= 1.0 || xPosition + xLevelDistance >= width - 1.0 || yPosition + yLevelDistance >= height - 1.0));

                    xPosition += xLevelDistance;
                    yPosition += yLevelDistance;
                }

                LevelSprite levelSprite = new LevelSprite(xPosition, yPosition, i, skillLevel, random);
                listMapSprite.Add(levelSprite);
            }
        }

        /// <summary>
        /// Whether level is close to another level
        /// </summary>
        /// <param name="xPosition">x Position</param>
        /// <param name="yPosition">y Position</param>
        /// <param name="listMapSprite">other levels</param>
        /// <param name="maxDistance">max distance</param>
        /// <returns>Whether level is close to another level</returns>
        private bool isCloseFromAnotherLevel(double xPosition, double yPosition, List<MapSprite> listMapSprite, double maxDistance)
        {
            foreach (MapSprite otherMapSprite in listMapSprite)
            {
                if (Math.Sqrt(Math.Pow(xPosition - otherMapSprite.XPosition, 2.0) + Math.Pow(yPosition - otherMapSprite.YPosition, 2.0)) >= maxDistance)
                    return true;
            }
            return false;
        }
        #endregion
    }
}
