using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Images;

//IGNORE SPELLING: webp webx RIFF WEBP WAVE

public class WebpValidatorTests
{
    private static readonly byte[] validWebpData = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50];
    private static readonly byte[] riffWaveData = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x41, 0x56, 0x45];
    private static readonly FileExtension webpExtension = new ("webp");
    private static readonly MimeType webpMimeType = new("image", "webp");


    [Fact]
    public void ExactTest()
    {
        WebpValidator definition = new();
        Assert.True(definition.WhiteListMatch(validWebpData, webpMimeType, webpExtension));
    }

    [Fact]
    public void Exact_Plus_Extra()
    {
        WebpValidator definition = new();
        Assert.True(definition.WhiteListMatch([.. validWebpData, 0x00], webpMimeType, webpExtension));
    }

    [Fact]
    public void Riff_Without_Webp_FourCc_ReturnsFalse()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch("RIFF"u8.ToArray(), webpMimeType, webpExtension));
    }

    [Fact]
    public void Riff_Wave_FourCc_ReturnsFalse()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch(riffWaveData, webpMimeType, webpExtension));
    }

    [Fact]
    public void To_Small()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch("RI"u8.ToArray(), webpMimeType, webpExtension));
    }

    [Fact]
    public void Wrong_Extension()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch(validWebpData, webpMimeType, new FileExtension("webx")));
    }

    [Fact]
    public void No_Extension()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch(validWebpData, webpMimeType, new FileExtension("")));
    }

    [Fact]
    public void Empty()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch([], webpMimeType, webpExtension));
    }

    [Fact]
    public void Bad_Value()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x52, 0x49, 0x36, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50], webpMimeType, webpExtension));
    }

    [Fact]
    public void Bad_Value2()
    {
        WebpValidator definition = new();
        Assert.False(definition.WhiteListMatch([0x52, 0x49, 0x46, 0x26, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50], webpMimeType, webpExtension));
    }
}
