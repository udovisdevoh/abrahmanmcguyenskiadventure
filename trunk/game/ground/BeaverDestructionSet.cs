﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Stores which part of a ground are destroyed by a beaver
    /// </summary>
    internal class BeaverDestructionSet
    {
        #region Field and parts
        /// <summary>
        /// Key: x position
        /// Value: y offset
        /// </summary>
        private Dictionary<int, float> internalDictionary = new Dictionary<int, float>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Dig a hole at x position
        /// </summary>
        /// <param name="xPosition">Dig a hole at x position</param>
        public void Dig(float xPosition)
        {
            int index = (int)(xPosition / (float)Program.beaverHoleDiameter);

            IncrementDepthOffet(index, Program.beaverHoleDepth);
            //IncrementDepthOffet(index - 1, Program.beaverHoleDepth / -2.0f);
            //IncrementDepthOffet(index + 1, Program.beaverHoleDepth / -2.0f);
        }

        /// <summary>
        /// Remove all holes
        /// </summary>
        public void Clear()
        {
            internalDictionary.Clear();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Increment (or decrement) depth offset
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="incrementationValue">incrementation value (negative or positive)</param>
        private void IncrementDepthOffet(int index, float incrementationValue)
        {
            float depthOffset;
            if (internalDictionary.TryGetValue(index, out depthOffset))
            {
                internalDictionary[index] = depthOffset + incrementationValue;
            }
            else
            {
                depthOffset = 0;
                internalDictionary.Add(index, depthOffset + incrementationValue);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Dept offset at x position
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <returns>Dept offset at x position</returns>
        public float this[float xPosition]
        {
            get
            {
                int index = (int)(xPosition / (float)Program.beaverHoleDiameter);

                float yOffset;
                if (internalDictionary.TryGetValue(index, out yOffset))
                    return yOffset;
                return 0.0f;
            }
        }
        #endregion
    }
}
