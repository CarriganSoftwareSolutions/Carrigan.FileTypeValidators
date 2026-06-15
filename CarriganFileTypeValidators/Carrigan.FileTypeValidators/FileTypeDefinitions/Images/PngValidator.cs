
using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

/// <summary>
/// PNG (Portable Network Graphics) is a widely used image format that supports lossless compression and transparency.
/// </summary>
public class PngValidator : FileTypeDefinition
{
    /// <summary>
    /// The PNG file format is defined by specific byte signatures at the beginning and end of the file.
    /// </summary>
    private static readonly IEnumerable<FileSignature> signatures =
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
                new ByteSignature([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]),
                new ByteTrailer([0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82])
            ]
            ,  new FileExtension("png")
        )

    ];

    /// <summary>
    /// The MIME type associated with PNG files is "image/png", which indicates that the file is an image in the PNG format.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "png")];

    /// <summary>
    /// The MimeTypes property returns the collection of MIME types associated with PNG files, which includes "image/png" to 
    /// indicate that the file is an image in the PNG format.
    /// </summary>
    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// The Signatures property returns the collection of file signatures that are used to identify PNG files based on their byte 
    /// patterns at the beginning and end of the file.
    /// </summary>
    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
