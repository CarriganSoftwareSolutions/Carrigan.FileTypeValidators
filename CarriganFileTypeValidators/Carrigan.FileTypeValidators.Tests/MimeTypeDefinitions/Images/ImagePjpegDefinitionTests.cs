using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;


namespace Carrigan.FileTypeValidators.Tests.MimeTypeDefinitions.Images;

//IGNORE SPELLING: JPEG jfif  jpe jtt jpg pjpeg

public class ImagePjpegDefinitionTests
{
    [Fact]
    public void ExactTest()
    {
        ImagePjpegDefinition definition = new();
        Assert.True(definition.IsValid([0xFF, 0xD8, 0xFF, 0xD9], new FileExtension("jpeg")));
    }
    [Fact]
    public void ExactTest2()
    {
        ImagePjpegDefinition definition = new();
        Assert.True(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new FileExtension("jfif")));
    }
    [Fact]
    public void ExactTest3()
    {
        ImagePjpegDefinition definition = new();
        Assert.True(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE8, 0xFF, 0xD9], new FileExtension("jpg")));
    }
    [Fact]
    public void ExactTest4()
    {
        ImagePjpegDefinition definition = new();
        Assert.True(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE1, 0xFF, 0xD9], new FileExtension("jpg")));
    }
    [Fact]
    public void ExactTest5()
    {
        ImagePjpegDefinition definition = new();
        Assert.True(definition.IsValid([0xFF, 0xD8, 0xFF, 0xD9], new FileExtension("jpe")));
    }

    [Fact]
    public void Exact_Plus_Extra_True()
    {
        ImagePjpegDefinition definition = new();
        Assert.True(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new FileExtension("jpg")));
    }

    [Fact]
    public void Exact_Plus_Extra_At_End_False()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE8, 0xFF, 0xD9, 0x00], new FileExtension("jpg")));
    }

    [Fact]
    public void Exact_Plus_Extra_At_Start_False()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0x00, 0xFF, 0xD8, 0xFF, 0xD9], new FileExtension("jpe")));
    }

    [Fact]
    public void To_Small_Sig()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xFF, 0xD9], new FileExtension("jfif")));
    }

    [Fact]
    public void To_Small_Footer()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE0, 0xD9], new FileExtension("jfif")));
    }


    [Fact]
    public void Wrong_Extension()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new FileExtension("jtt")));
    }
    [Fact]
    public void No_Extension()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9], new FileExtension("")));
    }
    [Fact]
    public void Empty()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([], new FileExtension("jpg")));
    }
    [Fact]
    public void Empty_Header()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD9], new FileExtension("jpg")));
    }
    [Fact]
    public void Empty_Footer()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE8], new FileExtension("jpg")));
    }
    [Fact]
    public void Bad_Value_Header()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xF0, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new FileExtension("jpg")));
    }
    [Fact]
    public void Bad_Value_Header2()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0x08, 0xFF, 0xE1, 0x00, 0xFF, 0xD9], new FileExtension("jpg")));
    }
    [Fact]
    public void Bad_Value_Footer1()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD0], new FileExtension("jpg")));
    }
    [Fact]
    public void Bad_Value_Footer2()
    {
        ImagePjpegDefinition definition = new();
        Assert.False(definition.IsValid([0xFF, 0xD8, 0xFF, 0xE1, 0x00, 0xFF, 0xD0], new FileExtension("jpg")));
    }
}
