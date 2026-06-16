using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class OutlookValidator : Office97ValidatorBase
{
    private const int OleSubheaderOffset = 0x200;

    private static readonly FileExtension FileExtension = new("msg");

    // File signature information for this validator was researched using Gary C. Kessler's
    // GCK File Signatures Table:
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    private static readonly ByteSignature Subheader = new(
        [0x52, 0x00, 0x6F, 0x00, 0x6F, 0x00, 0x74, 0x00, 0x20, 0x00, 0x45, 0x00, 0x6E, 0x00, 0x74, 0x00, 0x72, 0x00, 0x79, 0x00],
        OleSubheaderOffset);

    // MIME type reference: https://www.loc.gov/preservation/digital/formats/fdd/fdd000379.shtml
    private static readonly MimeType[] _MimeTypes = [new("application/vnd.ms-outlook")];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
    [
        new([LeadingBytes, Subheader], FileExtension)
    ];
}
