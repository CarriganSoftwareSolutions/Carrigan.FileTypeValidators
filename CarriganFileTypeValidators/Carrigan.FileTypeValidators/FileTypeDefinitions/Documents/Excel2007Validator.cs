using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class Excel2007Validator : Office2007ValidatorBase
{
    private bool AllowPasswordProtectedFiles { get; init; }

    public Excel2007Validator(bool allowPasswordProtectedFiles = false) =>
        AllowPasswordProtectedFiles = allowPasswordProtectedFiles;

    private static readonly FileExtension FileExtension = new("xlsx");

    // MIME type references:
    // https://www.iana.org/assignments/media-types/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
    // https://www.iana.org/assignments/media-types/application/vnd.ms-excel
    private static readonly MimeType[] _MimeTypes =
    [
        new("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
        new("application/vnd.ms-excel")
    ];

    public override IEnumerable<MimeType> MimeTypes => _MimeTypes;

    public override IEnumerable<FileSignature> Signatures =>
        AllowPasswordProtectedFiles
            ?
            [
                new([LeadingBytes, TrailingBytes], FileExtension),

                // File signature information for this validator was researched using Gary C. Kessler's
                // GCK File Signatures Table:
                // https://www.garykessler.net/library/file_sigs_GCK_latest.html
                //
                // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
                // Used with attribution. This project does not redistribute, vendor, scrape,
                // bulk-import, or mechanically translate the GCK File Signatures Table.
                //
                // According to Dr. Kessler, the 2007 Office documents use the 97 encoding for password-protected files.
                new([Excel97Validator.LeadingBytes, Excel97Validator.Subheader], FileExtension)
            ]
            : [new([LeadingBytes, TrailingBytes], FileExtension)];
}
