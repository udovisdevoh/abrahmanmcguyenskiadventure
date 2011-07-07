using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Monster dispatcher
    /// </summary>
    internal static class MonsterDispatcher
    {
        #region Fields and parts
        /// <summary>
        /// All possible monster types
        /// </summary>
        private static List<AbstractSprite> allPossibleDispatchableMonsterTypes;

        /// <summary>
        /// To be used temporarly without recreating the list
        /// </summary>
        private static List<MonsterSprite> __temporarySpriteList;
        #endregion

        #region Constructor
        /// <summary>
        /// Build Monster Dispatcher
        /// </summary>
        static MonsterDispatcher()
        {
            Random random = new Random();
            allPossibleDispatchableMonsterTypes = new List<AbstractSprite>();
            allPossibleDispatchableMonsterTypes.Add(new BlobSprite(0,0, random));
            allPossibleDispatchableMonsterTypes.Add(new DoctorSprite(0,0,random));
            allPossibleDispatchableMonsterTypes.Add(new FarmerSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new GypsySprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new HamburgerSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new HorseSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new JewSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new MormonSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new MouseSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new MuslimSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new PriestSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new PuppetSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new RaptorJesusSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new RaptorSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new RiotControlSprite(0, 0, random, true));
            allPossibleDispatchableMonsterTypes.Add(new RiotControlSprite(0, 0, random, false));
            allPossibleDispatchableMonsterTypes.Add(new RonaldSprite(0, 0, random));
            allPossibleDispatchableMonsterTypes.Add(new SnakeSprite(0, 0, random));

            __temporarySpriteList = new List<MonsterSprite>();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Dispatch sprite on level
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="skillLevel">skill level</param>
        /// <param name="random">random number generator</param>
        internal static void DispatchMonsters(Level level, SpritePopulation spritePopulation, int skillLevel, Random random)
        {
            /*burger: 1.17
            jew: 2.85
            raptor: 3.45
            riot1: 2.17
            riot2: 1.67
            mormon: 2.85
            farmer: 2.61*/
            double monsterDensity = random.NextDouble() * 0.1 + 0.05; //Random density, for easiest skill (0.05 to 0.15)
            double skillLevelAdjustmentRatio = Math.Sqrt(((double)skillLevel) + 1.0);
            monsterDensity *= skillLevelAdjustmentRatio;
            double availableMonsterPopulationMass = monsterDensity * level.Size;
            double monsterTypeEntropy = random.NextDouble();//0: all the same monster, 1: very diverse

            do
            {
                double maxDispatchRatioPerMonster = GetMaxDispatchRatioPerMonster(skillLevel);

                MonsterSprite monsterTypeSample = GetRandomMonsterTypeSample(monsterTypeEntropy, spritePopulation, maxDispatchRatioPerMonster, random);

                if (monsterTypeSample == null)
                    break;

                MonsterSprite monster = BuildMonsterFromSampleAtRandomPosition(monsterTypeSample, level, random);
                spritePopulation.Add(monster);

                availableMonsterPopulationMass -= monster.SkillDispatchRatio;
            } while (availableMonsterPopulationMass > 0.0);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build monster from sample at random position
        /// </summary>
        /// <param name="monsterTypeSample"></param>
        /// <param name="level">level</param>
        /// <param name="random">random number generator</param>
        /// <returns>monster</returns>
        private static MonsterSprite BuildMonsterFromSampleAtRandomPosition(MonsterSprite monsterTypeSample, Level level, Random random)
        {
            double xPosition;
            double yPosition;
            int tryCount = 0;
            do
            {
                xPosition = random.NextDouble() * level.Size + level.LeftBound;
                yPosition = GetRandomGround(level, random)[xPosition];
                tryCount++;
            } while (yPosition >= Program.holeHeight && tryCount < 30);

            Type type = monsterTypeSample.GetType();

            object[] constructorArgumentList;
            Type[] constructorArgumentTypeList;

            if (monsterTypeSample is RiotControlSprite)
            {
                constructorArgumentList = new object[4];
                constructorArgumentList[0] = xPosition;
                constructorArgumentList[1] = yPosition;
                constructorArgumentList[2] = random;
                constructorArgumentList[3] = monsterTypeSample.IsAvoidFall;

                constructorArgumentTypeList = new Type[4];
                constructorArgumentTypeList[0] = typeof(double);
                constructorArgumentTypeList[1] = typeof(double);
                constructorArgumentTypeList[2] = typeof(Random);
                constructorArgumentTypeList[3] = typeof(bool);
            }
            else
            {
                constructorArgumentList = new object[3];
                constructorArgumentList[0] = xPosition;
                constructorArgumentList[1] = yPosition;
                constructorArgumentList[2] = random;

                constructorArgumentTypeList = new Type[3];
                constructorArgumentTypeList[0] = typeof(double);
                constructorArgumentTypeList[1] = typeof(double);
                constructorArgumentTypeList[2] = typeof(Random);
            }


            ConstructorInfo constructorInfoGenericMonster = type.GetConstructor(constructorArgumentTypeList);

            return (MonsterSprite)constructorInfoGenericMonster.Invoke(constructorArgumentList);
        }

        /// <summary>
        /// Get random ground
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="random">random number generator</param>
        /// <returns>random ground</returns>
        private static Ground GetRandomGround(Level level, Random random)
        {
            return level[random.Next(level.Count)];
        }

        /// <summary>
        /// Get max dispatch ratio per monster
        /// </summary>
        /// <param name="skillLevel">skill level</param>
        /// <returns>max dispatch ratio per monster</returns>
        private static double GetMaxDispatchRatioPerMonster(int skillLevel)
        {
            double maxDispatchRatioPerMonster = ((double)(skillLevel + 1)) / 2.0 + 1.0;
            if (skillLevel >= 9)
                maxDispatchRatioPerMonster += 100;
            return maxDispatchRatioPerMonster;
        }

        /// <summary>
        /// Get random monster type sample
        /// </summary>
        /// <param name="monsterTypeEntropy">monster type entropy</param>
        /// <param name="spritePopulation">sprite population</param>
        /// <param name="maxDispatchRatioPerMonster">max skill dispatch ratio per monster</param>
        /// <param name="random">random number generator</param>
        /// <returns>random monster type sample</returns>
        private static MonsterSprite GetRandomMonsterTypeSample(double monsterTypeEntropy, SpritePopulation spritePopulation, double maxDispatchRatioPerMonster, Random random)
        {
            MonsterSprite monsterTypeSample;
            if (random.NextDouble() < monsterTypeEntropy)
                monsterTypeSample = GetRandomSpriteFrom(allPossibleDispatchableMonsterTypes, maxDispatchRatioPerMonster, random);
            else
            {
                monsterTypeSample = GetRandomSpriteFrom(spritePopulation.AllSpriteList, maxDispatchRatioPerMonster, random);
                if (monsterTypeSample == null)
                    monsterTypeSample = GetRandomSpriteFrom(allPossibleDispatchableMonsterTypes, maxDispatchRatioPerMonster, random);
            }
            return monsterTypeSample;
        }

        /// <summary>
        /// Get Random sprite (having dispatch ratio lower than maxDispatchRatioPerMonster) from list
        /// </summary>
        /// <param name="listToLookInto"></param>
        /// <param name="maxDispatchRatioPerMonster"></param>
        /// <param name="random">random number generator</param>
        /// <returns>Random sprite (having dispatch ratio lower than maxDispatchRatioPerMonster) from list
        /// Or null if nothing could be found</returns>
        private static MonsterSprite GetRandomSpriteFrom(IEnumerable<AbstractSprite> listToLookInto, double maxDispatchRatioPerMonster, Random random)
        {
            __temporarySpriteList.Clear();

            foreach (AbstractSprite sprite in listToLookInto)
                if (sprite is MonsterSprite && ((MonsterSprite)sprite).SkillDispatchRatio <= maxDispatchRatioPerMonster && IsContainType(sprite.GetType(), allPossibleDispatchableMonsterTypes))
                    __temporarySpriteList.Add((MonsterSprite)sprite);

            if (__temporarySpriteList.Count == 0)
                return null;

            return __temporarySpriteList[random.Next(__temporarySpriteList.Count)];
        }

        private static bool IsContainType(Type type, List<AbstractSprite> allPossibleDispatchableMonsterTypes)
        {
            foreach (AbstractSprite otherSprite in allPossibleDispatchableMonsterTypes)
                if (otherSprite.GetType() == type)
                    return true;
            return false;
        }
        #endregion
    }
}
