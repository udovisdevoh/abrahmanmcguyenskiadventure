using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AbrahmanAdventure.sprites;
using AbrahmanAdventure.hud;

namespace AbrahmanAdventure
{
    /// <summary>
    /// Persistant information when changing game state
    /// Stock to save and load when save game / load game
    /// </summary>
    [XmlRootAttribute(ElementName="GameMetaState", IsNullable=false)]
    public class GameMetaState
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
        /// Whether player is on beaver
        /// </summary>
        private bool isBeaver = false;

        /// <summary>
        /// Whether player is doped
        /// </summary>
        private bool isDoped = false;

        /// <summary>
        /// Whether player is currently a ninja
        /// </summary>
        private bool isNinja = false;

        /// <summary>
        /// Whether sprite is currently a bodhi
        /// </summary>
        private bool isBodhi = false;

        /// <summary>
        /// Whether game mode is "extreme action"
        /// </summary>
        private bool isExtremeAction = false;

        /// <summary>
        /// Whether game mode is "adventure rpg"
        /// </summary>
        private bool isAdventureRpg = false;

        /// <summary>
        /// Racing mode
        /// </summary>
        private bool isRacing = false;

        /// <summary>
        /// Skill level for unexplored levels
        /// </summary>
        private int skillLevelForUnknownLevels;

        /// <summary>
        /// Player's experience
        /// </summary>
        private int experience;

        /// <summary>
        /// Player's level
        /// </summary>
        private int level;

        /// <summary>
        /// Player's health
        /// </summary>
        private double health = 0.5;

        /// <summary>
        /// Previous seed (-1 means none)
        /// </summary>
        private int previousSeed = -1;

        /// <summary>
        /// How many music notes
        /// </summary>
        private int musicNoteCount = 0;

        /// <summary>
        /// Key: game state's seed
        /// Value: skill level (0: default)
        /// </summary>
        private Dictionary<int, int> mapSeedToSkillLevel = new Dictionary<int, int>();

        /// <summary>
        /// To remember warp-backs (vortex going in the other direction
        /// Key: gameState's seed in which we must spawn a vortex
        /// Value: list of target seed for the spawned vortex
        /// </summary>
        private Dictionary<int, List<int>> mapWarpBack = new Dictionary<int, List<int>>();
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
            isBeaver = playerSprite.IsBeaver;
            isNinja = playerSprite.IsNinja;
            isBodhi = playerSprite.IsBodhi;
            health = playerSprite.Health;
            musicNoteCount = playerSprite.MusicNoteCount;
            experience = playerSprite.Experience;
            level = playerSprite.Level;
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
            playerSprite.IsBeaver = isBeaver;
            playerSprite.IsNinja = isNinja;
            playerSprite.IsBodhi = isBodhi;
            playerSprite.Health = health;
            playerSprite.MusicNoteCount = musicNoteCount;
            playerSprite.Experience = experience;
            playerSprite.Level = level;
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

        /// <summary>
        /// Get skill level for seed
        /// </summary>
        /// <param name="seed">seed</param>
        /// <returns>skill level for seed</returns>
        internal int GetSkillLevel(int seed)
        {
            int skillLevel;
            if (!mapSeedToSkillLevel.TryGetValue(seed, out skillLevel))
            {
                mapSeedToSkillLevel.Add(seed, skillLevelForUnknownLevels);
                skillLevel = skillLevelForUnknownLevels;
            }
            return skillLevel;
        }

        internal void ClearWarpBack()
        {
            mapWarpBack.Clear();
        }

