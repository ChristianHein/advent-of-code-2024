﻿using System.Diagnostics;

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
        PrintDay7Solution();
        PrintDay8Solution();
        PrintDay9Solution();
    }

    public static void PrintDay1Solution()
    {
        var puzzle = new Day01.Puzzle(File.ReadAllLines("Day01/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay2Solution()
    {
        var puzzle = new Day02.Puzzle(File.ReadAllLines("Day02/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay3Solution()
    {
        var puzzle = new Day03.Puzzle(File.ReadAllLines("Day03/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay4Solution()
    {
        var puzzle = new Day04.Puzzle(File.ReadAllLines("Day04/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay5Solution()
    {
        var puzzle = new Day05.Puzzle(File.ReadAllLines("Day05/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay6Solution()
    {
        var puzzle = new Day06.Puzzle(File.ReadAllLines("Day06/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay7Solution()
    {
        var puzzle = new Day07.Puzzle(File.ReadAllLines("Day07/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay8Solution()
    {
        var puzzle = new Day08.Puzzle(File.ReadAllLines("Day08/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    public static void PrintDay9Solution()
    {
        var puzzle = new Day09.Puzzle(File.ReadAllLines("Day09/input"));
        Console.WriteLine(FormatDaySolutions(puzzle));
    }

    private static string FormatDaySolutions(BasePuzzle puzzle, bool printExecutionTimes = true)
    {
        var watch = Stopwatch.StartNew();
        var part1Solution = puzzle.Part1Solution();
        var part1ElapsedMs = watch.ElapsedMilliseconds;

        watch = Stopwatch.StartNew();
        var part2Solution = puzzle.Part2Solution();
        var part2ElapsedMs = watch.ElapsedMilliseconds;

        var output = $"Day {puzzle.Number:00}{Environment.NewLine}";

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
