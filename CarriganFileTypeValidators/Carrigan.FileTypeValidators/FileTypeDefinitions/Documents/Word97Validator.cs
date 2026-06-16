using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class Word97Validator : FileTypeValidatorBase
{
    private const int OleSubheaderOffset = 0x200;

    private static readonly FileExtension[] FileExtensions =
    [
        new("doc"),
        new("dot")
    ];

    // File signature information for this validator was researched using Gary C. Kessler's
    // GCK File Signatures Table:
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    private static readonly ByteSignature LeadingBytes = new([0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1]);
    private static readonly ByteSignature Subheader = new([0xEC, 0xA5, 0xC1, 0x00], OleSubheaderOffset);

    // MIME type reference: https://www.iana.org/assignments/media-types/application/msword
    private static readonly MimeType[] _MimeTypes = [new("application/msword")];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
    [
        new([LeadingBytes, Subheader], FileExtensions)
    ];
}
