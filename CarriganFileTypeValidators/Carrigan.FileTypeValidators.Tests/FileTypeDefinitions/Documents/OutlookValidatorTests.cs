using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class OutlookValidatorTests : OleCompoundDocumentValidatorTestBase
{
    protected override FileTypeValidatorBase ValidatorDefinition =>
        new OutlookValidator();

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(
            CreateOleCompoundDocumentSample([0x52, 0x00, 0x6F, 0x00, 0x6F, 0x00, 0x74, 0x00, 0x20, 0x00, 0x45, 0x00, 0x6E, 0x00, 0x74, 0x00, 0x72, 0x00, 0x79, 0x00]),
            [],
            new("msg"))
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/vnd.ms-outlook")
    ];
}
