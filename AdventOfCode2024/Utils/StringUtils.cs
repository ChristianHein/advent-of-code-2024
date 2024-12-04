using System.Diagnostics;

namespace AdventOfCode2024.Utils;

public class StringUtils
{
    public static char[][] ConvertStringArrayToCharJaggedArray(string[] input)
    {
        return input.Length == 0
            ? []
            : input.Select(elem => elem.ToCharArray()).ToArray();
    }
}