        internal void ClearMapSkillLevel()
        {
            mapSeedToSkillLevel.Clear();
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
        /// Whether player is on beaver
        /// </summary>
        public bool IsBeaver
        {
            get { return isBeaver; }
            set { isBeaver = value; }
        }

        /// <summary>
        /// Whether player is currently a ninja
        /// </summary>
        public bool IsNinja
        {
            get { return isNinja; }
            set { isNinja = value; }
        }

        /// <summary>
        /// Whether sprite is currently bodhi
        /// </summary>
        public bool IsBodhi
        {
            get { return isBodhi; }
            set { isBodhi = value; }
        }

        /// <summary>
        /// Whether game mode is "adventure rpg"
        /// </summary>
        public bool IsAdventureRpg
        {
            get { return isAdventureRpg; }
            set { isAdventureRpg = value; }
        }

        /// <summary>
        /// Whether game mode is "extreme action"
        /// </summary>
        public bool IsExtremeAction
        {
            get { return isExtremeAction; }
            set { isExtremeAction = value; }
        }

        /// <summary>
        /// Racing mode
        /// </summary>
        public bool IsRacing
        {
            get { return isRacing; }
            set { isRacing = value; }
        }

        /// <summary>
        /// Health
        /// </summary>
        public int Health
        {
            get { return (int)Math.Round((health * 100.0)); }
            set { health = ((double)value) / 100.0; }
        }

        /// <summary>
        /// Previous seed (-1 means none)
        /// </summary>
        public int PreviousSeed
        {
            get { return previousSeed; }
            set { previousSeed = value; }
        }

        /// <summary>
        /// Skill level for unexplored levels
        /// </summary>
        public int SkillLevelForUnknownLevels
        {
            get { return skillLevelForUnknownLevels; }
            set { skillLevelForUnknownLevels = value; }
        }

        /// <summary>
        /// How many music notes
        /// </summary>
        public int MusicNoteCount
        {
            get { return musicNoteCount; }
        }

        /// <summary>
        /// Player's experience
        /// </summary>
        public int Experience
        {
            get { return experience; }
        }

        /// <summary>
        /// Player's level
        /// </summary>
        public int Level
        {
            get { return level; }
        }

        /// <summary>
        /// Key: game state's seed
        /// Value: skill level (0: default)
        /// </summary>
        public string MapSeedToSkillLevel
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (KeyValuePair<int, int> seedAndSkillLevel in mapSeedToSkillLevel)
                {
                    int seed = seedAndSkillLevel.Key;
                    int skillLevel = seedAndSkillLevel.Value;
                    stringBuilder.Append(seed);
                    stringBuilder.Append(':');
                    stringBuilder.Append(skillLevel);
                    stringBuilder.Append(',');
                }
                return stringBuilder.ToString();
            }
            set
            {
                mapSeedToSkillLevel = new Dictionary<int, int>();

                string[] keyValuePairList = value.Split(',');

                foreach (string keyValuePair in keyValuePairList)
                {
                    if (keyValuePair.Length > 0)
                    {
                        string[] seedAndSkillLevel = keyValuePair.Split(':');
                        mapSeedToSkillLevel.Add(int.Parse(seedAndSkillLevel[0]), int.Parse(seedAndSkillLevel[1]));
                    }
                }
            }
        }

        /// <summary>
        /// To remember warp-backs (vortex going in the other direction
        /// Key: gameState's seed in which we must spawn a vortex
        /// Value: list of target seed for the spawned vortex
        /// </summary>
        public string MapWarpBack
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (KeyValuePair<int, List<int>> sourceAndTargetList in mapWarpBack)
                {
                    int source = sourceAndTargetList.Key;
                    List<int> targetList = sourceAndTargetList.Value;


                    stringBuilder.Append(source);
                    stringBuilder.Append(':');

                    foreach (int target in targetList)
                    {
                        stringBuilder.Append(target);
                        stringBuilder.Append('|');
                    }
                    stringBuilder.Append(',');
                }
                return stringBuilder.ToString();
            }
            set
            {
                mapWarpBack = new Dictionary<int, List<int>>();

                string[] keyValuePairList = value.Split(',');
                foreach (string keyValuePair in keyValuePairList)
                {
                    if (keyValuePair.Length > 0)
                    {
                        string[] sourceAndTargetList = keyValuePair.Split(':');

                        int source = int.Parse(sourceAndTargetList[0]);

                        string[] targetList = sourceAndTargetList[1].Split('|');

                        foreach (string target in targetList)
                        {
                            if (target.Length > 0)
                            {
                                this.SetWarpBack(source, int.Parse(target));
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
