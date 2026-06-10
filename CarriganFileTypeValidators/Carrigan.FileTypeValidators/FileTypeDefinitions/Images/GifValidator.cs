using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

public class GifValidator : FileTypeDefinition
{
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new([new ByteSignature([0x47, 0x49, 0x46, 0x38, 0x37, 0x61]), new ByteTrailer([0x00, 0x3B])], new FileExtension("gif")),
        new([new ByteSignature([0x47, 0x49, 0x46, 0x38, 0x39, 0x61]), new ByteTrailer([0x00, 0x3B])], new FileExtension("gif"))
    ];
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "gif")];

    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
