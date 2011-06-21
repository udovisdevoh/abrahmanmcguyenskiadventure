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

            long position = (long)(random.NextDouble() * (double)(fileInfo.Length - 100));
            
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    stream.Seek(position, 0);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        reader.ReadLine();
                        return reader.ReadLine();
                    }
                }
            }
            catch (Exception exception)
            {
                return "Undefined";
            }
        }
    }
}
