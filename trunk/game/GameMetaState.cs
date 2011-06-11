using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.sprites;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Persistant information when changing game state
    /// Stock to save and load when save game / load game
    /// </summary>
    internal class GameMetaState
    {
        #region Fields and parts
        /// <summary>
        /// Whether player is tiny
        /// </summary>
        private bool isTiny = true;

        /// <summary>
        /// Whether player is rasta
        /// </summary>
        private bool isRasta = false;

        /// <summary>
        /// Whether player is doped
        /// </summary>
        private bool isDoped = false;

        /// <summary>
        /// Player's health
        /// </summary>
        private double health = 0.5;

        /// <summary>
        /// Previous seed (-1 means none)
        /// </summary>
        private int previousSeed = -1;

        /// <summary>
        /// Key: game state's seed
        /// Value: skill level (0: default)
        /// </summary>
        private Dictionary<int, int> mapSeedToSkillLevel = new Dictionary<int,int>();

        /// <summary>
        /// To remember warp-backs (vortex going in the other direction
        /// Key: gameState's seed in which we must spawn a vortex
        /// Value: list of target seed for the spawned vortex
        /// </summary>
        private Dictionary<int, List<int>> mapWarpBack = new Dictionary<int,List<int>>();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get info from player
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        internal void GetInfoFromPlayer(PlayerSprite playerSprite)
        {
            isDoped = playerSprite.IsDoped;
            isRasta = playerSprite.IsRasta;
            isTiny = playerSprite.IsTiny;
            health = playerSprite.Health;
        }

        /// <summary>
        /// Apply player info
        /// </summary>
        /// <param name="playerSprite">player sprite</param>
        internal void ApplyPlayerInfoToSprite(PlayerSprite playerSprite)
        {
            playerSprite.IsDoped = isDoped;
            playerSprite.IsRasta = isRasta;
            playerSprite.IsTiny = isTiny;
            playerSprite.Health = health;
        }
        
        /// <summary>
        /// Try to get 
        /// </summary>
        /// <param name="sourceSeed">from seed</param>
        /// <param name="listTargetSeed">to seeds (list)</param>
        /// <returns>if could get target seed</returns>
        internal bool TryGetWarpBackTargetSeed(int sourceSeed, out List<int> listTargetSeed)
        {
            return mapWarpBack.TryGetValue(sourceSeed, out listTargetSeed);
        }

        /// <summary>
        /// Remember a "warp back"
        /// </summary>
        /// <param name="sourceSeed">seed used for gamestate that contain warp back</param>
        /// <param name="targetSeed">seed to return to</param>
        internal void SetWarpBack(int sourceSeed, int targetSeed)
        {
            List<int> targetSeedList;

            if (!mapWarpBack.TryGetValue(sourceSeed, out targetSeedList))
            {
                targetSeedList = new List<int>();
                mapWarpBack.Add(sourceSeed, targetSeedList);
            }

            if (!targetSeedList.Contains(targetSeed))
                targetSeedList.Add(targetSeed);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether player is tiny
        /// </summary>
        public bool IsTiny
        {
            get { return isTiny; }
            set { isTiny = value; }
        }

        /// <summary>
        /// Whether player is rasta
        /// </summary>
        public bool IsRasta
        {
            get { return isRasta; }
            set { isRasta = value; }
        }

        /// <summary>
        /// Whether player is doped
        /// </summary>
        public bool IsDoped
        {
            get { return isDoped; }
            set { isDoped = value; }
        }

        /// <summary>
        /// Health
        /// </summary>
        public double Health
        {
            get { return health; }
            set { health = value; }
        }

        /// <summary>
        /// Previous seed (-1 means none)
        /// </summary>
        public int PreviousSeed
        {
            get { return previousSeed; }
            set { previousSeed = value; }
        }
        #endregion
    }
}
