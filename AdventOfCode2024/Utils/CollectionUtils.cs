using System.Diagnostics;

namespace AdventOfCode2024.Utils;

public static class CollectionUtils
{
    public static void SetNextCombination<T>(List<T> list, List<T> allowedValues)
        where T : notnull
    {
        Debug.Assert(list.TrueForAll(allowedValues.Contains));

        for (var i = list.Count - 1; i >= 0;)
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
}
