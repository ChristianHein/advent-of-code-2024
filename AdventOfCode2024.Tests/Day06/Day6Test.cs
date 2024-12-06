using System.IO;
using AdventOfCode2024.Day06;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day06;

[TestFixture]
[TestOf(typeof(Day6))]
public class Day6Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day06/input");

    private readonly string[] _exampleInput =
    [
        "....#.....",
        ".........#",
        "..........",
        "..#.......",
        ".......#..",
        "..........",
        ".#..^.....",
        "........#.",
        "#.........",
        "......#..."
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var day = new Day6(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("4711"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var day = new Day6(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("1562"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var day = new Day6(_exampleInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("41"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var day = new Day6(_exampleInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("6"));
    }
}
