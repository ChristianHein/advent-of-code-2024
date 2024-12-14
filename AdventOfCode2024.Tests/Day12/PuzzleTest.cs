using System.IO;
using AdventOfCode2024.Day12;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day12;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day12/input");

    private readonly string[] _exampleInput1 =
    [
        "AAAA",
        "BBCD",
        "BBCC",
        "EEEC"
    ];

    private readonly string[] _exampleInput2 =
    [
        "OOOOO",
        "OXOXO",
        "OOOOO",
        "OXOXO",
        "OOOOO"
    ];

    private readonly string[] _exampleInput3 =
    [
        "EEEEE",
        "EXXXX",
        "EEEEE",
        "EXXXX",
        "EEEEE"
    ];

    private readonly string[] _exampleInput4 =
    [
        "AAAAAA",
        "AAABBA",
        "AAABBA",
        "ABBAAA",
        "ABBAAA",
        "AAAAAA"
    ];

    private readonly string[] _exampleInput5 =
    [
        "RRRRIICCFF",
        "RRRRIICCCF",
        "VVRRRCCFFF",
        "VVRCCCJFFF",
        "VVVVCJJCFE",
        "VVIVCCJJEE",
        "VVIIICJJEE",
        "MIIIIIJJEE",
        "MIIISIJEEE",
        "MMMISSJEEE"
    ];

    [Test]
    public void UseExampleInput1_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("140"));
    }

    [Test]
    public void UseExampleInput2_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("772"));
    }

    [Test]
    public void UseExampleInput5_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput5);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("1930"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("1374934"));
    }

    [Test]
    public void UseExampleInput1_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("80"));
    }

    [Test]
    public void UseExampleInput2_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("436"));
    }

    [Test]
    public void UseExampleInput3_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput3);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("236"));
    }

    [Test]
    public void UseExampleInput4_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput4);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("368"));
    }

    [Test]
    public void UseExampleInput5_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput5);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("1206"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("841078"));
    }
}
