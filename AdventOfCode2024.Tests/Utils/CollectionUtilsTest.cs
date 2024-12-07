using System.Collections.Generic;
using System.Linq;
using AdventOfCode2024.Utils;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Utils;

[TestFixture]
[TestOf(typeof(CollectionUtils))]
public class CollectionUtilsTest
{
    [Test]
    public void InputEmptyListAndNoAllowedValues_IncrementCombination_IsResultEmpty()
    {
        var list = new List<int>();
        var allowedValues = new List<int>();

        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int>()));
    }

    [Test]
    public void InputEmptyListAndMultipleAllowedValues_IncrementCombination_IsResultEmpty()
    {
        var list = new List<int>();
        var allowedValues = new List<int> { 1, 2, 3 };

        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int>()));
    }

    [Test]
    public void InputListWithForbiddenValues_IncrementCombination_ThrowsException()
    {
        var list = new List<int> { 3 };
        var allowedValues = new List<int> { 1, 2 };

        Assert.That(() => CollectionUtils.IncrementCombination(list, allowedValues), Throws.Exception);
    }

    [Test]
    public void InputSingleElementListAndMultipleAllowedValues_IncrementCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 3 };
        var allowedValues = new List<int> { 1, 2, 3 };

        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3 }));
    }

    [Test]
    public void InputThreeElementListAndTwoAllowedValues_IncrementCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 2, 2, 2 };
        var allowedValues = new List<int> { 1, 2 };

        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 2 }));
    }

    [Test]
    public void InputThreeElementListAndOneAllowedValue_IncrementCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 1, 1, 1 };
        var allowedValues = new List<int> { 1 };

        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 1 }));
    }

    [Test]
    public void InputThreeElementListAndThreeAllowedValues_IncrementCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 3, 3, 3 };
        var allowedValues = new List<int> { 1, 2, 3 };

        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 3, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 3, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 3, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 1, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 1, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 1, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 2, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 2, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 2, 3 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 3, 1 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 3, 2 }));
        CollectionUtils.IncrementCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 3, 3 }));
    }

    [Test]
    public void InputZeroAllowedValuesAndZeroLength_GetNthCombination_IsResultEmpty()
    {
        Assert.That(CollectionUtils.GetNthCombination(new List<object>(), 0, 0), Is.EquivalentTo(new List<int>()));
        Assert.That(CollectionUtils.GetNthCombination(new List<object>(), 1, 0), Is.EquivalentTo(new List<int>()));
        Assert.That(CollectionUtils.GetNthCombination(new List<object>(), 245, 0), Is.EquivalentTo(new List<int>()));
    }

    [Test]
    public void InputOneAllowedValue_GetNthCombination_IsResultRepeatedValueOfCorrectLength()
    {
        Assert.That(CollectionUtils.GetNthCombination([1], 0, 0), Is.EquivalentTo(new List<int>()));
        Assert.That(CollectionUtils.GetNthCombination([1], 0, 1), Is.EquivalentTo(new List<int> { 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1], 0, 2), Is.EquivalentTo(new List<int> { 1, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1], 0, 3), Is.EquivalentTo(new List<int> { 1, 1, 1 }));

        Assert.That(CollectionUtils.GetNthCombination([1], 1, 3), Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1], 2, 3), Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1], 3, 3), Is.EquivalentTo(new List<int> { 1, 1, 1 }));
    }

    [Test]
    public void InputMultipleAllowedValuesAndOneLength_GetNthCombination_IsResultCorrect()
    {
        Assert.That(CollectionUtils.GetNthCombination([1, 2, 3], 0, 1), Is.EquivalentTo(new List<int> { 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2, 3], 1, 1), Is.EquivalentTo(new List<int> { 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2, 3], 2, 1), Is.EquivalentTo(new List<int> { 3 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2, 3], 3, 1), Is.EquivalentTo(new List<int> { 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2, 3], 4, 1), Is.EquivalentTo(new List<int> { 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2, 3], 5, 1), Is.EquivalentTo(new List<int> { 3 }));
    }

    [Test]
    public void InputLengthEqualToNumberOfAllowedValues_GetNthCombination_IsResultCorrect()
    {
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 0, 2), Is.EquivalentTo(new List<int> { 1, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 1, 2), Is.EquivalentTo(new List<int> { 1, 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 2, 2), Is.EquivalentTo(new List<int> { 2, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 3, 2), Is.EquivalentTo(new List<int> { 2, 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 4, 2), Is.EquivalentTo(new List<int> { 1, 1 }));
    }

    [Test]
    public void InputLengthGreaterThanNumberOfAllowedValues_GetNthCombination_IsResultCorrect()
    {
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 0, 3), Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 1, 3), Is.EquivalentTo(new List<int> { 1, 1, 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 2, 3), Is.EquivalentTo(new List<int> { 1, 2, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 3, 3), Is.EquivalentTo(new List<int> { 1, 2, 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 4, 3), Is.EquivalentTo(new List<int> { 2, 1, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 5, 3), Is.EquivalentTo(new List<int> { 2, 1, 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 6, 3), Is.EquivalentTo(new List<int> { 2, 2, 1 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 7, 3), Is.EquivalentTo(new List<int> { 2, 2, 2 }));
        Assert.That(CollectionUtils.GetNthCombination([1, 2], 8, 3), Is.EquivalentTo(new List<int> { 1, 1, 1 }));
    }

    [Test, MaxTime(5_000)]
    public void InputLargeNAndLength_GetNthCombination_IsResultCorrect()
    {
        Assert.That(
            CollectionUtils.GetNthCombination([1], 100_000_000, 25),
            Is.EquivalentTo(Enumerable.Repeat(1, 25)));
    }
}
