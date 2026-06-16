using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class Publisher97ValidatorTests : OleCompoundDocumentValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new Publisher97Validator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(
            CreateOleCompoundDocumentSample([0xFD, 0xFF, 0xFF, 0xFF, 0x02]),
            [],
            new("pub"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/x-mspublisher")
    ];
}
