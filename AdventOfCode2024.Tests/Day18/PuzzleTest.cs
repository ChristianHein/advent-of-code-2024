using System.IO;
using AdventOfCode2024.Day18;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day18;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day18/input");

    private readonly string[] _exampleInput =
    [
        "5,4",
        "4,2",
        "4,5",
        "3,0",
        "2,1",
        "6,3",
        "2,4",
        "1,5",
        "0,6",
        "3,3",
        "2,6",
        "5,1",
        "1,2",
        "5,5",
        "2,5",
        "6,5",
        "1,4",
        "0,4",
        "6,4",
        "1,1",
        "6,1",
        "1,0",
        "0,5",
        "1,6",
        "2,0"
    ];

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(width: 7, height: 7, part1CorruptedBytes: 12, _exampleInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("22"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(width: 71, height: 71, part1CorruptedBytes: 1024, _realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("262"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(width: 7, height: 7, part1CorruptedBytes: 12, _exampleInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("6,1"));
    }

    [Test, Explicit]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(width: 71, height: 71, part1CorruptedBytes: 1024, _realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("22,20"));
    }
}
