using System.IO;
using AdventOfCode2024.Day17;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day17;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day17/input");

    private readonly string[] _exampleInput1 =
    [
        "Register A: 729",
        "Register B: 0",
        "Register C: 0",
        "",
        "Program: 0,1,5,4,3,0"
    ];

    private readonly string[] _exampleInput2 =
    [
        "Register A: 2024",
        "Register B: 0",
        "Register C: 0",
        "",
        "Program: 0,3,5,4,3,0"
    ];

    [Test]
    public void UseExampleInput1_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("4,3,2,6,4,5,3,2,4"));
    }

    [Test]
    public void UseExampleInput2_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("117440"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("164540892147389"));
    }
}
