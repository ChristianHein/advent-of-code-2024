using System.Diagnostics;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day15;

public class Puzzle(string[] input) : BasePuzzle(input)
{
    public override int Number => 15;

    private const char RobotSymbol = '@';
    private const char FloorSymbol = '.';
    private const char SmallBoxSymbol = 'O';
    private const char BigBoxLeftSymbol = '[';
    private const char BigBoxRightSymbol = ']';
    private const char WallSymbol = '#';

    private static (Grid2D<char>, (sbyte, sbyte)[]) ParseInput(string[] input)
    {
        var empty = 0;
        while (input[empty] != "")
        {
            empty++;
        }

        Debug.Assert(empty < input.Length);

        var grid = Grid2DCharFactory.Create(input[..empty]);
        var directions = string.Join(null, input[empty..])
            .Select(c =>
            {
                return c switch
                {
                    '^' => DirectionUtils.N,
                    '>' => DirectionUtils.E,
                    'v' => DirectionUtils.S,
                    '<' => DirectionUtils.W,
                    _ => throw new ArgumentException($"Unknown movement direction: {c}")
                };
            })
            .ToArray();

        return (grid, directions);
    }

    private static Grid2D<char> StretchWarehouseToDoubleWidth(Grid2D<char> grid)
    {
        var newGrid = new Grid2D<char>(grid.Width * 2, 0);
        foreach (var row in grid.GetRows())
        {
            var newRow = new List<char>(newGrid.Width);
            foreach (var symbol in row)
            {
                switch (symbol)
                {
                    case RobotSymbol: newRow.AddRange([RobotSymbol, FloorSymbol]); break;
                    case FloorSymbol: newRow.AddRange([FloorSymbol, FloorSymbol]); break;
                    case WallSymbol: newRow.AddRange([WallSymbol, WallSymbol]); break;
                    case SmallBoxSymbol: newRow.AddRange([BigBoxLeftSymbol, BigBoxRightSymbol]); break;
                }
            }

            newGrid.InsertRow(newGrid.Height, newRow);
        }

        return new Grid2D<char>(newGrid);
    }

    private static void ExecuteRobotMovements(Grid2D<char> grid, (int, int) robotStart, (sbyte, sbyte)[] moves)
    {
        Debug.Assert(moves.All(move => DirectionUtils.CardinalDirections.Contains(move)));

        var currPos = robotStart;
        foreach (var direction in moves)
        {
            var nextPos = DirectionUtils.Translate(currPos, direction);
            var nextPosSymbol = grid.GetCellValue(nextPos);

            switch (nextPosSymbol)
            {
                case WallSymbol:
                    continue;
                case FloorSymbol:
                    currPos = MoveRobot(direction);
                    continue;
                case SmallBoxSymbol or BigBoxLeftSymbol or BigBoxRightSymbol:
                {
                    var boxPartsToMove = new Stack<(int, int)>([nextPos]);

                    if (grid.GetCellValue(nextPos) == BigBoxLeftSymbol)
                    {
                        boxPartsToMove.Push(DirectionUtils.Translate(nextPos, DirectionUtils.E));
                    }
                    else if (grid.GetCellValue(nextPos) == BigBoxRightSymbol)
                    {
                        boxPartsToMove.Push(DirectionUtils.Translate(nextPos, DirectionUtils.W));
                    }

                    var isAnyBoxBlocked = false;
                    var uncheckedBoxParts = new Queue<(int, int)>(boxPartsToMove);
                    while (uncheckedBoxParts.Count > 0 && !isAnyBoxBlocked)
                    {
                        var currBoxPart = uncheckedBoxParts.Dequeue();
                        var nextBoxPart = DirectionUtils.Translate(currBoxPart, direction);
                        var nextBoxPartSymbol = grid.GetCellValue(nextBoxPart);

                        switch (nextBoxPartSymbol)
                        {
                            case WallSymbol:
                                isAnyBoxBlocked = true;
                                continue;
                            case FloorSymbol:
                                continue;
                            case BigBoxLeftSymbol or BigBoxRightSymbol or SmallBoxSymbol
                                when !boxPartsToMove.Contains(nextBoxPart):
                            {
                                boxPartsToMove.Push(nextBoxPart);
                                uncheckedBoxParts.Enqueue(nextBoxPart);

                                if (grid.GetCellValue(nextBoxPart) == BigBoxLeftSymbol)
                                {
                                    var nextBoxPartMirrorPart = DirectionUtils.Translate(nextBoxPart, DirectionUtils.E);

                                    boxPartsToMove.Push(nextBoxPartMirrorPart);
                                    uncheckedBoxParts.Enqueue(nextBoxPartMirrorPart);
                                }
                                else if (grid.GetCellValue(nextBoxPart) == BigBoxRightSymbol)
                                {
                                    var nextBoxPartMirrorPart = DirectionUtils.Translate(nextBoxPart, DirectionUtils.W);

                                    boxPartsToMove.Push(nextBoxPartMirrorPart);
                                    uncheckedBoxParts.Enqueue(nextBoxPartMirrorPart);
                                }

                                break;
                            }
                        }
                    }

                    if (isAnyBoxBlocked)
                    {
                        continue;
                    }

                    MoveBoxes(boxPartsToMove, direction);
                    currPos = MoveRobot(direction);

                    break;
                }
            }
        }

        return;

        void MoveBoxes(Stack<(int, int)> boxPartsToMove, (sbyte, sbyte) direction)
        {
            while (boxPartsToMove.TryPop(out var boxPart))
            {
                var boxPartNext = DirectionUtils.Translate(boxPart, direction);
                Debug.Assert(grid.GetCellValue(boxPartNext) == FloorSymbol);

                grid.SwapCells(boxPart, boxPartNext);
            }
        }

        (int, int) MoveRobot((int, int) direction)
        {
            var nextPos = DirectionUtils.Translate(currPos, direction);
            grid.SwapCells(currPos, nextPos);
            return nextPos;
        }
    }

    public override string Part1Solution()
    {
        var (grid, directions) = ParseInput(Input);

        var robotStart = grid
            .GetCoordinatesEnumerable()
            .First(coords => grid.GetCellValue(coords) == RobotSymbol);

        ExecuteRobotMovements(grid, robotStart, directions);

        var boxCoords = grid
            .GetCoordinatesEnumerable()
            .Where(coords => grid.GetCellValue(coords) == SmallBoxSymbol);

        return boxCoords.Sum(coords => coords.rowIndex * 100 + coords.columnIndex).ToString();
    }

    public override string Part2Solution()
    {
        var (grid, directions) = ParseInput(Input);
        grid = StretchWarehouseToDoubleWidth(grid);

        var robotStart = grid
            .GetCoordinatesEnumerable()
            .First(coords => grid.GetCellValue(coords) == RobotSymbol);

        ExecuteRobotMovements(grid, robotStart, directions);

        var boxLeftSideCoords = grid
            .GetCoordinatesEnumerable()
            .Where(coords => grid.GetCellValue(coords) == BigBoxLeftSymbol);

        return boxLeftSideCoords.Sum(coords => coords.rowIndex * 100 + coords.columnIndex).ToString();
    }
}
