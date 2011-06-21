using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AbrahmanAdventure.textGenerator
{
    /// <summary>
    /// To generate lines
    /// </summary>
    internal static class LineGenerator
    {
        private static string fileName = "./game/textGenerator/activismLines.txt";

        /// <summary>
        /// Get random line
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random line</returns>
        public static string GetRandomLine(Random random)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            long position = (long)(random.NextDouble() * (double)(fileInfo.Length - 500));
            
            List<string> lineList = new List<string>();

            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    stream.Seek(position, 0);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = reader.ReadLine();

                        for (int i = 0; i < 15; i++)
                        {
                            if (reader.EndOfStream)
                                break;
                            lineList.Add(reader.ReadLine());
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return "Undefined";
            }

            int shortestLineLength = -1;
            string shortestLine = "Undefined";
            foreach (string line in lineList)
            {
                if (line.Length < shortestLineLength || shortestLineLength == -1)
                {
                    shortestLine = line;
                    shortestLineLength = line.Length;
                }
            }

            return shortestLine;
        }
    }
}
