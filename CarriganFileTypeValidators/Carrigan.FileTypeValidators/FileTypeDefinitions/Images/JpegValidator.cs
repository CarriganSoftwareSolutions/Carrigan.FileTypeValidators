using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Images;

/// <summary>
/// JPEG (Joint Photographic Experts Group) is a widely used image format that supports lossy compression.
/// It is commonly used for photographs and complex images due to its ability to reduce file size while maintaining acceptable image quality. 
/// JPEG files typically have the extensions .jpg, .jpeg, or .jpe.
/// </summary>
public class JpegValidator : FileTypeValidatorBase
{
    /// <summary>
    /// The JPEG file format is defined by specific byte signatures at the beginning and end of the file.
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
        new([new ByteSignature([0xFF, 0xD8]), new ByteTrailer([0xFF, 0xD9])],  [new FileExtension("jpe"), new FileExtension("jpeg"), new FileExtension("jpg")]),
        new([new ByteSignature([0xFF, 0xD8, 0xFF, 0xE0]), new ByteTrailer([0xFF, 0xD9])], new FileExtension("jfif")),
    ];

    /// <summary>
    /// The MIME types associated with JPEG files include "image/jpeg" for standard JPEG files and "image/pjpeg" for progressive JPEG files,
    /// which are a variant of the JPEG format that allows for incremental loading of images.
    /// </summary>
    private static readonly IEnumerable<MimeType> mimeTypes = [new("image", "jpeg"), new("image", "pjpeg")];

    /// <summary>
    /// The MimeTypes property returns the collection of MIME types associated with JPEG files, which includes both standard and progressive JPEG formats.
    /// </summary>
    public sealed override IEnumerable<MimeType> MimeTypes => mimeTypes;

    /// <summary>
    /// The Signatures property returns the collection of file signatures that are used to identify JPEG files based on their byte 
    /// patterns at the beginning and end of the file.
    /// </summary>
    public sealed override IEnumerable<FileSignature> Signatures => signatures;
}
