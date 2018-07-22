using System.Collections.Generic;

namespace Task2.Logic
{
    public class BinarySearcher<T> : ISearchStrategy<T>
    {
        /// <summary>
        /// Gets array of T and element for searching 
        /// and using binary searching find position of value in array
        /// </summary>
        /// <param name="array"> Unsorted/sorted array (it's not matter) </param>
        /// <param name="value"> Value for searching </param>
        /// <returns> Index of value in array </returns>
        public int SearchByBinary(T[] array, T value, IComparer<T> comparer)
        {
            int left = 0;
            int right = array.Length;
            int middle;

            while (true)
            {
                middle = left + (right - left) / 2;

                if (comparer.Compare(array[middle], value) == 0)
                {
                    return middle;
                }

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
    }
}
