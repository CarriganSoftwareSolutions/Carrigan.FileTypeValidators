namespace Carrigan.FileTypeValidators.Enums;


public enum MimeType
{
    ImageBmp,
    ImageGif,
    ImageJpeg,
    ImagePjpeg,
    ImagePng,
    ImageTiff,
    ImageWebp
}
public static class MimeTypeExtensions
{
    public static string ToString(this MimeType mimeType) => 
        mimeType switch
        {
            MimeType.ImageBmp => "image/bmp",
            MimeType.ImageGif => "image/gif",
            MimeType.ImageJpeg => "image/jpeg",
            MimeType.ImagePjpeg => "image/pjpeg",
            MimeType.ImagePng => "image/png",
            MimeType.ImageTiff => "image/tiff",
            MimeType.ImageWebp => "image/webp",
            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, "Unsupported MIME type"),
        };
}