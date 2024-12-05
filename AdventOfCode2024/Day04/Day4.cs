using AdventOfCode2024.Utils;

namespace AdventOfCode2024.Day04;

public class Day4(string[] input) : Puzzle(input)
{
    public override int Number => 4;

    // ReSharper disable InconsistentNaming
    // @formatter:off
    private readonly (int, int) N =  (-1,  0);
    private readonly (int, int) NE = (-1,  1);
    private readonly (int, int) E =  ( 0,  1);
    private readonly (int, int) SE = ( 1,  1);
    private readonly (int, int) S =  ( 1,  0);
    private readonly (int, int) SW = ( 1, -1);
    private readonly (int, int) W =  ( 0, -1);
    private readonly (int, int) NW = (-1, -1);
    // @formatter:on
    // ReSharper restore InconsistentNaming

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
            if (MatchesInDirection(N))  { totalCount++; }
            if (MatchesInDirection(NE)) { totalCount++; }
            if (MatchesInDirection(E))  { totalCount++; }
            if (MatchesInDirection(SE)) { totalCount++; }
            if (MatchesInDirection(S))  { totalCount++; }
            if (MatchesInDirection(SW)) { totalCount++; }
            if (MatchesInDirection(W))  { totalCount++; }
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

        foreach (var coords in grid.GetCoordinatesEnumerable())
        {
            if (grid.GetCellValue(coords) != 'A')
                continue;

            if ((MatchesInDirection(NW, 'M') && MatchesInDirection(SE, 'S')
                 || MatchesInDirection(NW, 'S') && MatchesInDirection(SE, 'M'))
                && (MatchesInDirection(SW, 'M') && MatchesInDirection(NE, 'S')
                    || MatchesInDirection(SW, 'S') && MatchesInDirection(NE, 'M')))
            {
                totalCount++;
            }

            continue;

            bool MatchesInDirection((int, int) direction, char c) =>
                grid.AreCoordsValid(Translate(coords, direction)) &&
                grid.GetCellValue(Translate(coords, direction)) == c;
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
