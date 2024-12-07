using AdventOfCode2024.Utils;
using static AdventOfCode2024.Utils.DirectionUtils;

namespace AdventOfCode2024.Day06;

public class Day6(string[] input) : Puzzle(input)
{
    public override int Number => 6;

    public override string Part1Solution()
    {
        var grid = new Grid2DChar(Input);

        var pos = FindGuard(grid);
        while (true)
        {
            var guard = grid.GetCellValue(pos);

            var nextPos = Translate(pos, CharToDirection(guard));

            if (!grid.AreCoordsValid(nextPos))
            {
                grid.SetCellValue(pos, 'X');
                break;
            }

            if (grid.GetCellValue(nextPos) != '#')
            {
                grid.SetCellValue(nextPos, guard);
                grid.SetCellValue(pos, 'X');
                pos = nextPos;
            }
            else if (grid.GetCellValue(nextPos) == '#')
            {
                grid.SetCellValue(pos, RotateClockwise(guard));
            }
        }

        return grid.GetCoordinatesEnumerable()
            .Sum(coords => grid.GetCellValue(coords) == 'X' ? 1 : 0)
            .ToString();
    }

    public override string Part2Solution()
    {
        var originalGrid = new Grid2DChar(Input);
        var guardStartPosition = FindGuard(originalGrid);

        // Brute force: Copy grid, insert new obstacle, check if guard loops
        var obstaclesThatCreateGuardLoop = 0;
        foreach (var coords in originalGrid.GetCoordinatesEnumerable())
        {
            var symbol = originalGrid.GetCellValue(coords);
            if (symbol == '#' || "<>^v".Contains(symbol))
                continue;

            var grid = new Grid2DChar(originalGrid);
            grid.SetCellValue(coords, '#');

            if (DoesGuardLoop(grid, guardStartPosition))
            {
                obstaclesThatCreateGuardLoop++;
            }
        }

        return obstaclesThatCreateGuardLoop.ToString();
    }

    private static (int, int) FindGuard(Grid2DChar grid)
    {
        foreach (var coords in grid.GetCoordinatesEnumerable())
        {
            if ("<>^v".Contains(grid.GetCellValue(coords)))
            {
                return coords;
            }
        }

        throw new ArgumentException("No guard found");
    }

    private static bool DoesGuardLoop(Grid2DChar grid, (int, int) guardStartPosition)
    {
        var pos = guardStartPosition;
        var guard = grid.GetCellValue(pos);
        Dictionary<(int, int), List<char>> visited = [];
        while (true)
        {
            if (visited.TryGetValue(pos, out var directions))
            {
                directions.Add(guard);
            }
            else
            {
                visited.Add(pos, [guard]);
            }

            var nextPos = Translate(pos, CharToDirection(guard));

            if (!grid.AreCoordsValid(nextPos))
            {
                return false;
            }

            if (grid.GetCellValue(nextPos) == 'X')
            {
                if (visited.TryGetValue(nextPos, out var dirs) && dirs.Contains(guard))
                {
                    return true;
                }
            }

            if (grid.GetCellValue(nextPos) == '#')
            {
                guard = RotateClockwise(guard);
                grid.SetCellValue(pos, guard);
            }
            else
            {
                grid.SetCellValue(nextPos, guard);
                grid.SetCellValue(pos, 'X');
                pos = nextPos;
            }
        }
    }
    private static (int, int) CharToDirection(char c)
    {
        return c switch
        {
            '^' => N,
            '>' => E,
            'v' => S,
            '<' => W,
            _ => throw new ArgumentException($"Unknown direction '{c}'")
        };
    }

    private static char RotateClockwise(char guardDirection)
    {
        return guardDirection switch
        {
            '<' => '^',
            '^' => '>',
            '>' => 'v',
            'v' => '<',
            _ => throw new ArgumentException($"Unknown direction '{guardDirection}'")
        };
    }
}
