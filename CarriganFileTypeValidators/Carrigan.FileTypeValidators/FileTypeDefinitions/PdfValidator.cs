using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions;

public class PdfValidator : FileTypeValidatorBase
{
    // File signature information for this validator was researched using Gary C. Kessler's
    // GCK File Signatures Table:
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    //
    // MIME type reference for application/pdf:
    // https://www.iana.org/assignments/media-types/application/pdf
    //
    // The registered application/pdf media type documents the magic number as "%PDF-" followed by the PDF version.
    private static readonly ByteSignature Leader = new([0x25, 0x50, 0x44, 0x46, 0x2D]);

    internal static readonly ByteTrailer[] Trailers =
    [
        new([0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46]),
        new([0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46, 0x0A]),
        new([0x0D, 0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46, 0x0D, 0x0A]),
        new([0x0D, 0x25, 0x25, 0x45, 0x4F, 0x46, 0x0D]),
    ];

    // MIME type references:
    // https://www.iana.org/assignments/media-types/application/pdf
    // https://mimeapplication.net/x-pdf
    // https://mimeapplication.net/applications-vnd-pdf
    // https://mimeapplication.net/acrobat
    // https://mimeapplication.net/text-pdf
    // https://mimeapplication.net/text-x-pdf
    private static readonly MimeType[] _MimeTypes =
    [
        new("application/pdf"),
        new("application/x-pdf"),
        new("application/vnd.pdf"),
        new("application/acrobat"),
        new("text/pdf"),
        new("text/x-pdf")
    ];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
        Trailers.Select(trailer => new FileSignature([Leader, trailer], new FileExtension("pdf")));
}
