using Carrigan.FileTypeValidators.Signatures;
using Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions.Documents;

public abstract class DocumentValidatorTestBase : ValidatorTestBase
{
    private static readonly MimeType InvalidMimeType = new("application", "octet-stream");

    [Fact]
    public void InvalidDueToMimeType()
    {
        IEnumerable<SampleFileModel> validExamples = Data.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            bool result = Validator.IsValid([.. sampleFileModel.Bytes], InvalidMimeType, sampleFileModel.FileExtension);

            Assert.False(result);
        }
    }
}
