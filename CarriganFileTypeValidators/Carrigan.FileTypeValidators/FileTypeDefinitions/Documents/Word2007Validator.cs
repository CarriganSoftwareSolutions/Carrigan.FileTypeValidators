using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public class Word2007Validator : Office2007ValidatorBase
{
    private bool AllowPasswordProtectedFiles { get; init; }

    public Word2007Validator(bool allowPasswordProtectedFiles = false) =>
        AllowPasswordProtectedFiles = allowPasswordProtectedFiles;

    private static readonly FileExtension FileExtension = new("docx");

    // MIME type references:
    // https://www.iana.org/assignments/media-types/application/vnd.openxmlformats-officedocument.wordprocessingml.document
    // https://www.iana.org/assignments/media-types/application/msword
    private static readonly MimeType[] _MimeTypes =
    [
        new("application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
        new("application/msword")
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
                new([Word97Validator.LeadingBytes, Word97Validator.Subheader], FileExtension)
            ]
            : [new([LeadingBytes, TrailingBytes], FileExtension)];
}
