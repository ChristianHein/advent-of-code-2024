namespace AdventOfCode2024;

public abstract class Puzzle(string[] input)
{
    protected readonly string[] Input = input;

    public abstract int Number { get; }
    public abstract string Part1Solution();
    public abstract string Part2Solution();
}
