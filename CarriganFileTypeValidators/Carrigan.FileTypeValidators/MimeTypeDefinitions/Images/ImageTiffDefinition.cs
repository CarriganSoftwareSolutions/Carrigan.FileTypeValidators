using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

internal class ImageTiffDefinition : MimeImageTypeDefinition
{
    private static readonly string subtype = new FileExtension("tiff");
    private static readonly MimeType mimeTypeEnum = Enums.MimeType.ImageTiff;
    private static readonly IEnumerable<FileType> fileTypeEnums = [FileType.Tiff];
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new(new ByteSignature([0x49, 0x49, 0x2A, 0x00]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x4D, 0x4D, 0x00, 0x2A]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x49, 0x49, 0x2B, 0x00]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x4D, 0x4D, 0x00, 0x2B]), [new FileExtension("tif"), new FileExtension("tiff")])
    ];

    protected sealed override string Subtype => subtype;
    internal sealed override MimeType MimeTypeEnum => mimeTypeEnum;
    internal sealed override IEnumerable<FileType> FileTypeEnums => fileTypeEnums;
    internal sealed override IEnumerable<FileSignature> Signatures => signatures;
}
