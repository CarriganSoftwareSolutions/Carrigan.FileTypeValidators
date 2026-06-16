using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public abstract class OleCompoundDocumentValidatorTestBase : DocumentValidatorTestBase
{
    private const int OleSubheaderOffset = 0x200;

    private static readonly byte?[] OleCompoundFileHeader =
    [
        0xD0,
        0xCF,
        0x11,
        0xE0,
        0xA1,
        0xB1,
        0x1A,
        0xE1
    ];

    protected static byte?[] CreateOleCompoundDocumentSample(byte?[] subheader) =>
    [
        .. OleCompoundFileHeader,
        .. XNulls(OleSubheaderOffset - OleCompoundFileHeader.Length),
        .. subheader
    ];
}
