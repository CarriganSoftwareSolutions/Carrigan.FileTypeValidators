using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

public class BitmapValidator : FileTypeDefinition
{
    private static readonly IEnumerable<FileSignature>  signatures =
    [
        new (new ByteSignature([0x42, 0x4D]), new FileExtension("bmp"))
    ];
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "bmp")];

    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
