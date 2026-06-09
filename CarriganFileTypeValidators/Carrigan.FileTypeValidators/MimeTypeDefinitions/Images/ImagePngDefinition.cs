using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

internal class ImagePngDefinition : MimeImageTypeDefinition
{
    private static readonly string subtype = "png";
    private static readonly MimeType mimeTypeEnumMimeTypeEnum = Enums.MimeType.ImagePng;
    private static readonly IEnumerable<FileType> fileTypeEnums = [FileType.Png];
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new(new ByteSignature([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]), new ByteTrailer([0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82]),  "png")

    ];

    protected sealed override string Subtype => subtype;
    internal sealed override MimeType MimeTypeEnum => mimeTypeEnumMimeTypeEnum;
    internal sealed override IEnumerable<FileType> FileTypeEnums => fileTypeEnums;
    internal sealed override IEnumerable<FileSignature> Signatures => signatures;
}
