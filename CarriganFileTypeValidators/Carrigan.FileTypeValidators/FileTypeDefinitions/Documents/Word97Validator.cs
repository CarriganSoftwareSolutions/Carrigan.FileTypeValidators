using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class Word97Validator : Office97ValidatorBase
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
    internal static readonly ByteSignature Subheader = new([0xEC, 0xA5, 0xC1, 0x00], OleSubheaderOffset);

    // MIME type reference: https://www.iana.org/assignments/media-types/application/msword
    private static readonly MimeType[] _MimeTypes = [new("application/msword")];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
    [
        new([LeadingBytes, Subheader], FileExtensions)
    ];
}
