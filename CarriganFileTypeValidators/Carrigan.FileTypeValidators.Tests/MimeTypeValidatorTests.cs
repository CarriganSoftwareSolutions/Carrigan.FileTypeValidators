
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Images;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.Tests;

public class MimeTypeValidatorTests
{
    private static readonly byte[] bmpData = "BM"u8.ToArray();
    private static readonly byte[] gifData = [0x47, 0x49, 0x46, 0x38, 0x37, 0x61, 0x00, 0x3B];
    private static readonly byte[] jpgData = [0xFF, 0xD8, 0xFF, 0xD9];
    private static readonly byte[] jfifData = [0xFF, 0xD8, 0xFF, 0xE0, 0xFF, 0xD9];
    private static readonly byte[] pngData = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82];
    private static readonly byte[] tiffData = [0x49, 0x49, 0x2A, 0x00];
    private static readonly byte[] webpData = [0x52, 0x49, 0x46, 0x46, 0x06, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50];

    private static readonly MimeType bmpMime = new ("image", "bmp");
    private static readonly MimeType gifMime = new ("image", "gif");
    private static readonly MimeType jpgMime = new ("image", "jpeg");
    private static readonly MimeType jfifMime = new ("image", "pjpeg");
    private static readonly MimeType pngMime = new ("image", "png");
    private static readonly MimeType tiffMime = new ("image", "tiff");
    private static readonly MimeType webpMime = new ("image", "webp");

    private static bool FileTypeTest(byte[] data, string fileExtension, MimeType mimeData, FileTypeValidatorBase fileTypeDefinition)
    {
        FileTypeValidator validator = new([fileTypeDefinition]);
        return validator.IsValid(data, mimeData, new(fileExtension));
    }

    [Fact]
    public void ByteFileTest() =>
        Assert.True(FileTypeTest(bmpData, "bmp", bmpMime, new BitmapValidator()));

    [Fact]
    public void GifFileTest() =>
        Assert.True(FileTypeTest(gifData, ".gif", gifMime, new GifValidator()));

    [Fact]
    public void JpgFileTest() =>
        Assert.True(FileTypeTest(jpgData, "jpg", jpgMime, new JpegValidator()));

    [Fact]
    public void JfifFileTest() =>
        Assert.True(FileTypeTest(jfifData, ".jfif", jfifMime, new JpegValidator()));

    [Fact]
    public void PngFileTest() =>
        Assert.True(FileTypeTest(pngData, "png", pngMime, new PngValidator()));

    [Fact]
    public void TiffFileTest() =>
        Assert.True(FileTypeTest(tiffData, ".tiff", tiffMime, new TiffValidator()));

    [Fact]
    public void TifFileTest() =>
        Assert.True(FileTypeTest(tiffData, ".tif", tiffMime, new TiffValidator()));

    [Fact]
    public void WebpFileTest() =>
        Assert.True(FileTypeTest(webpData, "webp", webpMime, new WebpValidator()));

    [Fact]
    public void ByteFileTestFail() =>
        Assert.False(FileTypeTest(bmpData, ".sdf", bmpMime, new BitmapValidator()));

    [Fact]
    public void GifFileTestFail() =>
        Assert.False(FileTypeTest(jpgData, "gif", gifMime, new GifValidator()));

    [Fact]
    public void JpgFileTestFail() =>
        Assert.False(FileTypeTest(jpgData, ".jpg", jpgMime, new GifValidator()));
    [Fact]
    public void JfifFileTestFail() =>
        Assert.False(FileTypeTest(tiffData, "jfif", jfifMime, new JpegValidator()));

    [Fact]
    public void PngFileTestFail() =>
        Assert.False(FileTypeTest(pngData, ".gif", pngMime, new PngValidator()));

    [Fact]
    public void TiffFileTestFail() =>
        Assert.False(FileTypeTest(tiffData, "tiff", bmpMime, new TiffValidator()));

    [Fact]
    public void WebpFileTestFail() =>
        Assert.False(FileTypeTest(webpData, ".webp", webpMime, new JpegValidator()));
}
