using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class Excel2007ValidatorTests : Office2007DocumentValidatorTestBase
{
    private static readonly FileExtension FileExtension = new("xlsx");

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new Excel2007Validator();

    protected override FileTypeValidatorBase PasswordProtectedValidatorDefinition =>
        new Excel2007Validator(allowPasswordProtectedFiles: true);

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(Office2007LeadingBytes, Office2007TrailingBytes, FileExtension)
    ];

    protected override IEnumerable<SingleSample> PasswordProtectedSamples =>
    [
        new(
            CreateOleCompoundDocumentSample([0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00]),
            [],
            FileExtension)
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
        new("application/vnd.ms-excel")
    ];
}
