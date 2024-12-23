using System.IO;
using AdventOfCode2024.Day16;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day16;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day16/input");

    private readonly string[] _exampleInput1 =
    [
        "###############",
        "#.......#....E#",
        "#.#.###.#.###.#",
        "#.....#.#...#.#",
        "#.###.#####.#.#",
        "#.#.#.......#.#",
        "#.#.#####.###.#",
        "#...........#.#",
        "###.#.#####.#.#",
        "#...#.....#.#.#",
        "#.#.#.###.#.#.#",
        "#.....#...#.#.#",
        "#.###.#.#.#.#.#",
        "#S..#.....#...#",
        "###############"
    ];

    private readonly string[] _exampleInput2 =
    [
        "#################",
        "#...#...#...#..E#",
        "#.#.#.#.#.#.#.#.#",
        "#.#.#.#...#...#.#",
        "#.#.#.#.###.#.#.#",
        "#...#.#.#.....#.#",
        "#.#.#.#.#.#####.#",
        "#.#...#.#.#.....#",
        "#.#.#####.#.###.#",
        "#.#.#.......#...#",
        "#.#.###.#####.###",
        "#.#.#...#.....#.#",
        "#.#.#.#####.###.#",
        "#.#.#.........#.#",
        "#.#.#.#########.#",
        "#S#.............#",
        "#################"
    ];

    [Test]
    public void UseExampleInput1_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("7036"));
    }

    [Test]
    public void UseExampleInput2_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("11048"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("92432"));
    }

    [Test]
    public void UseExampleInput1_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("-1"));
    }

    [Test]
    public void UseExampleInput2_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("-1"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("-1"));
    }
}
