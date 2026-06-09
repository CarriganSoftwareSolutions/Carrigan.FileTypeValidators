using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.MimeTypeDefinitions.Images;

public class ImageGifDefinitionTests
{
    [Fact]
    public void ExactTest()
    {
        ImageGifDefinition definition = new();
        Assert.True(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B], new FileExtension("gif")));
    }
    [Fact]
    public void ExactTest2()
    {
        ImageGifDefinition definition = new();
        Assert.True(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x3B], new FileExtension("gif")));
    }

    [Fact]
    public void Exact_Plus_Extra_True()
    {
        ImageGifDefinition definition = new();
        Assert.True(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x00, 0x3B],  new FileExtension("gif")));
    }

    [Fact]
    public void Exact_Plus_Extra_At_End_False()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B, 0x00], new FileExtension("gif")));
    }

    [Fact]
    public void Exact_Plus_Extra_At_Start_False()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x00, 0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B], new FileExtension("gif")));
    }

    [Fact]
    public void To_Small_Sig()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x49, 0x46, 0x00, 0x3B], new FileExtension("gif")));
    }

    [Fact]
    public void To_Small_Footer()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x49, 0x46, 0x61, 0x00], new FileExtension("gif")));
    }


    [Fact]
    public void Wrong_Extension()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x3B], new FileExtension("git")));
    }
    [Fact]
    public void No_Extension()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B], new FileExtension("")));
    }
    [Fact]
    public void Empty()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([],  new FileExtension("gif")));
    }
    [Fact]
    public void Empty_Header()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x00, 0x3B],  new FileExtension("gif")));
    }
    [Fact]
    public void Empty_Footer()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid("GIF87a"u8.ToArray(), new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Header()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x00, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B], new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Header2()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x40, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x3B], new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Footer1()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid([0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x00, 0x75], new FileExtension("gif")));
    }
    [Fact]
    public void Bad_Value_Footer2()
    {
        ImageGifDefinition definition = new();
        Assert.False(definition.IsValid("GIF89aP;"u8.ToArray(), new FileExtension("gif")));
    }
}
