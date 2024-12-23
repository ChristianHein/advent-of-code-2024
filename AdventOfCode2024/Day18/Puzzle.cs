using System.Diagnostics;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day18;

public class Puzzle(int width, int height, int part1CorruptedBytes, string[] input) : BasePuzzle(input)
{
    public override int Number => 18;

    private static List<(int row, int col)> ParseInput(string[] input)
    {
        var positions = new List<(int row, int col)>();
        foreach (var line in input)
        {
            var col = int.Parse(line.Split(',', 2)[0]);
            var row = int.Parse(line.Split(',', 2)[1]);
            positions.Add((row, col));
        }

        return positions;
    }

    public override string Part1Solution()
    {
        Debug.Assert(width != 0 && height != 0);
        var bytePositions = ParseInput(Input);

        var startPosition = (0, 0);
        var endPosition = (height - 1, width - 1);

        var grid = new Grid2D<char>(width, height, '.');
        for (var positionIdx = 0; positionIdx < part1CorruptedBytes; positionIdx++)
        {
            var bytePosition = bytePositions[positionIdx];
            grid.SetCellValue(bytePosition, '#');
        }

        return StepCountToEnd(grid, startPosition, endPosition).ToString() ?? throw new InvalidOperationException();
    }

    public override string Part2Solution()
    {
        Debug.Assert(width != 0 && height != 0);
        var bytePositions = ParseInput(Input);

        var startPosition = (0, 0);
        var endPosition = (height - 1, width - 1);

        var grid = new Grid2D<char>(width, height, '.');
        var loopIdx = 0;
        foreach (var bytePosition in bytePositions)
        {
            grid.SetCellValue(bytePosition, '#');
            if (!StepCountToEnd(grid, startPosition, endPosition).HasValue)
            {
                return $"{bytePosition.col},{bytePosition.row}";
            }

            if (loopIdx % 100 == 0)
            {
                Console.WriteLine($"{DateTime.Now}: {loopIdx}");
            }

            loopIdx++;
        }

        throw new InvalidOperationException();
    }

    private static int? StepCountToEnd(Grid2D<char> grid, (int, int) startPosition, (int, int) endPosition)
    {
        var unvisited = new HashSet<(int row, int col)>(
            grid.GetCoordinatesEnumerable()
                .Where(coords => grid.GetCellValue(coords) != '#'));

        var stepCounts = unvisited.ToDictionary(key => key, _ => int.MaxValue);
        stepCounts[startPosition] = 0;

        while (unvisited.Count > 0)
        {
            var currentStepCount = unvisited.Select(pos => stepCounts[pos]).Min();
            if (currentStepCount == int.MaxValue)
            {
                break;
            }

            var current = unvisited.First(key => stepCounts[key] == currentStepCount);
            unvisited.Remove(current);

            var neighbors = grid
                .GetValidNeighborsCoords(current, DirectionUtils.CardinalDirections)
                .Where(coords => grid.GetCellValue(coords) != '#')
                .ToList();

            foreach (var neighbor in neighbors)
            {
                var steps = currentStepCount + 1;
                if (stepCounts.TryGetValue(neighbor, out var prevSteps))
                {
                    stepCounts[neighbor] = Math.Min(prevSteps, steps);
                }
                else
                {
                    stepCounts.Add(neighbor, steps);
                }
            }
        }

        var resultStepCount = stepCounts[endPosition];
        if (resultStepCount == int.MaxValue)
        {
            return null;
        }

        return resultStepCount;
    }
}
