using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

//IGNORE SPELLING: png pwn

public class PngValidatorTests
{
    [Fact]
    public void ExactTest()
    {
        PngValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("png")));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        PngValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("png")));
    }

    [Fact]
    public void To_Small()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("png")));
    }
    [Fact]
    public void Wrong_Extension()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("pwn")));
    }
    [Fact]
    public void No_Extension()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("")));
    }
    [Fact]
    public void Empty()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([], new("image", "png"), new FileExtension("png")));
    }
    [Fact]
    public void Bad_Value()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x80, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("png")));
    }
    [Fact]
    public void Bad_Value2()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x89, 0x00, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82], new("image", "png"), new FileExtension("png")));
    }
    [Fact]
    public void Bad_Value3()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x02], new("image", "png"), new FileExtension("png")));
    }
    [Fact]
    public void Bad_Value24()
    {
        PngValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x00, 0x82], new("image", "png"), new FileExtension("png")));
    }
}
