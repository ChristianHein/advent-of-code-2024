using System.Diagnostics;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day10;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 10;

    private static readonly int MinTrailHeight = 0;
    private static readonly int MaxTrailHeight = 9;

    private static Grid2D<byte> ParseInput(string[] input)
    {
        Debug.Assert(input is { Length: > 0 });

        var grid = new Grid2D<byte>(input[0].Length, 0);
        foreach (var line in input)
        {
            grid.InsertRow(grid.Height, line.Select(c => (byte)(c - '0')).ToList());
        }

        return grid;
    }

    private static int CalculateTrailHeadScore(Grid2D<byte> grid, (int, int) trailHeadCoords)
    {
        Debug.Assert(grid.AreCoordsValid(trailHeadCoords) && grid.GetCellValue(trailHeadCoords) == MinTrailHeight);
        return CountTrailsAndTrailEnds(grid, trailHeadCoords).trailEnds;
    }

    private static int CalculateTrailHeadRating(Grid2D<byte> grid, (int, int) trailHeadCoords)
    {
        Debug.Assert(grid.AreCoordsValid(trailHeadCoords) && grid.GetCellValue(trailHeadCoords) == MinTrailHeight);
        return CountTrailsAndTrailEnds(grid, trailHeadCoords).trailCount;
    }

    private static (int trailCount, int trailEnds) CountTrailsAndTrailEnds(Grid2D<byte> grid, (int, int) origin)
    {
        var nextCoords = new Stack<(int, int)>();
        nextCoords.Push(origin);
        var visitedCoords = new HashSet<(int, int)>();

        var trailCount = 0;
        var trailEnds = 0;

        while (nextCoords.Count != 0)
        {
            var currentCoords = nextCoords.Pop();
            var currentHeight = grid.GetCellValue(currentCoords);

            if (currentHeight == MaxTrailHeight)
            {
                trailCount++;
                if (visitedCoords.Add(currentCoords))
                {
                    trailEnds++;
                }
            }

            var nextHighestNeighbors = grid
                .GetValidNeighborsCoords(currentCoords, DirectionUtils.CardinalDirections)
                .Where(neighbor => grid.GetCellValue(neighbor) == currentHeight + 1);

            foreach (var neighbor in nextHighestNeighbors)
            {
                nextCoords.Push(neighbor);
            }
        }

        return (trailCount, trailEnds);
    }

    public override string Part1Solution()
    {
        var grid = ParseInput(Input);

        var trailheadScoresSum = grid
            .GetCoordinatesEnumerable()
            .Where(pos => grid.GetCellValue(pos) == MinTrailHeight)
            .Sum(pos => CalculateTrailHeadScore(grid, pos));

        return trailheadScoresSum.ToString();
    }

    public override string Part2Solution()
    {
        var grid = ParseInput(Input);

        var trailheadRatingsSum = grid
            .GetCoordinatesEnumerable()
            .Where(pos => grid.GetCellValue(pos) == MinTrailHeight)
            .Sum(pos => CalculateTrailHeadRating(grid, pos));

        return trailheadRatingsSum.ToString();
    }
}
