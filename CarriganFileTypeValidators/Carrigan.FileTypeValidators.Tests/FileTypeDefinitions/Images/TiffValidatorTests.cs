
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

//ignore spelling: tif dfg BigTIFF endian

public class TiffValidatorTests
{
    [Fact]
    public void LittleEndianClassicTiff_Tif_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x49, 0x49, 0x2A, 0x00], new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void LittleEndianClassicTiff_Tiff_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x49, 0x49, 0x2A, 0x00], new("image", "tiff"), new FileExtension("tiff")));
    }

    [Fact]
    public void BigEndianClassicTiff_Tif_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2A], new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void BigEndianClassicTiff_Tiff_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2A], new("image", "tiff"), new FileExtension("tiff")));
    }

    [Fact]
    public void LittleEndianBigTiff_Tif_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x49, 0x49, 0x2B, 0x00], new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void LittleEndianBigTiff_Tiff_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x49, 0x49, 0x2B, 0x00], new("image", "tiff"), new FileExtension("tiff")));
    }

    [Fact]
    public void BigEndianBigTiff_Tif_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2B], new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void BigEndianBigTiff_Tiff_ReturnsTrue()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2B], new("image", "tiff"), new FileExtension("tiff")));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        TiffValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2B, 0x44], new("image", "tiff"), new FileExtension("tiff")));
    }

    [Fact]
    public void Old_Invalid_I_Space_I_Signature_ReturnsFalse()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch("I I"u8.ToArray(), new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void To_Small()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x49, 0x49], new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void Wrong_Extension()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2B], new("image", "tiff"), new FileExtension("dfg")));
    }

    [Fact]
    public void No_Extension()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2B], new("image", "tiff"), new FileExtension("")));
    }

    [Fact]
    public void Empty()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch([], new("image", "tiff"), new FileExtension("tiff")));
    }

    [Fact]
    public void Bad_Value()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch("MME+"u8.ToArray(), new("image", "tiff"), new FileExtension("tif")));
    }

    [Fact]
    public void Bad_Value2()
    {
        TiffValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x4D, 0x4D, 0x00, 0x2C], new("image", "tiff"), new FileExtension("tif")));
    }
}
