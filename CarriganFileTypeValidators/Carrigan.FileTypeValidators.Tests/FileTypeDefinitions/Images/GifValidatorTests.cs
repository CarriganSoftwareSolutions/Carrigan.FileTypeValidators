using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

public class GifValidatorTests
{
    private static readonly byte[] valid = [0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B];
    private static readonly byte[] valid2 = [0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x3B];

    [Fact]
    public void ExactTest()
    {
        GifValidator definition = new();
        Assert.True(definition.WhiteListMatch(valid, new ("image", "gif"), new FileExtension("gif")));
    }
    [Fact]
    public void ExactTest2()
    {
        GifValidator definition = new();
        Assert.True(definition.WhiteListMatch(valid2, new("image", "gif"), new FileExtension("gif")));
    }

    [Fact]
    public void Exact_Plus_Extra_True()
    {
        GifValidator definition = new();
        Assert.True(definition.WhiteListMatch([0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x00, 0x3B], new("image", "gif"),  new FileExtension("gif")));
    }

    [Fact]
    public void Exact_Plus_Extra_At_End_False()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([.. valid, 0x00], new("image", "gif"), new FileExtension("gif")));
    }

    [Fact]
    public void Exact_Plus_Extra_At_Start_False()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x00, .. valid], new("image", "gif"), new FileExtension("gif")));
    }

    [Fact]
    public void To_Small_Sig()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x47, 0x49, 0x46, 0x00, 0x3B], new("image", "gif"), new FileExtension("gif")));
    }

    [Fact]
    public void To_Small_Footer()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x47, 0x49, 0x46, 0x61, 0x00], new("image", "gif"), new FileExtension("gif")));
    }


    [Fact]
    public void Wrong_Extension()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch(valid2, new("image", "gif"), new FileExtension("git")));
    }
    [Fact]
    public void No_Extension()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch(valid, new("image", "gif"), new FileExtension("")));
    }
    [Fact]
    public void Empty()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([], new("image", "gif"),  new FileExtension("gif")));
    }
    [Fact]
    public void Empty_Header()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x00, 0x3B], new("image", "gif"),  new FileExtension("gif")));
    }
    [Fact]
    public void Empty_Footer()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch("GIF87a"u8.ToArray(), new("image", "gif"), new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Header()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x47, 0x00, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B], new("image", "gif"), new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Header2()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x40, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x3B], new("image", "gif"), new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Footer1()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x75], new("image", "gif"), new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Footer2()
    {
        GifValidator definition = new();
        Assert.False(definition.WhiteListMatch("GIF89aP;"u8.ToArray(), new("image", "gif"), new FileExtension("gif")));
    }
}
