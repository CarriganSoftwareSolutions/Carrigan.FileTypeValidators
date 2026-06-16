using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;

/// <summary>
/// Detects DOS MZ / Windows executable-style files for deny-list scenarios.
/// </summary>
public sealed class WindowsExeDetector : FileTypeDetectorBase
{
    /// <summary>
    /// The known MZ executable-style file signature.
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
        new
        (
            signatureFragments: new ByteSignature("MZ"u8),
            fileExtensions:
            [
                new FileExtension("COM"),
                new FileExtension("DLL"),
                new FileExtension("DRV"),
                new FileExtension("EXE"),
                new FileExtension("PIF"),
                new FileExtension("QTS"),
                new FileExtension("QTX"),
                new FileExtension("SYS"),
                new FileExtension("ACM"),
                new FileExtension("AX"),
                new FileExtension("CPL"),
                new FileExtension("FON"),
                new FileExtension("OCX"),
                new FileExtension("OLB"),
                new FileExtension("SCR"),
                new FileExtension("VBX"),
                new FileExtension("VXD"),
                new FileExtension("386")
            ]
        )
    ];

    /// <summary>
    /// MIME types commonly associated with Windows executable-style files.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes =
    [
        new("application", "vnd.microsoft.portable-executable"),
        new("application", "x-msdownload")
    ];

    /// <summary>
    /// Gets the MIME types commonly associated with Windows executable-style files.
    /// </summary>
    public override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// Gets the known Windows executable-style file signatures.
    /// </summary>
    public override IEnumerable<FileSignature> Signatures => signatures;
}
