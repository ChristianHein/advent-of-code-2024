﻿using System.IO;
using AdventOfCode2024.Day24;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Day24;

[TestFixture]
[TestOf(typeof(Puzzle))]
public class PuzzleTest
{
    private readonly string[] _realInput = File.ReadAllLines("Day24/input");

    private readonly string[] _exampleInput1 =
    [
        "x00: 1",
        "x01: 1",
        "x02: 1",
        "y00: 0",
        "y01: 1",
        "y02: 0",
        "",
        "x00 AND y00 -> z00",
        "x01 XOR y01 -> z01",
        "x02 OR y02 -> z02"
    ];

    private readonly string[] _exampleInput2 =
    [
        "x00: 1",
        "x01: 0",
        "x02: 1",
        "x03: 1",
        "x04: 0",
        "y00: 1",
        "y01: 1",
        "y02: 1",
        "y03: 1",
        "y04: 1",
        "",
        "ntg XOR fgs -> mjb",
        "y02 OR x01 -> tnw",
        "kwq OR kpj -> z05",
        "x00 OR x03 -> fst",
        "tgd XOR rvg -> z01",
        "vdt OR tnw -> bfw",
        "bfw AND frj -> z10",
        "ffh OR nrd -> bqk",
        "y00 AND y03 -> djm",
        "y03 OR y00 -> psh",
        "bqk OR frj -> z08",
        "tnw OR fst -> frj",
        "gnj AND tgd -> z11",
        "bfw XOR mjb -> z00",
        "x03 OR x00 -> vdt",
        "gnj AND wpb -> z02",
        "x04 AND y00 -> kjc",
        "djm OR pbm -> qhw",
        "nrd AND vdt -> hwm",
        "kjc AND fst -> rvg",
        "y04 OR y02 -> fgs",
        "y01 AND x02 -> pbm",
        "ntg OR kjc -> kwq",
        "psh XOR fgs -> tgd",
        "qhw XOR tgd -> z09",
        "pbm OR djm -> kpj",
        "x03 XOR y03 -> ffh",
        "x00 XOR y04 -> ntg",
        "bfw OR bqk -> z06",
        "nrd XOR fgs -> wpb",
        "frj XOR qhw -> z04",
        "bqk OR frj -> z07",
        "y03 OR x01 -> nrd",
        "hwm AND bqk -> z03",
        "tgd XOR rvg -> z12",
        "tnw OR pbm -> gnj"
    ];

    [Test]
    public void UseExampleInput1_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput1);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("4"));
    }

    [Test]
    public void UseExampleInput2_Part1SolutionCorrect()
    {
        var puzzle = new Puzzle(_exampleInput2);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("2024"));
    }

    [Test]
    public void UseRealInput_Part1Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part1Solution(), Is.EqualTo("50411513338638"));
    }

    [Test]
    public void UseRealInput_Part2Solution_IsCorrect()
    {
        var puzzle = new Puzzle(_realInput);
        Assert.That(puzzle.Part2Solution(), Is.EqualTo("-1"));
    }
}
