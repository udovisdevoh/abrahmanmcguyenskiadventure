using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AbrahmanAdventure.textGenerator
{
    /// <summary>
    /// To generate words
    /// </summary>
    internal static class TextGenerator
    {
        #region Fields and parts
        /// <summary>
        /// English language matrix
        /// </summary>
        private static Matrix englishMatrix;
        #endregion

        #region Constructor
        /// <summary>
        /// Text generator
        /// </summary>
        static TextGenerator()
        {
            XmlMatrixSaverLoader xmlMatrixSaverLoader = new XmlMatrixSaverLoader();
            englishMatrix = xmlMatrixSaverLoader.Load("./game/textGenerator/english.language.matrix.xml");
        }
        #endregion

        #region Internal Methods
        internal static string GenerateName(Random random)
        {
            int sentenceLetterCount = 200;
            string previousCharPair = englishMatrix.GetRandomStartingPair(random);
            previousCharPair += " ";
            previousCharPair = previousCharPair.Substring(1, 2);

            string text = previousCharPair.Substring(1, 1);

            for (int charCounter = 0; charCounter < sentenceLetterCount; charCounter++)
            {
                Dictionary<string, float> row;
                while (!englishMatrix.NormalData.TryGetValue(previousCharPair, out row))
                {
                    previousCharPair = englishMatrix.GetRandomStartingPair(random);
                }

                string letter = Probabilities.GetPonderatedRandom(row, random);

                text += letter;

                previousCharPair = previousCharPair.Substring(1) + letter;
            }

            text = text.Trim().ToLowerInvariant();

            Regex regex = new Regex(@"[^a-z ]");

            text = regex.Replace(text, "");

            string[] wordList = text.Split(' ');

            int averageWordLength = 0;
            foreach (string word in wordList)
                averageWordLength += word.Length;
            averageWordLength /= wordList.Length;
            averageWordLength+=random.Next(-2,6);
            averageWordLength = Math.Max(4, averageWordLength);

            string wordWithBestLength = "noname";
            int leastDistance = -1;
            foreach (string word in wordList)
            {
                if (leastDistance == -1 || Math.Abs(word.Length - averageWordLength) < leastDistance)
                {
                    leastDistance = Math.Abs(word.Length - averageWordLength);
                    wordWithBestLength = word;
                }
            }

            if (wordWithBestLength.Length > 1)
                wordWithBestLength = wordWithBestLength.Substring(0, 1).ToUpperInvariant() + wordWithBestLength.Substring(1).ToLowerInvariant();

            return wordWithBestLength;
        }
        #endregion
    }
}
