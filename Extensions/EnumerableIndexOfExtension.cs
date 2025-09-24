using System.Diagnostics.CodeAnalysis;

namespace Acidmanic.Mathematics.Extensions;

public static class EnumerableIndexOfExtension
{


    public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> comparison)
    {
        var index = 0;

        foreach (var item in enumerable)
        {
            if (comparison(item))
            {
                return index;
            }

            index++;
        }

        return -1;
    }


    public static List<T> SortSelf<T>(this List<T> list)
    {
        list.Sort();

        return list;
    }
    
    public static List<T> SortSelf<T>(this List<T> list,[NotNull] Comparison<T> comparison)
    {
        list.Sort(comparison);

        return list;
    }
    
    public static List<T> SortSelf<T>(this List<T> list,IComparer<T> comparer)
    {
        list.Sort(comparer);

        return list;
    }
}