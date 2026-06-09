
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.Signatures;

public class SignatureTests
{
    #region Signature Tests with zero offset
    [Fact]
    public void MatchingSignatureTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        ByteSignature signature = new([1, 2], 0);

        Assert.True(signature.IsMatching(data));
    }
    [Fact]
    public void ExactMatchingSignatureTest()
    {
        byte[] data = [1, 2];
        ByteSignature signature = new([1, 2], 0);

        Assert.True(signature.IsMatching(data));
    }

    [Fact]
    public void SignatureOverflowTest()
    {
        byte[] data = [1, 2];
        ByteSignature signature = new([1, 2, 3], 0);

        Assert.False(signature.IsMatching(data));
    }

    [Fact]
    public void IncorrectSignatureTest()
    {
        byte[] data = [1, 2, 3];
        ByteSignature signature = new([3, 4], 0);

        Assert.False(signature.IsMatching(data));
    }

    [Fact]
    public void NullSignatureTest()
    {
        byte[] data = [1, 2, 3];
        ByteSignature signature = new([], 0);

        Assert.True(signature.IsMatching(data));
    }

    [Fact]
    public void NullDataSignatureTest()
    {
        byte[] data = [];
        ByteSignature signature = new([1, 2, 3], 0);

        Assert.False(signature.IsMatching(data));
    }
    #endregion


    #region Signature Tests with offset

    [Fact]
    public void MatchingSignatureWithOffsetTest()
    {
        byte[] data = [1, 2, 3, 4, 5];
        ByteSignature signature = new([3, 4], 2);

        Assert.True(signature.IsMatching(data));
    }

    [Fact]
    public void SignatureOffsetOverflow()
    {
        byte[] data = [1];
        ByteSignature signature = new([3, 4], 2);

        Assert.False(signature.IsMatching(data));
    }
    [Fact]
    public void ExactMatchingSignatureWithOffsetTest()
    {
        byte[] data = [1, 2, 3, 4];
        ByteSignature signature = new([3, 4], 2);

        Assert.True(signature.IsMatching(data));
    }

    [Fact]
    public void SignatureWithOffsetOverflowTest()
    {
        byte[] data = [1, 2];
        ByteSignature signature = new([1, 2, 3], 1);

        Assert.False(signature.IsMatching(data));
    }

    [Fact]
    public void IncorrectSignatureWithOffsetTest()
    {
        byte[] data = [1, 2, 3];
        ByteSignature signature = new([3, 4], 1);

        Assert.False(signature.IsMatching(data));
    }

    [Fact]
    public void NullDataSignatureWithOffsetTest()
    {
        byte[] data = ""u8.ToArray();
        ByteSignature signature = new([1, 2, 3], 1);

        Assert.False(signature.IsMatching(data));
    }
    #endregion
}