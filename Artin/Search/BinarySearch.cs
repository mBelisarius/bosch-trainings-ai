namespace Artin.Search;

public static partial class Search
{
    public static int BinarySearch<T>(IList<T> collection, T value, int begin = 0, int end = -1)
    {
        end = end == -1 ? collection.Count - 1 : end;
        
        var comparer = Comparer<T>.Default;

        while (end - begin > 1)
        {
            var mid = begin + (end - begin) / 2;
            var midValue = collection[mid];

            if (comparer.Compare(midValue, value) == 0)
                return mid;
            
            if (comparer.Compare(midValue, value) < 0)
                begin = mid;
            else
                end = mid;
        }

        return -1;
    }
    
    public static int BinarySearchLower<T>(IList<T> collection, T value, int begin = 0, int end = -1)
    {
        end = end == -1 ? collection.Count - 1 : end;
        
        var comparer = Comparer<T>.Default;

        while (end - begin > 1)
        {
            var mid = begin + (end - begin) / 2;
            var midValue = collection[mid];

            if (comparer.Compare(midValue, value) == 0)
                return mid;
            
            if (comparer.Compare(midValue, value) < 0)
                begin = mid;
            else
                end = mid;
        }

        return begin;
    }
}