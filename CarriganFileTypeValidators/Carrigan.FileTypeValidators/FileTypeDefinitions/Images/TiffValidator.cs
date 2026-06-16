using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

/// <summary>
/// TIFF (Tagged Image File Format) is a flexible image format that can store a wide range of image types, including photographs, line art, 
/// and scanned documents. It supports both lossless and lossy compression methods, making it suitable for various applications.
/// </summary>
public class TiffValidator : FileTypeValidatorBase
{
    /// <summary>
    /// The TIFF file format is defined by specific byte signatures at the beginning of the file. 
    /// </summary>
    private static readonly IEnumerable<FileSignature> signatures =
    [
        //big tiff [0x49, 0x49, 0x2B, 0x00]: https://www.loc.gov/preservation/digital/formats/fdd/fdd000328.shtml
        new(new ByteSignature([0x49, 0x49, 0x2B, 0x00]), [new FileExtension("tif"), new FileExtension("tiff")]),
        
        // File signature information for this validator was researched using Gary C. Kessler's
        // GCK File Signatures Table:
        // https://www.garykessler.net/library/file_sigs_GCK_latest.html
        //
        // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
        // Used with attribution. This project does not redistribute, vendor, scrape,
        // bulk-import, or mechanically translate the GCK File Signatures Table.
        new(new ByteSignature([0x49, 0x20, 0x49]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x49, 0x49, 0x2A, 0x00]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x4D, 0x4D, 0x00, 0x2A]), [new FileExtension("tif"), new FileExtension("tiff")]),
        new(new ByteSignature([0x4D, 0x4D, 0x00, 0x2B]), [new FileExtension("tif"), new FileExtension("tiff")]),
    ];

    /// <summary>
    /// The MIME type associated with TIFF files is "image/tiff", which indicates that the file is an image in the TIFF format.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "tiff")];

    /// <summary>
    /// The MimeTypes property returns the collection of MIME types associated with TIFF files, which includes "image/tiff" to 
    /// indicate that the file is an image in the TIFF format.
    /// </summary>
    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// The Signatures property returns the collection of file signatures that are used to identify TIFF files based on their byte
    /// patterns at the beginning of the file.
    /// </summary>
    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
