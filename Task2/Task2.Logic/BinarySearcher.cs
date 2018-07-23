using System;
using System.Collections.Generic;

namespace Task2.Logic
{
    public static class BinarySearcher
    {
        /// <summary>
        /// Gets sorted array of T and element for searching 
        /// and using binary searching find position of value in array
        /// </summary>
        /// <param name="array"> Sorted array (it's not matter) </param>
        /// <param name="comparer"> Rules for comparing </param>
        /// <param name="value"> Value for searching </param>
        /// <returns> Index of value in array </returns>
        /// <exception cref="ArgumentNullException"> When array or comparer are null </exception>
        public static int SearchByBinary<T>(T[] array, T value, IComparer<T> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"The {nameof(array)} parameter can't be null!");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"The {nameof(comparer)} parameter can't be null!");
            }

            int left = 0;
            int right = array.Length;
            int middle;

            while (left < right)
            {
                middle = left + (right - left) / 2;

                if (comparer.Compare(array[left], value) == 0)
                {
                    return left;
                }

                if (comparer.Compare(array[middle], value) == 0)
                {
                    if (middle == left + 1)
                    {
                        return middle;
                    }

                    right = middle + 1;
                }
                else
                {
                    if (comparer.Compare(array[middle], value) > 0)
                    {
                        right = middle;
                    }
                    else
                    {
                        left = middle + 1;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets sorted array of T and element for searching 
        /// and using binary searching find position of value in array
        /// </summary>
        /// <param name="array"> Sorted array (it's not matter) </param>
        /// <param name="compare"> Rules for comparing </param>
        /// <param name="value"> Value for searching </param>
        /// <returns> Index of value in array </returns>
        /// <exception cref="ArgumentNullException"> When array or comparer are null </exception>
        public static int SearchByBinary<T>(T[] array, T value, Comparison<T> compare)
        {
            return SearchByBinary(array, value, new Nested<T>(compare));
        }

        private class Nested<T> : IComparer<T>
        {
            private Comparison<T> _compareMethod;

            public Nested(Comparison<T> compare)
            {
                _compareMethod = compare;
            }

            public int Compare(T left, T right)
            {
                return _compareMethod(left, right);
            }
        }
    }
}
