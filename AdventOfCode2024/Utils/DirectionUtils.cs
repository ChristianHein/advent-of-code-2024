using System.Numerics;

namespace AdventOfCode2024.Utils;

public static class DirectionUtils
{
    // ReSharper disable InconsistentNaming
    // @formatter:off
    public static readonly (sbyte, sbyte) N =  (-1,  0);
    public static readonly (sbyte, sbyte) NE = (-1,  1);
    public static readonly (sbyte, sbyte) E =  ( 0,  1);
    public static readonly (sbyte, sbyte) SE = ( 1,  1);
    public static readonly (sbyte, sbyte) S =  ( 1,  0);
    public static readonly (sbyte, sbyte) SW = ( 1, -1);
    public static readonly (sbyte, sbyte) W =  ( 0, -1);
    public static readonly (sbyte, sbyte) NW = (-1, -1);
    // @formatter:on
    // ReSharper restore InconsistentNaming

    public static readonly List<(sbyte, sbyte)> CardinalDirections = [N, E, S, W];
    public static readonly List<(sbyte, sbyte)> OrdinalDirections = [NE, SE, SW, NW];
    public static readonly List<(int, int)> CardinalAndOrdinalDirections = [N, NE, E, SE, S, SW, W, NW];

    public static bool IsVertical<T>((T, T) direction)
    {
        return direction.Equals(N) || direction.Equals(S);
    }

    public static bool IsHorizontal<T>((T, T) direction)
    {
        return direction.Equals(E) || direction.Equals(W);
    }

    public static (T, T) Translate<T>((T x, T y) vec1, (T dx, T dy) vec2)
        where T : IAdditionOperators<T, T, T>
    {
        return (vec1.x + vec2.dx, vec1.y + vec2.dy);
    }

    public static (T, T) Scale<T>((T x, T y) vec, T scalar)
        where T : IMultiplyOperators<T, T, T>
    {
        return (vec.x * scalar, vec.y * scalar);
    }

    public static (T, T) Difference<T>((T x, T y) vec1, (T x, T y) vec2)
        where T : ISubtractionOperators<T, T, T>
    {
        return (vec1.x - vec2.x, vec1.y - vec2.y);
    }
}
