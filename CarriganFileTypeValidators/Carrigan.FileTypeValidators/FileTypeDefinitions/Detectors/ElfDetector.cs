using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;

/// <summary>
/// Detects Executable and Linkable Format (ELF) files for deny-list scenarios.
/// </summary>
public sealed class ElfDetector : FileTypeDetectorBase
{
    /// <summary>
    /// The known ELF file signature.
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
            signatureFragments: new ByteSignature([0x7F, 0x45, 0x4C, 0x46]),
            fileExtension: new FileExtension("elf")
        )
    ];

    /// <summary>
    /// MIME types commonly associated with ELF files.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes =
    [
        //https://www.digipres.org/formats/sources/tika/formats/#application/x-elf
        new("application/x-elf")
    ];

    /// <summary>
    /// Gets the MIME types commonly associated with ELF files.
    /// </summary>
    public override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// Gets the known ELF file signatures.
    /// </summary>
    public override IEnumerable<FileSignature> Signatures => signatures;
}
