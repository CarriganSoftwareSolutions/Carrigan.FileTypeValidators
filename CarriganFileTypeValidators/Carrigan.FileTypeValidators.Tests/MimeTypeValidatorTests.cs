using Carrigan.FileTypeValidators;
using Carrigan.FileTypeValidators.Enums;

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

    private static readonly string bmpMime = @"image/bmp";
    private static readonly string gifMime = @"image/gif";
    private static readonly string jpgMime = @"image/jpeg";
    private static readonly string jfifMime = @"image/pjpeg";
    private static readonly string pngMime = @"image/png";
    private static readonly string tiffMime = @"image/tiff";
    private static readonly string webpMime = @"image/webp";

    private static bool FileTypeTest(byte[] data, string fileExtension, string mimeData, params FileType[] ftp)
    {
        MimeTypeValidator validator = new(ftp);
        return validator.IsValid(data, mimeData, fileExtension);
    }

    private static bool MimeTypeTest(byte[] data, string fileExtension, string mimeData, params MimeType[] mime)
    {
        MimeTypeValidator validator = new(mime);
        return validator.IsValid(data, mimeData, fileExtension);
    }

    [Fact]
    public void ByteFileTest() =>
        Assert.True(FileTypeTest(bmpData, "bmp", bmpMime, FileType.Bmp));

    [Fact]
    public void GifFileTest() =>
        Assert.True(FileTypeTest(gifData, ".gif", gifMime, FileType.Gif));

    [Fact]
    public void JpgFileTest() =>
        Assert.True(FileTypeTest(jpgData, "jpg", jpgMime, FileType.Jpg));

    [Fact]
    public void JfifFileTest() =>
        Assert.True(FileTypeTest(jfifData, ".jfif", jfifMime, FileType.Jfif));

    [Fact]
    public void PngFileTest() =>
        Assert.True(FileTypeTest(pngData, "png", pngMime, FileType.Png));

    [Fact]
    public void TiffFileTest() =>
        Assert.True(FileTypeTest(tiffData, ".tiff", tiffMime, FileType.Tiff));

    [Fact]
    public void TifFileTest() =>
        Assert.True(FileTypeTest(tiffData, ".tif", tiffMime, FileType.Tiff));

    [Fact]
    public void WebpFileTest() =>
        Assert.True(FileTypeTest(webpData, "webp", webpMime, FileType.Webp));

    [Fact]
    public void ByteFileTestFail() =>
        Assert.False(FileTypeTest(bmpData, ".sdf", bmpMime, FileType.Bmp));

    [Fact]
    public void GifFileTestFail() =>
        Assert.False(FileTypeTest(jpgData, "gif", gifMime, FileType.Gif));

    [Fact]
    public void JpgFileTestFail() =>
        Assert.False(FileTypeTest(jpgData, ".jpg", jpgMime, FileType.Gif));

    [Fact]
    public void JfifFileTestFail() =>
        Assert.False(FileTypeTest(tiffData, "jfif", jfifMime, FileType.Jfif));

    [Fact]
    public void PngFileTestFail() =>
        Assert.False(FileTypeTest(pngData, ".gif", pngMime, FileType.Png));

    [Fact]
    public void TiffFileTestFail() =>
        Assert.False(FileTypeTest(tiffData, "tiff", bmpMime, FileType.Tiff));

    [Fact]
    public void WebpFileTestFail() =>
        Assert.False(FileTypeTest(webpData, ".webp", webpMime, FileType.Jpeg));

    [Fact]
    public void BmpMimeTest() =>
        Assert.True(MimeTypeTest(bmpData, ".bmp", bmpMime, MimeType.ImageBmp));

    [Fact]
    public void GifMimeTest() =>
        Assert.True(MimeTypeTest(gifData, "gif", gifMime, MimeType.ImageGif));

    [Fact]
    public void JpgMimeTest() =>
        Assert.True(MimeTypeTest(jpgData, ".jpg", jpgMime, MimeType.ImageJpeg));

    [Fact]
    public void JfifMimeTest() =>
        Assert.True(MimeTypeTest(jfifData, "jfif", jfifMime, MimeType.ImagePjpeg));

    [Fact]
    public void PngMimeTest() =>
        Assert.True(MimeTypeTest(pngData, ".png", pngMime, MimeType.ImagePng));

    [Fact]
    public void TiffMimeTest() =>
        Assert.True(MimeTypeTest(tiffData, ".tiff", tiffMime, MimeType.ImageTiff));

    [Fact]
    public void TifMimeTest() =>
        Assert.True(MimeTypeTest(tiffData, ".tif", tiffMime, MimeType.ImageTiff));

    [Fact]
    public void WebpMimeTest() =>
        Assert.True(MimeTypeTest(webpData, "webp", webpMime, MimeType.ImageWebp));
}
