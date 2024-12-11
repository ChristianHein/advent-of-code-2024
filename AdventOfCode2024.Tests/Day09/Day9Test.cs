using System.IO;
using AdventOfCode2024.Day09;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day09;

[TestFixture]
[TestOf(typeof(Day9))]
public class Day9Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day09/input");

    private readonly string[] _exampleInput =
    [
        "2333133121414131402"
    ];

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var day = new Day9(_exampleInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("1928"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var day = new Day9(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("6448989155953"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var day = new Day9(_exampleInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("2858"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var day = new Day9(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("6476642796832"));
    }
}
