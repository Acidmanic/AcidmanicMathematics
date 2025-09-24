using Acidmanic.Mathematics.Utilities;

namespace Acidmanic.Mathematics.Extensions;

public static class IndexibleExtensions
{
    public static T[] Compose<T>(this T[] values, int[] indexes)
    {
        return Compose(values, size => new T[size], indexes);
    }

    public static List<T> Compose<T>(this List<T> values, int[] indexes)
    {
        return Compose(values, size => new T[size], indexes);
    }

    private static dynamic Compose(dynamic values, Func<int, dynamic> factory, int[] indexes)
    {
        var composed = factory(indexes.Length);

        for (int i = 0; i < indexes.Length; i++)
        {
            composed[i] = values[indexes[i]];
        }

        return composed;
    }

    public static T[] SubSet<T>(this T[] values, int start, int length)
    {
        return SubSet(values, size => new T[size], start, length);
    }

    public static List<T> SubSet<T>(this List<T> values, int start, int length)
    {
        return SubSet(values, size => new T[size], start, length);
    }

    

    private static dynamic SubSet(dynamic values, Func<int, dynamic> factory, int start, int length)
    {
        if (start < 0)
        {
            start = 0;
        }

        var excludingBound = start + length;

        if (excludingBound >= values.Length)
        {
            excludingBound = values.Length;
        }

        var arranged = factory(length);

        for (int i = start; i < excludingBound; i++)
        {
            arranged[i] = values[i];
        }

        return arranged;
    }

    public static int[] SortedIndexes<T>(this T[] values, Func<T, T, bool> isSorted)
    {
        var indexes = new int[values.Length].FillVectorIndexes();

        var sorted = false;

        while (!sorted)
        {
            sorted = true;

            for (int i = 0; i < values.Length - 1; i++)
            {
                if (!isSorted(values[indexes[i]], values[indexes[i + 1]]))
                {
                    (indexes[i], indexes[i + 1]) = (indexes[i + 1], indexes[i]);
                    sorted = false;
                }
            }
        }

        return indexes;
    }


    private class ComparerByIndex<T> : IComparer<int>
    {
        private readonly Func<T, T, int> _comparison;
        private readonly dynamic _indexibleValues;


        public ComparerByIndex(dynamic indexibleValues, Func<T, T, int> comparison)
        {
            _comparison = comparison;
            _indexibleValues = indexibleValues;
        }

        public int Compare(int x, int y)
        {
            return _comparison(_indexibleValues[x], _indexibleValues[y]);
        }
    }


    public static int[] SortedIndexes<T>(this T[] values, Func<T, T, int> comparison)
    {
        var indexes = new int[values.Length].FillVectorIndexes().ToList();

        indexes.Sort(new ComparerByIndex<T>(values, comparison));

        return indexes.ToArray();
    }

    public static int[] SortedIndexes<T>(this List<T> values, Func<T, T, int> comparison)
    {
        var indexes = new int[values.Count].FillVectorIndexes().ToList();

        indexes.Sort(new ComparerByIndex<T>(values, comparison));

        return indexes.ToArray();
    }


    public static int[] PickIndexes<T>(this T[] values, Func<T, bool> selector)
    {
        return PickIndexes(values, values.Length, selector);
    }
    
    public static int[] PickIndexes<T>(this List<T> values, Func<T, bool> selector)
    {
        return PickIndexes(values, values.Count, selector);
    }

    private static int[] PickIndexes<T>(dynamic values, int length, Func<T, bool> selector)
    {
        var indexesList = new List<int>();

        for (int i = 0; i < length; i++)
        {
            if (selector(values[i]))
            {
                indexesList.Add(i);
            }
        }

        return indexesList.ToArray();
    }
    
}