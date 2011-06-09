using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.textGenerator
{
    /// <summary>
    /// For probability mathematics
    /// </summary>
    public static class Probabilities
    {
        /// <summary>
        /// Get random key from row according to probabilities in value
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="random">random number generator</param>
        /// <returns>random key from row according to probabilities in value</returns>
        public static string GetPonderatedRandom(this Dictionary<string, float> row, Random random)
        {
            float totalSum = row.Values.Sum();
            float randomFloat = totalSum * (float)random.NextDouble();
            float counter = 0;

            string text;
            float probability;
            string selectedValue = null;

            foreach (KeyValuePair<string, float> textAndProbability in row)
            {
                text = textAndProbability.Key;
                probability = textAndProbability.Value;

                selectedValue = text;

                counter += probability;

                if (counter > randomFloat)
                    break;
            }
            return selectedValue;
        }

        /// <summary>
        /// Get random key from row according to probabilities in value
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="row">row</param>
        /// <returns>random key from row according to probabilities in value</returns>
        public static string GetPonderatedRandom(this Dictionary<string, int> row, Random random)
        {
            int totalDiscreteSum = row.Values.Sum();
            int randomInt = random.Next(0, totalDiscreteSum);
            int counter = 0;

            string text;
            int probability;
            string selectedValue = null;

            foreach (KeyValuePair<string, int> textAndProbability in row)
            {
                text = textAndProbability.Key;
                probability = textAndProbability.Value;

                selectedValue = text;

                counter += probability;

                if (counter > randomInt)
                    break;
            }
            return selectedValue;
        }

        /// <summary>
        /// Get random key from row according to probabilities in value
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="row">row</param>
        /// <returns>random key from row according to probabilities in value</returns>
        public static char GetPonderatedRandom(this Dictionary<char, int> row, Random random)
        {
            int totalDiscreteSum = row.Values.Sum();
            int randomInt = random.Next(0, totalDiscreteSum);
            int counter = 0;

            char letter;
            int probability;
            char selectedValue = ' ';

            foreach (KeyValuePair<char, int> textAndProbability in row)
            {
                letter = textAndProbability.Key;
                probability = textAndProbability.Value;

                selectedValue = letter;

                counter += probability;

                if (counter > randomInt)
                    break;
            }
            return selectedValue;
        }

        /// <summary>
        /// Get random value from list
        /// </summary>
        /// <param name="enumeration">enumeration</param>
        /// <param name="random">random number generator</param>
        /// <returns>random value from list</returns>
        public static object GetRandomValue(this IEnumerable<object> enumeration, Random random)
        {
            IList<object> list;
            if (enumeration is IList<object>)
            {
                list = (IList<object>)enumeration;
            }
            else
            {
                list = new List<object>(enumeration);
            }

            return list[random.Next(list.Count)];
        }
    }
}
