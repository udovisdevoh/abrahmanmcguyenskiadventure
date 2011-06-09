using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.textGenerator
{
    /// <summary>
    /// Represents a matrix
    /// key: strings
    /// value: float
    /// </summary>
    internal class Matrix
    {
        #region Fields
        /// <summary>
        /// Normal matrix representation
        /// </summary>
        private Dictionary<string, Dictionary<string, float>> normalData = new Dictionary<string, Dictionary<string, float>>();

        /// <summary>
        /// 90 degree rotated matrix representation
        /// </summary>
        private Dictionary<string, Dictionary<string, float>> reversedData = new Dictionary<string, Dictionary<string, float>>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Multiply statistics
        /// </summary>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <param name="toMultiply">multiplicator</param>
        public void MultiplyStatistics(string fromValue, string toValue, float toMultiply)
        {
            MultiplyStatisticsTo(normalData, fromValue, toValue, toMultiply);
            MultiplyStatisticsTo(reversedData, toValue, fromValue, toMultiply);
        }

        /// <summary>
        /// Add 1 to existing statistics count
        /// </summary>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        public void AddStatistics(string fromValue, string toValue)
        {
            AddStatistics(fromValue, toValue, 1);
        }

        /// <summary>
        /// Add a number to existing statistics count
        /// </summary>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <param name="toAdd">add to existing count</param>
        public void AddStatistics(string fromValue, string toValue, float toAdd)
        {
            AddStatisticsTo(normalData, fromValue, toValue, toAdd);
            AddStatisticsTo(reversedData, toValue, fromValue, toAdd);
        }

        /// <summary>
        /// Set statistics count number for values
        /// </summary>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <param name="newCount">new count</param>
        public void SetStatistics(string fromValue, string toValue, float newCount)
        {
            SetStatisticsTo(normalData, fromValue, toValue, newCount);
            SetStatisticsTo(reversedData, toValue, fromValue, newCount);
        }

        /// <summary>
        /// Whether a key name is present in the matrix
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns>whether a key name is present in the matrix</returns>
        public bool ContainsKey(string keyName)
        {
            if (normalData.ContainsKey(keyName))
                return true;
            else if (reversedData.ContainsKey(keyName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Try get normal value
        /// </summary>
        /// <param name="concept1">subject concept name</param>
        /// <param name="concept2">other concept name</param>
        /// <returns>current value</returns>
        public float this[string concept1, string concept2]
        {
            get
            {
                Dictionary<string, float> vector;
                float value;

                if (!normalData.TryGetValue(concept1, out vector))
                    return 0.0f;

                if (!vector.TryGetValue(concept2, out value))
                    return 0.0f;

                return value;
            }
        }

        /// <summary>
        /// Compare to other matrix
        /// </summary>
        /// <param name="other">other matrix</param>
        /// <returns>difference between two matrixes</returns>
        public double CompareTo(Matrix other)
        {
            return CompareTo(other, false);
        }

        /// <summary>
        /// Compare to other matrix
        /// </summary>
        /// <param name="other">other matrix</param>
        /// <param name="isSecondMatrixLarger">whether other matrix is larger</param>
        /// <returns>difference between two matrixes</returns>
        public double CompareTo(Matrix other, bool isSecondMatrixLarger)
        {
            double difference = 0.0;

            foreach (string sourceKey in this.normalData.Keys)
            {
                Dictionary<string, float> row1 = this.normalData[sourceKey];
                Dictionary<string, float> row2;

                if (!other.normalData.TryGetValue(sourceKey, out row2))
                {
                    row2 = new Dictionary<string, float>();
                }

                if (isSecondMatrixLarger)
                    difference += CompareRows(row1, row2, true);
                else
                    difference += CompareRows(row1, row2);
            }

            if (!isSecondMatrixLarger)
            {
                foreach (string sourceKey in other.normalData.Keys)
                {
                    Dictionary<string, float> row1 = other.normalData[sourceKey];
                    Dictionary<string, float> row2;

                    if (!this.normalData.TryGetValue(sourceKey, out row2))
                    {
                        row2 = new Dictionary<string, float>();
                        difference += CompareRows(row1, row2);
                    }
                }
            }

            return difference;
        }

        /// <summary>
        /// Get random starting pair
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random starting pair</returns>
        public string GetRandomStartingPair(Random random)
        {
            List<string> availablePairList = new List<string>();

            string key;
            Dictionary<string, float> value;
            foreach (KeyValuePair<string, Dictionary<string, float>> keyValuePair in NormalData)
            {
                key = keyValuePair.Key;
                value = keyValuePair.Value;

                if (key.StartsWith(" ") && value.Count > 2)
                {
                    availablePairList.Add(key);
                }
            }

            if (availablePairList.Count > 0)
                return (string)((IEnumerable<object>)availablePairList.ToArray()).GetRandomValue(random);
            else
                return (string)((IEnumerable<object>)NormalData.Keys.ToArray()).GetRandomValue(random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Multiply statistics
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <param name="toMultiply">multiplicator</param>
        private void MultiplyStatisticsTo(Dictionary<string, Dictionary<string, float>> data, string fromValue, string toValue, float toMultiply)
        {
            Dictionary<string, float> row;
            if (!data.TryGetValue(fromValue, out row))
            {
                row = new Dictionary<string, float>();
                data.Add(fromValue, row);
            }

            float totalOccurence;
            if (row.TryGetValue(toValue, out totalOccurence))
            {
                row[toValue] = (float)(Math.Sqrt(totalOccurence) * Math.Sqrt(toMultiply));
            }
            else
            {
                row.Add(toValue, toMultiply);
            }
        }

        /// <summary>
        /// Add statistics
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <param name="toAdd">number to add</param>
        private void AddStatisticsTo(Dictionary<string, Dictionary<string, float>> data, string fromValue, string toValue, float toAdd)
        {
            Dictionary<string, float> row;
            if (!data.TryGetValue(fromValue, out row))
            {
                row = new Dictionary<string, float>();
                data.Add(fromValue, row);
            }

            float totalOccurence;
            if (row.TryGetValue(toValue, out totalOccurence))
            {
                row[toValue] = totalOccurence + toAdd;
            }
            else
            {
                row.Add(toValue, toAdd);
            }
        }

        /// <summary>
        /// Set statistics
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="fromValue">from value</param>
        /// <param name="toValue">to value</param>
        /// <param name="newCount">new number to set</param>
        private void SetStatisticsTo(Dictionary<string, Dictionary<string, float>> data, string fromValue, string toValue, float newCount)
        {
            Dictionary<string, float> row;
            if (!data.TryGetValue(fromValue, out row))
            {
                row = new Dictionary<string, float>();
                data.Add(fromValue, row);
            }

            float totalOccurence;
            if (row.TryGetValue(toValue, out totalOccurence))
            {
                row[toValue] = newCount;
            }
            else
            {
                row.Add(toValue, newCount);
            }
        }

        /// <summary>
        /// Compare two rows and return difference
        /// </summary>
        /// <param name="row1">row 1</param>
        /// <param name="row2">row 2</param>
        /// <returns>difference between two rows</returns>
        private double CompareRows(Dictionary<string, float> row1, Dictionary<string, float> row2)
        {
            return CompareRows(row1, row2, false);
        }

        /// <summary>
        /// Compare two rows and return difference
        /// </summary>
        /// <param name="row1">row 1</param>
        /// <param name="row2">row 2</param>
        /// <param name="isSecondRowLarger">whether second row is larger</param>
        /// <returns>difference between two rows</returns>
        private double CompareRows(Dictionary<string, float> row1, Dictionary<string, float> row2, bool isSecondRowLarger)
        {
            if (isSecondRowLarger)
                return CompareRowsSingleWay(row1, row2);
            else
                return CompareRowsSingleWay(row1, row2) + CompareRowsSingleWay(row2, row1);
        }

        /// <summary>
        /// Compare two rows and return difference
        /// </summary>
        /// <param name="row1">row 1</param>
        /// <param name="row2">row 2</param>
        /// <returns>difference between two rows</returns>
        private double CompareRowsSingleWay(Dictionary<string, float> row1, Dictionary<string, float> row2)
        {
            double difference = 0.0;

            float value1;
            float value2;
            foreach (string key in row1.Keys)
            {
                value1 = row1[key];
                if (row2.TryGetValue(key, out value2))
                {
                    value1 = NormalizeValue(value1, row1);
                    value2 = NormalizeValue(value2, row2);

                    difference += Math.Abs(value1 - value2);
                }
                else
                {
                    difference += 1.0;
                }
            }
            return difference;
        }

        /// <summary>
        /// Get normalized value from row
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="row">row</param>
        /// <returns>normalized value</returns>
        private float NormalizeValue(float value, Dictionary<string, float> row)
        {
            float sum = row.Values.Sum();
            if (sum == 0)
                return 0;

            return value / sum;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Normal matrix representation
        /// </summary>
        public Dictionary<string, Dictionary<string, float>> NormalData
        {
            get { return normalData; }
            set { normalData = value; }
        }

        /// <summary>
        /// 90 degree rotated matrix representation
        /// </summary>
        public Dictionary<string, Dictionary<string, float>> ReversedData
        {
            get { return reversedData; }
            set { reversedData = value; }
        }
        #endregion
    }
}
