using Carrigan.FileTypeValidators;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public abstract class Office2007DocumentValidatorTestBase : OleCompoundDocumentValidatorTestBase
{
    private const int ZipEndOfCentralDirectoryRemainingBytes = 18;
    private static readonly MimeType InvalidMimeType = new("application", "octet-stream");

    protected static readonly byte?[] Office2007LeadingBytes =
    [
        0x50,
        0x4B,
        0x03,
        0x04,
        0x14,
        0x00,
        0x06,
        0x00
    ];

    protected static readonly byte?[] Office2007TrailingBytes =
    [
        0x50,
        0x4B,
        0x05,
        0x06,
        .. XNulls(ZipEndOfCentralDirectoryRemainingBytes)
    ];

    protected abstract FileTypeValidatorBase PasswordProtectedValidatorDefinition { get; }
    protected abstract IEnumerable<SingleSample> PasswordProtectedSamples { get; }

    [Fact]
    public void PasswordProtectedSamplesAreInvalidByDefault()
    {
        SampleData passwordProtectedData = new(PasswordProtectedSamples, MimeTypes);
        IEnumerable<SampleFileModel> validExamples = passwordProtectedData.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            bool result = Validator.IsValid([.. sampleFileModel.Bytes], sampleFileModel.MimeType, sampleFileModel.FileExtension);

            Assert.False(result);
        }
    }

    [Fact]
    public void PasswordProtectedSamplesAreValidWhenAllowed()
    {
        FileTypeValidator validator = new([PasswordProtectedValidatorDefinition]);
        SampleData passwordProtectedData = new(PasswordProtectedSamples, MimeTypes);
        IEnumerable<SampleFileModel> validExamples = passwordProtectedData.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            bool result = validator.IsValid([.. sampleFileModel.Bytes], sampleFileModel.MimeType, sampleFileModel.FileExtension);

            Assert.True(result);
        }
    }

    [Fact]
    public void PasswordProtectedSamplesAreInvalidDueToMimeTypeWhenAllowed()
    {
        FileTypeValidator validator = new([PasswordProtectedValidatorDefinition]);
        SampleData passwordProtectedData = new(PasswordProtectedSamples, MimeTypes);
        IEnumerable<SampleFileModel> validExamples = passwordProtectedData.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            bool result = validator.IsValid([.. sampleFileModel.Bytes], InvalidMimeType, sampleFileModel.FileExtension);

            Assert.False(result);
        }
    }
}
