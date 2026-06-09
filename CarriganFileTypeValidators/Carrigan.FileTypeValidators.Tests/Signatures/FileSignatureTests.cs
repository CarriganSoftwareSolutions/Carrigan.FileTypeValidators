
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.Signatures;

public class FileSignatureTests
{
    [Fact]
    public void Validate_SingleSignatureAndExtension_MatchingData_ReturnsTrue()
    {
        ByteSignature signature = new("BM"u8.ToArray()); // Example: BM header for BMP files
        FileSignature fileSignature = new(signature, ".bmp");

        byte[] data = [0x42, 0x4D, 0xFF, 0xFF]; // Matching data
        bool result = fileSignature.Validate(data, ".bmp");

        Assert.True(result);
    }

    [Fact]
    public void Validate_SingleSignatureAndExtension_NonMatchingData_ReturnsFalse()
    {
        ByteSignature signature = new("BM"u8.ToArray()); // Example: BM header for BMP files
        FileSignature fileSignature = new(signature, ".bmp");

        byte[] data = [0xFF, 0xFF, 0x42, 0x4D]; // Non-matching data
        bool result = fileSignature.Validate(data, ".bmp");

        Assert.False(result);
    }

    [Fact]
    public void Validate_SignaturesAndMultipleExtensions_MatchingData_ReturnsTrue()
    {
        ByteSignature signature = new([0xFF, 0xD8]);
        FileSignature fileSignature = new(signature, [".bmp", ".jpg"]);

        byte[] data = [0xFF, 0xD8, 0xFF, 0xFF];
        bool result = fileSignature.Validate(data, ".jpg");

        Assert.True(result);
    }

    [Fact]
    public void Validate_TrailerAndSignature_MatchingData_ReturnsTrue()
    {
        ByteSignature signature = new([0xFF, 0xD8]);
        ByteTrailer trailer = new([0xFF, 0xD9]);
        FileSignature fileSignature = new(signature, trailer, ".jpg");

        byte[] data = [0xFF, 0xD8, 0xAA, 0xBB, 0xFF, 0xD9]; // Matching data for JPG
        bool result = fileSignature.Validate(data, ".jpg");

        Assert.True(result);
    }

    [Fact]
    public void Validate_IncorrectExtension_ReturnsFalse()
    {
        ByteSignature signature = new([0xFF, 0xD8]);
        FileSignature fileSignature = new(signature, ".jpg");

        byte[] data = [0xFF, 0xD8, 0xAA, 0xBB];
        bool result = fileSignature.Validate(data, ".png"); // Incorrect extension

        Assert.False(result);
    }
}