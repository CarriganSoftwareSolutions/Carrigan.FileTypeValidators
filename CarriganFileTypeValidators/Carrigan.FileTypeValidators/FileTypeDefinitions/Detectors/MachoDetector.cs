using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;

/// <summary>
/// Detects Mach-O files for deny-list scenarios.
/// </summary>
public sealed class MachoDetector : FileTypeDetectorBase
{
    private static readonly IEnumerable<FileExtension> fileExtensions =
    [
        new FileExtension("dylib"),
        new FileExtension("bundle"),
        new FileExtension("o")
    ];

    /// <summary>
    /// The known Mach-O file signatures.
    /// </summary>
    private static readonly IEnumerable<FileSignature> signatures =
    [
        //https://github.com/apple/darwin-xnu/blob/main/EXTERNAL_HEADERS/mach-o/loader.h
        new
        (
            signatureFragments: new ByteSignature([0xFE, 0xED, 0xFA, 0xCE]),
            fileExtensions: fileExtensions
        ),
        new
        (
            signatureFragments: new ByteSignature([0xCE, 0xFA, 0xED, 0xFE]),
            fileExtensions: fileExtensions
        ),
        new
        (
            signatureFragments: new ByteSignature([0xFE, 0xED, 0xFA, 0xCF]),
            fileExtensions: fileExtensions
        ),
        new
        (
            signatureFragments: new ByteSignature([0xCF, 0xFA, 0xED, 0xFE]),
            fileExtensions: fileExtensions
        ),
    ];

    /// <summary>
    /// Gets the MIME types commonly associated with Mach-O files.
    /// No MIME types are defined because this detector is intended to use signatures and extensions only.
    /// </summary>
    public override IEnumerable<MimeType> MimeTypes => [new("application/x-mach-binary")];

    /// <summary>
    /// Gets the known Mach-O file signatures.
    /// </summary>
    public override IEnumerable<FileSignature> Signatures => signatures;
}
