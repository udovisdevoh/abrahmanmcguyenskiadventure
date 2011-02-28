using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// For physics
    /// </summary>
    internal class Physics
    {
        /// <summary>
        /// Update physics for sprite
        /// </summary>
        /// <param name="sprite">sprite</param>
        internal void Update(AbstractSprite sprite, Level level, double timeDelta)
        {
            if (sprite.IsOnGround)
            {
                sprite.CurrentJumpAcceleration = 0;
            }
            else
            {
                double closestDownGroundHeight = GetClosestDownGroundHeight(sprite, level);
                sprite.YPosition += sprite.CurrentJumpAcceleration / 500 * timeDelta;
                sprite.CurrentJumpAcceleration -= 1.0 * timeDelta;
                sprite.YPosition = Math.Max(closestDownGroundHeight, sprite.YPosition);

                if (sprite.YPosition <= closestDownGroundHeight)
                {
                    sprite.IsOnGround = true;
                }
            }
        }

        /// <summary>
        /// Whether sprite is on a ground
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>Whether sprite is on a ground</returns>
        private bool IsOnAGround(AbstractSprite sprite, Level level)
        {
            foreach (Ground ground in level)
                if (sprite.YPosition == ground.TerrainWave[sprite.XPosition])
                    return true;
            return false;
        }

        /// <summary>
        /// Closest down ground's height
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <param name="level">level</param>
        /// <returns>Closest down ground's height</returns>
        private double GetClosestDownGroundHeight(AbstractSprite sprite, Level level)
        {
            #warning remove dummy code
            return 30.0;
            double previousHeight = double.NegativeInfinity;
            foreach (Ground ground in level)
            {
                double currentHeight = ground.TerrainWave[sprite.XPosition];
                if (sprite.YPosition >= currentHeight)
                    return previousHeight;

                previousHeight = currentHeight;
            }
            return double.PositiveInfinity;
        }
    }
}