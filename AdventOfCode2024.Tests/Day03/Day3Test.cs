using System.IO;
using AdventOfCode2024.Day03;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day03;

[TestFixture]
[TestOf(typeof(Day3))]
public class Day3Test
{
    private readonly string[] _realInput = File.ReadAllLines("Day03/input");

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var day = new Day3(_realInput);
        Assert.That(day.Part1Solution(), Is.EqualTo("165225049"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var day = new Day3(_realInput);
        Assert.That(day.Part2Solution(), Is.EqualTo("108830766"));
    }
}
