using System;
using System.Runtime.CompilerServices;
using AdventOfCode2024.Utils;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Utils;

[TestFixture]
[TestOf(typeof(Grid2D<>))]
public class Grid2DTest
{
    [TestCase<int>(0)]
    [TestCase<string>("")]
    [TestCase<object>(null)]
    public void CreateEmptyGrid_GridIsEmptyAndAccessThrowsException<T>(T _)
    {
        var grid = new Grid2D<T>();

        Assert.That(grid.Width, Is.EqualTo(0));
        Assert.That(grid.Height, Is.EqualTo(0));

        Assert.That(() => grid.GetCellValue(0, 0), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        Assert.That(() => grid.GetRow(0), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        Assert.That(() => grid.GetColumn(0), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        Assert.That(() => grid.RemoveRow(0), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        Assert.That(() => grid.RemoveColumn(0), Throws.Exception.TypeOf<IndexOutOfRangeException>());
    }

    [TestCase<int>(0)]
    [TestCase<string>("")]
    [TestCase<object>(null)]
    public void CreateTwoEmptyGrids_EmptyGridsOfSameTypeAreEqual<T>(T fillValue)
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.That(new Grid2D<T>() == new Grid2D<T>());
        Assert.That(new Grid2D<T>(), Is.EqualTo(new Grid2D<T>()));
        Assert.That(new Grid2D<T>(), Is.EqualTo(new Grid2D<T>(0, 0)));
        Assert.That(new Grid2D<T>(0, 0), Is.EqualTo(new Grid2D<T>()));
        Assert.That(new Grid2D<T>(0, 0), Is.EqualTo(new Grid2D<T>(0, 0)));

        Assert.That(new Grid2D<T>(0, 0, fillValue), Is.EqualTo(new Grid2D<T>()));
        Assert.That(new Grid2D<T>(0, 0, fillValue), Is.EqualTo(new Grid2D<T>(0, 0)));
        Assert.That(new Grid2D<T>(0, 0, fillValue), Is.EqualTo(new Grid2D<T>(0, 0, fillValue)));
    }

    [TestCase<int>(0)]
    [TestCase<string>("")]
    [TestCase<object>(null)]
    public void CreateTwoEqualSizeGrids_AreEqual<T>(T _)
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.That(new Grid2D<T>() == new Grid2D<T>());
        Assert.That(new Grid2D<T>(), Is.EqualTo(new Grid2D<T>()));
        Assert.That(new Grid2D<T>(0, 0), Is.EqualTo(new Grid2D<T>(0, 0)));
        Assert.That(new Grid2D<T>(0, 1), Is.EqualTo(new Grid2D<T>(0, 1)));
        Assert.That(new Grid2D<T>(1, 0), Is.EqualTo(new Grid2D<T>(1, 0)));
        Assert.That(new Grid2D<T>(1, 1), Is.EqualTo(new Grid2D<T>(1, 1)));
    }

    [TestCase<int>(0)]
    [TestCase<string>("")]
    [TestCase<object>(null)]
    public void CreateTwoDifferentSizeGrids_AreNotEqual<T>(T _)
    {
        Assert.That(new Grid2D<T>(0, 1) != new Grid2D<T>(0, 0));
        Assert.That(new Grid2D<T>(0, 0) != new Grid2D<T>(0, 1));

        Assert.That(new Grid2D<T>(0, 0), Is.Not.EqualTo(new Grid2D<T>(1, 1)));
        Assert.That(new Grid2D<T>(1, 1), Is.Not.EqualTo(new Grid2D<T>(0, 0)));

        Assert.That(new Grid2D<T>(0, 1), Is.Not.EqualTo(new Grid2D<T>(0, 0)));
        Assert.That(new Grid2D<T>(0, 0), Is.Not.EqualTo(new Grid2D<T>(0, 1)));

        Assert.That(new Grid2D<T>(0, 1), Is.Not.EqualTo(new Grid2D<T>(1, 0)));
        Assert.That(new Grid2D<T>(1, 0), Is.Not.EqualTo(new Grid2D<T>(0, 1)));

        Assert.That(new Grid2D<T>(0, 1), Is.Not.EqualTo(new Grid2D<T>(1, 1)));
        Assert.That(new Grid2D<T>(1, 1), Is.Not.EqualTo(new Grid2D<T>(0, 1)));

        Assert.That(new Grid2D<T>(1, 0), Is.Not.EqualTo(new Grid2D<T>(1, 1)));
        Assert.That(new Grid2D<T>(1, 1), Is.Not.EqualTo(new Grid2D<T>(1, 0)));
    }

    [Test]
    public void DeleteFirstRow_IsFirstRowDeleted()
    {
        var grid = new Grid2D<string>(new[,] { { "A", "B" }, { "C", "D" }, { "E", "F" } });
        Assert.That(grid.Width, Is.EqualTo(2));
        Assert.That(grid.Height, Is.EqualTo(3));
        grid.RemoveRow(0);
        Assert.That(grid.Width, Is.EqualTo(2));
        Assert.That(grid.Height, Is.EqualTo(2));
        Assert.That(grid, Is.EqualTo(new Grid2D<string>(new[,] { { "C", "D" }, { "E", "F" } })));
    }

    [Test]
    public void DeleteMiddleRow_IsMiddleRowDeleted()
    {
        var grid = new Grid2D<string>(new[,] { { "A", "B" }, { "C", "D" }, { "E", "F" } });
        Assert.That(grid.Width, Is.EqualTo(2));
        Assert.That(grid.Height, Is.EqualTo(3));
        grid.RemoveRow(1);
        Assert.That(grid.Width, Is.EqualTo(2));
        Assert.That(grid.Height, Is.EqualTo(2));
        Assert.That(grid, Is.EqualTo(new Grid2D<string>(new[,] { { "A", "B" }, { "E", "F" } })));
    }

    [Test]
    public void DeleteLastRow_IsLastRowDeleted()
    {
        var grid = new Grid2D<string>(new[,] { { "A", "B" }, { "C", "D" }, { "E", "F" } });
        Assert.That(grid.Width, Is.EqualTo(2));
        Assert.That(grid.Height, Is.EqualTo(3));
        grid.RemoveRow(2);
        Assert.That(grid.Width, Is.EqualTo(2));
        Assert.That(grid.Height, Is.EqualTo(2));
        Assert.That(grid, Is.EqualTo(new Grid2D<string>(new[,] { { "A", "B" }, { "C", "D" } })));
    }
}
