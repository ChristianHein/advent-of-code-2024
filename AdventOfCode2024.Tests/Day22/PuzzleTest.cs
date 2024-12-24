using System.IO;
using AdventOfCode2024.Day22;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day22;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day22/input");

    private readonly string[] _exampleInput1 =
    [
        "1",
        "10",
        "100",
        "2024"
    ];

    private readonly string[] _exampleInput2 =
    [
        "1",
        "2",
        "3",
        "2024"
    ];

    [Test]
    public void UseExampleInput1_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("37327623"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("14082561342"));
    }

    [Test]
    public void UseExampleInput2_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("23"));
    }

    [Test, Explicit("Estimated run time: 20 hours")]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("1568"));
    }
}
