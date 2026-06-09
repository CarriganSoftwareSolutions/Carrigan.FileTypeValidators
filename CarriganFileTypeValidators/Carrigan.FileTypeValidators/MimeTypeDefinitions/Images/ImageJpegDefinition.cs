using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

internal class ImageJpegDefinition : MimeImageTypeDefinition
{
    private static readonly string subtype = "jpeg";
    private static readonly MimeType mimeTypeEnumMimeTypeEnum = Enums.MimeType.ImageJpeg;
    private static readonly IEnumerable<FileType> fileTypeEnums = [FileType.Jpe, FileType.Jpg, FileType.Jpeg, FileType.Jfif];
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new(new ByteSignature([0xFF, 0xD8]), new ByteTrailer([0xFF, 0xD9]),  ["jpe", "jpeg", "jpg"]),
        new(new ByteSignature([0xFF, 0xD8, 0xFF, 0xE0]), new ByteTrailer([0xFF, 0xD9]), "jfif"),
        new(new ByteSignature([0xFF, 0xD8, 0xFF, 0xE1]), new ByteTrailer([0xFF, 0xD9]),  "jpg"),
        new(new ByteSignature([0xFF, 0xD8, 0xFF, 0xE8]), new ByteTrailer([0xFF, 0xD9]),  "jpg"),

    ];

    protected sealed override string Subtype => subtype;
    internal sealed override MimeType MimeTypeEnum => mimeTypeEnumMimeTypeEnum;
    internal sealed override IEnumerable<FileType> FileTypeEnums => fileTypeEnums;
    internal sealed override IEnumerable<FileSignature> Signatures => signatures;
}
