using System.IO;
using AdventOfCode2024.Day23;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day23;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day23/input");

    private readonly string[] _exampleInput =
    [
        "kh-tc",
        "qp-kh",
        "de-cg",
        "ka-co",
        "yn-aq",
        "qp-ub",
        "cg-tb",
        "vc-aq",
        "tb-ka",
        "wh-tc",
        "yn-cg",
        "kh-ub",
        "ta-co",
        "de-co",
        "tc-td",
        "tb-wq",
        "wh-td",
        "ta-ka",
        "td-qp",
        "aq-cg",
        "wq-ub",
        "ub-vc",
        "de-ta",
        "wq-aq",
        "wq-vc",
        "wh-yn",
        "ka-de",
        "kh-ta",
        "co-tc",
        "wh-qp",
        "tb-vc",
        "td-yn"
    ];

    [Test]
    public void UseExampleInput_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("7"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("1512"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("co,de,ka,ta"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("ac,ed,fh,kd,lf,mb,om,pe,qt,uo,uy,vr,wg"));
    }
}
