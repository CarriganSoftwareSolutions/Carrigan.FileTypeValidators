using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class Excel97ValidatorTests : OleCompoundDocumentValidatorTestBase
{
    private static readonly FileExtension[] FileExtensions =
    [
        new("xls"),
        new("xlt"),
        new("xla")
    ];

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new Excel97Validator();

    protected override IEnumerable<SingleSample> Samples =>
        FileExtensions.Select(fileExtension => new SingleSample(
            CreateOleCompoundDocumentSample([0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00]),
            [],
            fileExtension));

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/vnd.ms-excel")
    ];
}
