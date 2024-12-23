using System.Diagnostics;
using AdventOfCode2024.Utils;
using static AdventOfCode2024.Utils.DirectionUtils;

namespace AdventOfCode2024.Day16;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 16;

    private static Grid2D<char> ParseInput(string[] input)
    {
        return Grid2DCharFactory.Create(input);
    }

    public override string Part1Solution()
    {
        var grid = ParseInput(Input);
        const int movementCost = 1;
        const int rotationCost = 1000;
        var initialDirection = E;

        var startPos = (-1, -1);
        var endPos = (-1, -1);

        var unvisited = new HashSet<((int, int) pos, (sbyte, sbyte) entryDir)>();

        foreach (var coords in grid.GetCoordinatesEnumerable())
        {
            switch (grid.GetCellValue(coords))
            {
                case 'S':
                    startPos = coords;
                    goto case '.';
                case 'E':
                    endPos = coords;
                    goto case '.';
                case '.':
                    unvisited.Add((coords, N));
                    unvisited.Add((coords, E));
                    unvisited.Add((coords, S));
                    unvisited.Add((coords, W));
                    break;
            }
        }

        Debug.Assert(grid.AreCoordsValid(startPos) && grid.AreCoordsValid(endPos));

        var costs = unvisited.ToDictionary(key => key, _ => int.MaxValue);
        costs[(startPos, initialDirection)] = 0;

        int currentCost;
        while (unvisited.Count != 0)
        {
            currentCost = unvisited.Select(key => costs[key]).Min();
            if (currentCost == int.MaxValue)
            {
                break;
            }

            var key = unvisited.First(key => costs[key] == currentCost);
            unvisited.Remove(key);

            ((int row, int col) currentPos, (sbyte row, sbyte col) currentDir) = key;

            foreach (var nextDir in CardinalDirections)
            {
                var nextPos = Translate(currentPos, nextDir);
                if (unvisited.Contains((nextPos, nextDir)))
                {
                    var rotations = TimesToRotateFromTo(currentDir, nextDir);
                    var nextCost = currentCost + movementCost + rotationCost * rotations;
                    costs[(nextPos, nextDir)] = Math.Min(costs[(nextPos, nextDir)], nextCost);
                }
            }
        }

        //var gridCosts = new Grid2D<string>(grid.Width, grid.Height, "");
        //foreach (var coords in grid.GetCoordinatesEnumerable())
        //{
        //    if (grid.GetCellValue(coords) == 'S')
        //    {
        //        gridCosts.SetCellValue(coords, "S");
        //    }
        //    else if (grid.GetCellValue(coords) == 'E')
        //    {
        //        gridCosts.SetCellValue(coords, "E");
        //    }
        //    else if (grid.GetCellValue(coords) == '#')
        //    {
        //        gridCosts.SetCellValue(coords, "#");
        //    }
        //    else
        //    {
        //        var costStr = costs[(coords, N)] + "," +
        //                      costs[(coords, E)] + "," +
        //                      costs[(coords, S)] + "," +
        //                      costs[(coords, W)];
        //        gridCosts.SetCellValue(coords, costStr);
        //    }
        //}

        //Console.WriteLine(gridCosts.ToString(str => str, "\t"));

        var endCosts = new List<int>(4);

        foreach (var cardinalDirection in CardinalDirections)
        {
            if (costs.TryGetValue((endPos, cardinalDirection), out var endCost))
            {
                endCosts.Add(endCost);
            }
        }

        return endCosts.Min().ToString();
    }

    private static int TimesToRotateFromTo((sbyte, sbyte) fromDir, (sbyte, sbyte) toDir)
    {
        Debug.Assert(CardinalDirections.Contains(fromDir) && CardinalDirections.Contains(toDir));

        if (fromDir == toDir) return 0;

        if (fromDir == N)
        {
            if (toDir == E) return 1;
            if (toDir == S) return 2;
            if (toDir == W) return 1;
        }
        else if (fromDir == E)
        {
            if (toDir == N) return 1;
            if (toDir == S) return 1;
            if (toDir == W) return 2;
        }
        else if (fromDir == S)
        {
            if (toDir == N) return 2;
            if (toDir == E) return 1;
            if (toDir == W) return 1;
        }
        else if (fromDir == W)
        {
            if (toDir == N) return 1;
            if (toDir == E) return 2;
            if (toDir == S) return 1;
        }

        throw new UnreachableException();
    }

    public override string Part2Solution()
    {
        return "TODO";
    }
}
