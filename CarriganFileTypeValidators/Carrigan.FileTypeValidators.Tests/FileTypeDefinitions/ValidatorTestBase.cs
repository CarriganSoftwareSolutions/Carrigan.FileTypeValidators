using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

public abstract class ValidatorTestBase
{
    protected abstract FileTypeDefinition ValidatorDefinition { get; }
    protected abstract IEnumerable<SingleSample> Samples { get; }
    protected abstract IEnumerable<MimeType> MimeTypes { get; }

    internal SampleData Data { get; init; }

    protected FileTypeValidator Validator;

    protected ValidatorTestBase()
    {
        Data = new(Samples, MimeTypes);
        Validator = new FileTypeValidator([ValidatorDefinition]);

    }

    private bool IsValid(SampleFileModel sampleFileModel) => 
        Validator.IsValid([.. sampleFileModel.Bytes], sampleFileModel.MimeType, sampleFileModel.FileExtension);

    [Fact]
    public void ExactTest()
    {
        IEnumerable<SampleFileModel> exactSamples = Data.GetExactSamples();

            foreach (SampleFileModel sampleFileModel in exactSamples)
            {
                Assert.True(IsValid(sampleFileModel));
            }
    }
    [Fact]
    public void Valid()
    {
        IEnumerable<SampleFileModel> validExamples = Data.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            Assert.True(IsValid(sampleFileModel));
        }
    }

    [Fact]
    public void ValidDueToFileExtension()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidDueToFileExtension();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsValid(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidLeader()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidsDueToLeader();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsValid(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailer()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidsDueToTrailer();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsValid(sampleFileModel));
        }
    }



    [Fact]
    public void InvalidLeaderDueToOffset()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidLeaderDueToOffset();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsValid(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailerDueToOffset()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidTrailerDueToOffset();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsValid(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailerEmptyBytes()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidDueToEmptyBytes();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsValid(sampleFileModel));
        }
    }
}
