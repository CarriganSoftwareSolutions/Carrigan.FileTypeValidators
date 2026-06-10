using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

public class TiffValidator : FileTypeDefinition
{
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new(new ByteSignature([0x49, 0x49, 0x2A, 0x00]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x4D, 0x4D, 0x00, 0x2A]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x49, 0x49, 0x2B, 0x00]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x4D, 0x4D, 0x00, 0x2B]), [new FileExtension("tif"), new FileExtension("tiff")])
    ];
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "tiff")];

    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
