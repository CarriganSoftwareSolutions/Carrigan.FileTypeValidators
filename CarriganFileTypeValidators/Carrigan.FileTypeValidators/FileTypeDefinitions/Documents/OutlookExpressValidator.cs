using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class OutlookExpressValidator : FileTypeValidatorBase
{
    private static readonly FileExtension FileExtension = new("eml");

    // File signature information for this validator was researched using Gary C. Kessler's
    // GCK File Signatures Table:
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    private static readonly ByteSignature[] LeadingBytes =
    [
        new([0x46, 0x72, 0x6F, 0x6D, 0x20, 0x20, 0x20]),
        new([0x46, 0x72, 0x6F, 0x6D, 0x20, 0x3F, 0x3F, 0x3F]),
        new([0x46, 0x72, 0x6F, 0x6D, 0x3A, 0x20]),
    ];

    // MIME type reference: https://www.w3.org/Protocols/rfc1341/7_3_Message.html
    private static readonly MimeType[] _MimeTypes = [new("message/rfc822")];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
        LeadingBytes.Select(leader => new FileSignature(leader, FileExtension));
}
