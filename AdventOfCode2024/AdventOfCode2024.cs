using System.Diagnostics;
using AdventOfCode2024.Day01;
using AdventOfCode2024.Day02;
using AdventOfCode2024.Day03;
using AdventOfCode2024.Day04;
using AdventOfCode2024.Day05;
using AdventOfCode2024.Day06;

namespace AdventOfCode2024;

using JetBrains.Annotations;

[PublicAPI]
public sealed class AdventOfCode2024
{
    public static void Main()
    {
        PrintDay1Solution();
        PrintDay2Solution();
        PrintDay3Solution();
        PrintDay4Solution();
        PrintDay5Solution();
        PrintDay6Solution();
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

    public static void PrintDay3Solution()
    {
        var day = new Day3(File.ReadAllLines("Day03/input"));
        Console.WriteLine(FormatDaySolutions(day));
    }

    public static void PrintDay4Solution()
    {
        var day = new Day4(File.ReadAllLines("Day04/input"));
        Console.WriteLine(FormatDaySolutions(day));
    }

    public static void PrintDay5Solution()
    {
        var day = new Day5(File.ReadAllLines("Day05/input"));
        Console.WriteLine(FormatDaySolutions(day));
    }

    public static void PrintDay6Solution()
    {
        var day = new Day6(File.ReadAllLines("Day06/input"));
        Console.WriteLine(FormatDaySolutions(day));
    }

    private static string FormatDaySolutions(Puzzle day, bool printExecutionTimes = true)
    {
        var watch = Stopwatch.StartNew();
        var part1Solution = day.Part1Solution();
        var part1ElapsedMs = watch.ElapsedMilliseconds;

        watch = Stopwatch.StartNew();
        var part2Solution = day.Part2Solution();
        var part2ElapsedMs = watch.ElapsedMilliseconds;

        var output = $"Day {day.Number:00}{Environment.NewLine}";

        output += $"  Part 1 solution: '{part1Solution}'{Environment.NewLine}";
        if (printExecutionTimes)
        {
            output += $"  (execution time: {part1ElapsedMs} ms){Environment.NewLine}";
        }

        output += $"  Part 2 solution: '{part2Solution}'{Environment.NewLine}";
        if (printExecutionTimes)
        {
            output += $"  (execution time: {part2ElapsedMs} ms){Environment.NewLine}";
        }

        return output;
    }
}
