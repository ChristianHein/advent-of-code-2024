using System.IO;
using AdventOfCode2024.Day01;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day01;

[TestFixture]
[TestOf(typeof(Day1))]
public class Day1Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day01/input");

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var day = new Day1(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("2769675"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var day = new Day1(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("24643097"));
    }
}
