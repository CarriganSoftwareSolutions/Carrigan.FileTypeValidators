using Carrigan.FileTypeValidators.Signatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;

public abstract class Office97ValidatorBase : FileTypeValidatorBase
{
    // https://www.garykessler.net/library/file_sigs_GCK_latest.html
    //
    // GCK File Signatures Table copyright © 2002-2026 Gary C. Kessler.
    // Used with attribution. This project does not redistribute, vendor, scrape,
    // bulk-import, or mechanically translate the GCK File Signatures Table.
    internal static readonly ByteSignature LeadingBytes = new([0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1]);
}
