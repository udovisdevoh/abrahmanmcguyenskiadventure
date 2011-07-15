using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;
using AbrahmanAdventure.physics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// To dispatch pipes
    /// </summary>
    internal static class PipeDispatcher
    {
        #region Internal Methods
        /// <summary>
        /// Dispatch pipes in level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="addedBlockMemory">memory of added blocks</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchPipes(Level level, SpritePopulation spritePopulation, Random random)
        {
            double pipeDensity = random.NextDouble() * 0.01 + 0.02;
            int pipeCount = (int)Math.Round(pipeDensity * level.Size);
            int pluggedPipeCount = (int)Math.Round((random.NextDouble() * 0.6 + 0.4) * (double)pipeCount);

            DispatchUpwardPipes((int)Math.Ceiling(((double)pipeCount) / 2.0), level, spritePopulation, random);

            List<PipeSprite> pipeList = GetPipeList(spritePopulation);

            PlugSomePipes(pluggedPipeCount, pipeList, random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Dispatch upwards
        /// </summary>
        /// <param name="pipeCount">how many upward pipes</param>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">all other sprites</param>
        /// <param name="random">random number generator</param>
        private static void DispatchUpwardPipes(int pipeCount, Level level, SpritePopulation spritePopulation, Random random)
        {
            const int maxTryCount = 40;
            for (int i = 0; i < pipeCount; i++)
            {
                for (int tryCount = 0; tryCount < maxTryCount; tryCount++)
                {
                    double xPosition = random.NextDouble() * level.Size + level.LeftBound;
                    double yPosition = SpriteDispatcher.GetRandomVisibleGround(level, random, xPosition)[xPosition];

                    PipeSprite pipeSprite = new PipeSprite(xPosition, yPosition, random);

                    spritePopulation.Add(pipeSprite);

                    if (IsAtAcceptablePosition(pipeSprite) && IsInAcceptableDistance(pipeSprite, spritePopulation))
                    {
                        break;
                    }
                    else
                    {
                        spritePopulation.Remove(pipeSprite);
                    }
                }
            }
        }

        /// <summary>
        /// Whether pipe sprite is in acceptable distance to other sprites
        /// </summary>
        /// <param name="pipeSprite">pipe sprite</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns>Whether pipe sprite is in acceptable distance to other sprites</returns>
        private static bool IsInAcceptableDistance(PipeSprite pipeSprite, SpritePopulation spritePopulation)
        {
            foreach (AbstractSprite otherSprite in spritePopulation.AllSpriteList)
            {
                if (pipeSprite == otherSprite)
                    continue;

                if (Physics.IsDetectCollision(pipeSprite, pipeSprite.XPosition, pipeSprite.YPosition - 1.0, 2.0, otherSprite))
                    return false;
                else if (Physics.IsDetectCollision(pipeSprite, pipeSprite.XPosition, pipeSprite.YPosition + 1.0, 2.0, otherSprite))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Whether pipe is at acceptable position in level
        /// </summary>
        /// <param name="pipeSprite">pipe sprite/param>
        /// <returns>Whether pipe is at acceptable position in level</returns>
        private static bool IsAtAcceptablePosition(PipeSprite pipeSprite)
        {
            return pipeSprite.XPosition <= -2.0 || pipeSprite.XPosition >= 2.0; //Clear the entrance portal
        }

        /// <summary>
        /// List of pipes in sprite population
        /// </summary>
        /// <param name="spritePopulation">sprite population</param>
        /// <returns></returns>
        private static List<PipeSprite> GetPipeList(SpritePopulation spritePopulation)
        {
            List<PipeSprite> pipeList = new List<PipeSprite>();
            foreach (AbstractSprite sprite in spritePopulation.AllSpriteList)
                if (sprite is PipeSprite)
                    pipeList.Add((PipeSprite)sprite);
            return pipeList;
        }

        /// <summary>
        /// Plug some pipes together
        /// </summary>
        /// <param name="pipeCountToPlug">how many pipes to plug</param>
        /// <param name="pipeList">all the pipes</param>
        /// <param name="random">random number generator</param>
        private static void PlugSomePipes(int pipeToPlugCount, List<PipeSprite> pipeList, Random random)
        {
            while (pipeToPlugCount >= 2 && pipeList.Count >= 2) //If there is one pipe left to plug, it can't be plugged
            {
                PipeSprite pipe1 = GetRandomPipe(pipeList, random);
                pipeList.Remove(pipe1);
                PipeSprite pipe2 = GetRandomPipe(pipeList, random);
                pipe1.LinkedPipe = pipe2;
            }
        }

        /// <summary>
        /// Random pipe from list
        /// </summary>
        /// <param name="pipeList">list of pipes</param>
        /// <param name="random">random number generator</param>
        /// <returns>Random pipe from list</returns>
        private static PipeSprite GetRandomPipe(List<PipeSprite> pipeList, Random random)
        {
            return pipeList[random.Next(pipeList.Count)];
        }
        #endregion
    }
}
