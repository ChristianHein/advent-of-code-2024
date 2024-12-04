using System.IO;
using AdventOfCode2024.Day04;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day04;

[TestFixture]
[TestOf(typeof(Day4))]
public class Day4Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day04/input");

    [Test]
    public void UseRealInput_Part1SolutionCorrect()
    {
        var day = new Day4(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("2493"));
    }

    [Test]
    public void UseRealInput_Part2SolutionCorrect()
    {
        var day = new Day4(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("1890"));
    }

    [Test]
    public void UseExampleInput_Part2SolutionCorrect()
    {
        var day = new Day4(
        [
            ".M.S......",
            "..A..MSMS.",
            ".M.S.MAA..",
            "..A.ASMSM.",
            ".M.S.M....",
            "..........",
            "S.S.S.S.S.",
            ".A.A.A.A..",
            "M.M.M.M.M.",
            ".........."
        ]);
        Assert.That(day.Part2Solution(), Is.EqualTo("9"));
    }
}
