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
        new FileExtension("macho")
    ];

    /// <summary>
    /// The known Mach-O file signatures.
    /// </summary>
    private static readonly IEnumerable<FileSignature> signatures =
    [
        // https://developer.apple.com/documentation/kernel/mach_header
        new
        (
            signatureFragments: new ByteSignature("MH_CIGAM"u8),
            fileExtensions: fileExtensions
        ),
        new
        (
            signatureFragments: new ByteSignature("MH_MAGIC"u8),
            fileExtensions: fileExtensions
        )
    ];

    /// <summary>
    /// Gets the MIME types commonly associated with Mach-O files.
    /// No MIME types are defined because this detector is intended to use signatures and extensions only.
    /// </summary>
    public override IEnumerable<MimeType> MimeTypes => [];

    /// <summary>
    /// Gets the known Mach-O file signatures.
    /// </summary>
    public override IEnumerable<FileSignature> Signatures => signatures;
}
