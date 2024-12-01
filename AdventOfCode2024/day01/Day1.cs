using System.Diagnostics;

namespace advent_of_code_2024.day01;

public class Day1(string[] input) : Puzzle(input)
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

        leftColumn.Sort();
        rightColumn.Sort();

        var distancesSum = leftColumn
            .Select((left, i) => Math.Abs(left - rightColumn[i]))
            .Sum();

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
