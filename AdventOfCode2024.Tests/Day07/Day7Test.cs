using System.IO;
using AdventOfCode2024.Day07;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day07;

[TestFixture]
[TestOf(typeof(Day7))]
public class Day7Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day07/input");

    private readonly string[] _exampleInput =
    [
        "190: 10 19",
        "3267: 81 40 27",
        "83: 17 5",
        "156: 15 6",
        "7290: 6 8 6 15",
        "161011: 16 10 13",
        "192: 17 8 14",
        "21037: 9 7 18 13",
        "292: 11 6 16 20"
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var day = new Day7(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("850435817339"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var day = new Day7(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("104824810233437"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var day = new Day7(_exampleInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("3749"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var day = new Day7(_exampleInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("11387"));
    }
}
