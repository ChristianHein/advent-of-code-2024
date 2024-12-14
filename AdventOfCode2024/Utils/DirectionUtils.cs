namespace AdventOfCode2024.Utils;

public static class DirectionUtils
{
    // ReSharper disable InconsistentNaming
    // @formatter:off
    public static readonly (int, int) N =  (-1,  0);
    public static readonly (int, int) NE = (-1,  1);
    public static readonly (int, int) E =  ( 0,  1);
    public static readonly (int, int) SE = ( 1,  1);
    public static readonly (int, int) S =  ( 1,  0);
    public static readonly (int, int) SW = ( 1, -1);
    public static readonly (int, int) W =  ( 0, -1);
    public static readonly (int, int) NW = (-1, -1);
    // @formatter:on
    // ReSharper restore InconsistentNaming

    public static readonly List<(int, int)> CardinalDirections = [N, E, S, W];
    public static readonly List<(int, int)> OrdinalDirections = [NE, SE, SW, NW];
    public static readonly List<(int, int)> CardinalAndOrdinalDirections = [N, NE, E, SE, S, SW, W, NW];

    public static (int, int) Translate((int x, int y) vec1, (int dx, int dy) vec2)
    {
        return (vec1.x + vec2.dx, vec1.y + vec2.dy);
    }

    public static (int, int) Scale((int x, int y) vec, int scalar)
    {
        return (vec.x * scalar, vec.y * scalar);
    }

    public static (int, int) Difference((int x, int y) vec1, (int x, int y) vec2)
    {
        return (vec1.x - vec2.x, vec1.y - vec2.y);
    }
}
