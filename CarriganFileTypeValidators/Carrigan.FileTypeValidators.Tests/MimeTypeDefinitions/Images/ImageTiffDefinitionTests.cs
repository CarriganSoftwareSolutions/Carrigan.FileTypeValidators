using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

namespace Carrigan.FileTypeValidators.Tests.MimeTypeDefinitions.Images;

//ignore spelling: tif dfg BigTIFF endian

public class ImageTiffDefinitionTests
{
    [Fact]
    public void LittleEndianClassicTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2A, 0x00], "tif"));
    }

    [Fact]
    public void LittleEndianClassicTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2A, 0x00], "tiff"));
    }

    [Fact]
    public void BigEndianClassicTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2A], "tif"));
    }

    [Fact]
    public void BigEndianClassicTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2A], "tiff"));
    }

    [Fact]
    public void LittleEndianBigTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2B, 0x00], "tif"));
    }

    [Fact]
    public void LittleEndianBigTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2B, 0x00], "tiff"));
    }

    [Fact]
    public void BigEndianBigTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], "tif"));
    }

    [Fact]
    public void BigEndianBigTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], "tiff"));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B, 0x44], "tiff"));
    }

    [Fact]
    public void Old_Invalid_I_Space_I_Signature_ReturnsFalse()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid("I I"u8.ToArray(), "tif"));
    }

    [Fact]
    public void To_Small()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x49, 0x49], "tif"));
    }

    [Fact]
    public void Wrong_Extension()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], "dfg"));
    }

    [Fact]
    public void No_Extension()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], ""));
    }

    [Fact]
    public void Empty()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([], "tiff"));
    }

    [Fact]
    public void Bad_Value()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid("MME+"u8.ToArray(), "tif"));
    }

    [Fact]
    public void Bad_Value2()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x4D, 0x4D, 0x00, 0x2C], "tif"));
    }
}
