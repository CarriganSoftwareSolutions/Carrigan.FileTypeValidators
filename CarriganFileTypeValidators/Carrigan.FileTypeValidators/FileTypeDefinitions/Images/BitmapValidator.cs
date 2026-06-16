using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

/// <summary>
/// Validator for Bitmap (BMP) image files.
/// </summary>
public class BitmapValidator : FileTypeValidatorBase
{
    /// <summary>
    /// Defines the file signatures and MIME types associated with Bitmap (BMP) image files.
    /// </summary>
    private static readonly IEnumerable<FileSignature>  signatures =
    [
        // File signature information for this validator was researched using Gary C. Kessler's
        // GCK File Signatures Table:
        // https://www.garykessler.net/library/file_sigs_GCK_latest.html
        //
        // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
        // Used with attribution. This project does not redistribute, vendor, scrape,
        // bulk-import, or mechanically translate the GCK File Signatures Table.
        new (new ByteSignature([0x42, 0x4D]), new FileExtension("bmp"))
    ];

    /// <summary>
    /// Defines the MIME type associated with Bitmap (BMP) image files.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "bmp")];

    /// <summary>
    /// Gets the MIME types associated with Bitmap (BMP) image files.
    /// </summary>
    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// Gets the file signatures associated with Bitmap (BMP) image files.
    /// </summary>
    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
