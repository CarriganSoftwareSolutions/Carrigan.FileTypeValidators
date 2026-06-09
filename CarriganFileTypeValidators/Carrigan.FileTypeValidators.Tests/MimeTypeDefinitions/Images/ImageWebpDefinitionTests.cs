using Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.MimeTypeDefinitions.Images;

//IGNORE SPELLING: webp webx RIFF WEBP WAVE

public class ImageWebpDefinitionTests
{
    private static readonly byte[] validWebpData = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50];
    private static readonly byte[] riffWaveData = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x41, 0x56, 0x45];

    [Fact]
    public void ExactTest()
    {
        ImageWebpDefinition definition = new();
        Assert.True(definition.IsValid(validWebpData, new FileExtension("webp")));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        ImageWebpDefinition definition = new();
        Assert.True(definition.IsValid([.. validWebpData, 0x00], new FileExtension("webp")));
    }

    [Fact]
    public void Riff_Without_Webp_FourCc_ReturnsFalse()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid("RIFF"u8.ToArray(), new FileExtension("webp")));
    }

    [Fact]
    public void Riff_Wave_FourCc_ReturnsFalse()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid(riffWaveData, new FileExtension("webp")));
    }

    [Fact]
    public void To_Small()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid("RI"u8.ToArray(), new FileExtension("webp")));
    }

    [Fact]
    public void Wrong_Extension()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid(validWebpData, new FileExtension("webx")));
    }

    [Fact]
    public void No_Extension()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid(validWebpData, new FileExtension("")));
    }

    [Fact]
    public void Empty()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid([], new FileExtension("webp")));
    }

    [Fact]
    public void Bad_Value()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid([0x52, 0x49, 0x36, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50], new FileExtension("webp")));
    }

    [Fact]
    public void Bad_Value2()
    {
        ImageWebpDefinition definition = new();
        Assert.False(definition.IsValid([0x52, 0x49, 0x46, 0x26, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50], new FileExtension("webp")));
    }
}
