

namespace Carrigan.FileTypeValidators.Tests.ExtensionMethods;

public sealed class ByteEnumerableExtensionsTests
{
    [Fact]
    public void SwapAt_ReplacesByteAtMiddleIndex()
    {
        byte[] source = [0x01, 0x02, 0x03];

        byte[] result = [.. source.SwapAt<byte>(1, 0xFF)];

        Assert.Equal([0x01, 0xFF, 0x03], result);
    }

    [Fact]
    public void SwapAt_ReplacesByteAtFirstIndex()
    {
        byte[] source = [0x01, 0x02, 0x03];

        byte[] result = [.. source.SwapAt<byte>(0, 0xFF)];

        Assert.Equal([0xFF, 0x02, 0x03], result);
    }

    [Fact]
    public void SwapAt_ReplacesByteAtLastIndex()
    {
        byte[] source = [0x01, 0x02, 0x03];

        byte[] result = [.. source.SwapAt<byte>(2, 0xFF)];

        Assert.Equal([0x01, 0x02, 0xFF], result);
    }

    [Fact]
    public void SwapAt_DoesNotModifyOriginalArray()
    {
        byte[] source = [0x01, 0x02, 0x03];

        byte[] result = [.. source.SwapAt<byte>(1, 0xFF)];

        Assert.Equal([0x01, 0x02, 0x03], source);
        Assert.Equal([0x01, 0xFF, 0x03], result);
    }

    [Fact]
    public void SwapAt_AllowsReplacingWithSameValue()
    {
        byte[] source = [0x01, 0x02, 0x03];

        byte[] result = [.. source.SwapAt<byte>(1, 0x02)];

        Assert.Equal([0x01, 0x02, 0x03], result);
    }

    [Fact]
    public void SwapAt_ThrowsWhenSourceIsNull()
    {
        IEnumerable<byte>? source = null;

        Assert.Throws<ArgumentNullException>(() =>
            source!
                .SwapAt<byte>(0, 0xFF)
                .ToArray());
    }

    [Fact]
    public void SwapAt_ThrowsWhenIndexIsNegative()
    {
        byte[] source = [0x01, 0x02, 0x03];

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            source
                .SwapAt<byte>(-1, 0xFF)
                .ToArray());
    }

    [Fact]
    public void SwapAt_ThrowsWhenIndexIsPastEnd()
    {
        byte[] source = [0x01, 0x02, 0x03];

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            source
                .SwapAt<byte>(3, 0xFF)
                .ToArray());
    }

    [Fact]
    public void SwapAt_ThrowsWhenSourceIsEmpty()
    {
        byte[] source = [];

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            source
                .SwapAt<byte>(0, 0xFF)
                .ToArray());
    }

    [Fact]
    public void SwapAt_IsDeferredUntilEnumerated()
    {
        bool enumerated = false;

        IEnumerable<byte> source = GetSource();

        IEnumerable<byte> result = source.SwapAt<byte>(1, 0xFF);

        Assert.False(enumerated);

        byte[] values = [.. result];

        Assert.True(enumerated);
        Assert.Equal([0x01, 0xFF, 0x03], values);

        IEnumerable<byte> GetSource()
        {
            enumerated = true;

            yield return 0x01;
            yield return 0x02;
            yield return 0x03;
        }
    }
}