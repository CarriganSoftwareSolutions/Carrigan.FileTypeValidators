using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.Signatures;

public class TrailersTests
{
    #region Signature Tests with zero offset
    [Fact]
    public void MatchingTrailerTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        ByteTrailer signature = new([4, 5], 0);

        Assert.True(signature.IsMatch(data));
    }
    [Fact]
    public void ExactMatchingTrailerTest()
    {
        byte[] data = [1, 2];
        ByteTrailer signature = new([1, 2], 0);

        Assert.True(signature.IsMatch(data));
    }

    [Fact]
    public void TrailerOverflowTestA()
    {
        byte[] data = [1, 2];
        ByteTrailer signature = new([0, 1, 2], 0);

        Assert.False(signature.IsMatch(data));
    }

    [Fact]
    public void TrailerOverflowTestB()
    {
        byte[] data = [1, 2];
        ByteTrailer signature = new([1, 2, 3], 0);

        Assert.False(signature.IsMatch(data));
    }

    [Fact]
    public void IncorrectTrailerTest()
    {
        byte[] data = [1, 2, 3];
        ByteTrailer signature = new([3, 4], 0);

        Assert.False(signature.IsMatch(data));
    }

    [Fact]
    public void EmptyTrailerTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            byte[] data = [1, 2, 3];
            ByteTrailer signature = new(""u8.ToArray(), 0);

            Assert.True(signature.IsMatch(data));
        });

    [Fact]
    public void NullDataTrailerTest()
    {
        byte[] data = ""u8.ToArray();
        ByteTrailer signature = new([1, 2, 3], 0);

        Assert.False(signature.IsMatch(data));
    }
    #endregion


    #region Signature Tests with offset

    [Fact]
    public void MatchingTrailerWithOffsetTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        ByteTrailer signature = new([3, 4], 1);

        Assert.True(signature.IsMatch(data));
    }

    [Fact]
    public void TrailerOffsetOverflow()
    {
        byte[] data = [1];
        ByteTrailer signature = new([1], 2);

        Assert.False(signature.IsMatch(data));
    }
    [Fact]
    public void ExactMatchingTrailerWithOffsetTest()
    {
        byte[] data = [1, 2, 3, 4];
        ByteTrailer signature = new([1, 2], 2);

        Assert.True(signature.IsMatch(data));
    }

    [Fact]
    public void TrailerWithOffsetOverflowTest()
    {
        byte[] data = [1, 2];
        ByteTrailer signature = new([1, 2, 3], 1);

        Assert.False(signature.IsMatch(data));
    }

    [Fact]
    public void IncorrectTrailerWithOffsetTest()
    {
        byte[] data = [1, 2, 3];
        ByteTrailer signature = new([3, 4], 1);

        Assert.False(signature.IsMatch(data));
    }

    [Fact]
    public void NullDataTrailerWithOffsetTest()
    {
        byte[] data = ""u8.ToArray();
        ByteTrailer signature = new([1, 2, 3], 1);

        Assert.False(signature.IsMatch(data));
    }
    #endregion
}