using System.IO;
using AdventOfCode2024.Day08;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day08;

[TestFixture]
[TestOf(typeof(Day8))]
public class Day8Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day08/input");

    private readonly string[] _exampleInput =
    [
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............"
    ];

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var day = new Day8(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("254"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var day = new Day8(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("951"));
    }

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var day = new Day8(_exampleInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("14"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var day = new Day8(_exampleInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("34"));
    }
}
