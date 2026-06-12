using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

public class JpegValidator : FileTypeDefinition
{
    private static readonly IEnumerable<FileSignature> signatures =
    [
        new([new ByteSignature([0xFF, 0xD8]), new ByteTrailer([0xFF, 0xD9])],  [new FileExtension("jpe"), new FileExtension("jpeg"), new FileExtension("jpg")]),
        new([new ByteSignature([0xFF, 0xD8, 0xFF, 0xE0]), new ByteTrailer([0xFF, 0xD9])], new FileExtension("jfif")),
    ];
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "jpeg"), new("image", "pjpeg")];

    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
