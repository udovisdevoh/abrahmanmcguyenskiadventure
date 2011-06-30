using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.audio;

namespace AbrahmanAdventure.physics
{
    /// <summary>
    /// Manages pipe (player going into pipes)
    /// </summary>
    internal class PipeManager
    {
        /// <summary>
        /// Schedule a teleportation from source pipe to target pipe (linked to source pipe)
        /// </summary>
        /// <param name="playerSprite">player</param>
        /// <param name="sourcePipe">source pipe</param>
        internal void SchedulePipeTeleportation(PlayerSprite playerSprite, PipeSprite sourcePipe)
        {
            SoundManager.PlayHit2Sound();
            PipeSprite targetPipe = sourcePipe.LinkedPipe;
            playerSprite.DestinationPipe = targetPipe;
        }

        internal void ContinuePipeTeleportation(PlayerSprite playerSprite)
        {
            double destinationX = playerSprite.DestinationPipe.XPosition;
            double destinationY;

            if (playerSprite.DestinationPipe.IsUpSide)
                destinationY = playerSprite.DestinationPipe.TopBound;
            else
                destinationY = playerSprite.DestinationPipe.YPosition + playerSprite.Height;

            bool xMatch = false;
            bool yMatch = false;

            if (playerSprite.XPosition < destinationX)
            {
                playerSprite.XPosition += Program.pipeTeleportSpeed;

                if (playerSprite.XPosition >= destinationX)
                {
                    xMatch = true;
                    playerSprite.XPosition = destinationX;
                }
            }
            else if (playerSprite.XPosition > destinationX)
            {
                playerSprite.XPosition -= Program.pipeTeleportSpeed;

                if (playerSprite.XPosition <= destinationX)
                {
                    xMatch = true;
                    playerSprite.XPosition = destinationX;
                }
            }
            else
            {
                xMatch = true;
            }

            if (playerSprite.YPosition < destinationY)
            {
                playerSprite.YPosition += Program.pipeTeleportSpeed;

                if (playerSprite.YPosition >= destinationY)
                {
                    yMatch = true;
                    playerSprite.YPosition = destinationY;
                }
            }
            else if (playerSprite.YPosition > destinationY)
            {
                playerSprite.YPosition -= Program.pipeTeleportSpeed;

                if (playerSprite.YPosition <= destinationY)
                {
                    yMatch = true;
                    playerSprite.YPosition = destinationY;
                }
            }
            else
            {
                yMatch = true;
            }

            if (xMatch && yMatch)
            {
                if (!playerSprite.DestinationPipe.IsUpSide)
                    playerSprite.IGround = null;
                playerSprite.DestinationPipe = null;
                playerSprite.FromVortexCycle.Fire();
            }
        }
    }
}
