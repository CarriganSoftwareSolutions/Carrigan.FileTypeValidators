using Carrigan.FileTypeValidators.Enums;

namespace Carrigan.FileTypeValidators.Tests.Enums;

public class MimeTypeExtensionsTests
{
    [Theory]
    [InlineData(MimeType.ImageBmp, "image/bmp")]
    [InlineData(MimeType.ImageGif, "image/gif")]
    [InlineData(MimeType.ImageJpeg, "image/jpeg")]
    [InlineData(MimeType.ImagePjpeg, "image/pjpeg")]
    [InlineData(MimeType.ImagePng, "image/png")]
    [InlineData(MimeType.ImageTiff, "image/tiff")]
    [InlineData(MimeType.ImageWebp, "image/webp")]
    public void ToMimeTypeString_ReturnsMimeTypeString(MimeType mimeType, string expected) =>
        Assert.Equal(expected, mimeType.ToMimeTypeString());

    [Fact]
    public void EnumToString_ReturnsEnumName()
    {
        Assert.Equal("ImagePng", MimeType.ImagePng.ToString());
        Assert.Equal("image/png", MimeType.ImagePng.ToMimeTypeString());
    }
}
