using System.Collections.Generic;
using AdventOfCode2024.Utils;
using NUnit.Framework;

namespace AdventOfCode2024.Tests.Utils;

[TestFixture]
[TestOf(typeof(CollectionUtils))]
public class CollectionUtilsTest
{
    [Test]
    public void InputEmptyListAndNoAllowedValues_NextCombination_IsResultEmpty()
    {
        var list = new List<int>();
        var allowedValues = new List<int>();

        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int>()));
    }

    [Test]
    public void InputEmptyListAndMultipleAllowedValues_NextCombination_IsResultEmpty()
    {
        var list = new List<int>();
        var allowedValues = new List<int> { 1, 2, 3 };

        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int>()));
    }

    [Test]
    public void InputListWithForbiddenValues_NextCombination_ThrowsException()
    {
        var list = new List<int> { 3 };
        var allowedValues = new List<int> { 1, 2 };

        Assert.That(() => CollectionUtils.SetNextCombination(list, allowedValues), Throws.Exception);
    }

    [Test]
    public void InputSingleElementListAndMultipleAllowedValues_NextCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 3 };
        var allowedValues = new List<int> { 1, 2, 3 };

        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3 }));
    }

    [Test]
    public void InputThreeElementListAndTwoAllowedValues_NextCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 2, 2, 2 };
        var allowedValues = new List<int> { 1, 2 };

        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 2 }));
    }

    [Test]
    public void InputThreeElementListAndOneAllowedValue_NextCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 1, 1, 1 };
        var allowedValues = new List<int> { 1 };

        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 1 }));
    }

    [Test]
    public void InputThreeElementListAndThreeAllowedValues_NextCombination_AreCombinationsCorrect()
    {
        var list = new List<int> { 3, 3, 3 };
        var allowedValues = new List<int> { 1, 2, 3 };

        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 1, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 1, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 2, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 3, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 3, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 2, 3, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 1, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 1, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 1, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 2, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 2, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 2, 3 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 3, 1 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 3, 2 }));
        CollectionUtils.SetNextCombination(list, allowedValues);
        Assert.That(list, Is.EquivalentTo(new List<int> { 3, 3, 3 }));
    }
}
