
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

public class PngValidator : FileTypeDefinition
{
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new
        (
            [
                new ByteSignature([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]),
                new ByteTrailer([0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82])
            ]
            ,  new FileExtension("png")
        )

    ];
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "png")];

    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
