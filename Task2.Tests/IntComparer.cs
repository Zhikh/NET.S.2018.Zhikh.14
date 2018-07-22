using System.Collections.Generic;

namespace Task2.Tests
{
    public sealed class IntComparer : IComparer<int>
    {
        /// <summary>
        /// Compare int values
        /// </summary>
        /// <param name="x"> First value </param>
        /// <param name="y"> Second value </param>
        /// <returns> Integer value in range [-1, 1] </returns>
        public int Compare(int x, int y)
        {
            if (x < y)
            {
                return -1;
            }

            if (x > y)
            {
                return 1;
            }

            return 0;
        }
    }
}
