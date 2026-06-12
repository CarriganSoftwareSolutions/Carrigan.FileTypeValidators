
using System;
using System.Collections.Generic;
using Xunit;

namespace Carrigan.FileTypeValidators.Tests.ExtensionMethods;

public sealed class CreateCyclerTests
{
    [Fact]
    public void CreateCycler_WhenValuesIsNull_ThrowsArgumentNullException()
    {
        IEnumerable<int>? values = null;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => values!.CreateCycler());

        Assert.Equal("values", exception.ParamName);
    }

    [Fact]
    public void CreateCycler_WhenValuesIsEmpty_ThrowsArgumentException()
    {
        int[] values = [];

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => values.CreateCycler());

        Assert.Equal("values", exception.ParamName);
    }

    [Fact]
    public void CreateCycler_WhenCalledFirstTime_ReturnsFirstItem()
    {
        int[] values = [1, 2, 3];

        Func<int> next = values.CreateCycler();

        Assert.Equal(1, next());
    }

    [Fact]
    public void CreateCycler_WhenCalledRepeatedly_ReturnsItemsInOrder()
    {
        int[] values = [1, 2, 3];

        Func<int> next = values.CreateCycler();

        Assert.Equal(1, next());
        Assert.Equal(2, next());
        Assert.Equal(3, next());
    }

    [Fact]
    public void CreateCycler_WhenEndIsReached_WrapsBackToBeginning()
    {
        int[] values = [1, 2, 3];

        Func<int> next = values.CreateCycler();

        Assert.Equal(1, next());
        Assert.Equal(2, next());
        Assert.Equal(3, next());
        Assert.Equal(1, next());
        Assert.Equal(2, next());
    }

    [Fact]
    public void CreateCycler_WhenCreatedMultipleTimes_EachCyclerHasIndependentState()
    {
        int[] values = [1, 2, 3];

        Func<int> firstCycler = values.CreateCycler();
        Func<int> secondCycler = values.CreateCycler();

        Assert.Equal(1, firstCycler());
        Assert.Equal(2, firstCycler());

        Assert.Equal(1, secondCycler());

        Assert.Equal(3, firstCycler());
        Assert.Equal(2, secondCycler());
    }

    [Fact]
    public void CreateCycler_WhenDelegateReferenceIsCopied_SharesSameState()
    {
        int[] values = [1, 2, 3];

        Func<int> next = values.CreateCycler();
        Func<int> sameNext = next;

        Assert.Equal(1, next());
        Assert.Equal(2, sameNext());
        Assert.Equal(3, next());
        Assert.Equal(1, sameNext());
    }

    [Fact]
    public void CreateCycler_SnapshotsValuesWhenCreated()
    {
        List<int> values = [1, 2, 3];

        Func<int> next = values.CreateCycler();

        values[0] = 99;
        values.Add(4);

        Assert.Equal(1, next());
        Assert.Equal(2, next());
        Assert.Equal(3, next());
        Assert.Equal(1, next());
    }

    [Fact]
    public void CreateCycler_EnumeratesValuesOnlyOnceWhenCreated()
    {
        int enumerationCount = 0;

        IEnumerable<int> values = GetValues();

        Func<int> next = values.CreateCycler();

        Assert.Equal(1, enumerationCount);

        Assert.Equal(1, next());
        Assert.Equal(2, next());
        Assert.Equal(3, next());
        Assert.Equal(1, next());

        Assert.Equal(1, enumerationCount);

        IEnumerable<int> GetValues()
        {
            enumerationCount++;

            yield return 1;
            yield return 2;
            yield return 3;
        }
    }

    [Fact]
    public void CreateCycler_WorksWithReferenceTypes()
    {
        string[] values = ["alpha", "bravo", "charlie"];

        Func<string> next = values.CreateCycler();

        Assert.Equal("alpha", next());
        Assert.Equal("bravo", next());
        Assert.Equal("charlie", next());
        Assert.Equal("alpha", next());
    }
}