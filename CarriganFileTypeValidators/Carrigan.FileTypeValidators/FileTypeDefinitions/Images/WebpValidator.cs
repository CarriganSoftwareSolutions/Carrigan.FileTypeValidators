using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

public class WebpValidator : FileTypeDefinition
{
    internal static readonly IEnumerable<FileSignature> signatures =
    [
        new
        (
            [
                new ByteSignature("RIFF"u8.ToArray(), 0),
                new ByteSignature("WEBP"u8.ToArray(), 8)
            ],
            new FileExtension("webp")
        )
    ];
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "webp")];

    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
