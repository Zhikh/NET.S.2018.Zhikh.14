using System;

namespace Task4.Logic
{
    public static class FibonacciGenerator
    {
        private static readonly double _sqrtFromFive;

        static FibonacciGenerator()
        {
            _sqrtFromFive = Math.Sqrt(5);
        }

        /// <summary>
        /// Find Fibonacci's sequence from start index to index = count - 1
        /// </summary>
        /// <param name="startIndex"> Degree of Binet value </param>
        /// <param name="count"> Length of sequence</param>
        /// <returns> Sequence </returns>
        /// <exception cref="ArgumentException"> When count is less or is zero </exception>
        public static int[] Generate(int startIndex, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException($"The parameter {nameof(count)} can't be negative!");
            }

            if (count == 0)
            {
                throw new ArgumentException($"The parameter {nameof(count)} can't be zero!");
            }

            int[] array = new int[count];
            double firstExpression = (1 + _sqrtFromFive) / 2;
            double secondExpression = (1 - _sqrtFromFive) / 2;

            int sign;
            int index;
            for (int i = 0; i < count; i++)
            {
                sign = 1;
                index = startIndex;

                if (startIndex < 0)
                {
                    index *= -1;
                    if (index % 2 == 0)
                    {
                        sign = -1;
                    }
                }

                array[i] = sign * (int)((Math.Pow(firstExpression, index) - Math.Pow(secondExpression, index)) / _sqrtFromFive);
                startIndex++;
            }

            return array;
        }
    }
}
