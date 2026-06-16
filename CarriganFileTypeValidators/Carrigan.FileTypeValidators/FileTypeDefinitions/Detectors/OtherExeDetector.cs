using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;

/// <summary>
/// Detects additional executable-style COM/SYS byte patterns for deny-list scenarios.
/// </summary>
public sealed class OtherExeDetector : FileTypeDetectorBase
{
    private static readonly IEnumerable<FileExtension> fileExtensions =
    [
        new FileExtension("com"),
        new FileExtension("sys")
    ];

    /// <summary>
    /// The known additional executable-style file signatures.
    /// </summary>
    private static readonly IEnumerable<FileSignature> signatures =
    [
        // File signature information for this detector was researched using Gary C. Kessler's
        // GCK File Signatures Table:
        // https://www.garykessler.net/library/file_sigs_GCK_latest.html
        //
        // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
        // Used with attribution. This project does not redistribute, vendor, scrape,
        // bulk-import, or mechanically translate the GCK File Signatures Table.
        new(new ByteSignature([0xE8]), fileExtensions),
        new(new ByteSignature([0xE9]), fileExtensions),
        new(new ByteSignature([0xEB]), fileExtensions),
        new(new ByteSignature([0xFF]), new FileExtension("sys")),
    ];

    /// <summary>
    /// Gets the MIME types commonly associated with these additional executable-style signatures.
    /// No MIME types are defined because this detector is intended to use signatures and extensions only.
    /// </summary>
    public override IEnumerable<MimeType> MimeTypes => [];

    /// <summary>
    /// Gets the known additional executable-style file signatures.
    /// </summary>
    public override IEnumerable<FileSignature> Signatures => signatures;
}
