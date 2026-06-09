using Carrigan.FileTypeValidators.Abstracts;
using Carrigan.FileTypeValidators.Enums;
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.MimeTypeDefinitions.Images;

internal class ImageWebpDefinition : MimeImageTypeDefinition
{
    protected sealed override string Subtype => "webp";
    internal sealed override MimeType MimeTypeEnum => Enums.MimeType.ImageWebp;
    internal sealed override IEnumerable<FileType> FileTypeEnums => [FileType.Webp];

    internal override IEnumerable<FileSignature> Signatures =>
    [
        new(new ByteSignature("RIFF"u8.ToArray()), "webp")

    ];
}
