using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

public class ImageBmpDefinition : MimeImageTypeDefinition
{
    private static readonly string subtype = "bmp";
    private static readonly MimeType mimeTypeEnumMimeTypeEnum = Enums.MimeType.ImageBmp;
    private static readonly IEnumerable<FileType> fileTypeEnums = [FileType.Bmp];
    private static readonly IEnumerable<FileSignature>  signatures =
    [
        new (new ByteSignature([0x42, 0x4D]), new FileExtension("bmp"))
    ];

    //public override MimeName MimeName { get; init; } = new("Image", "bmp");

    protected sealed override string Subtype => subtype;
    internal sealed override MimeType MimeTypeEnum => mimeTypeEnumMimeTypeEnum;
    internal sealed override IEnumerable<FileType> FileTypeEnums => fileTypeEnums;

    internal sealed override IEnumerable<FileSignature> Signatures => signatures;
}
