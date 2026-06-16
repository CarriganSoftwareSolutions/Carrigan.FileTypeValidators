using Carrigan.FileTypeValidators.Signatures;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public abstract class Office2007ValidatorBase : FileTypeValidatorBase
{
    // File signature information for this validator was researched using Gary C. Kessler's
    // GCK File Signatures Table:
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    protected static readonly ByteSignature LeadingBytes = new([0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00]);
    protected static readonly ByteTrailer TrailingBytes = new([0x50, 0x4B, 0x05, 0x06], 18);
}
