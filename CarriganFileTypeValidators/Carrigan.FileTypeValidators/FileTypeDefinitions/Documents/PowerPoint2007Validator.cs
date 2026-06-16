using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class PowerPoint2007Validator : Office2007ValidatorBase
{
    private bool AllowPasswordProtectedFiles { get; init; }

    public PowerPoint2007Validator(bool allowPasswordProtectedFiles = false) =>
        AllowPasswordProtectedFiles = allowPasswordProtectedFiles;

    private static readonly FileExtension FileExtension = new("pptx");

    // MIME type references:
    // https://www.iana.org/assignments/media-types/application/vnd.openxmlformats-officedocument.presentationml.presentation
    // https://www.iana.org/assignments/media-types/application/vnd.ms-powerpoint
    private static readonly MimeType[] _MimeTypes =
    [
        new("application/vnd.openxmlformats-officedocument.presentationml.presentation"),
        new("application/vnd.ms-powerpoint")
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
                .. PowerPoint97Validator
                    .SubHeaders.Select(subHeader => new FileSignature([PowerPoint97Validator.LeadingBytes, .. subHeader], FileExtension)),
            ]
            : [new([LeadingBytes, TrailingBytes], FileExtension)];
}
