using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace AdventOfCode2024.Utils;

public static class CollectionUtils
{
    public static string DictionaryToString<TKey, T>(Dictionary<TKey, List<T>> dictionary)
        where TKey : notnull
    {
        var sb = new StringBuilder();
        foreach (var (key, value) in dictionary)
        {
            sb.AppendLine($"{{{key}: {string.Join(", ", value)}}}");
        }

        return sb.ToString();
    }

    public static void IncrementCombination<T>(List<T> list, List<T> allowedValues, int incrementIndexFromRight = 0)
        where T : notnull
    {
        Debug.Assert(list.TrueForAll(allowedValues.Contains));
        Debug.Assert(incrementIndexFromRight >= 0);

        for (var i = list.Count - 1 - incrementIndexFromRight; i >= 0;)
        {
            if (!list[i].Equals(allowedValues.Last()))
            {
                var nextIndex = allowedValues.IndexOf(list[i]) + 1;
                list[i] = allowedValues[nextIndex];
                return;
            }

            list[i] = allowedValues.First();
            i--;
        }
    }

    public static List<T> GetNthCombination<T>(List<T> allowedValues, int n, int length)
        where T : notnull
    {
        if (length == 0)
        {
            return [];
        }

        if (allowedValues.Count == 0)
        {
            throw new ArgumentException(null, nameof(allowedValues));
        }

        var result = new List<T>(length);
        var totalCombinations = BigInteger.Pow(allowedValues.Count, length);
        n = (int)(new BigInteger(n) % totalCombinations);

        for (var i = 0; i < length; i++)
        {
            var placeValueAtIndex = BigInteger.Pow(allowedValues.Count, length - 1 - i);
            var valueAtIndex = (int)(n / placeValueAtIndex % allowedValues.Count);
            result.Add(allowedValues[valueAtIndex]);
            if (valueAtIndex > 0)
            {
                n -= (int)placeValueAtIndex * valueAtIndex;
            }
        }

        return result;
    }
}
