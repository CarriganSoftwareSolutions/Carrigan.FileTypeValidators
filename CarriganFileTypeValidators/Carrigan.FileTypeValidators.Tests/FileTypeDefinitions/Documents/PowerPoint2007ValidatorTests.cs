using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class PowerPoint2007ValidatorTests : Office2007DocumentValidatorTestBase
{
    private static readonly FileExtension FileExtension = new("pptx");

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new PowerPoint2007Validator();

    protected override FileTypeValidatorBase PasswordProtectedValidatorDefinition =>
        new PowerPoint2007Validator(allowPasswordProtectedFiles: true);

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(Office2007LeadingBytes, Office2007TrailingBytes, FileExtension)
    ];

    protected override IEnumerable<SingleSample> PasswordProtectedSamples =>
    [
        new(
            CreateOleCompoundDocumentSample([0xFD, 0xFF, 0xFF, 0xFF, null, null, 0x00, 0x00]),
            [],
            FileExtension),
        new(
            CreateOleCompoundDocumentSample([0x00, 0x6E, 0x1E, 0xF0]),
            [],
            FileExtension),
        new(
            CreateOleCompoundDocumentSample([0x0F, 0x00, 0xE8, 0x03]),
            [],
            FileExtension),
        new(
            CreateOleCompoundDocumentSample([0xA0, 0x46, 0x1D, 0xF0]),
            [],
            FileExtension)
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/vnd.openxmlformats-officedocument.presentationml.presentation"),
        new("application/vnd.ms-powerpoint")
    ];
}
