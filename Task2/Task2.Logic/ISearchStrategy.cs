using System.Collections.Generic;

namespace Task2.Logic
{
    public interface ISearchStrategy<T>
    {
        int SearchByBinary(T[] array, T value, IComparer<T> comparer);
    }
}
