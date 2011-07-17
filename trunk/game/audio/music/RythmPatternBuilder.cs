using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Builds time split rythm patterns
    /// </summary>
    internal static class RythmPatternBuilder
    {   
        #region Internal Methods
        /// <summary>
        /// Build rythm pattern
        /// </summary>
        /// <returns>rythm pattern</returns>
        internal static RythmPattern Build(Random random)
        {
            return Build(random, 1.0, false);
        }

        internal static RythmPattern Build(Random random, double length, bool isAllowedTernary)
        {
            return Build(random, length, 0.015625, 0.125, isAllowedTernary);
        }

        internal static RythmPattern Build(Random random, double desiredRythmLength, double minimumNoteLength, double maximumNoteLength, bool isAllowedTernary)
        {
            return Build(random, desiredRythmLength, minimumNoteLength, maximumNoteLength, isAllowedTernary, false, 0.2, 0.1, 0.3);
        }

        /// <summary>
        /// Build rythm pattern
        /// </summary>
        /// <returns>rythm pattern</returns>
        internal static RythmPattern Build(Random random, double desiredRythmLength, double minimumNoteLength, double maximumNoteLength, bool isAllowedTernary, bool isAllowedQuinternary, double ternaryProbability, double quinternaryProbability, double dottedProbability)
        {
            RythmPattern rythmPattern = new RythmPattern();
            while (rythmPattern.Sum < desiredRythmLength)
                rythmPattern.Add(maximumNoteLength);

            int timeOut = 0;
            while (rythmPattern.Min() > minimumNoteLength)
            {
                rythmPattern = TrySplit(rythmPattern, random, minimumNoteLength, maximumNoteLength, isAllowedTernary, isAllowedQuinternary, ternaryProbability, quinternaryProbability, dottedProbability);
                timeOut++;
                if (timeOut > 100)
                    break;
            }

            return rythmPattern;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Try split notes in shorter notes
        /// </summary>
        /// <param name="oldRythmPattern">pattern for which we split notes</param>
        /// <returns>new rythm pattern</returns>
        private static RythmPattern TrySplit(RythmPattern oldRythmPattern, Random random, double minimumNoteLength, double maximumNoteLength, bool isAllowedTernary, bool isAllowedQuinternary, double ternaryProbability, double quinternaryProbability, double dottedProbability)
        {
            RythmPattern newRythmPattern = new RythmPattern();
            for (int i = 0; i < oldRythmPattern.Count; i++)
            {
                if (random.Next(0, 2) == 1 && oldRythmPattern[i] / 2.0 >= minimumNoteLength)
                {
                    newRythmPattern.Add(oldRythmPattern[i] / 2.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 2.0);
                }
                else if (isAllowedTernary && !IsTernary(oldRythmPattern[i], minimumNoteLength, maximumNoteLength) && random.NextDouble() <= ternaryProbability && oldRythmPattern[i] / 3.0 >= minimumNoteLength)
                {
                    newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                }
                else if (isAllowedQuinternary && !IsTernary(oldRythmPattern[i], minimumNoteLength, maximumNoteLength) && !IsQuinternary(oldRythmPattern[i], minimumNoteLength, maximumNoteLength) && random.NextDouble() <= quinternaryProbability && oldRythmPattern[i] / 5.0 >= minimumNoteLength)
                {
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                }
                else if (random.NextDouble() <= dottedProbability && !IsTernary(oldRythmPattern[i], minimumNoteLength, maximumNoteLength) && !IsQuinternary(oldRythmPattern[i], minimumNoteLength, maximumNoteLength))
                {
                    if (IsDotted(oldRythmPattern[i], minimumNoteLength, maximumNoteLength))
                    {
                        int junctionType = random.Next(0, 3);
                        if (junctionType == 0)
                        {
                            newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                            newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                            newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                        }
                        else if (junctionType == 1)
                        {
                            newRythmPattern.Add(oldRythmPattern[i] / 1.5);
                            newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                        }
                        else if (junctionType == 2)
                        {
                            newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                            newRythmPattern.Add(oldRythmPattern[i] / 1.5);
                        }
                    }
                    else
                    {
                        if (random.Next(0, 2) == 1)
                        {
                            newRythmPattern.Add(oldRythmPattern[i] / 4.0 * 3.0);
                            newRythmPattern.Add(oldRythmPattern[i] / 4.0);
                        }
                        else
                        {
                            newRythmPattern.Add(oldRythmPattern[i] / 4.0);
                            newRythmPattern.Add(oldRythmPattern[i] / 4.0 * 3.0);
                        }
                    }
                }
                else
                {
                    newRythmPattern.Add(oldRythmPattern[i]);
                }
            }
            return newRythmPattern;
        }

        /// <summary>
        /// Whether time interval matches dotted note
        /// </summary>
        /// <param name="scalar">time interval</param>
        /// <returns>Whether time interval matches dotted note</returns>
        private static bool IsDotted(double scalar, double minimumNoteLength, double maximumNoteLength)
        {
            double comparator = minimumNoteLength / 4.0 * 0.75;

            do
            {
                if (scalar == comparator)
                    return true;

                comparator *= 2.0;
            }
            while (comparator <= maximumNoteLength);

            return false;
        }

        /// <summary>
        /// Whether time interval is quinternary
        /// </summary>
        /// <param name="scalar">time interval</param>
        /// <returns>Whether time interval is quinternary</returns>
        private static bool IsQuinternary(double scalar, double minimumNoteLength, double maximumNoteLength)
        {
            double comparator = minimumNoteLength / 4.0;
            scalar *= 5.0;

            do
            {
                if (scalar == comparator)
                    return true;

                comparator *= 2.0;
            }
            while (comparator <= maximumNoteLength);

            return false;
        }

        /// <summary>
        /// Whether time interval is ternary
        /// </summary>
        /// <param name="scalar">time interval</param>
        /// <returns>Whether time interval is ternary</returns>
        private static bool IsTernary(double scalar, double minimumNoteLength, double maximumNoteLength)
        {
            double comparator = minimumNoteLength / 4.0;
            scalar *= 3.0;
            
            do
            {
                if (scalar == comparator)
                    return true;

                comparator *= 2.0;
            }
            while (comparator <= maximumNoteLength);

            return false;
        }
        #endregion
    }
}
