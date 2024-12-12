using System.Diagnostics;

namespace AdventOfCode2024.Day01;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 1;

    private static (List<int>, List<int>) ParseInput(string[] input)
    {
        var leftColumn = new List<int>(input.Length);
        var rightColumn = new List<int>(input.Length);
        foreach (var row in input)
        {
            var split = row.Split(null, 2);
            Debug.Assert(split.Length == 2);

            leftColumn.Add(int.Parse(split[0]));
            rightColumn.Add(int.Parse(split[1]));
        }

        return (leftColumn, rightColumn);
    }

    public override string Part1Solution()
    {
        var (leftColumn, rightColumn) = ParseInput(Input);
        Debug.Assert(leftColumn.Count == rightColumn.Count);

        leftColumn.Sort();
        rightColumn.Sort();

        var distancesSum = leftColumn.Zip(rightColumn, (l, r) => Math.Abs(l - r)).Sum();

        return distancesSum.ToString();
    }

    public override string Part2Solution()
    {
        var (leftColumn, rightColumn) = ParseInput(Input);

        var rightOccurrencesDict = rightColumn
            .GroupBy(row => row)
            .ToDictionary(group => group.Key, group => group.Count());

        var similarityScore = leftColumn.Sum(left => left * rightOccurrencesDict.GetValueOrDefault(left, 0));

        return similarityScore.ToString();
    }
}
