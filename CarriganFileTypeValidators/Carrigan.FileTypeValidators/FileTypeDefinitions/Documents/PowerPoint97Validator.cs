using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class PowerPoint97Validator : Office97ValidatorBase
{
    private const int OleSubheaderOffset = 0x200;

    private static readonly FileExtension[] FileExtensions =
    [
        new("ppt"),
        new("pot"),
        new("pps"),
        new("ppa")
    ];

    // File signature information for this validator was researched using Gary C. Kessler's
    // GCK File Signatures Table:
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    internal static readonly ISignatureFragment[][] SubHeaders =
    [
        [
            new ByteSignature([0xFD, 0xFF, 0xFF, 0xFF], OleSubheaderOffset),
            new ByteSignature([0x00, 0x00], OleSubheaderOffset + 6)
        ],
        [new ByteSignature([0x00, 0x6E, 0x1E, 0xF0], OleSubheaderOffset)],
        [new ByteSignature([0x0F, 0x00, 0xE8, 0x03], OleSubheaderOffset)],
        [new ByteSignature([0xA0, 0x46, 0x1D, 0xF0], OleSubheaderOffset)]
    ];

    // MIME type reference: https://www.iana.org/assignments/media-types/application/vnd.ms-powerpoint
    private static readonly MimeType[] _MimeTypes = [new("application/vnd.ms-powerpoint")];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
        SubHeaders.Select(subHeader => new FileSignature([LeadingBytes, .. subHeader], FileExtensions));
}
