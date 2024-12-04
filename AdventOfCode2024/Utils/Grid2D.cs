namespace AdventOfCode2024.Utils;

public class Grid2D<T> : IEquatable<Grid2D<T>>
{
    private List<T?> _flatGrid;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Grid2D()
    {
        Width = 0;
        Height = 0;
        _flatGrid = [];
    }

    public static Grid2D<char> CreateGrid2D(string[] matrix)
    {
        return new Grid2D<char>
        {
            Width = matrix.Length != 0 ? matrix[0].Length : 0,
            Height = matrix.Length,
            _flatGrid = string.Join("", matrix).ToCharArray().ToList(),
        };
    }

    public Grid2D(int width, int height)
    {
        Width = width;
        Height = height;
        _flatGrid = new List<T?>(width * height);
    }

    public Grid2D(int width, int height, T? fillValue)
    {
        Width = width;
        Height = height;
        _flatGrid = Enumerable.Repeat(fillValue, width * height).ToList();
    }

    public T? GetCellValue(int rowIndex, int columnIndex)
    {
        if (rowIndex < 0 || rowIndex >= Height)
            throw new IndexOutOfRangeException();

        return _flatGrid[rowIndex * Width + columnIndex];
    }

    public List<T?> GetRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= Height)
            throw new IndexOutOfRangeException();

        return _flatGrid.GetRange(rowIndex * Width, Width);
    }

    public List<List<T?>> GetRows()
    {
        var result = new List<List<T?>>();
        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            result.Add(GetRow(rowIndex));
        }

        return result;
    }

    public List<List<T?>> GetColumns()
    {
        var result = new List<List<T?>>();
        for (var columnIndex = 0; columnIndex < Height; columnIndex++)
        {
            result.Add(GetColumn(columnIndex));
        }

        return result;
    }

    public List<T?> GetColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= Width)
            throw new IndexOutOfRangeException();

        var result = new List<T?>(Height);
        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            result.Add(_flatGrid[rowIndex * Width + columnIndex]);
        }

        return result;
    }

    public void InsertRow(int rowIndex, List<T> row)
    {
        if (rowIndex < 0 || rowIndex > Height)
            throw new IndexOutOfRangeException();
        if (row.Count != Width)
            throw new ArgumentException("Inserted row must be same width as grid");

        _flatGrid.InsertRange(rowIndex * Width, row);
        Height++;
    }

    public void InsertColumn(int columnIndex, List<T> column)
    {
        if (columnIndex < 0 || columnIndex > Width)
            throw new IndexOutOfRangeException();
        if (column.Count != Width)
            throw new ArgumentException("Inserted column must be same height as grid");

        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            _flatGrid.Insert(rowIndex * Width + columnIndex, column[rowIndex]);
        }

        Width++;
    }

    public void RemoveRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= Height)
            throw new IndexOutOfRangeException();

        _flatGrid.RemoveRange(rowIndex * Width, Width);
        Height--;
    }

    public void RemoveColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= Width)
            throw new IndexOutOfRangeException();

        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            _flatGrid.RemoveAt(rowIndex * Width + columnIndex);
        }

        Width--;
    }

    public void Transpose()
    {
        for (var rowIndex = 0; rowIndex < Height; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < Width; columnIndex++)
            {
                var a = GetCellValue(rowIndex, columnIndex);
                var b = GetCellValue(columnIndex, rowIndex);
                SetCellValue(rowIndex, columnIndex, b);
                SetCellValue(rowIndex, columnIndex, a);
            }
        }
    }

    private void SetCellValue(int rowIndex, int columnIndex, T? cellValue)
    {
        if (rowIndex < 0 || rowIndex >= Height || columnIndex < 0 || columnIndex >= Width)
        {
            throw new IndexOutOfRangeException();
        }

        _flatGrid[rowIndex * Width + columnIndex] = cellValue;
    }

    public bool Equals(Grid2D<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _flatGrid.SequenceEqual(other._flatGrid)
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

    public override int GetHashCode()
    {
        return _flatGrid.GetHashCode();
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
