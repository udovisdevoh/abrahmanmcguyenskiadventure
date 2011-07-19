using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Builds time split rythm patterns
    /// </summary>
    public class RythmPatternBuilderTimeSplit
    {
        #region Fields
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random;

        /// <summary>
        /// Desired rythm length
        /// </summary>
        private double desiredRythmLength;

        /// <summary>
        /// Minimum note length
        /// </summary>
        private double minimumNoteLength;

        /// <summary>
        /// Maximum note length
        /// </summary>
        private double maximumNoteLength;

        /// <summary>
        /// Probability to have ternary rythm
        /// </summary>
        private double ternaryProbability;

        /// <summary>
        /// Probability to have quiternary probability
        /// </summary>
        private double quinternaryProbability;

        /// <summary>
        /// Probability to have a dotted note
        /// </summary>
        private double dottedProbability;

        /// <summary>
        /// Whether we allow ternary beats
        /// </summary>
        private bool isAllowedTernary;

        /// <summary>
        /// Whether we allow quiternary beats
        /// </summary>
        private bool isAllowedQuinternary;
        #endregion

        #region Constructors
        /// <summary>
        /// Build time split rythm pattern builder
        /// </summary>
        public RythmPatternBuilderTimeSplit()
        {
            ResetConstructionSettings();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build rythm pattern
        /// </summary>
        /// <returns>rythm pattern</returns>
        public virtual RythmPattern Build()
        {
            if (desiredRythmLength == -1)
                throw new RythmPatternBuilderException("Missing DesiredRythmLength property");
            else if (random == null)
                throw new RythmPatternBuilderException("Missing Random property");

            RythmPattern rythmPattern = new RythmPattern();
            while (rythmPattern.Sum < desiredRythmLength)
                rythmPattern.Add(maximumNoteLength);

            int timeOut = 0;
            while (rythmPattern.Min() > minimumNoteLength)
            {
                rythmPattern = TrySplit(rythmPattern);
                timeOut++;
                if (timeOut > 100)
                    break;
            }

            return rythmPattern;
        }

        /// <summary>
        /// Reset construction settings
        /// </summary>
        public void ResetConstructionSettings()
        {
            random = null;
            desiredRythmLength = -1;
            minimumNoteLength = 0.015625;
            maximumNoteLength = 0.125;
            isAllowedTernary = true;
            isAllowedQuinternary = true;
            ternaryProbability = 0.2;
            quinternaryProbability = 0.1;
            dottedProbability = 0.3;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Try split notes in shorter notes
        /// </summary>
        /// <param name="oldRythmPattern">pattern for which we split notes</param>
        /// <returns>new rythm pattern</returns>
        protected RythmPattern TrySplit(RythmPattern oldRythmPattern)
        {
            RythmPattern newRythmPattern = new RythmPattern();
            for (int i = 0; i < oldRythmPattern.Count; i++)
            {
                if (random.Next(0, 2) == 1 && oldRythmPattern[i] / 2.0 >= minimumNoteLength)
                {
                    newRythmPattern.Add(oldRythmPattern[i] / 2.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 2.0);
                }
                else if (isAllowedTernary && !IsTernary(oldRythmPattern[i]) && random.NextDouble() <= ternaryProbability && oldRythmPattern[i] / 3.0 >= minimumNoteLength)
                {
                    newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 3.0);
                }
                else if (isAllowedQuinternary && !IsTernary(oldRythmPattern[i]) && !IsQuinternary(oldRythmPattern[i]) && random.NextDouble() <= quinternaryProbability && oldRythmPattern[i] / 5.0 >= minimumNoteLength)
                {
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                    newRythmPattern.Add(oldRythmPattern[i] / 5.0);
                }
                else if (random.NextDouble() <= dottedProbability && !IsTernary(oldRythmPattern[i]) && !IsQuinternary(oldRythmPattern[i]))
                {
                    if (IsDotted(oldRythmPattern[i]))
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
        private bool IsDotted(double scalar)
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
        private bool IsQuinternary(double scalar)
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
        private bool IsTernary(double scalar)
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

        #region Properties
        /// <summary>
        /// Desired rythm length
        /// </summary>
        public double DesiredRythmLength
        {
            get { return desiredRythmLength; }
            set { desiredRythmLength = value; }
        }

        /// <summary>
        /// Minimum note length
        /// </summary>
        public double MinimumNoteLength
        {
            get { return minimumNoteLength; }
            set { minimumNoteLength = value; }
        }

        /// <summary>
        /// Maximum note length
        /// </summary>
        public double MaximumNoteLength
        {
            get { return maximumNoteLength; }
            set { maximumNoteLength = value; }
        }

        /// <summary>
        /// Probability to have ternary note
        /// </summary>
        public double TernaryProbability
        {
            get { return ternaryProbability; }
            set { ternaryProbability = value; }
        }

        /// <summary>
        /// Probability to have a quinternary note
        /// </summary>
        public double QuinternaryProbability
        {
            get { return quinternaryProbability; }
            set { quinternaryProbability = value; }
        }

        /// <summary>
        /// Probability to have a dotted note
        /// </summary>
        public double DottedProbability
        {
            get { return dottedProbability; }
            set { dottedProbability = value; }
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        public Random Random
        {
            get { return random; }
            set { random = value; }
        }

        /// <summary>
        /// Whether we allow ternary notes
        /// </summary>
        public bool IsAllowedTernary
        {
            get { return isAllowedTernary; }
            set { isAllowedTernary = value; }
        }

        /// <summary>
        /// Whether we allow quinternary notes
        /// </summary>
        public bool IsAllowedQuinternary
        {
            get { return isAllowedQuinternary; }
            set { isAllowedQuinternary = value; }
        }
        #endregion
    }
}
