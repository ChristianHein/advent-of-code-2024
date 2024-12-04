using System.Text.RegularExpressions;
using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day04;

public class Day4(string[] input) : Puzzle(input)
{
    public override int Number => 4;

    public override string Part1Solution()
    {
        var totalCount = 0;
        var grid = Grid2D<char>.CreateGrid2D(Input); // TODO: Generic type necessary?

        for (var rowIndex = 0; rowIndex < grid.Height; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < grid.Width; columnIndex++)
            {
                if (grid.GetCellValue(columnIndex, rowIndex) != 'X')
                    continue;

                var inGridRangeAndValueEquals = (int rowI, int colI, char cellValue) =>
                    rowI >= 0 && rowI < grid.Height && colI >= 0 && colI < grid.Width
                    && grid.GetCellValue(colI, rowI) == cellValue;

                // NE diagonal
                if (inGridRangeAndValueEquals(rowIndex - 1, columnIndex + 1, 'M') &&
                    inGridRangeAndValueEquals(rowIndex - 2, columnIndex + 2, 'A') &&
                    inGridRangeAndValueEquals(rowIndex - 3, columnIndex + 3, 'S'))
                {
                    totalCount++;
                }

                // SE diagonal
                if (inGridRangeAndValueEquals(rowIndex + 1, columnIndex + 1, 'M') &&
                    inGridRangeAndValueEquals(rowIndex + 2, columnIndex + 2, 'A') &&
                    inGridRangeAndValueEquals(rowIndex + 3, columnIndex + 3, 'S'))
                {
                    totalCount++;
                }

                // SW diagonal
                if (inGridRangeAndValueEquals(rowIndex + 1, columnIndex - 1, 'M') &&
                    inGridRangeAndValueEquals(rowIndex + 2, columnIndex - 2, 'A') &&
                    inGridRangeAndValueEquals(rowIndex + 3, columnIndex - 3, 'S'))
                {
                    totalCount++;
                }

                // NW diagonal
                if (inGridRangeAndValueEquals(rowIndex - 1, columnIndex - 1, 'M') &&
                    inGridRangeAndValueEquals(rowIndex - 2, columnIndex - 2, 'A') &&
                    inGridRangeAndValueEquals(rowIndex - 3, columnIndex - 3, 'S'))
                {
                    totalCount++;
                }

                // N vertical
                if (inGridRangeAndValueEquals(rowIndex - 1, columnIndex, 'M') &&
                    inGridRangeAndValueEquals(rowIndex - 2, columnIndex, 'A') &&
                    inGridRangeAndValueEquals(rowIndex - 3, columnIndex, 'S'))
                {
                    totalCount++;
                }

                // S vertical
                if (inGridRangeAndValueEquals(rowIndex + 1, columnIndex, 'M') &&
                    inGridRangeAndValueEquals(rowIndex + 2, columnIndex, 'A') &&
                    inGridRangeAndValueEquals(rowIndex + 3, columnIndex, 'S'))
                {
                    totalCount++;
                }

                // E horizontal
                if (inGridRangeAndValueEquals(rowIndex, columnIndex + 1, 'M') &&
                    inGridRangeAndValueEquals(rowIndex, columnIndex + 2, 'A') &&
                    inGridRangeAndValueEquals(rowIndex, columnIndex + 3, 'S'))
                {
                    totalCount++;
                }

                // W horizontal
                if (inGridRangeAndValueEquals(rowIndex, columnIndex - 1, 'M') &&
                    inGridRangeAndValueEquals(rowIndex, columnIndex - 2, 'A') &&
                    inGridRangeAndValueEquals(rowIndex, columnIndex - 3, 'S'))
                {
                    totalCount++;
                }
            }
        }

        return totalCount.ToString();
    }

    public override string Part2Solution()
    {
        var totalCount = 0;
        var grid = Grid2D<char>.CreateGrid2D(Input); // TODO: Generic type necessary?

        for (var rowIndex = 1; rowIndex < grid.Height - 1; rowIndex++)
        {
            for (var columnIndex = 1; columnIndex < grid.Width - 1; columnIndex++)
            {
                if (grid.GetCellValue(columnIndex, rowIndex) != 'A')
                    continue;

                var inGridRangeAndValueEquals = (int rowI, int colI, char cellValue) =>
                    rowI >= 0 && rowI < grid.Height && colI >= 0 && colI < grid.Width
                    && grid.GetCellValue(colI, rowI) == cellValue;

                if ((inGridRangeAndValueEquals(rowIndex + 1, columnIndex - 1, 'M') &&
                     inGridRangeAndValueEquals(rowIndex - 1, columnIndex + 1, 'S') ||
                     inGridRangeAndValueEquals(rowIndex + 1, columnIndex - 1, 'S') &&
                     inGridRangeAndValueEquals(rowIndex - 1, columnIndex + 1, 'M')) && (
                        inGridRangeAndValueEquals(rowIndex - 1, columnIndex - 1, 'M') &&
                        inGridRangeAndValueEquals(rowIndex + 1, columnIndex + 1, 'S') ||
                        inGridRangeAndValueEquals(rowIndex - 1, columnIndex - 1, 'S') &&
                        inGridRangeAndValueEquals(rowIndex + 1, columnIndex + 1, 'M')))
                {
                    totalCount++;
                }
            }
        }

        return totalCount.ToString();
    }
}
