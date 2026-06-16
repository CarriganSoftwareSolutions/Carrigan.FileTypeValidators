using Carrigan.Core.Extensions;
using Carrigan.FileTypeValidators.FileTypeDefinitions;
using Carrigan.FileTypeValidators.Signatures;
using System.ComponentModel.DataAnnotations;

namespace Carrigan.FileTypeValidators.Tests.FileTypeDefinitions;

public abstract class ValidatorTestBase
{
    protected static byte?[] FromReadOnlySpan(ReadOnlySpan<byte> bytes) =>
        [.. bytes.ToArray().AsEnumerable().Select(aByte => (byte?)aByte)];
    protected static IEnumerable<byte?> XNulls(int x) =>
        Enumerable.Repeat<byte?>(null, x);

    protected abstract FileTypeValidatorBase ValidatorDefinition { get; }
    protected abstract IEnumerable<SingleSample> Samples { get; }
    protected abstract IEnumerable<MimeType> MimeTypes { get; }

    internal SampleData Data { get; init; }

    protected FileTypeValidator Validator;

    protected ValidatorTestBase()
    {
        Data = new(Samples, MimeTypes);
        Validator = new FileTypeValidator([ValidatorDefinition]);
    }

    private bool IsAllowed(SampleFileModel sampleFileModel) => 
        Validator.IsValid([.. sampleFileModel.Bytes], sampleFileModel.MimeType, sampleFileModel.FileExtension);

    private bool IsBlacklisted(SampleFileModel sampleFileModel) =>
        ValidatorDefinition.BlackListMatch([.. sampleFileModel.Bytes], sampleFileModel.MimeType, sampleFileModel.FileExtension);

    [Fact]
    public void ExactTest()
    {
        IEnumerable<SampleFileModel> exactSamples = Data.GetExactSamples();

            foreach (SampleFileModel sampleFileModel in exactSamples)
            {
                Assert.True(IsAllowed(sampleFileModel));
            }
    }
    [Fact]
    public void ExactBlacklistTest()
    {
        IEnumerable<SampleFileModel> exactSamples = Data.GetExactSamples();

        foreach (SampleFileModel sampleFileModel in exactSamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void Valid()
    {
        IEnumerable<SampleFileModel> validExamples = Data.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            Assert.True(IsAllowed(sampleFileModel));
        }
    }

    [Fact]
    public void ValidBlacklistTest()
    {
        IEnumerable<SampleFileModel> validExamples = Data.GetValidExamples();

        foreach (SampleFileModel sampleFileModel in validExamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidDueToFileExtension()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidDueToFileExtension();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsAllowed(sampleFileModel));
        }
    }


    [Fact]
    public void InvalidFileExtensionBlacklist()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidDueToFileExtension();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidLeader()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidsDueToLeader();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsAllowed(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailer()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidsDueToTrailer();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsAllowed(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailerBlacklist()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidsDueToTrailer();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidLeaderDueToOffset()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidLeaderDueToOffset();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsAllowed(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidOffsetBlacklist()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidLeaderDueToOffset();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailerDueToOffset()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidTrailerDueToOffset();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsAllowed(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailerOffsetBlacklist()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidTrailerDueToOffset();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void InvalidTrailerEmptyBytes()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidDueToEmptyBytes();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.False(IsAllowed(sampleFileModel));
        }
    }

    [Fact]
    public void TrailerEmptyBlacklist()
    {
        IEnumerable<SampleFileModel> invalidExamples = Data.GetInvalidDueToEmptyBytes();

        foreach (SampleFileModel sampleFileModel in invalidExamples)
        {
            Assert.True(IsBlacklisted(sampleFileModel));
        }
    }

    [Fact]
    public void MatchesNonBlackList()
    {
        SampleFileModel sampleFileModel = Data.MatchesNone();

        Assert.False(IsBlacklisted(sampleFileModel));
    }

    [Fact]
    public void MatchesNonWhiteList()
    {
        SampleFileModel sampleFileModel = Data.MatchesNone();

        Assert.False(IsAllowed(sampleFileModel));
    }
}
