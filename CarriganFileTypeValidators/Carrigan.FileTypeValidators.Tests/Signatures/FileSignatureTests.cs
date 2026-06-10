using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.Signatures;

public class FileSignatureTests
{
    [Fact]
    public void Validate_SingleSignatureAndExtension_MatchingData_ReturnsTrue()
    {
        ByteSignature signature = new("BM"u8.ToArray());
        FileSignature fileSignature = new(signature, new FileExtension(".bmp"));

        byte[] data = [0x42, 0x4D, 0xFF, 0xFF];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".bmp"));

        Assert.True(result);
    }

    [Fact]
    public void Validate_SingleSignatureAndExtension_NonMatchingData_ReturnsFalse()
    {
        ByteSignature signature = new("BM"u8.ToArray());
        FileSignature fileSignature = new(signature, new FileExtension(".bmp"));

        byte[] data = [0xFF, 0xFF, 0x42, 0x4D];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".bmp"));

        Assert.False(result);
    }

    [Fact]
    public void Validate_SignaturesAndMultipleExtensions_MatchingData_ReturnsTrue()
    {
        ByteSignature signature = new([0xFF, 0xD8]);
        FileSignature fileSignature = new(signature, [new FileExtension(".bmp"), new FileExtension(".jpg")]);

        byte[] data = [0xFF, 0xD8, 0xFF, 0xFF];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".jpg"));

        Assert.True(result);
    }

    [Fact]
    public void Validate_MultipleSignatures_AllMatching_ReturnsTrue()
    {
        FileSignature fileSignature = new(
            [
                new ByteSignature("RIFF"u8.ToArray(), 0),
                new ByteSignature("WEBP"u8.ToArray(), 8)
            ],
            new FileExtension(".webp"));

        byte[] data = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".webp"));

        Assert.True(result);
    }

    [Fact]
    public void Validate_MultipleSignatures_OnlyFirstMatching_ReturnsFalse()
    {
        FileSignature fileSignature = new(
            [
                new ByteSignature("RIFF"u8.ToArray(), 0),
                new ByteSignature("WEBP"u8.ToArray(), 8)
            ],
            new FileExtension(".webp"));

        byte[] data = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x41, 0x56, 0x45];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".webp"));

        Assert.False(result);
    }

    [Fact]
    public void Validate_TrailerAndSignature_MatchingData_ReturnsTrue()
    {
        ByteSignature signature = new([0xFF, 0xD8]);
        ByteTrailer trailer = new([0xFF, 0xD9]);
        FileSignature fileSignature = new([signature, trailer], new FileExtension(".jpg"));

        byte[] data = [0xFF, 0xD8, 0xAA, 0xBB, 0xFF, 0xD9];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".jpg"));

        Assert.True(result);
    }

    [Fact]
    public void Validate_IncorrectExtension_ReturnsFalse()
    {
        ByteSignature signature = new([0xFF, 0xD8]);
        FileSignature fileSignature = new(signature, new FileExtension(".jpg"));

        byte[] data = [0xFF, 0xD8, 0xAA, 0xBB];
        bool result = fileSignature.WhiteListMatch(data, new FileExtension(".png"));

        Assert.False(result);
    }
}
