using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AdventOfCode2024.Utils;

public class Grid2DCharFactory : Grid2D<char>
{
    public static Grid2D<char> Create(string[] matrix)
    {
        return new Grid2DCharFactory(matrix);
    }

    private Grid2DCharFactory(string[] matrix)
    {
        Width = matrix.Length == 0 ? 0 : matrix[0].Length;
        Height = matrix.Length;
        FlatGrid = string.Join("", matrix).ToCharArray().ToList();
    }
}

public class Grid2D<T> : IEquatable<Grid2D<T>>
{
    protected List<T> FlatGrid;

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    public Grid2D()
    {
        Width = 0;
        Height = 0;
        FlatGrid = [];
    }

    public Grid2D(Grid2D<T> other)
    {
        Width = other.Width;
        Height = other.Height;
        FlatGrid = [..other.FlatGrid];
    }

    public Grid2D(int width, int height)
    {
        Width = width;
        Height = height;
        FlatGrid = new List<T>(width * height);
    }

    public Grid2D(int width, int height, T fillValue)
    {
        Width = width;
        Height = height;
        FlatGrid = Enumerable.Repeat(fillValue, width * height).ToList();
    }

    public IEnumerable<(int rowIndex, int columnIndex)> GetCoordinatesEnumerable()
    {
        var rowIndexes = Enumerable.Range(0, Height);
        var columnIndexes = Enumerable.Range(0, Width);
        return CollectionUtils.CartesianProduct(rowIndexes, columnIndexes);
    }

    public bool AreCoordsValid((int row, int column) coords)
    {
        return coords.row >= 0 && coords.row < Height && coords.column >= 0 && coords.column < Width;
    }

    public T GetCellValue((int row, int column) coords)
    {
        if (coords.row < 0 || coords.row >= Height)
            throw new IndexOutOfRangeException("Row index is outside grid: " + coords.row);
        if (coords.column < 0 || coords.column >= Width)
            throw new IndexOutOfRangeException("Column index is outside of grid: " + coords.column);

        return FlatGrid[coords.row * Width + coords.column];
    }

    public List<T> GetRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= Height)
            throw new IndexOutOfRangeException();

        return FlatGrid.GetRange(rowIndex * Width, Width);
    }

    public List<List<T>> GetRows()
    {
        return Enumerable.Range(0, Height)
            .Select(GetRow)
            .ToList();
    }

    public List<T> GetColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= Width)
            throw new IndexOutOfRangeException();

        var result = new List<T>(Height);
        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            result.Add(FlatGrid[rowIndex * Width + columnIndex]);
        }

        return result;
    }

    public List<List<T>> GetColumns()
    {
        return Enumerable.Range(0, Width)
            .Select(GetColumn)
            .ToList();
    }

    public List<(int rowIndex, int columnIndex)> GetValidNeighborsCoords((int row, int column) origin,
        List<(sbyte, sbyte)> directions)
    {
        return directions
            .Select(dir => DirectionUtils.Translate(origin, dir))
            .Where(AreCoordsValid).ToList();
    }

    public void InsertRow(int rowIndex, List<T> row)
    {
        if (rowIndex < 0 || rowIndex > Height)
            throw new IndexOutOfRangeException();
        if (row.Count != Width)
            throw new ArgumentException(
                $"Inserted row must be same width as grid. Row size: {row.Count}, grid width: {Width}");

        FlatGrid.InsertRange(rowIndex * Width, row);
        Height++;
    }

    public void InsertColumn(int columnIndex, List<T> column)
    {
        if (columnIndex < 0 || columnIndex > Width)
            throw new IndexOutOfRangeException();
        if (column.Count != Width)
            throw new ArgumentException(
                $"Inserted column must be same height as grid. Column size: {column.Count}, grid height: {Height}");

        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            FlatGrid.Insert(rowIndex * Width + columnIndex, column[rowIndex]);
        }

        Width++;
    }

    public void RemoveRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= Height)
            throw new IndexOutOfRangeException();

        FlatGrid.RemoveRange(rowIndex * Width, Width);
        Height--;
    }

    public void RemoveColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= Width)
            throw new IndexOutOfRangeException();

        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            FlatGrid.RemoveAt(rowIndex * Width + columnIndex);
        }

        Width--;
    }

    public void Transpose()
    {
        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < Width; columnIndex++)
            {
                var a = GetCellValue((rowIndex, columnIndex));
                var b = GetCellValue((columnIndex, rowIndex));
                SetCellValue((rowIndex, columnIndex), b);
                SetCellValue((rowIndex, columnIndex), a);
            }
        }
    }

    public void SetCellValue((int row, int column) coords, T cellValue)
    {
        if (coords.row < 0 || coords.row >= Height || coords.column < 0 || coords.column >= Width)
        {
            throw new IndexOutOfRangeException();
        }

        FlatGrid[coords.row * Width + coords.column] = cellValue;
    }

    public void SwapCells((int row, int column) cellA, (int row, int column) cellB)
    {
        if (!AreCoordsValid(cellA) || !AreCoordsValid(cellB))
        {
            throw new IndexOutOfRangeException();
        }

        (FlatGrid[cellA.row * Width + cellA.column], FlatGrid[cellB.row * Width + cellB.column]) =
            (FlatGrid[cellB.row * Width + cellB.column], FlatGrid[cellA.row * Width + cellA.column]);
    }

    public bool Equals(Grid2D<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return FlatGrid.SequenceEqual(other.FlatGrid)
               && Width == other.Width
               && Height == other.Height;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType()
               && Equals((Grid2D<T>)obj);
    }

    public string ToString(Func<T, string> toStringFunction, string? separator = " ")
    {
        if (Width == 0 || Height == 0)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        for (var row = 0; row < Height; row++)
        {
            for (var column = 0; column < Width; column++)
            {
                sb.Append(toStringFunction(FlatGrid[row * Width + column]));
                if (column != Width - 1)
                {
                    sb.Append(separator);
                }
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        // Don't know how better to define hash code without setting fields to readonly
        return HashCode.Combine(FlatGrid, Width, Height);
    }

    public static bool operator ==(Grid2D<T>? left, Grid2D<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Grid2D<T>? left, Grid2D<T>? right)
    {
        return !Equals(left, right);
    }
}
