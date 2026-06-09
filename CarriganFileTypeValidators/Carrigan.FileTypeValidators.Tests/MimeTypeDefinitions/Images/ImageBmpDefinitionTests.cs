using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;


namespace Carrigan.FileTypeValidators.Tests.MimeTypeDefinitions.Images;

//IGNORE SPELLING: bmx bmp

public class ImageBmpDefinitionTests
{
    [Fact]
    public void ExactTest()
    {
        ImageBmpDefinition definition = new();
        Assert.True(definition.IsValid("BM"u8.ToArray(), "bmp"));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        ImageBmpDefinition definition = new();
        Assert.True(definition.IsValid([0x42, 0x4D, 0x00], "bmp"));
    }

    [Fact]
    public void To_Small()
    {
        ImageBmpDefinition definition = new();
        Assert.False(definition.IsValid("B"u8.ToArray(), "bmp"));
    }
    [Fact]
    public void Wrong_Extension()
    {
        ImageBmpDefinition definition = new();
        Assert.False(definition.IsValid("BM"u8.ToArray(), "bmx"));
    }
    [Fact]
    public void No_Extension()
    {
        ImageBmpDefinition definition = new();
        Assert.False(definition.IsValid("BM"u8.ToArray(), ""));
    }
    [Fact]
    public void Empty()
    {
        ImageBmpDefinition definition = new();
        Assert.False(definition.IsValid([], "bmp"));
    }
    [Fact]
    public void Bad_Value()
    {
        ImageBmpDefinition definition = new();
        Assert.False(definition.IsValid([0x42, 0x00], "bmp"));
    }
    [Fact]
    public void Bad_Value2()
    {
        ImageBmpDefinition definition = new();
        Assert.False(definition.IsValid("@M"u8.ToArray(), "bmp"));
    }
}
