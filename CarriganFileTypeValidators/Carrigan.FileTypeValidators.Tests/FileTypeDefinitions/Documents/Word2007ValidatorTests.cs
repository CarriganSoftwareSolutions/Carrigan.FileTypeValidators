using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.FileTypeDefinitions.Documents;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public class Word2007ValidatorTests : Office2007DocumentValidatorTestBase
{
    private static readonly FileExtension FileExtension = new("docx");

    protected override FileTypeValidatorBase ValidatorDefinition =>
        new Word2007Validator();

    protected override FileTypeValidatorBase PasswordProtectedValidatorDefinition =>
        new Word2007Validator(allowPasswordProtectedFiles: true);

    protected override IEnumerable<SingleSample> Samples =>
    [
        new(Office2007LeadingBytes, Office2007TrailingBytes, FileExtension)
    ];

    protected override IEnumerable<SingleSample> PasswordProtectedSamples =>
    [
        new(
            CreateOleCompoundDocumentSample([0xEC, 0xA5, 0xC1, 0x00]),
            [],
            FileExtension)
    ];

    protected override IEnumerable<MimeType> MimeTypes =>
    [
        new("application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
        new("application/msword")
    ];
}
