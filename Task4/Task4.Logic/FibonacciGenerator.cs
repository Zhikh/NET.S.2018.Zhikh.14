using System;
using System.Collections.Generic;
using System.Numerics;

namespace Task4.Logic
{
    public static class FibonacciGenerator
    {
        #region Public methods
        /// <summary>
        /// Find Fibonacci's sequence
        /// </summary>
        /// <param name="count"> Length of sequence</param>
        /// <returns> Sequence </returns>
        /// <exception cref="ArgumentException"> When count is less or is zero </exception>
        public static IEnumerable<BigInteger> Generate(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException($"The parameter {nameof(count)} can't be negative!");
            }

            if (count == 0)
            {
                throw new ArgumentException($"The parameter {nameof(count)} can't be zero!");
            }

            return GenerateSequence(count);
        }
        #endregion

        #region Private methods
        private static IEnumerable<BigInteger> GenerateSequence(int count)
        {
            BigInteger preElement = 1;
            BigInteger prePreElement = 0;
            BigInteger element;

            yield return preElement;

            for (int i = 0; i < count - 1; i++)
            {
                element = BigInteger.Add(prePreElement, preElement);

                yield return element;

                prePreElement = preElement;
                preElement = element;
            }
        }
        #endregion
    }
}
