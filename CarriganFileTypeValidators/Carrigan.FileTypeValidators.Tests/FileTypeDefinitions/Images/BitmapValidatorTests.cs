using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

//IGNORE SPELLING: bmx bmp

public class BitmapValidatorTests
{
    private static readonly FileExtension bmpExt = new("bmp");
    private static readonly MimeType mimeType = new ("image", "bmp");

    [Fact]
    public void ExactTest()
    {
        BitmapValidator definition = new();
        Assert.True(definition.WhiteListMatch("BM"u8.ToArray(), mimeType, bmpExt));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        BitmapValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x42, 0x4D, 0x00], mimeType, bmpExt));
    }

    [Fact]
    public void To_Small()
    {
        BitmapValidator definition = new();
        Assert.False(definition.WhiteListMatch("B"u8.ToArray(), mimeType, bmpExt));
    }
    [Fact]
    public void Wrong_Extension()
    {
        BitmapValidator definition = new();
        Assert.False(definition.WhiteListMatch("BM"u8.ToArray(), mimeType, new FileExtension("bmx")));
    }
    [Fact]
    public void No_Extension()
    {
        BitmapValidator definition = new();
        Assert.False(definition.WhiteListMatch("BM"u8.ToArray(), mimeType, new FileExtension(string.Empty)));
    }
    [Fact]
    public void Empty()
    {
        BitmapValidator definition = new();
        Assert.False(definition.WhiteListMatch([], mimeType, bmpExt));
    }
    [Fact]
    public void Bad_Value()
    {
        BitmapValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x42, 0x00], mimeType, bmpExt));
    }
    [Fact]
    public void Bad_Value2()
    {
        BitmapValidator definition = new();
        Assert.False(definition.WhiteListMatch("@M"u8.ToArray(), mimeType, bmpExt));
    }
}
