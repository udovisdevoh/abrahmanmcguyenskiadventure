using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.map
{
    /// <summary>
    /// Array of bits (mostly used for collision detection on map)
    /// </summary>
    internal class BitMatrix
    {
        #region Constants
        private const int wordSize = 32;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Internal data
        /// </summary>
        private UInt32[] internalData;

        /// <summary>
        /// Width
        /// </summary>
        private int width;

        /// <summary>
        /// Height
        /// </summary>
        private int height;

        /// <summary>
        /// Width * height
        /// </summary>
        private int totalSize;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a bit matrix
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="defaultValue"></param>
        public BitMatrix(int width, int height, bool defaultValue)
        {
            this.width = width;
            this.height = height;
            totalSize = (width * height) / wordSize;
            internalData = new UInt32[totalSize];

            UInt32 fullWord = GetFullWord();

            if (defaultValue)
                for (int i = 0; i < totalSize; i++)
                    internalData[i] = fullWord;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get internal data's index from x,y
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>internal data's index from x,y</returns>
        private int GetWordIndex(int x, int y)
        {
            return (x + y * width) / wordSize;
        }

        /// <summary>
        /// Get ID within a word
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>ID within a word</returns>
        private int GetBitId(int x, int y)
        {
            return (x + y * width) % wordSize;
        }

        /// <summary>
        /// Get word with all true value
        /// </summary>
        /// <returns>Get word with all true value</returns>
        private UInt32 GetFullWord()
        {
            UInt32 fullWord = 0;
            for (int i = 0; i < wordSize; i++)
                fullWord |= ((UInt32)1) << i;
            return fullWord;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set value at X,Y
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>value at X,Y</returns>
        public bool this[int x, int y]
        {
            get
            {
                UInt32 word = internalData[GetWordIndex(x, y)];
                UInt32 mask = ((UInt32)1) << GetBitId(x, y);
                return (mask & word) == mask;
            }
            set
            {
                int wordIndex = GetWordIndex(x, y);
                UInt32 word = internalData[wordIndex];
                UInt32 mask = ((UInt32)1) << GetBitId(x, y);

                if (value)
                    word = (word | mask);
                else
                   word = (word | ~(mask));

                internalData[wordIndex] = word;
            }
        }
        #endregion
    }
}
