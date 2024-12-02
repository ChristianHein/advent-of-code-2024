using AdventOfCode2024.Day01;
using AdventOfCode2024.Day02;

namespace AdventOfCode2024;

using JetBrains.Annotations;

[PublicAPI]
public sealed class AdventOfCode2024
{
    public static void Main()
    {
        PrintDay1Solution();
        PrintDay2Solution();
    }

    private static string FormatDaySolutions(Puzzle day)
    {
        return $"Day {day:00}{Environment.NewLine}"
               + $"  Part 1 solution: '{day.Part1Solution()}'{Environment.NewLine}"
               + $"  Part 2 solution: '{day.Part2Solution()}'{Environment.NewLine}";
    }

    public static void PrintDay1Solution()
    {
        var day = new Day1(File.ReadAllLines("Day01/input"));
        Console.WriteLine(FormatDaySolutions(day));
    }

    public static void PrintDay2Solution()
    {
        var day = new Day2(File.ReadAllLines("Day02/input"));
        Console.WriteLine(FormatDaySolutions(day));
    }
}
