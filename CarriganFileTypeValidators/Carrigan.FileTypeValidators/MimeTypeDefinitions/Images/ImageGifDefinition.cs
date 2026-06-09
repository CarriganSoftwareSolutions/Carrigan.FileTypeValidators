using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

internal class ImageGifDefinition : MimeImageTypeDefinition
{
    private static readonly string subtype = "gif";
    private static readonly MimeType mimeTypeEnumMimeTypeEnum = Enums.MimeType.ImageGif;
    private static readonly IEnumerable<FileType> fileTypeEnums = [FileType.Gif];
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new([new ByteSignature([0x47, 0x49, 0x46, 0x38, 0x37, 0x61]), new ByteTrailer([0x00, 0x3B])], new FileExtension("gif")),
        new([new ByteSignature([0x47, 0x49, 0x46, 0x38, 0x39, 0x61]), new ByteTrailer([0x00, 0x3B])], new FileExtension("gif"))
    ];

    protected sealed override string Subtype => subtype;
    internal sealed override MimeType MimeTypeEnum => mimeTypeEnumMimeTypeEnum;
    internal sealed override IEnumerable<FileType> FileTypeEnums => fileTypeEnums;
    internal sealed override IEnumerable<FileSignature> Signatures => signatures;
}
