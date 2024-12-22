using System.IO;
using AdventOfCode2024.Day14;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day14;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day14/input");

    private readonly string[] _exampleInput =
    [
        "p=0,4 v=3,-3",
        "p=6,3 v=-1,-3",
        "p=10,3 v=-1,2",
        "p=2,0 v=2,-1",
        "p=0,0 v=1,3",
        "p=3,0 v=-2,-2",
        "p=7,6 v=-1,-3",
        "p=3,0 v=-1,-2",
        "p=9,3 v=2,3",
        "p=7,3 v=-1,2",
        "p=2,4 v=2,-3",
        "p=9,5 v=-3,-3"
    ];

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(11, 7, _exampleInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("12"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(101, 103, _realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("225810288"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(101, 103, _realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("6752"));
    }
}
