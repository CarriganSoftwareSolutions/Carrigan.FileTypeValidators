using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

public class PdfValidatorTests : DocumentValidatorTestBase
{
    private static readonly byte?[] LeadingBytes =
    [
        0x25,
        0x50,
        0x44,
        0x46,
        0x2D
    ];

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new PdfValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(LeadingBytes, [0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46], new("pdf")),
        new(LeadingBytes, [0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46, 0x0A], new("pdf")),
        new(LeadingBytes, [0x0D, 0x0A, 0x25, 0x25, 0x45, 0x4F, 0x46, 0x0D, 0x0A], new("pdf")),
        new(LeadingBytes, [0x0D, 0x25, 0x25, 0x45, 0x4F, 0x46, 0x0D], new("pdf"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/pdf"),
        new("application/x-pdf"),
        new("application/vnd.pdf"),
        new("application/acrobat"),
        new("text/pdf"),
        new("text/x-pdf")
    ];
}
