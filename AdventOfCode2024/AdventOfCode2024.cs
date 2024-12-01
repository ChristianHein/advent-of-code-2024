using advent_of_code_2024.day01;

namespace advent_of_code_2024;

using JetBrains.Annotations;

[PublicAPI]
public sealed class AdventOfCode2024
{
    public static void Main()
    {
        PrintDay1Solution();
    }

    private static string FormatDaySolution(Puzzle day)
    {
        return $"Day {day:00}{Environment.NewLine}"
               + $"  Part 1 solution: '{day.Part1Solution()}'{Environment.NewLine}"
               + $"  Part 2 solution: '{day.Part2Solution()}'{Environment.NewLine}";
    }

    public static void PrintDay1Solution()
    {
        var day = new Day1(File.ReadAllLines("day01/input"));
        Console.WriteLine(FormatDaySolution(day));
    }
}
