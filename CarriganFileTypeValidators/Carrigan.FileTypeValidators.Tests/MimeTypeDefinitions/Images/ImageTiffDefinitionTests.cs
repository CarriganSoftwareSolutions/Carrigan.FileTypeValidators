using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.MimeTypeDefinitions.Images;

//ignore spelling: tif dfg BigTIFF endian

public class ImageTiffDefinitionTests
{
    [Fact]
    public void LittleEndianClassicTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2A, 0x00], new FileExtension("tif")));
    }

    [Fact]
    public void LittleEndianClassicTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2A, 0x00], new FileExtension("tiff")));
    }

    [Fact]
    public void BigEndianClassicTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2A], new FileExtension("tif")));
    }

    [Fact]
    public void BigEndianClassicTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2A], new FileExtension("tiff")));
    }

    [Fact]
    public void LittleEndianBigTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2B, 0x00], new FileExtension("tif")));
    }

    [Fact]
    public void LittleEndianBigTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x49, 0x49, 0x2B, 0x00], new FileExtension("tiff")));
    }

    [Fact]
    public void BigEndianBigTiff_Tif_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], new FileExtension("tif")));
    }

    [Fact]
    public void BigEndianBigTiff_Tiff_ReturnsTrue()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], new FileExtension("tiff")));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        ImageTiffDefinition definition = new();
        Assert.True(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B, 0x44], new FileExtension("tiff")));
    }

    [Fact]
    public void Old_Invalid_I_Space_I_Signature_ReturnsFalse()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid("I I"u8.ToArray(), new FileExtension("tif")));
    }

    [Fact]
    public void To_Small()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x49, 0x49], new FileExtension("tif")));
    }

    [Fact]
    public void Wrong_Extension()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], new FileExtension("dfg")));
    }

    [Fact]
    public void No_Extension()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x4D, 0x4D, 0x00, 0x2B], new FileExtension("")));
    }

    [Fact]
    public void Empty()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([], new FileExtension("tiff")));
    }

    [Fact]
    public void Bad_Value()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid("MME+"u8.ToArray(), new FileExtension("tif")));
    }

    [Fact]
    public void Bad_Value2()
    {
        ImageTiffDefinition definition = new();
        Assert.False(definition.IsValid([0x4D, 0x4D, 0x00, 0x2C], new FileExtension("tif")));
    }
}
