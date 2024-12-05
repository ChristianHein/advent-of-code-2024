using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day04;

public class Day4(string[] input) : Puzzle(input)
{
    public override int Number => 4;

    public override string Part1Solution()
    {
        const string searchString = "XMAS";

        var grid = new Grid2DChar(Input);

        var totalCount = 0;
        foreach (var coords in grid.GetCoordinatesEnumerable())
        {
            if (grid.GetCellValue(coords) != 'X')
                continue;

            // @formatter:off
            // ReSharper disable InconsistentNaming
            var N =  (-1,  0);
            var NE = (-1,  1);
            var E =  ( 0,  1);
            var SE = ( 1,  1);
            var S =  ( 1,  0);
            var SW = ( 1, -1);
            var W =  ( 0, -1);
            var NW = (-1, -1);
            // ReSharper restore InconsistentNaming

            if (MatchesInDirection(N)) { totalCount++; }
            if (MatchesInDirection(NE)) { totalCount++; }
            if (MatchesInDirection(E)) { totalCount++; }
            if (MatchesInDirection(SE)) { totalCount++; }
            if (MatchesInDirection(S)) { totalCount++; }
            if (MatchesInDirection(SW)) { totalCount++; }
            if (MatchesInDirection(W)) { totalCount++; }
            if (MatchesInDirection(NW)) { totalCount++; }
            // @formatter:on

            continue;

            bool MatchesInDirection((int, int) direction) =>
                Enumerable.Range(0, searchString.Length)
                    .All(i => grid.AreCoordsValid(Translate(coords, Scale(direction, i))) &&
                              grid.GetCellValue(Translate(coords, Scale(direction, i))) == searchString[i]);
        }

        return totalCount.ToString();
    }

    public override string Part2Solution()
    {
        var totalCount = 0;
        var grid = new Grid2DChar(Input);

        foreach (var (rowIndex, columnIndex) in grid.GetCoordinatesEnumerable())
        {
            if (grid.GetCellValue((columnIndex, rowIndex)) != 'A')
                continue;

            var inGridRangeAndValueEquals = (int rowI, int colI, char cellValue) =>
                rowI >= 0 && rowI < grid.Height && colI >= 0 && colI < grid.Width
                && grid.GetCellValue((colI, rowI)) == cellValue;

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

        return totalCount.ToString();
    }

    private static (int, int) Translate((int x, int y) vec1, (int dx, int dy) vec2)
    {
        return (vec1.x + vec2.dx, vec1.y + vec2.dy);
    }

    private static (int, int) Scale((int x, int y) vec, int scalar)
    {
        return (vec.x * scalar, vec.y * scalar);
    }
}
