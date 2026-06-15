using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

/// <summary>
/// WebP is an image format developed by Google that provides both lossy and lossless compression.
/// </summary>
public class WebpValidator : FileTypeDefinition
{
    /// <summary>
    /// The WebP file format is defined by specific byte signatures at the beginning of the file.
    /// </summary>
    internal static readonly IEnumerable<FileSignature> signatures =
    [
        
        // File signature information for this validator was researched using Gary C. Kessler's
        // GCK File Signatures Table:
        // https://www.garykessler.net/library/file_sigs_GCK_latest.html
        //
        // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
        // Used with attribution. This project does not redistribute, vendor, scrape,
        // bulk-import, or mechanically translate the GCK File Signatures Table.
        new
        (
            [
                new ByteSignature("RIFF"u8.ToArray(), 0),
                new ByteSignature("WEBP"u8.ToArray(), 8)
            ],
            new FileExtension("webp")
        )
    ];
    /// <summary>
    /// The MIME type associated with WebP files is "image/webp", which indicates that the file is an image in the WebP format.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "webp")];

    /// <summary>
    /// The MimeTypes property returns the collection of MIME types associated with WebP files, which includes "image/webp" to 
    /// indicate that the file is an image in the WebP format.
    /// </summary>
    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// The Signatures property returns the collection of file signatures that are used to identify WebP files based on their byte
    /// patterns at the beginning of the file, which include the "RIFF" signature followed by the "WEBP" signature at specific offsets to
    /// indicate that the file is in the WebP format.
    /// </summary>
    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
