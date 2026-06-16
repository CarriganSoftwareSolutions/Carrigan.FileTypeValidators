using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Detectors;

/// <summary>
/// Detects Java class files for deny-list scenarios.
/// </summary>
public sealed class JavaClassDetector : FileTypeDetectorBase
{
    /// <summary>
    /// The known Java class file signature.
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
            signatureFragments: new ByteSignature([0xCA, 0xFE, 0xBA, 0xBE]),
            fileExtension: new FileExtension("class")
        )
    ];

    /// <summary>
    /// MIME types commonly associated with Java class files.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes =
    [
        //https://www.digipres.org/formats/sources/wikidata/formats/#q2193155
        new("application", "x-java"),
        new("application", "java"),
        new("application", "x-java-class"),
        new("application", "x-httpd-java"),
        new("application", "java-vm"),
        new("application", "java-byte-code"),
        new("application", "x-java-vm")
    ];

    /// <summary>
    /// Gets the MIME types commonly associated with Java class files.
    /// </summary>
    public override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// Gets the known Java class file signatures.
    /// </summary>
    public override IEnumerable<FileSignature> Signatures => signatures;
}
