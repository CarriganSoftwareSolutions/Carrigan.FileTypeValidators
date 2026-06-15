using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

/// <summary>
/// The Graphics Interchange Format (GIF) is a bitmap image format that was developed by CompuServe in 1987. 
/// It is widely used for images on the web due to its support for animation and transparency. 
/// GIF files typically have the file extension .gif and are identified by specific byte signatures at the beginning and end of the file. 
/// The header of a GIF file starts with the ASCII characters "GIF87a" or "GIF89a", followed by a trailer that ends with the byte sequence 0x00, 0x3B.
/// These signatures allow software to recognize and validate GIF files based on their content rather than just their file extension.
/// </summary>
public class GifValidator : FileTypeDefinition
{
    /// <summary>
    /// The signatures for GIF files consist of a specific byte sequence at the beginning of the file (the header) and a specific 
    /// byte sequence at the end of the file (the trailer).
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
        new([new ByteSignature([0x47, 0x49, 0x46, 0x38, 0x37, 0x61]), new ByteTrailer([0x00, 0x3B])], new FileExtension("gif")),
        new([new ByteSignature([0x47, 0x49, 0x46, 0x38, 0x39, 0x61]), new ByteTrailer([0x00, 0x3B])], new FileExtension("gif"))
    ];

    /// <summary>
    /// The MIME type for GIF files is "image/gif". This MIME type is used to indicate that the file is an image in the GIF format,
    /// and it is commonly used in web applications and other contexts where the file type needs to be specified.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "gif")];

    /// <summary>
    /// The MimeTypes property returns the collection of MIME types associated with GIF files.
    /// </summary>
    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// The Signatures property returns the collection of file signatures that can be used to identify GIF files based on their content.
    /// </summary>
    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
